using ClickNPick.Application.DeliveryModels;
using Newtonsoft.Json;

namespace ClickNPick.Application.DtoModels.Delivery.Response
{
    public class QuartersResponseDto
    {
        [JsonProperty("quarters")]
        public IEnumerable<Quarter>? Quarters { get; set; }
    }
}
