namespace ClickNPick.Application.Services.Payment;

public interface IPaymentService
{
    public Task<string> CreatePaymentIntent(decimal amount, string receiptEmail);
}


