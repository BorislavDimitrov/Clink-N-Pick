using ClickNPick.Application.Enumerations;
using Newtonsoft.Json;

namespace ClickNPick.Application.Common;

public class CdPayOptions
{
    [JsonProperty("num")]
    public string? Num { get; set; }

    [JsonProperty("client")]
    public ClientProfile? Client { get; set; }

    [JsonProperty("moneyTransfer")]
    public bool? MoneyTransfer { get; set; }

    [JsonProperty("express")]
    public bool? Express { get; set; }

    [JsonProperty("method")]
    public string? Method { get; set; }

    [JsonProperty("address")]
    public Address? Address { get; set; }

    [JsonProperty("officeCode")]
    public string? OfficeCode { get; set; }

    [JsonProperty("IBAN")]
    public string? Iban { get; set; }

    [JsonProperty("BIC")]
    public string? Bic { get; set; }

    [JsonProperty("bankCurrency")]
    public string? BankCurrency { get; set; }

    [JsonProperty("payDays")]
    public List<int>? PayDays { get; set; }

    [JsonProperty("payWeekdays")]
    public List<Weekday>? PayWeekdays { get; set; }

    [JsonProperty("additionalInstructions")]
    public string? AdditionalInstructions { get; set; }

    [JsonProperty("departamentNum")]
    public int? DepartamentNum { get; set; }
}
