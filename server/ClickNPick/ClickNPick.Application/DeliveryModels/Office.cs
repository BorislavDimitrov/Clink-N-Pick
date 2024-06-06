using Newtonsoft.Json;

namespace ClickNPick.Application.Common;

public class Office
{
    [JsonProperty("id")]
    public int? Id { get; set; }

    [JsonProperty("code")]
    public string? Code { get; set; }

    [JsonProperty("isMPS")]
    public bool? IsMps { get; set; }

    [JsonProperty("isAPS")]
    public bool? IsAps { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("nameEn")]
    public string? NameEn { get; set; }

    [JsonProperty("phones")]
    public IEnumerable<string>? Phones { get; set; }

    [JsonProperty("emails")]
    public IEnumerable<string>? Emails { get; set; }

    [JsonProperty("address")]
    public Address? Address { get; set; }

    [JsonProperty("info")]
    public string? Info { get; set; }

    [JsonProperty("currency")]
    public string? Currency { get; set; }

    [JsonProperty("language")]
    public string? Language { get; set; }

    [JsonProperty("normalBusinessHoursFrom")]
    private long? NormalBusinessHoursFrom { get; set; }

    public string NormalBusinessHoursFromAsDate
        => ConvertFromTimeStamp(NormalBusinessHoursFrom);

    [JsonProperty("normalBusinessHoursTo")]
    private long? NormalBusinessHoursTo { get; set; }

    public string NormalBusinessHoursToAsDate
        => ConvertFromTimeStamp(NormalBusinessHoursTo);

    [JsonProperty("halfDayBusinessHoursFrom")]
    private long? HalfDayBusinessHoursFrom { get; set; }

    public string HalfDayBusinessHoursFromAsDate
        => ConvertFromTimeStamp(HalfDayBusinessHoursFrom);

    [JsonProperty("halfDayBusinessHoursTo")]
    private long? HalfDayBusinessHoursTo { get; set; }

    public string HalfDayBusinessHoursToAsDate
        => ConvertFromTimeStamp(HalfDayBusinessHoursTo);

    [JsonProperty("sundayBusinessHoursFrom")]
    private long? SundayBusinessHoursFrom { get; set; }

    public string SundayBusinessHoursFromAsDate
        => ConvertFromTimeStamp(SundayBusinessHoursFrom);

    [JsonProperty("sundayBusinessHoursTo")]
    private long? SundayBusinessHoursTo { get; set; }

    public string SundayBusinessHoursToAsDate
        => ConvertFromTimeStamp(SundayBusinessHoursTo);

    [JsonProperty("shipmentTypes")]
    public IEnumerable<string>? ShipmentTypes { get; set; }

    [JsonProperty("partnerCode")]
    public string? PartnerCode { get; set; }

    [JsonProperty("hubCode")]
    public string? HubCode { get; set; }

    [JsonProperty("hubName")]
    public string? HubName { get; set; }

    [JsonProperty("hubNameEn")]
    public string? HubNameEn { get; set; }

    [JsonProperty("isDrive")]
    public bool? IsDrive { get; set; }

    private string ConvertFromTimeStamp(long? milliseconds)
        => milliseconds.HasValue ? DateTimeOffset
            .FromUnixTimeMilliseconds(milliseconds.Value)
            .LocalDateTime
            .ToString("F") : string.Empty;
}
