using ClickNPick.Application.DtoModels.Products.Request;

namespace ClickNPick.Web.Models.Products.Request;

public class PromoteProductRequestModel
{
    public string ProductId { get; set; }

    public string PromotionPricingId { get; set; }

    public PromoteProductRequestDto ToPromoteProductRequestDto()
    {
        return new PromoteProductRequestDto
        {
            ProductId = this.ProductId,
            PromotionPricingId = this.PromotionPricingId
        };
    }
}
