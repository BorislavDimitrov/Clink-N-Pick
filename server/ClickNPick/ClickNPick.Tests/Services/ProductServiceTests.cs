using AutoFixture;
using AutoFixture.AutoMoq;
using ClickNPick.Application.Abstractions.Services;
using ClickNPick.Application.DtoModels.Products.Request;
using ClickNPick.Application.Services.Categories;
using ClickNPick.Application.Services.Images;
using ClickNPick.Application.Services.Payment;
using ClickNPick.Application.Services.Products;
using ClickNPick.Application.Services.PromotionPricings;
using ClickNPick.Application.Services.Users;
using ClickNPick.Domain.Models;
using ClickNPick.Infrastructure.Data;
using ClickNPick.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ClickNPick.Tests.Services;

[TestFixture]
public class ProductServiceTests
{
    private IFixture _fixture;
    private Mock<ICloudinaryService> _mockedICloudinaryService;
    private Mock<IFormFile> _mockedIFormFile;
    private Mock<IUsersService> _mockedUsersService;
    private Mock<ImagesService> _mockedImagesService;
    private Mock<IPromotionPricingService> _mockedPromotionPricingService;
    private Mock<IPaymentService> _mockedStripeSerivce;
    private Mock<ICategoriesService> _mockedCategoriesService;
    private DbContextOptionsBuilder<ClickNPickDbContext> _options;
    private Repository<Product> _productRepository;
    private ClickNPickDbContext _context;
    private IProductsService _productsService;

    [SetUp]
    [TearDown]
    public void Setup()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _options = new DbContextOptionsBuilder<ClickNPickDbContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString());
        _mockedICloudinaryService = new Mock<ICloudinaryService>();
        _mockedUsersService = _fixture.Freeze< Mock<IUsersService>>();
        _mockedPromotionPricingService = _fixture.Freeze < Mock < IPromotionPricingService >>();
        _mockedImagesService = _fixture.Freeze < Mock < ImagesService >>();
        _mockedStripeSerivce = _fixture.Freeze < Mock < IPaymentService >>();
        _mockedCategoriesService = _fixture.Freeze < Mock < ICategoriesService >>();
        _mockedIFormFile = _fixture.Freeze < Mock<IFormFile>>();
        _context = new ClickNPickDbContext(_options.Options);
        _productRepository = new Repository<Product>(_context);
        //_mockedUsersService.Setup(x => x.GetByIdAsync("1")).Returns(Task.FromResult(new User()));
        //_mockedCategoriesService.Setup(x => x.GetByIdAsync("1")).Returns(Task.FromResult(new Category()));
        //_mockedImagesService.Setup(x => x.CreateImageAsync(_mockedIFormFile.Object, 100,100)).Returns(Task.FromResult("g"));
        //_mockedImagesService.Setup(x => x.GetImageByIdAsync("gegrg")).Returns(Task.FromResult(new Image()));
        _productsService = new ProductsService(
            _productRepository,
            _mockedUsersService.Object,_mockedImagesService.Object,_mockedStripeSerivce.Object,_mockedPromotionPricingService.Object, _mockedCategoriesService.Object);
    }


    [Test]

    public async Task CreateProductShouldBeCreateSuccessfully()
    {
        var newProduct = new CreateProductRequestDto
        {
            Title = "Product 1",
            CategoryId = "1",
            Description = "Test",
            CreatorId = "1",
            Price = 1,
            //Images = new List<IFormFile> { _mockedIFormFile.Object },
            //ThumbnailImage = _mockedIFormFile.Object
        };

        //var newProduct = _fixture.Create<CreateProductRequestDto>();
        await _productsService.CreateProductAsync(newProduct);


        Assert.AreEqual(1, _productRepository.AllAsNoTracking().Count());

    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }
}
