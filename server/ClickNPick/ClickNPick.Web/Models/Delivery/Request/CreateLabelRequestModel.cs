using ClickNPick.Application.Attributes;
using ClickNPick.Application.Common;
using Newtonsoft.Json;

namespace ClickNPick.Web.Models.Delivery.Request;

public class CreateLabelRequestModel
{
    [JsonProperty("label")]
    public ShippingLabel? Label { get; set; }

    [JsonProperty("requestCourierTimeFrom")]
    public string? RequestCourierTimeFrom { get; set; }

    [JsonProperty("requestCourierTimeTo")]
    public string? RequestCourierTimeTo { get; set; }

    [JsonRequired]
    [JsonProperty("mode")]
    [RequestSupportedValues<string>("validate", "calculate", "create", "calculate_with_block")]
    public string? Mode { get; set; }

    public CreateLabelRequestDto ToCreateLabelRequestDto()
    {
        return new CreateLabelRequestDto()
        {
            Label = this.Label,
            RequestCourierTimeFrom = RequestCourierTimeFrom,
            RequestCourierTimeTo = RequestCourierTimeTo,
            Mode = this.Mode,
        };
    }
}
