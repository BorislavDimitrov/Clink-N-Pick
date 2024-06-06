using Newtonsoft.Json;

namespace ClickNPick.Application.Common;

public class PackingListElement
{
    [JsonProperty("inventoryNum")]
    public string? InventoryNumber { get; set; }

    [JsonProperty("description")]
    public string? Description { get; set; }

    [JsonProperty("weight")]
    public double? Weight { get; set; }

    [JsonProperty("count")]
    public int? Count { get; set; }

    [JsonProperty("price")]
    public double? Price { get; set; }

    //[JsonProperty("file")]
    //public HostedFile? File { get; set; }

    [JsonProperty("alternativeDepartment")]
    public string? AlternativeDepartment { get; set; }
}
