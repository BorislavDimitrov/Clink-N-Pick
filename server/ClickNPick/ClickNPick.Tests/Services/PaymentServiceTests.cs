using ClickNPick.Infrastructure.Services;
using Moq;
using Stripe;

namespace ClickNPick.Tests.Services
{
    public class PaymentServiceTests
    {
        private PaymentService _paymentService;

        [SetUp]
        public void SetUp()
        {
            _paymentService = new PaymentService();

        }

        [Test]
        public async Task CreatePaymentIntentShouldThrowExcetionDueToApiKEy()
        {
            decimal amount = 10.0m;
            string receiptEmail = "test@example.com";

            Assert.ThrowsAsync<StripeException>(async () =>
            {
                await _paymentService.CreatePaymentIntent(amount, receiptEmail);
            });
        }
    }
}
