using Newtonsoft.Json;

namespace ClickNPick.Web.Models.Delivery.Request;

public class DeleteLabelsRequestDto
{
    [JsonProperty("shipmentNumbers")]
    public IEnumerable<string>? ShipmentNumbers { get; set; }
}
