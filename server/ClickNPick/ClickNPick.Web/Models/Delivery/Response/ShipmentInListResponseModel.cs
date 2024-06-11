using ClickNPick.Application.DtoModels.Delivery.Response;
using ClickNPick.Domain.Models;

namespace ClickNPick.Web.Models.Delivery.Response
{
    public class ShipmentInListResponseModel
    {
        public string Id { get; set; }

        public string SellerId { get; set; }

        public string SellerUsername { get; set; }

        public string BuyerId { get; set; }

        public string BuyerUsername { get; set; }

        public string ProductId { get; set; }

        public string ProductTitle { get; set; }

        public string ShipmentStatus { get; set; }

        public static ShipmentInListResponseModel FromShipmentInListResponseDto(ShipmentInListResponseDto dto)
        {
            return new ShipmentInListResponseModel
            {
                Id = dto.Id,
                SellerId = dto.SellerId,
                SellerUsername = dto.SellerUsername,
                BuyerId = dto.BuyerId,
                BuyerUsername = dto.BuyerUsername,
                ProductId = dto.ProductId,
                ProductTitle = dto.ProductTitle,
                ShipmentStatus = dto.ShipmentStatus.ToString()
            };
        }
    }
}
}
