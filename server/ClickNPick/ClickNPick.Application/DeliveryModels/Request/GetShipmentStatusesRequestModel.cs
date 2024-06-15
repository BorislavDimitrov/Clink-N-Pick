using Newtonsoft.Json;

namespace ClickNPick.Application.DeliveryModels.Request;

public class GetShipmentStatusesRequestModel
{
    [JsonProperty(
        PropertyName = "shipmentNumbers",
        Required = Required.Always)]
    public IEnumerable<string> ShipmentNumbers { get; set; }
}
