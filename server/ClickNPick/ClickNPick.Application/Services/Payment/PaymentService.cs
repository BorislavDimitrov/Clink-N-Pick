using ClickNPick.Application.Services.Payment;
using Serilog;
using Stripe;

namespace ClickNPick.Infrastructure.Services;

public class PaymentService : IPaymentService
{
    private const string PaymentDescription = "Payment with amount of {0}$ was made by {1}.";

    private const string Currency = "eur";

    public async Task<string> CreatePaymentIntent(decimal amount, string receiptEmail)
    {
        var cents = (long)amount * 100;
        var paymentDescription = string.Format(PaymentDescription, amount, receiptEmail);

        var paymentOptions = new PaymentIntentCreateOptions
        {
            Amount = cents,
            Currency = Currency,
            ReceiptEmail = receiptEmail,
            AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
            {
                Enabled = true,
            },
            Description = paymentDescription,
        };

        var paymentIntentService = new PaymentIntentService();
        var paymentIntent = await paymentIntentService.CreateAsync(paymentOptions);

        Log.Information(paymentDescription);

        return paymentIntent.ClientSecret;
    }
}
