using ClickNPick.Application.DtoModels.Delivery.Response;

namespace ClickNPick.Web.Models.Delivery.Response
{
    public class ShipmentListingResponseModel
    {
        public List<ShipmentInListResponseModel> Shipments { get; set; }

        public static ShipmentListingResponseModel FromShipmentListingResponseDto(ShipmentListingResponseDto dto)
        {
            var shipments = dto.Shipments.Select(x => ShipmentInListResponseModel.FromShipmentInListResponseDto(x)).ToList();

            return new ShipmentListingResponseModel { Shipments = shipments };
        }
    }
}
