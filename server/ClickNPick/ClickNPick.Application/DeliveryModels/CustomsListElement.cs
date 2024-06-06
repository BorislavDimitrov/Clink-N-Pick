using Newtonsoft.Json;

namespace ClickNPick.Application.Common;

public class CustomsListElement
{
    [JsonProperty("cn")]
    public string? TaricCode { get; set; }

    [JsonProperty("description")]
    public string? Description { get; set; }

    [JsonProperty("sum")]
    public double? TotalSum { get; set; }

    [JsonProperty("currency")]
    public string? Currency { get; set; }
}
