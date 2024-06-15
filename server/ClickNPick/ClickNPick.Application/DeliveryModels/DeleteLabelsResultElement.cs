using ClickNPick.Application.Common;
using Newtonsoft.Json;

namespace ClickNPick.Application.DeliveryModels;

public class DeleteLabelsResultElement
{
    [JsonProperty("shipmentNum")]
    public string? ShipmentNum { get; set; }

    [JsonProperty("error")]
    public Error? Error { get; set; }
}
