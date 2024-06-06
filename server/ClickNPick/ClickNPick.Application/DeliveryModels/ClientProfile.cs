using Newtonsoft.Json;

namespace ClickNPick.Application.Common;

public class ClientProfile
{
    [JsonProperty("id")]
    public int? Id { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("nameEn")]
    public string? NameEn { get; set; }

    [JsonProperty("phones")]
    public List<string>? Phones { get; set; }

    [JsonProperty("email")]
    public string? Email { get; set; }

    [JsonProperty("skypeAccounts")]
    public List<string>? SkypeAccounts { get; set; }

    [JsonProperty("clientNumber")]
    public string? ClientNumber { get; set; }

    [JsonProperty("clientNumberEn")]
    public string? ClientNumberEn { get; set; }

    [JsonProperty("juridicalEntity")]
    public bool? JuridicalEntity { get; set; }

    [JsonProperty("personalIDType")]
    public string? PersonalIdType { get; set; }

    [JsonProperty("personalIDNumber")]
    public string? PersonalIdNumber { get; set; }

    [JsonProperty("companyType")]
    public string? CompanyType { get; set; }

    [JsonProperty("ein")]
    public string? Ein { get; set; }

    [JsonProperty("ddsEinPrefix")]
    public string? DdsEinPrefix { get; set; }

    [JsonProperty("ddsEin")]
    public string? DdsEin { get; set; }

    [JsonProperty("registrationAddress")]
    public string? RegistrationAddress { get; set; }

    [JsonProperty("molName")]
    public string? MolName { get; set; }

    [JsonProperty("molEGN")]
    public string? MolEgn { get; set; }

    [JsonProperty("molIDNum")]
    public string? MolIdNum { get; set; }
}
