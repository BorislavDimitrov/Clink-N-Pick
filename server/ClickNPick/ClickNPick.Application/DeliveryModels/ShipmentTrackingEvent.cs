using Newtonsoft.Json;

namespace ClickNPick.Application.Common;

public class ShipmentTrackingEvent
{
    [JsonProperty("isReceipt")]
    public bool? IsReceipt { get; set; }

    [JsonProperty("destinationType")]
    public string? DestinationType { get; set; }

    [JsonProperty("destinationDetails")]
    public string? DestinationDetails { get; set; }

    [JsonProperty("destinationDetailsEn")]
    public string? DestinationDetailsEn { get; set; }

    [JsonProperty("officeName")]
    public string? OfficeName { get; set; }

    [JsonProperty("officeNameEn")]
    public string? OfficeNameEn { get; set; }

    [JsonProperty("cityName")]
    public string? CityName { get; set; }

    [JsonProperty("cityNameEn")]
    public string? CityNameEn { get; set; }

    [JsonProperty("countryCode")]
    public string? CountryCode { get; set; }

    [JsonProperty("officeCode")]
    public string? OfficeCode { get; set; }

    [JsonProperty("time")]
    public long Time { get; set; }
}
