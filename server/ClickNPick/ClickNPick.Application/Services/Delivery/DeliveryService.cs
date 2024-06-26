﻿using ClickNPick.Application.Abstractions.Repositories;
using ClickNPick.Application.Abstractions.Services;
using ClickNPick.Application.Common;
using ClickNPick.Application.Constants;
using ClickNPick.Application.DeliveryModels.Request;
using ClickNPick.Application.DeliveryModels.Response;
using ClickNPick.Application.DtoModels.Delivery.Request;
using ClickNPick.Application.DtoModels.Delivery.Response;
using ClickNPick.Application.Exceptions.Delivery;
using ClickNPick.Application.Exceptions.Identity;
using ClickNPick.Application.Exceptions.Products;
using ClickNPick.Application.Services.Products;
using ClickNPick.Application.Services.Users;
using ClickNPick.Domain.Models;
using ClickNPick.Domain.Models.Enums;
using ClickNPick.Web.Models.Delivery.Request;
using ClickNPick.Web.Models.Delivery.Response;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ShipmentStatus = ClickNPick.Domain.Models.Enums.ShipmentStatus;

namespace ClickNPick.Application.Services.Delivery;

public class DeliveryService : IDeliveryService
{
    private const string CountryCode = "BGR";
    private const string CdType = "get";
    private const string Mode = "create";
    private const string Currency = "EUR";
    private const double CacheExpirationMinutes = 60;
    private const string CacheKeyPrefix = "CacheKey:{0}";
    private const string JsonMediaType = "application/json";

    private readonly HttpClient _httpClient;
    private readonly ICacheService _cacheService;
    private readonly IRepository<ShipmentRequest> _shipmentRequestRepository;
    private readonly IUsersService _usersService;
    private readonly IProductsService _productsService;

    public DeliveryService(
        HttpClient httpClient,
        ICacheService cacheService,
         IRepository<ShipmentRequest> shipmentRequestRepository,
         IUsersService usersService,
         IProductsService productsService
         )
    {
        _httpClient = httpClient;
        _cacheService = cacheService;
        _shipmentRequestRepository = shipmentRequestRepository;
        _usersService = usersService;
        _productsService = productsService;
    }      

    public async Task<string> CreateShipmentRequestAsync(RequestShipmentRequestDto model)
    {
        var product = await _productsService.GetByIdAsync(model.ProductId);

        if (product == null)
        {
            throw new ProductNotFoundException($"Product with id {model.ProductId} doesnt exist.");
        }

        if (model.BuyerId == product.CreatorId)
        {
            throw new InvalidOperationException("A creator of a product cannot request its own shipment.");
        }

        var buyer = await _usersService.GetByIdAsync(model.BuyerId);

        if (buyer == null)
        {
            throw new UserNotFoundException($"User with id {model.BuyerId} doesnt exist.");
        }

        var seller = await _usersService.GetByIdAsync(product.CreatorId);

        if (seller == null)
        {
            throw new UserNotFoundException($"User with id {product.CreatorId} doesnt exist.");
        }

        var newShipmentRequest = model.ToShipmentRequest();
        newShipmentRequest.BuyerId = buyer.Id;
        newShipmentRequest.SellerId = seller.Id;
        buyer.ShipmentsAsBuyer.Add(newShipmentRequest);
        seller.ShipmentsAsSeller.Add(newShipmentRequest);

        await _shipmentRequestRepository.AddAsync(newShipmentRequest);
        await _shipmentRequestRepository.SaveChangesAsync();

        return newShipmentRequest.Id;
    }

    public async Task AcceptShipmentAsync(AcceptShipmentRequestDto model)
    {
        var user = await _usersService.GetByIdAsync(model.UserId);

        if (user == null)
        {
            throw new UserNotFoundException($"User with id {model.UserId} doesnt exist.");
        }

        var shipmentRequest = await GetByIdAsync(model.RequestShipmentId);

        if (shipmentRequest == null)
        {
            throw new ShipmentRequestNotFoundException($"Shipment request with id {model.RequestShipmentId} doesnt exist.");
        }

        if (await IsUserSenderOfShipment(model.RequestShipmentId, model.UserId) == false)
        {
            throw new InvalidOperationException($"User who is not sender of a shipment cannot accept a shipment request.");
        }

        if (shipmentRequest.ShipmentStatus != ShipmentStatus.Requested)
        {
            throw new InvalidOperationException("Shipment cannot be accepted.");
        }

        var labelRequest = new CreateLabelRequestModel();
        labelRequest.Mode = Mode;

        var shippingLabel = new ShippingLabel();

        var receiverProfile = new ClientProfile()
        {
            Name = shipmentRequest.ReceiverName,
            Phones = new List<string> { shipmentRequest.ReceiverPhoneNumber },
        };

        shippingLabel.ReceiverClient = receiverProfile;

        var senderProfile = new ClientProfile()
        {
            Name = model.SenderName,
            Phones = new List<string> { model.SenderPhoneNumber }
        };

        var country = new Country();
        country.Code3 = CountryCode;

        if (shipmentRequest.DeliveryLocation == DeliveryLocation.Office)
        {
            shippingLabel.ReceiverOfficeCode = shipmentRequest.ReceiverOfficeCode;
        }
        else
        {
            var receiverCity = new City();
            receiverCity.Country = country;
            receiverCity.Name = shipmentRequest.CityOrVillage;
            receiverCity.PostCode = shipmentRequest.PostCode;

            var receiverAddress = new Address();
            receiverAddress.City = receiverCity;
            receiverAddress.Quarter = shipmentRequest.Quarter;
            receiverAddress.Street = shipmentRequest.Street;
            receiverAddress.Num = shipmentRequest.StreetNumber;
            receiverAddress.Other = shipmentRequest.DeliverAddressInfo;

            shippingLabel.ReceiverAddress = receiverAddress;
        }

        var requestCourierModel = new RequestCourierRequestModel
        {
            RequestTimeFrom = model.RequestTimeFrom,
            RequestTimeTo = model.RequestTimeTo,
            SenderClient = senderProfile,
            ShipmentPackCount = model.PackCount,
            ShipmentWeight = model.Weight,
            ShipmentType = model.ShipmentType,               
        };

        if (model.DeliveryLocation == DeliveryLocation.Office.ToString())
        {
            shippingLabel.SenderOfficeCode = model.SenderOfficeCode;
        }
        else
        {
            var senderCity = new City();
            senderCity.Country = country;
            senderCity.Name = model.CityOrVillage;
            senderCity.PostCode = model.PostCode;

            var senderAddress = new Address();
            senderAddress.City = senderCity;
            senderAddress.Quarter = model.Quarter;
            senderAddress.Street = model.Street;
            senderAddress.Num = model.StreetNumber;
            senderAddress.Other = model.DeliverAddressInfo;

            shippingLabel.SenderAddress = senderAddress;

            requestCourierModel.SenderAddress = senderAddress;
        }

        shippingLabel.SenderClient = senderProfile;

        shippingLabel.EmailOnDelivery = shipmentRequest.EmailOnDelivery;
        shippingLabel.SmsOnDelivery = shipmentRequest.ReceiverPhoneNumber;

        var shippingServices = new ShippingLabelServices();
        shippingServices.SmsNotification = shipmentRequest.SmsNotification;
        shippingServices.GoodsReceipt = shipmentRequest.GoodsReceipt;
        shippingServices.DeliveryReceipt = shipmentRequest.DeliveryReceipt;
        shippingServices.CdAmount = model.PaymentReceiverAmount;
        shippingServices.CdType = CdType;
        shippingServices.CdCurrency = Currency;

        shippingLabel.SendDate = model.SendDate;
        shippingLabel.PackCount = model.PackCount;
        shippingLabel.PaymentReceiverAmount = model.PaymentReceiverAmount;
        shippingLabel.ShipmentType = model.ShipmentType;
        shippingLabel.Weight = model.Weight;
        shippingLabel.ShipmentDescription = model.ShipmentDescription;
        shippingLabel.OrderNumber = model.OrderNumber;
        
       
        shippingLabel.Services = shippingServices;          

        labelRequest.Label = shippingLabel;

        var result = await CreateLabelAsync(labelRequest);

        shipmentRequest.ShipmentStatus = ShipmentStatus.Accepted;

        shipmentRequest.ShipmentNumber = result.Label.ShipmentNumber;

        if (model.DeliveryLocation == DeliveryLocation.Address.ToString())
        {
            requestCourierModel.AttachShipments = new List<string> { result.Label.ShipmentNumber };

            var requestCourierResponse = await RequestCourierAsync(requestCourierModel);

            shipmentRequest.RequestCourierId = requestCourierResponse.CourierRequestId;
        }

        await _shipmentRequestRepository.SaveChangesAsync();
    }

    public async Task CancelShipmentRequestAsync(CancelShipmentRequestDto model)
    {
        var shipmentRequest = await _shipmentRequestRepository
            .All()
            .FirstOrDefaultAsync(x => x.Id == model.ShipmentId);

        if (shipmentRequest == null)
        {
            throw new ShipmentRequestNotFoundException($"Shipment request with id {model.ShipmentId} doesnt exist.");
        }

        if (shipmentRequest.SellerId != model.UserId && shipmentRequest.BuyerId != model.UserId)
        {
            throw new InvalidOperationException($"Shipment with id {shipmentRequest.Id} cannot be canceled because a user with id {model.UserId} is not receiver or sender.");
        }

        if (shipmentRequest.ShipmentStatus == ShipmentStatus.Canceled)
        {
            throw new InvalidOperationException("Shipment is already canceled.");
        }

        var deleteLabelModel = new DeleteLabelsRequestModel
        {
            ShipmentNumbers = new List<string> { shipmentRequest.ShipmentNumber }
        };

        if (shipmentRequest.ShipmentStatus != ShipmentStatus.Requested)
        {
            var deleteResponse = await DeleteLabelsAsync(deleteLabelModel);

            if (deleteResponse.Results.Any(x => string.IsNullOrEmpty(x.ShipmentNum)))
            {
                throw new InvalidOperationException("Deleting of label has failed.");
            }
        }

        shipmentRequest.ShipmentStatus = ShipmentStatus.Canceled;

        await _shipmentRequestRepository .SaveChangesAsync();
    }

    public async Task DeclineShipmentAsync(DeclineShipmentRequestDto model)
    {
        var shipmentRequest = await _shipmentRequestRepository
            .All()
            .FirstOrDefaultAsync(x => x.Id == model.ShipmentId);

        if (shipmentRequest == null)
        {
            throw new ShipmentRequestNotFoundException($"Shipment request with id {model.ShipmentId} doesnt exist.");
        }

        if (shipmentRequest.SellerId != model.UserId)
        {
            throw new InvalidOperationException($"User with id {model.UserId} cannot decline shipment request with id {shipmentRequest.Id}");
        }

        if (shipmentRequest.ShipmentStatus != ShipmentStatus.Requested)
        {
            throw new InvalidOperationException($"Shipment request with id {shipmentRequest.Id} and status {shipmentRequest.ShipmentStatus} cannot be declined.");
        }

        shipmentRequest.ShipmentStatus = ShipmentStatus.Declined;

        await _shipmentRequestRepository.SaveChangesAsync();
    }

    public async Task<ShipmentDetailsResponseDto> GetShipmentDetailsAsync(ShipmentDetailsRequestDto model)
    {
        var shipmentRequest = await _shipmentRequestRepository
            .All()
            .FirstOrDefaultAsync(x => x.Id == model.ShipmentId);

        if (shipmentRequest == null)
        {
            throw new ShipmentRequestNotFoundException($"Shipment request with id {model.ShipmentId} doesnt exist.");
        }

        if (shipmentRequest.SellerId != model.UserId && shipmentRequest.BuyerId != model.UserId)
        {
            throw new InvalidOperationException($"User with id {model.UserId} cannot access shipment request with id {model.ShipmentId}");
        }

        if (shipmentRequest.ShipmentStatus != ShipmentStatus.Accepted)
        {
            throw new InvalidOperationException($"Shipment request with id {shipmentRequest.Id} and status {shipmentRequest.ShipmentStatus} cannot be declined.");
        }

        var shipmentsStatusesRequestModel = new GetShipmentStatusesRequestModel 
        { 
            ShipmentNumbers = new List<string> { shipmentRequest.ShipmentNumber } 
        };

        var shipmentsStatusesResponse = await GetShipmentStatusesAsync(shipmentsStatusesRequestModel);

        var courierStatusesRequestModel = new GetCourierStatusesRequestModel { RequestCourierIds = new List<string> { shipmentRequest.RequestCourierId } };

        var courierStatusesResponseModel = await GetCourierStatusesAsync(courierStatusesRequestModel);

        var result = ShipmentDetailsResponseDto.FromShipmentStatus(shipmentsStatusesResponse.ShipmentStatuses.ToList()[0].Status);

        result.CourierStatus = courierStatusesResponseModel.RequestCourierStatuses.ToList().FirstOrDefault()?.Status.Status.ToString();

        return result;
    }

    public async Task<ShipmentListingResponseDto> GetShipmentsToSendAsync(string userId)
    {
        var user = await _usersService.GetByIdAsync(userId);

        if (user == null)
        {
            throw new UserNotFoundException($"User with id {userId} doesnt exist.");
        }

        var shipmentsToSend = await _shipmentRequestRepository
            .All()
            .Where(x => x.SellerId == userId)
            .Include(x => x.Product)
            .Include(x => x.Buyer)
            .Include(x => x.Seller)
            .ToListAsync();

        return ShipmentListingResponseDto.FromShipmentRequests(shipmentsToSend);         
    }

    public async Task<ShipmentListingResponseDto> GetShipmentsToReceiveAsync(string userId)
    {
        var user = await _usersService.GetByIdAsync(userId);

        if (user == null)
        {
            throw new UserNotFoundException($"User with id {userId} doesnt exist.");
        }

        var shipmentsToReceive = await _shipmentRequestRepository
            .All()
            .Where(x => x.BuyerId == userId)
            .Include(x => x.Product)
            .Include(x => x.Seller)
            .Include(x => x.Buyer)
            .ToListAsync();

        return ShipmentListingResponseDto.FromShipmentRequests(shipmentsToReceive);
    }

    public async Task<ShipmentRequest> GetByIdAsync(string id)
        => await _shipmentRequestRepository
        .All()
        .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<CitiesResponseDto?> GetCitiesAsync(CancellationToken cancellationToken = default)
        => await _cacheService.GetOrCreateAsync<CitiesResponseDto>(
            CountryCode,
            async () => await PostAsync<CitiesResponseDto>(EcontClientEndpoints.Cities, new { countryCode = CountryCode }, cancellationToken),
            TimeSpan.FromMinutes(CacheExpirationMinutes));

    public async Task<QuartersResponseDto?> GetQuartersAsync(int cityId, CancellationToken cancellationToken = default)
        => await _cacheService.GetOrCreateAsync<QuartersResponseDto>(
            $"{nameof(GetQuartersAsync)}{cityId.ToString()}",
            async () => await PostAsync<QuartersResponseDto>(EcontClientEndpoints.Quarters, new { cityID = cityId }, cancellationToken),
            TimeSpan.FromMinutes(CacheExpirationMinutes));

    public async Task<StreetsResponseDto?> GetStreetsAsync(int cityId, CancellationToken cancellationToken = default)
        => await _cacheService.GetOrCreateAsync<StreetsResponseDto>(
            $"{nameof(GetStreetsAsync)}{cityId.ToString()}",
            async () => await PostAsync<StreetsResponseDto>(EcontClientEndpoints.Streets, new { cityID = cityId}, cancellationToken),
            TimeSpan.FromMinutes(CacheExpirationMinutes));

    public async Task<bool> IsUserSenderOfShipment(string shipmentId, string userId)
        => await _shipmentRequestRepository
        .All()
        .FirstOrDefaultAsync(x => x.Id == shipmentId && x.SellerId == userId) == null ? false : true;

    private async Task<CreateLabelResponse?> CreateLabelAsync(CreateLabelRequestModel requestModel, CancellationToken cancellationToken = default)
        => await PostAsync<CreateLabelResponse>(EcontClientEndpoints.CreateLabel, requestModel, cancellationToken);

    private async Task<RequestCourierResponseModel?> RequestCourierAsync(RequestCourierRequestModel requestModel, CancellationToken cancellationToken = default)
    => await PostAsync<RequestCourierResponseModel>(EcontClientEndpoints.RequestCourier, requestModel, cancellationToken);

    private async Task<DeleteLabelsResponseDto?> DeleteLabelsAsync(DeleteLabelsRequestModel requestModel, CancellationToken cancellationToken = default)
        => await PostAsync<DeleteLabelsResponseDto>(EcontClientEndpoints.DeleteLabels, requestModel, cancellationToken);

    private async Task<GetShipmentStatusesResponse?> GetShipmentStatusesAsync(GetShipmentStatusesRequestModel requestModel, CancellationToken cancellationToken = default)
        => await PostAsync<GetShipmentStatusesResponse>(EcontClientEndpoints.GetShipmentStatuses, requestModel, cancellationToken);

    private async Task<GetCourierStatusesResponseModel?> GetCourierStatusesAsync(GetCourierStatusesRequestModel requestModel, CancellationToken cancellationToken = default)
 => await PostAsync<GetCourierStatusesResponseModel>(EcontClientEndpoints.GetCourierStatuses, requestModel, cancellationToken);

    private async Task<T?> PostAsync<T>(string path, object body, CancellationToken cancellationToken = default)
    {
        var jsonData = JsonConvert.SerializeObject(body);

        var content = new StringContent(jsonData, Encoding.UTF8, JsonMediaType);
        var response = await _httpClient.PostAsync(path, content, cancellationToken);
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var message = JsonConvert.DeserializeObject<Error>(responseContent)?.Message;

            Log.Error(message);

            throw new ValidationException(message ?? string.Empty);
        }
        Log.Information(responseContent);
        return JsonConvert.DeserializeObject<T>(responseContent, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });
    }
}
