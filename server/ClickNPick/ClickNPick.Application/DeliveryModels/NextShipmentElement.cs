using Newtonsoft.Json;

namespace ClickNPick.Application.Common;

public class NextShipmentElement
{
    [JsonProperty("shipmentNumber")]
    public string? ShipmentNumber { get; set; }

    [JsonProperty("reason")]
    public string? Reason { get; set; }

    [JsonProperty("pdfURL")]
    public string? PdfUrl { get; set; }
}
