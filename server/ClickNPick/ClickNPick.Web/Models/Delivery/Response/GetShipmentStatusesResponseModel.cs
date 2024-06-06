using ClickNPick.Application.Common;
using Newtonsoft.Json;

namespace ClickNPick.Web.Models.Delivery.Response;

public class GetShipmentStatusesResponseModel
{
    [JsonProperty("shipmentStatuses")]
    public IEnumerable<ShipmentStatusResultElement> ShipmentStatuses { get; set; }

    public static GetShipmentStatusesResponseModel FromGetShipmentStatusesResponseDto(GetShipmentStatusesResponseDto dto)
    {
        return new GetShipmentStatusesResponseModel
        {
            ShipmentStatuses = dto.ShipmentStatuses,
        };
    }
}
