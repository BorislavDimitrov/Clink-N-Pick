using ClickNPick.Domain.Models;

namespace ClickNPick.Application.DtoModels
{
    public class RequestShipmentRequestDto
    {
        public string ShipmentNumber { get; set; }

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
                EmailOnDelivery = this.EmailOnDelivery,
                ReceiverPhoneNumber = this.ReceiverPhoneNumber,
                ReceiverName = this.ReceiverName,
                InvoiceBeforePayCD = this.InvoiceBeforePayCD,
                SmsOnDelivery = this.SmsOnDelivery,
                SmsNotification = this.SmsNotification,
                GoodsReceipt = this.GoodsReceipt,
                DeliveryReceipt = this.DeliveryReceipt,
                ReceiverOfficeCode = this.ReceiverOfficeCode,
                ProductId = this.ProductId,
            };
        }
    }
}
