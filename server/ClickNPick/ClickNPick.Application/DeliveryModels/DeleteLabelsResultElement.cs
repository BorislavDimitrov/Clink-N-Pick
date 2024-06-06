using Newtonsoft.Json;

namespace ClickNPick.Application.Common;

public class DeleteLabelsResultElement
{
    [JsonProperty("shipmentNum")]
    public string? ShipmentNum { get; set; }

    [JsonProperty("error")]
    public Error? Error { get; set; }
}
