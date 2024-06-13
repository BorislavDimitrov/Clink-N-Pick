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

        public string ReceiverOfficeCode { get; set; }

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
                ProductId = ProductId,
            };
        }
    }
}
