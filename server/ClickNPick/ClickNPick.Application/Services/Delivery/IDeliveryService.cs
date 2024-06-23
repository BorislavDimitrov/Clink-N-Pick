using ClickNPick.Application.DtoModels.Delivery.Request;
using ClickNPick.Application.DtoModels.Delivery.Response;
using ClickNPick.Domain.Models;
using ClickNPick.Web.Models.Delivery.Response;

namespace ClickNPick.Application.Services.Delivery;

public interface IDeliveryService
{
    Task<CitiesResponseDto?> GetCitiesAsync(CancellationToken cancellationToken = default);

    Task CancelShipmentRequestAsync(CancelShipmentRequestDto model);

    Task<QuartersResponseDto?> GetQuartersAsync(int cityId, CancellationToken cancellationToken = default);

    Task<StreetsResponseDto?> GetStreetsAsync(int cityId, CancellationToken cancellationToken = default);

    Task<ShipmentDetailsResponseDto> GetShipmentDetailsAsync(ShipmentDetailsRequestDto model);

    Task<string> CreateShipmentRequestAsync(RequestShipmentRequestDto model);

    Task AcceptShipmentAsync(AcceptShipmentRequestDto model);

    Task DeclineShipmentAsync(DeclineShipmentRequestDto model);

    Task<ShipmentListingResponseDto> GetShipmentsToSendAsync(string userId);

    Task<ShipmentListingResponseDto> GetShipmentsToReceiveAsync(string userId);

    public Task<ShipmentRequest> GetByIdAsync(string id);
}
