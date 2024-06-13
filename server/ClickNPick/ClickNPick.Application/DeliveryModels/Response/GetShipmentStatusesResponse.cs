using ClickNPick.Application.Common;
using Newtonsoft.Json;

namespace ClickNPick.Application.DeliveryModels.Response;

public class GetShipmentStatusesResponse
{
    [JsonProperty("shipmentStatuses")]
    public IEnumerable<ShipmentStatusResultElement> ShipmentStatuses { get; set; }
}
