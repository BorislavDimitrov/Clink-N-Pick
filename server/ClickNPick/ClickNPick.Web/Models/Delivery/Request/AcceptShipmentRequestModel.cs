using ClickNPick.Application.DtoModels.Delivery.Request;

namespace ClickNPick.Web.Models.Delivery.Request;

public class AcceptShipmentRequestModel
{
    public string RequestShipmentId { get; set; }

    public string SenderName { get; set; }

    public string SenderPhoneNumber { get; set; }

    public DateTime SendDate { get; set; }

    public int PackCount { get; set; }

    public double PaymentReceiverAmount { get; set; }

    public string ShipmentType { get; set; }

    public double Weight { get; set; }

    public string ShipmentDescription { get; set; }

    public string OrderNumber { get; set; }

    public string CityOrVillage { get; set; }

    public string PostCode { get; set; }

    public string Quarter { get; set; }

    public string Street { get; set; }

    public string StreetNumber { get; set; }

    public string DeliverAddressInfo { get; set; }

    public string SenderOfficeCode { get; set; }

    public DateTime RequestTimeFrom { get; set; }

    public DateTime RequestTimeTo { get; set; }

    public string DeliveryLocation { get; set; }


    public AcceptShipmentRequestDto ToAcceptShipmentRequestDto()
    {
        var dto = new AcceptShipmentRequestDto();

        dto.RequestShipmentId = this.RequestShipmentId;
        dto.SenderName = this.SenderName;
        dto.SenderPhoneNumber = this.SenderPhoneNumber;
        dto.SendDate = this.SendDate;
        dto.PackCount = this.PackCount;
        dto.PaymentReceiverAmount = this.PaymentReceiverAmount;
        dto.ShipmentType = this.ShipmentType;
        dto.Weight = this.Weight;
        dto.ShipmentDescription = this.ShipmentDescription;
        dto.OrderNumber = this.OrderNumber;
        dto.SenderOfficeCode = this.SenderOfficeCode;
        dto.CityOrVillage = this.CityOrVillage;
        dto.PostCode = this.PostCode;
        dto.Quarter = this.Quarter;
        dto.Street = this.Street;
        dto.StreetNumber = this.StreetNumber;
        dto.DeliverAddressInfo = this.DeliverAddressInfo;
        dto.DeliveryLocation = this.DeliveryLocation;
        dto.RequestTimeFrom = this.RequestTimeFrom;
        dto.RequestTimeTo = this.RequestTimeTo;

        return dto;
    }
}
