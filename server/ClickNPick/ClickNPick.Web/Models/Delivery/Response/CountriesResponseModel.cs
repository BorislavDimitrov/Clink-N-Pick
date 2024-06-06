using ClickNPick.Application.Common;
using Newtonsoft.Json;

namespace ClickNPick.Web.Models.Delivery.Response;

public class CountriesResponseModel
{
    [JsonProperty("countries")]
    public IEnumerable<Country>? Countries { get; set; }

    public static CountriesResponseModel FromCountriesResponseModelDto(CountriesResponseDto dto)
    {
        return new CountriesResponseModel
        {
            Countries = dto.Countries,
        };
    }
}
