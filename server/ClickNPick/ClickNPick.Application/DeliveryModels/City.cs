using Newtonsoft.Json;

namespace ClickNPick.Application.Common;

public class City
{
    [JsonProperty("id")]
    public int? Id { get; set; }

    [JsonProperty("country")]
    public Country? Country { get; set; }

    [JsonProperty("postCode")]
    public string? PostCode { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("nameEn")]
    public string? NameEn { get; set; }

    [JsonProperty("regionName")]
    public string? RegionName { get; set; }

    [JsonProperty("regionNameEn")]
    public string? RegionNameEn { get; set; }

    [JsonProperty("phoneCode")]
    public string? PhoneCode { get; set; }

    [JsonProperty("location")]
    public GeoLocation? Location { get; set; }

    [JsonProperty("expressCityDeliveries")]
    public bool? ExpressCityDeliveries { get; set; }

    [JsonProperty("monday")]
    public bool? Monday { get; set; }

    [JsonProperty("tuesday")]
    public bool? Tuesday { get; set; }

    [JsonProperty("wednesday")]
    public bool? Wednesday { get; set; }

    [JsonProperty("thursday")]
    public bool? Thursday { get; set; }

    [JsonProperty("friday")]
    public bool? Friday { get; set; }

    [JsonProperty("saturday")]
    public bool? Saturday { get; set; }

    [JsonProperty("sunday")]
    public bool? Sunday { get; set; }

    [JsonProperty("serviceDays")]
    public int? ServiceDays { get; set; }

    [JsonProperty("zoneId")]
    public int? ZoneId { get; set; }

    [JsonProperty("zoneName")]
    public string? ZoneName { get; set; }

    [JsonProperty("zoneNameEn")]
    public string? ZoneNameEn { get; set; }
}