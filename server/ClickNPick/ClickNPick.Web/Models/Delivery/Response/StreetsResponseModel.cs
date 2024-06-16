using ClickNPick.Application.DeliveryModels;
using ClickNPick.Application.DtoModels.Delivery.Response;
using Newtonsoft.Json;

namespace ClickNPick.Web.Models.Delivery.Response
{
    public class StreetsResponseModel
    {
        [JsonProperty("streets")]
        public IEnumerable<Street>? Streets { get; set; }

        public static StreetsResponseModel FromStreetsResponseDto(StreetsResponseDto dto)
        {
            return new StreetsResponseModel
            {
                Streets = dto.Streets,
            };
        }
    }
}
