using ClickNPick.Application.DeliveryModels.Request;
using ClickNPick.Application.DeliveryModels.Response;
using ClickNPick.Application.DtoModels.Delivery.Request;
using ClickNPick.Application.DtoModels.Delivery.Response;
using ClickNPick.Web.Models.Delivery.Response;

namespace ClickNPick.Application.Services.Delivery
{
    public interface IDeliveryService
    {

        Task<CitiesResponseDto?> GetCitiesAsync(CancellationToken cancellationToken = default);

        //Task<DeleteLabelsResponseDto?> DeleteLabelsAsync(DeleteLabelsRequestDto requestModel, CancellationToken cancellationToken = default);

        Task<QuartersResponseDto?> GetQuartersAsync(int cityId, CancellationToken cancellationToken = default);

        Task<StreetsResponseDto?> GetStreetsAsync(int cityId, CancellationToken cancellationToken = default);

        Task<GetShipmentStatusesResponse?> GetShipmentStatusesAsync(GetShipmentStatusesRequest requestModel, CancellationToken cancellationToken = default);

        Task<string> CreateShipmentRequestAsync(RequestShipmentRequestDto model);

        Task AcceptShipmentAsync(AcceptShipmentRequestDto model);

        Task<ShipmentListingResponseDto> GetShipmentsToSendAsync(string userId);

        Task<ShipmentListingResponseDto> GetShipmentsToReceiveAsync(string userId);
    }
}
