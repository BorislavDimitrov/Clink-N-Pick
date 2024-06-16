using ClickNPick.Application.Common;
using Newtonsoft.Json;

namespace ClickNPick.Application.DeliveryModels
{
    public class RequestCourierStatusResultElement
    {
        [JsonProperty("status")]
        public RequestCourierStatus Status { get; set; }

        [JsonProperty("error")]
        public Error Error { get; set; }
    }
}
