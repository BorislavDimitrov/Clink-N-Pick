using Newtonsoft.Json;

namespace ClickNPick.Application.DeliveryModels.Request;

public class ShipmentStatusService
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("count")]
    public double? Count { get; set; }

    [JsonProperty("paymentSide")]
    public string PaymentSide { get; set; }

    [JsonProperty("price")]
    public double? Price { get; set; }

    [JsonProperty("currency")]
    public string Currency { get; set; }
}
