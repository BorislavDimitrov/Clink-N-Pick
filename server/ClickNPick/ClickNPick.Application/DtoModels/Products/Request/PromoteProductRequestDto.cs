namespace ClickNPick.Application.DtoModels.Products.Request;

public class PromoteProductRequestDto
{
    public string ProductId { get; set; }

    public string PromotionPricingId { get; set; }

    public string UserId { get; set; }
}
