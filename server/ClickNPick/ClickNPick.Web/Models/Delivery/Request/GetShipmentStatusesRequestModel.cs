using Newtonsoft.Json;

namespace ClickNPick.Web.Models.Delivery.Request;

public class GetShipmentStatusesRequestModel
{
    [JsonProperty(
        PropertyName = "shipmentNumbers",
        Required = Required.Always)]
    public IEnumerable<string> ShipmentNumbers { get; set; }

    public GetShipmentStatusesRequestDto ToGetShipmentStatusesRequestDto()
    {
        return new GetShipmentStatusesRequestDto
        {
            ShipmentNumbers = this.ShipmentNumbers,
        };
    }
}
