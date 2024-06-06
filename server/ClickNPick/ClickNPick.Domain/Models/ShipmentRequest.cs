using ClickNPick.Domain.Models.Common;

namespace ClickNPick.Domain.Models
{
    public class ShipmentRequest : BaseModel<string>
    {
        public ShipmentRequest()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string? ShipmentNumber { get; set; }

        public string EmailOnDelivery { get; set; }

        public string ReceiverPhoneNumber { get; set; }

        public string ReceiverName { get; set; }

        public bool InvoiceBeforePayCD { get; set; }

        public string SmsOnDelivery { get; set; }

        public bool SmsNotification { get; set; }

        public bool GoodsReceipt { get; set; }

        public bool DeliveryReceipt { get; set; }

        public string ReceiverOfficeCode { get; set; }

        public ShipmentStatus ShipmentStatus { get; set; } = ShipmentStatus.Requested;

        public string ProductId { get; set; }

        public Product Product { get; set; }

        public string BuyerId { get; set; }

        public User Buyer { get; set; }

        public string SellerId { get; set; }

        public User Seller { get; set; }

    }
}