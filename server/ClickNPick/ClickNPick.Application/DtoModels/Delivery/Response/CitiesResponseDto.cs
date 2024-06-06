using ClickNPick.Application.Common;
using Newtonsoft.Json;

namespace ClickNPick.Web.Models.Delivery.Response;

public class CitiesResponseDto
{
    [JsonProperty("cities")]
    public IEnumerable<City>? Cities { get; set; }
}
