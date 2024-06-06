namespace ClickNPick.Application.Exceptions.PromotionPricings;

public class PromotionPricingNotFoundException : Exception
{
    private const string DefaultMessage = "Promotion pricing not found.";

    public PromotionPricingNotFoundException() : base(DefaultMessage) { }

    public PromotionPricingNotFoundException(string message) : base(message) { }
}
