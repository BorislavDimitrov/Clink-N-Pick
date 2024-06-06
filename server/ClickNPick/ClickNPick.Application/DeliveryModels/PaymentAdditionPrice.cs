using Newtonsoft.Json;

namespace ClickNPick.Application.Common;

public class PaymentAdditionPrice
{
    [JsonProperty("side")]
    public string Side { get; set; }

    [JsonProperty("shareAmount")]
    public double ShareAmount { get; set; }

    [JsonProperty("method")]
    public string Method { get; set; }

    [JsonProperty("otherClientNumber")]
    public string OtherClientNumber { get; set; }
}
