using ClickNPick.Application.DtoModels.Delivery.Request;

namespace ClickNPick.Web.Models.Delivery.Request;

public class RquestShipmentRequestModel
{
    public string EmailOnDelivery { get; set; }

    public string ReceiverPhoneNumber { get; set; }

    public string ReceiverName { get; set; }

    public bool SmsNotification { get; set; }

    public bool GoodsReceipt { get; set; }

    public bool DeliveryReceipt { get; set; }

    public string ReceiverOfficeCode { get; set; }

    public string CityOrVillage { get; set; }

    public string PostCode { get; set; }

    public string Quarter { get; set; }

    public string Street { get; set; }

    public string StreetNumber { get; set; }

    public string DeliverAddressInfo { get; set; }

    public string DeliveryLocation { get; set; }

    public string ProductId { get; set; }

    public RequestShipmentRequestDto ToRequestShipmentRequestDto()
    {
        var dto = new RequestShipmentRequestDto();

        dto.EmailOnDelivery = this.EmailOnDelivery;
        dto.ReceiverPhoneNumber = this.ReceiverPhoneNumber;
        dto.ReceiverName = this.ReceiverName;
        dto.SmsNotification = this.SmsNotification;
        dto.GoodsReceipt = this.GoodsReceipt;
        dto.DeliveryReceipt = this.DeliveryReceipt;
        dto.ReceiverOfficeCode = this.ReceiverOfficeCode;
        dto.CityOrVillage = this.CityOrVillage;
        dto.PostCode = this.PostCode;
        dto.Quarter = this.Quarter;
        dto.Street = this.Street;
        dto.StreetNumber = this.StreetNumber;
        dto.DeliverAddressInfo = this.DeliverAddressInfo;
        dto.DeliveryLocation = this.DeliveryLocation;
        dto.ProductId = this.ProductId;

        return dto;          
    }
}
