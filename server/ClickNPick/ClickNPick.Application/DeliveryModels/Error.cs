using Newtonsoft.Json;

namespace ClickNPick.Application.Common;

public class Error
{
    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("message")]
    public string? Message { get; set; }

    [JsonProperty("fields")]
    public IEnumerable<string>? Fields { get; set; }

    [JsonProperty("innerErrors")]
    public IEnumerable<Error>? InnerErrors { get; set; }
}
