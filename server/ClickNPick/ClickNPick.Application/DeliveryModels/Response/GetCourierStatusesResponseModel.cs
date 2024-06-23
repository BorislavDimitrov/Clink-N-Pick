using Newtonsoft.Json;

namespace ClickNPick.Application.DeliveryModels.Response;

public class GetCourierStatusesResponseModel
{
    [JsonProperty("requestCourierStatus")]
    public IEnumerable<RequestCourierStatusResultElement> RequestCourierStatuses { get; set; }
}
