using Newtonsoft.Json;

namespace ClickNPick.Application.Common;

public class ShipmentStatusResultElement
{
    [JsonProperty("status")]
    public ShipmentStatus Status { get; set; }

    [JsonProperty("error")]
    public Error? Error { get; set; }
}
