using ClickNPick.Application.Common;
using Newtonsoft.Json;

namespace ClickNPick.Web.Models.Delivery.Response;

public class CountriesResponseDto
{
    [JsonProperty("countries")]
    public IEnumerable<Country>? Countries { get; set; }
}
