using ClickNPick.Application.DeliveryModels;
using ClickNPick.Application.DtoModels.Delivery.Response;
using Newtonsoft.Json;

namespace ClickNPick.Web.Models.Delivery.Response;

public class QuartersResponseModel
{
    [JsonProperty("quarters")]
    public IEnumerable<Quarter>? Quarters { get; set; }

    public static QuartersResponseModel FromQuartersResponseDto(QuartersResponseDto dto)
    {
        var model = new QuartersResponseModel();

        model.Quarters = dto.Quarters;

        return model;
    }
}   
