using ClickNPick.Application.Common;
using Newtonsoft.Json;

namespace ClickNPick.Web.Models.Delivery.Response;

public class OfficesResponseModel
{
    [JsonProperty("offices")]
    public IEnumerable<Office>? Offices { get; set; }

    public static OfficesResponseModel FromOfficesResponseDto(OfficesResponseDto dto)
    {
        return new OfficesResponseModel
        {
           Offices = dto.Offices,
        };
    }
}
