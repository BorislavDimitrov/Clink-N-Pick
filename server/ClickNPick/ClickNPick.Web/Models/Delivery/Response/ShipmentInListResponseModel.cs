using ClickNPick.Application.DtoModels.Delivery.Response;

namespace ClickNPick.Web.Models.Delivery.Response;

public class ShipmentInListResponseModel
{
    public string Id { get; set; }

    public string SellerId { get; set; }

    public string SellerUsername { get; set; }

    public string BuyerId { get; set; }

    public string BuyerUsername { get; set; }

    public string ProductId { get; set; }

    public string ProductTitle { get; set; }

    public string Status { get; set; }

    public static ShipmentInListResponseModel FromShipmentInListResponseDto(ShipmentInListResponseDto dto)
    {
        var model =  new ShipmentInListResponseModel();

        model.Id = dto.Id;
        model.SellerId = dto.SellerId;
        model.SellerUsername = dto.SellerUsername;
        model.BuyerId = dto.BuyerId;
        model.BuyerUsername = dto.BuyerUsername;
        model.ProductId = dto.ProductId;
        model.ProductTitle = dto.ProductTitle;
        model.Status = dto.Status.ToString();
        
        return model;
    }
}

