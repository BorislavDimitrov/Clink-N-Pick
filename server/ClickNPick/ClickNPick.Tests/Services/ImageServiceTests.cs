using AutoFixture;
using AutoFixture.AutoMoq;
using ClickNPick.Application.Abstractions.Repositories;
using ClickNPick.Application.Abstractions.Services;
using ClickNPick.Application.Exceptions.General;
using ClickNPick.Application.Services.Images;
using ClickNPick.Infrastructure.Data;
using ClickNPick.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ClickNPick.Tests.Services
{
    public class ImageServiceTests
    {
        private IFixture _fixture;
        private Mock<ICloudinaryService> _mockCloudinaryService;
        private IRepository<Domain.Models.Image> _imagesRepository;
        private ImagesService _imagesService;
        private DbContextOptionsBuilder<ClickNPickDbContext> _options;
        private ClickNPickDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _mockCloudinaryService = _fixture.Freeze<Mock<ICloudinaryService>>();
            _options = new DbContextOptionsBuilder<ClickNPickDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            _context = new ClickNPickDbContext(_options.Options);
            _imagesRepository = new Repository<Domain.Models.Image>(_context);
            _imagesService = new ImagesService(_mockCloudinaryService.Object, _imagesRepository);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task CreateImageAsyncShouldCreateImage()
        {
            var mockImageStream = new MemoryStream();
            using (var imagee = new Image<Rgba32>(1, 1))
            {
                imagee.SaveAsPng(mockImageStream);
            }
            mockImageStream.Position = 0;

            var formFileMock = new Mock<IFormFile>();
            formFileMock.Setup(_ => _.OpenReadStream()).Returns(mockImageStream);
            formFileMock.Setup(_ => _.FileName).Returns("test.png");
            formFileMock.Setup(_ => _.Length).Returns(mockImageStream.Length);

            _mockCloudinaryService
                .Setup(s => s.UploadPictureAsync(It.IsAny<byte[]>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((byte[] b, string s1, string s2, int w, int h) => ("http://example.com/image.jpg", "publicId"));

            var result = await _imagesService.CreateImageAsync(formFileMock.Object, 200, 200);

            var image = await _imagesRepository.AllAsNoTracking().FirstOrDefaultAsync();
            Assert.NotNull(image);
            Assert.AreEqual(result, image.Id);
            _mockCloudinaryService.Verify(s => s.UploadPictureAsync(It.IsAny<byte[]>(), It.IsAny<string>(), It.IsAny<string>(), 200, 200), Times.Once); ;
        }

        [Test]
        public void CreateImageAsyncShouldHandleExceptionDuringImageProcessing()
        {
            var formFileMock = new Mock<IFormFile>();
            formFileMock.Setup(_ => _.OpenReadStream()).Throws<Exception>();

            Assert.ThrowsAsync<Exception>(async () => await _imagesService.CreateImageAsync(formFileMock.Object, 200, 200));
        }

        [Test]
        public async Task DeleteImageAsyncShouldDeleteImage()
        {
            var image = new Domain.Models.Image { Id = "1", Url = "http://example.com/image.jpg", PublicId = "publicId" };
            _context.Images.Add(image);
            _context.SaveChanges();

            await _imagesService.DeleteImageAsync("1");

            var deletedImage = await _imagesRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == "1");
            Assert.Null(deletedImage);
            _mockCloudinaryService.Verify(s => s.DeleteImageAsync("publicId"), Times.Once);
        }

        [Test]
        public void DeleteImageAsyncShouldThrowNotFoundExceptionWhenImageNotFound()
        {
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await _imagesService.DeleteImageAsync("invalidId"));
            Assert.AreEqual("Image with id invalidId doesnt exist.", ex.Message);
        }

        [Test]
        public async Task GetImageByIdAsyncShouldReturnImage()
        {
            var image = new Domain.Models.Image { Id = "1", Url = "http://example.com/image.jpg", PublicId = "kkk" };
            _context.Images.Add(image);
            _context.SaveChanges();

            var result = await _imagesService.GetImageByIdAsync("1");

            Assert.NotNull(result);
            Assert.AreEqual(image.Id, result.Id);
        }

        [Test]
        public async Task GetImageByIdAsyncShouldReturnNullWhenImageNotFound()
        {
            var result = await _imagesService.GetImageByIdAsync("invalidId");
            Assert.IsNull(result);
        }
    }
}
