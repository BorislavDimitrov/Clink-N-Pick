using ClickNPick.Application.DtoModels;
using ClickNPick.Application.DtoModels.Delivery.Response;
using ClickNPick.Web.Models.Delivery.Request;
using ClickNPick.Web.Models.Delivery.Response;

namespace ClickNPick.Application.Services.Delivery
{
    public interface IDeliveryService
    {
        Task<CountriesResponseDto> GetCountriesAsync(CancellationToken cancellationToken = default);

        Task<CitiesResponseDto?> GetCitiesAsync(GetCitiesRequestDto requestModel, CancellationToken cancellationToken = default);

        Task<OfficesResponseDto?> GetOfficesAsync(GetOfficesRequestDto requestModel, CancellationToken cancellationToken = default);

        Task<CreateLabelResponseDto?> CreateLabelAsync(CreateLabelRequestDto requestModel, CancellationToken cancellationToken = default);

        Task<DeleteLabelsResponseDto?> DeleteLabelsAsync(DeleteLabelsRequestDto requestModel, CancellationToken cancellationToken = default);

        Task<GetShipmentStatusesResponseDto?> GetShipmentStatusesAsync(GetShipmentStatusesRequestDto requestModel, CancellationToken cancellationToken = default);

        Task<string> CreateShipmentRequestAsync(RequestShipmentRequestDto model);

        Task<ShipmentListingResponseDto> GetShipmentsToSendAsync(string userId);

        Task<ShipmentListingResponseDto> GetShipmentsToReceiveAsync(string userId);
    }
}
