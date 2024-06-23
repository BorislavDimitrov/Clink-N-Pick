using Newtonsoft.Json;

namespace ClickNPick.Application.DeliveryModels.Request;

public class GetCourierStatusesRequestModel
{
    [JsonProperty(
   PropertyName = "requestCourierIds",
   Required = Required.Always)]
    public IEnumerable<string> RequestCourierIds { get; set; }
}
