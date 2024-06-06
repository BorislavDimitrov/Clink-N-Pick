using Newtonsoft.Json;

namespace ClickNPick.Application.Common;

public class PreviousShipment
{
    [JsonProperty("shipmentNumber")]
    public int? ShipmentNumber { get; set; }

    [JsonProperty("reason")]
    public string? Reason { get; set; }

    [JsonProperty("pdfURL")]
    public string? PdfUrl { get; set; }
}
