using ClickNPick.Application.DeliveryModels;
using Newtonsoft.Json;

namespace ClickNPick.Web.Models.Delivery.Response;

public class DeleteLabelsResponseDto
{
    [JsonProperty("results")]
    public IEnumerable<DeleteLabelsResultElement>? Results { get; set; }
}
