using ClickNPick.Application.DtoModels.Products.Request;

namespace ClickNPick.Web.Models.Products.Request;

public class PromoteProductRequestModel
{
    public string ProductId { get; set; }

    public string PromotionPricingId { get; set; }

    public PromoteProductRequestDto ToPromoteProductRequestDto()
    {
        var dto = new PromoteProductRequestDto();

        dto.ProductId = this.ProductId;
        dto.PromotionPricingId = this.PromotionPricingId;

        return dto;
    }
}
