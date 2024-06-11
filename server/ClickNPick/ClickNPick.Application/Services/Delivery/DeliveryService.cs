using ClickNPick.Application.Abstractions.Repositories;
using ClickNPick.Application.Abstractions.Services;
using ClickNPick.Application.Common;
using ClickNPick.Application.Configurations.Cache;
using ClickNPick.Application.Constants;
using ClickNPick.Application.DtoModels;
using ClickNPick.Application.DtoModels.Delivery.Response;
using ClickNPick.Application.Exceptions.Identity;
using ClickNPick.Application.Exceptions.Products;
using ClickNPick.Application.Services.Products;
using ClickNPick.Application.Services.Users;
using ClickNPick.Domain.Models;
using ClickNPick.Web.Models.Delivery.Request;
using ClickNPick.Web.Models.Delivery.Response;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClickNPick.Application.Services.Delivery
{
    public class DeliveryService : IDeliveryService
    {
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

        public async Task<CountriesResponseDto> GetCountriesAsync(CancellationToken cancellationToken = default)
            => await _cacheService.GetOrCreateAsync<CountriesResponseDto>(
                $"{nameof(this.GetCountriesAsync)}",
                async () => await PostAsync<CountriesResponseDto>(EcontClientEndpoints.Countries, new { }, cancellationToken),
                TimeSpan.FromMinutes(CacheExpirationMinutes));

        public async Task<CitiesResponseDto?> GetCitiesAsync(GetCitiesRequestDto requestModel, CancellationToken cancellationToken = default)
            => await _cacheService.GetOrCreateAsync<CitiesResponseDto>(
                this.GetCacheKey(requestModel),
                async () => await PostAsync<CitiesResponseDto>(EcontClientEndpoints.Cities, requestModel, cancellationToken),
                TimeSpan.FromMinutes(CacheExpirationMinutes));

        public async Task<OfficesResponseDto?> GetOfficesAsync(GetOfficesRequestDto requestModel, CancellationToken cancellationToken = default)
            => await _cacheService.GetOrCreateAsync<OfficesResponseDto>(
                this.GetCacheKey(requestModel),
                async () => await PostAsync<OfficesResponseDto>(EcontClientEndpoints.Offices, requestModel, cancellationToken),
                TimeSpan.FromMinutes(CacheExpirationMinutes));

        public async Task<CreateLabelResponseDto?> CreateLabelAsync(CreateLabelRequestDto requestModel, CancellationToken cancellationToken = default)
            => await PostAsync<CreateLabelResponseDto>(EcontClientEndpoints.CreateLabel, requestModel, cancellationToken);

        public async Task<DeleteLabelsResponseDto?> DeleteLabelsAsync(DeleteLabelsRequestDto requestModel, CancellationToken cancellationToken = default)
            => await PostAsync<DeleteLabelsResponseDto>(EcontClientEndpoints.DeleteLabels, requestModel, cancellationToken);

        public async Task<GetShipmentStatusesResponseDto?> GetShipmentStatusesAsync(GetShipmentStatusesRequestDto requestModel, CancellationToken cancellationToken = default)
            => await PostAsync<GetShipmentStatusesResponseDto>(EcontClientEndpoints.GetShipmentStatuses, requestModel, cancellationToken);

        public async Task<string> CreateShipmentRequestAsync(RequestShipmentRequestDto model)
        {

            var product = await _productsService.GetByIdAsync(model.ProductId);

            if (product == null)
            {
                throw new ProductNotFoundException();
            }

            if (model.BuyerId == product.CreatorId)
            {
                throw new InvalidOperationException("A creator of a product cannot request its own shipment.");
            }

            var buyer = await _usersService.GetByIdAsync(model.BuyerId);

            if (buyer == null)
            {
                throw new UserNotFoundException();
            }

            var seller = await _usersService.GetByIdAsync(product.CreatorId);

            if (seller == null)
            {
                throw new UserNotFoundException();
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

        public async Task<ShipmentListingResponseDto> GetShipmentsToSendAsync(string userId)
        {
            var user = await _usersService.GetByIdAsync(userId);

            if (user == null)
            {
                throw new UserNotFoundException();
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
                throw new UserNotFoundException();
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

        private string GetCacheKey<T>(T requestModel)
            where T : ICacheable
                => CacheKeyGenerator<T>.GenerateCacheKey(
                    string.Format(CacheKeyPrefix, typeof(T).Name),
                    new CacheParameterCollection<T>(requestModel));
    }
}
