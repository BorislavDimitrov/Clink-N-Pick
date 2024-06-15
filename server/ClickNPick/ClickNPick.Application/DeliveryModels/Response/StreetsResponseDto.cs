using Newtonsoft.Json;

namespace ClickNPick.Application.DeliveryModels.Response
{
    public class StreetsResponseDto
    {
        [JsonProperty("streets")]
        public IEnumerable<Street>? Streets { get; set; }
    }
}
