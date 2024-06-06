using ClickNPick.Application.Common;
using Newtonsoft.Json;

namespace ClickNPick.Web.Models.Delivery.Response;

public class GetShipmentStatusesResponseDto
{
    [JsonProperty("shipmentStatuses")]
    public IEnumerable<ShipmentStatusResultElement> ShipmentStatuses { get; set; }
}
