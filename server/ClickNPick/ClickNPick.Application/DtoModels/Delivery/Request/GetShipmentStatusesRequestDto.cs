using Newtonsoft.Json;

namespace ClickNPick.Web.Models.Delivery.Request;

public class GetShipmentStatusesRequestDto
{
    [JsonProperty(
        PropertyName = "shipmentNumbers",
        Required = Required.Always)]
    public IEnumerable<string> ShipmentNumbers { get; set; }
}
