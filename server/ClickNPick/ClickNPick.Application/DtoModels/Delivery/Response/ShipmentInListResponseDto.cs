using ClickNPick.Domain.Models;

namespace ClickNPick.Application.DtoModels.Delivery.Response;

public class ShipmentInListResponseDto
{
    public string Id { get; set; }

    public string SellerId { get; set; }

    public string SellerUsername { get; set; }

    public string BuyerId { get; set; }

    public string BuyerUsername { get; set; }

    public string ProductId { get; set; }

    public string ProductTitle { get; set; }

    public string Status { get; set; }

    public static ShipmentInListResponseDto FromShipmentRequest(ShipmentRequest shipmentRequest)
    {
        var dto = new ShipmentInListResponseDto();

        dto.Id = shipmentRequest.Id;
        dto.SellerId = shipmentRequest.SellerId;
        dto.SellerUsername = shipmentRequest.Seller.UserName;
        dto.BuyerId = shipmentRequest.BuyerId;
        dto.BuyerUsername = shipmentRequest.Buyer.UserName;
        dto.ProductId = shipmentRequest.ProductId;
        dto.ProductTitle = shipmentRequest.Product.Title;
        dto.Status = shipmentRequest.ShipmentStatus.ToString();

        return dto;
    }
}
