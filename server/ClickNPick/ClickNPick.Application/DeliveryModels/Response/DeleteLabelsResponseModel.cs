using ClickNPick.Application.DeliveryModels;
using Newtonsoft.Json;

namespace ClickNPick.Web.Models.Delivery.Response;

public class DeleteLabelsResponseModel
{
    [JsonProperty("results")]
    public IEnumerable<DeleteLabelsResultElement>? Results { get; set; }

    public static DeleteLabelsResponseModel FromDeleteLabelsResponseDto(DeleteLabelsResponseDto dto)
    {
        return new DeleteLabelsResponseModel
        {
            Results = dto.Results,
        };
    }
}
