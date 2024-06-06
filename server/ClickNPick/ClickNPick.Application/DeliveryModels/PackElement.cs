using Newtonsoft.Json;

namespace ClickNPick.Application.Common;

public class PackElement
{
    [JsonProperty("width")]
    public float? Width { get; set; }

    [JsonProperty("height")]
    public float? Height { get; set; }

    [JsonProperty("length")]
    public float? Length { get; set; }

    [JsonProperty("weight")]
    public float? Weight { get; set; }
}
