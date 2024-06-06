using ClickNPick.Application.Common;
using Newtonsoft.Json;

namespace ClickNPick.Web.Models.Delivery.Response;

public class CreateLabelResponseModel
{
    [JsonProperty("label")]
    public ShipmentStatus? Label { get; set; }

    [JsonProperty("blockingPaymentURL")]
    public string? BlockingPaymentUrl { get; set; }

    [JsonProperty("courierRequestID")]
    public int? CourierRequestId { get; set; }

    [JsonProperty("payAfterAcceptIgnored")]
    public string? PayAfterAcceptIgnored { get; set; }

    public static CreateLabelResponseModel FromCreateLabelResponseDto(CreateLabelResponseDto dto)
    {
        return new CreateLabelResponseModel
        {
            Label = dto.Label,
            BlockingPaymentUrl = dto.BlockingPaymentUrl,
            CourierRequestId = dto.CourierRequestId,
            PayAfterAcceptIgnored = dto.PayAfterAcceptIgnored,
        };
    }
}
