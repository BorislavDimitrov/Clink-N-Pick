using Newtonsoft.Json;

namespace ClickNPick.Web.Models.Delivery.Request;

public class DeleteLabelsRequestModel
{
    [JsonProperty("shipmentNumbers")]
    public IEnumerable<string>? ShipmentNumbers { get; set; }

}
