using ClickNPick.Domain.Models;
using ClickNPick.Domain.Models.Enums;

namespace ClickNPick.Application.DtoModels.Delivery.Request;

public class RequestShipmentRequestDto
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

    public string BuyerId { get; set; }

    public ShipmentRequest ToShipmentRequest()
    {
        var shipmentRequest = new ShipmentRequest();

        shipmentRequest.EmailOnDelivery = EmailOnDelivery;
        shipmentRequest.ReceiverPhoneNumber = ReceiverPhoneNumber;
        shipmentRequest.ReceiverName = ReceiverName;
        shipmentRequest.SmsOnDelivery = ReceiverPhoneNumber;
        shipmentRequest.SmsNotification = SmsNotification;
        shipmentRequest.GoodsReceipt = GoodsReceipt;
        shipmentRequest.DeliveryReceipt = DeliveryReceipt;
        shipmentRequest.ReceiverOfficeCode = ReceiverOfficeCode;
        shipmentRequest.CityOrVillage = CityOrVillage;
        shipmentRequest.PostCode = PostCode;
        shipmentRequest.Quarter = Quarter;
        shipmentRequest.Street = Street;
        shipmentRequest.StreetNumber = StreetNumber;
        shipmentRequest.DeliverAddressInfo = DeliverAddressInfo;
        shipmentRequest.DeliveryLocation = Enum.Parse<DeliveryLocation>(DeliveryLocation);
        shipmentRequest.ProductId = ProductId;

        return shipmentRequest;
    }
}
