using ClickNPick.Application.DtoModels.Delivery.Request;

namespace ClickNPick.Web.Models.Delivery.Request
{
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

        public string? CityOrVillage { get; set; }

        public string? PostCode { get; set; }

        public string? Quarter { get; set; }

        public string? Street { get; set; }

        public string? StreetNumber { get; set; }

        public string? DeliverAddressInfo { get; set; }

        public string? SenderOfficeCode { get; set; }

        public string DeliveryLocation { get; set; }


        public AcceptShipmentRequestDto ToAcceptShipmentRequestDto()
        {
            return new AcceptShipmentRequestDto
            {
                RequestShipmentId = this.RequestShipmentId,
                SenderName = this.SenderName,
                SenderPhoneNumber = this.SenderPhoneNumber,
                SendDate = this.SendDate,
                PackCount = this.PackCount,
                PaymentReceiverAmount = this.PaymentReceiverAmount,
                ShipmentType = this.ShipmentType,
                Weight = this.Weight,
                ShipmentDescription = this.ShipmentDescription,
                OrderNumber = this.OrderNumber,
                SenderOfficeCode = this.SenderOfficeCode,
                CityOrVillage = this.CityOrVillage,
                PostCode = this.PostCode,
                Quarter = this.Quarter,
                Street = this.Street,
                StreetNumber = this.StreetNumber,
                DeliverAddressInfo = this.DeliverAddressInfo,
                DeliveryLocation = this.DeliveryLocation,
            };
        }
    }
}
