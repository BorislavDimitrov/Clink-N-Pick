using AutoFixture;
using AutoFixture.AutoMoq;
using ClickNPick.Application.Abstractions.Repositories;
using ClickNPick.Application.Services.PromotionPricings;
using ClickNPick.Domain.Models;
using ClickNPick.Infrastructure.Data;
using ClickNPick.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClickNPick.Tests.Services
{
    public class PromotionPricingServiceTests
    {
        private IFixture _fixture;
        private IRepository<PromotionPricing> _promotionPricingRepository;
        private PromotionPricingService _promotionPricingService;
        private DbContextOptionsBuilder<ClickNPickDbContext> _options;
        private ClickNPickDbContext _context;

        [SetUp]
        [TearDown]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _options = new DbContextOptionsBuilder<ClickNPickDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());
            _context = new ClickNPickDbContext(_options.Options);
            _promotionPricingRepository = new Repository<PromotionPricing>(_context);
            _promotionPricingService = new PromotionPricingService(_promotionPricingRepository);
        }

        [Test]
        public async Task GetAllAsync_PromotionsExist_ReturnsOrderedList()
        {
            var promotion1 = new PromotionPricing { Name = "premium", Id = "1", Price = 10 };
            var promotion2 = new PromotionPricing { Name = "diamon", Id = "2", Price = 5 };

            _context.Add(promotion1);
            _context.Add(promotion2);
            _context.SaveChanges();
            var result = await _promotionPricingService.GetAllAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Promotions.Count);
        }

        [Test]
        public async Task GetByIdAsyncShouldReturnNullForNonExistingPromotion()
        {
            var promotionPricing = await _promotionPricingService.GetByIdAsync("1");

            Assert.IsNull(promotionPricing);
        }

        [Test]
        public async Task GetByIdAsyncShouldReturnRightPricing()
        {

            var promotion1 = new PromotionPricing { Name = "premium", Id = "1", Price = 10 };
            _context.Add(promotion1);
            _context.SaveChanges();
            var promotionPricing = await _promotionPricingService.GetByIdAsync("1");

            Assert.NotNull(promotionPricing);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
    }
}

