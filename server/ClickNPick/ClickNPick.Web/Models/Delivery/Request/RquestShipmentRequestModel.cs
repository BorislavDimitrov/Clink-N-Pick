using ClickNPick.Application.DtoModels.Delivery.Request;

namespace ClickNPick.Web.Models.Delivery.Request
{
    public class RquestShipmentRequestModel
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
                CityOrVillage = this.CityOrVillage,
                PostCode = this.PostCode,
                Quarter = this.Quarter,
                Street = this.Street,
                StreetNumber = this.StreetNumber,
                DeliverAddressInfo = this.DeliverAddressInfo,
                DeliveryLocation = this.DeliveryLocation,
                ProductId = this.ProductId
            };
        }
    }
}
