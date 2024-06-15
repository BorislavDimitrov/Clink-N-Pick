using ClickNPick.Domain.Models;

namespace ClickNPick.Application.DtoModels.Delivery.Request
{
    public class RequestShipmentRequestDto
    {
        public string EmailOnDelivery { get; set; }

        public string ReceiverPhoneNumber { get; set; }

        public string ReceiverName { get; set; }

        public bool InvoiceBeforePayCD { get; set; }

        public string SmsOnDelivery { get; set; }

        public bool SmsNotification { get; set; }

        public bool GoodsReceipt { get; set; }

        public bool DeliveryReceipt { get; set; }

        public string? ReceiverOfficeCode { get; set; }

        public string? CityOrVillage { get; set; }

        public string? PostCode { get; set; }

        public string? Quarter { get; set; }

        public string? Street { get; set; }

        public string? StreetNumber { get; set; }

        public string? DeliverAddressInfo { get; set; }

        public string DeliveryLocation { get; set; }

        public string ProductId { get; set; }

        public string BuyerId { get; set; }

        public ShipmentRequest ToShipmentRequest()
        {
            return new ShipmentRequest
            {
                EmailOnDelivery = EmailOnDelivery,
                ReceiverPhoneNumber = ReceiverPhoneNumber,
                ReceiverName = ReceiverName,
                InvoiceBeforePayCD = InvoiceBeforePayCD,
                SmsOnDelivery = SmsOnDelivery,
                SmsNotification = SmsNotification,
                GoodsReceipt = GoodsReceipt,
                DeliveryReceipt = DeliveryReceipt,
                ReceiverOfficeCode = ReceiverOfficeCode,
                CityOrVillage = CityOrVillage,
                PostCode = PostCode,
                Quarter = Quarter,
                Street = Street,
                StreetNumber = StreetNumber,
                DeliverAddressInfo = DeliverAddressInfo,
                DeliveryLocation = Enum.Parse<DeliveryLocation>(DeliveryLocation),
                ProductId = ProductId,
            };
        }
    }
}
