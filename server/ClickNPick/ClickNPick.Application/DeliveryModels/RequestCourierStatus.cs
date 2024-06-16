using ClickNPick.Application.Enumerations;
using Newtonsoft.Json;

namespace ClickNPick.Application.DeliveryModels
{
    public class RequestCourierStatus
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("status")]
        public RequestCourierStatusType Status { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("reject_reason")]
        public string RejectReason { get; set; }
    }
}
