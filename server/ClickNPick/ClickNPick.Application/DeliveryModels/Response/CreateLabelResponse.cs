using ClickNPick.Application.Common;
using Newtonsoft.Json;

namespace ClickNPick.Web.Models.Delivery.Response;

public class CreateLabelResponse
{
    [JsonProperty("label")]
    public ShipmentStatus? Label { get; set; }

    [JsonProperty("blockingPaymentURL")]
    public string? BlockingPaymentUrl { get; set; }

    [JsonProperty("courierRequestID")]
    public int? CourierRequestId { get; set; }

    [JsonProperty("payAfterAcceptIgnored")]
    public string? PayAfterAcceptIgnored { get; set; }

}
