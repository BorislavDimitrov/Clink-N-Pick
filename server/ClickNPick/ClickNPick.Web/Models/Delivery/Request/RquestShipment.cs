using ClickNPick.Application.DtoModels;

namespace ClickNPick.Web.Models.Delivery.Request
{
    public class RquestShipment
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

        public RequestShipmentRequestDto ToRequestShipmentRequestDto()
        {
            return new RequestShipmentRequestDto
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
                ProductId = this.ProductId
            };
        }
    }
}
