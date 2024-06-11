using ClickNPick.Domain.Models;

namespace ClickNPick.Application.DtoModels.Delivery.Response
{
    public class ShipmentInListResponseDto
    {
        public string Id { get; set; }

        public string SellerId { get; set; }

        public string SellerUsername { get; set; }

        public string BuyerId { get; set; }

        public string BuyerUsername { get; set; }

        public string ProductId { get; set; }

        public string ProductTitle { get; set; }

        public string ShipmentStatus { get; set; }

        public static ShipmentInListResponseDto FromShipmentRequest(ShipmentRequest shipmentRequest)
        {
            return new ShipmentInListResponseDto
            {
                Id = shipmentRequest.Id,
                SellerId = shipmentRequest.SellerId,
                SellerUsername = shipmentRequest.Seller.UserName,
                BuyerId = shipmentRequest.BuyerId,
                BuyerUsername = shipmentRequest.Buyer.UserName,
                ProductId = shipmentRequest.ProductId,
                ProductTitle = shipmentRequest.Product.Title,
                ShipmentStatus = shipmentRequest.ShipmentStatus.ToString()
            };
        }
    }
}
