using ClickNPick.Application.DeliveryModels.Enumerations;
using Newtonsoft.Json;

namespace ClickNPick.Application.Common;

public class Instruction
{
    [JsonProperty("id")]
    public int? Id { get; set; }

    [JsonProperty("type")]
    public InstructionType? Type { get; set; }

    [JsonProperty("title")]
    public string? Title { get; set; }

    [JsonProperty("description")]
    public string? Description { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("applyToAllParcels")]
    public bool? ApplyToAllParcels { get; set; }

    [JsonProperty("applyToReceivers")]
    public List<string>? ApplyToReceivers { get; set; }
}
