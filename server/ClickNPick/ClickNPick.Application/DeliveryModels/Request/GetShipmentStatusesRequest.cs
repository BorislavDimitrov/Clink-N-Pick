using Newtonsoft.Json;

namespace ClickNPick.Application.DeliveryModels.Request;

public class GetShipmentStatusesRequest
{
    [JsonProperty(
        PropertyName = "shipmentNumbers",
        Required = Required.Always)]
    public IEnumerable<string> ShipmentNumbers { get; set; }
}
