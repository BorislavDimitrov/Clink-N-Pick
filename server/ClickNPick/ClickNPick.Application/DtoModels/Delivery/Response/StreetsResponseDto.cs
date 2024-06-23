using ClickNPick.Application.DeliveryModels;
using Newtonsoft.Json;

namespace ClickNPick.Application.DtoModels.Delivery.Response;

public class StreetsResponseDto
{
    [JsonProperty("streets")]
    public IEnumerable<Street>? Streets { get; set; }
}
