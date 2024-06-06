using ClickNPick.Application.Common;
using Newtonsoft.Json;

namespace ClickNPick.Web.Models.Delivery.Response;

public class OfficesResponseDto
{
    [JsonProperty("offices")]
    public IEnumerable<Office>? Offices { get; set; }
}
