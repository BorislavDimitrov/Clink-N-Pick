using Newtonsoft.Json;

namespace ClickNPick.Application.DeliveryModels;

public class Street
{
    [JsonProperty("id")]
    public int? Id { get; set; }

    [JsonProperty("cityID")]
    public int? CityId { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("nameEn")]
    public string? NameEn { get; set; }
}
