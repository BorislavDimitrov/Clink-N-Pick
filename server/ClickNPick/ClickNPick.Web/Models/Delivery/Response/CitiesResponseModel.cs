using ClickNPick.Application.Common;
using Newtonsoft.Json;

namespace ClickNPick.Web.Models.Delivery.Response;

public class CitiesResponseModel
{
    [JsonProperty("cities")]
    public IEnumerable<City>? Cities { get; set; }

    public static CitiesResponseModel FromCitiesResponseDto(CitiesResponseDto dto)
    {
        var model = new CitiesResponseModel();

        model.Cities = dto.Cities;
        
        return model; 
    }
}
