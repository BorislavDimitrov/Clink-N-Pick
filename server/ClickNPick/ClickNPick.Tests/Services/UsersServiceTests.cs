using AutoFixture;
using AutoFixture.AutoMoq;
using ClickNPick.Application.Abstractions.Repositories;
using ClickNPick.Application.DtoModels.Users.Request;
using ClickNPick.Application.Exceptions.Identity;
using ClickNPick.Application.Services.Images;
using ClickNPick.Application.Services.Users;
using ClickNPick.Domain.Models;
using ClickNPick.Infrastructure.Data;
using ClickNPick.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ClickNPick.Tests.Services
{
    public class UsersServiceTests
    {
        private IFixture _fixture;
        private Mock<IImagesService> _mockImagesService;
        private IRepository<User> _usersRepository;
        private UsersService _usersService;
        private DbContextOptionsBuilder<ClickNPickDbContext> _options;
        private ClickNPickDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _mockImagesService = _fixture.Freeze<Mock<IImagesService>>();
            _options = new DbContextOptionsBuilder<ClickNPickDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            _context = new ClickNPickDbContext(_options.Options);
            _usersRepository = new Repository<User>(_context);
            _usersService = new UsersService(_mockImagesService.Object, _usersRepository);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task EditProfileAsync_ShouldEditProfileWithoutChangingImage()
        {
            // Arrange
            var user = new User { Id = "1", UserName = "OldName", PhoneNumber = "123", Address = "Old Address", Bio = "Old Bio", ImageId = "oldImageId" };
            _context.Users.Add(user);
            _context.SaveChanges();

            var model = new EditProfileRequestDto
            {
                UserId = "1",
                Username = "NewName",
                PhoneNumber = "456",
                Address = "New Address",
                Bio = "New Bio",
                Image = null
            };

            var result = await _usersService.EditProfileAsync(model);

            var updatedUser = await _usersRepository.AllAsNoTracking().FirstOrDefaultAsync(u => u.Id == "1");
            Assert.NotNull(updatedUser);
            Assert.AreEqual("NewName", updatedUser.UserName);
            Assert.AreEqual("456", updatedUser.PhoneNumber);
            Assert.AreEqual("New Address", updatedUser.Address);
            Assert.AreEqual("New Bio", updatedUser.Bio);
            Assert.AreEqual("oldImageId", updatedUser.ImageId);
            _mockImagesService.Verify(i => i.DeleteImageAsync(It.IsAny<string>()), Times.Never);
            _mockImagesService.Verify(i => i.CreateImageAsync(It.IsAny<IFormFile>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Test]
        public async Task EditProfileAsyncShouldEditProfileAndChangeImage()
        {
            var user = new User { Id = "1", UserName = "OldName", PhoneNumber = "123", Address = "Old Address", Bio = "Old Bio", ImageId = "oldImageId" };
            _context.Users.Add(user);
            _context.SaveChanges();

            var model = new EditProfileRequestDto
            {
                UserId = "1",
                Username = "NewName",
                PhoneNumber = "456",
                Address = "New Address",
                Bio = "New Bio",
            };

            _mockImagesService.Setup(i => i.CreateImageAsync(It.IsAny<IFormFile>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync("newImageId");
            _mockImagesService.Setup(i => i.GetImageByIdAsync(It.IsAny<string>())).ReturnsAsync(new Image { Url = "newImageUrl" });

            var result = await _usersService.EditProfileAsync(model);

            var updatedUser = await _usersRepository.AllAsNoTracking().FirstOrDefaultAsync(u => u.Id == "1");
            Assert.NotNull(updatedUser);
            Assert.AreEqual("NewName", updatedUser.UserName);
            Assert.AreEqual("456", updatedUser.PhoneNumber);
            Assert.AreEqual("New Address", updatedUser.Address);
            Assert.AreEqual("New Bio", updatedUser.Bio);
        }

        [Test]
        public void EditProfileAsync_ShouldThrowUserNotFoundException()
        {
            var model = new EditProfileRequestDto { UserId = "invalidId", Username = "NewName" };

            var ex = Assert.ThrowsAsync<UserNotFoundException>(async () => await _usersService.EditProfileAsync(model));
            Assert.AreEqual("User with id invalidId doesnt exist.", ex.Message);
        }

        [Test]
        public async Task GetUserEditProfile_ShouldReturnProfileInfo()
        {
            var image = new Image { Id = "imageId", Url = "http://example.com/image.jpg", PublicId = "kkkk" };
            _context.Images.Add(image);
            var user = new User { Id = "1", UserName = "TestUser", PhoneNumber = "123", Address = "Test Address", Bio = "Test Bio", ImageId = image.Id };
            _context.Users.Add(user);
            _context.SaveChanges();

            var result = await _usersService.GetUserEditProfile("1");

            Assert.NotNull(result);
            Assert.AreEqual(user.UserName, result.Username);
            Assert.AreEqual(user.PhoneNumber, result.PhoneNumber);
            Assert.AreEqual(user.Address, result.Address);
            Assert.AreEqual(user.Bio, result.Bio);
        }

        [Test]
        public void GetUserEditProfileShouldThrowUserNotFoundException()
        {
            var ex = Assert.ThrowsAsync<UserNotFoundException>(async () => await _usersService.GetUserEditProfile("invalidId"));
            Assert.AreEqual("User with id invalidId doesnt exist.", ex.Message);
        }

        [Test]
        public async Task GetProfileInfoAsync_ShouldReturnProfileInfo()
        {
            var image = new Image { Id = "imageId", Url = "http://example.com/image.jpg", PublicId = "kkkk" };
            _context.Images.Add(image);
            var user = new User { Id = "1", UserName = "TestUser", PhoneNumber = "123", Address = "Test Address", Bio = "Test Bio", ImageId = image.Id };

            _context.Users.Add(user);
            _context.SaveChanges();

            var result = await _usersService.GetProfileInfoAsync("1");

            Assert.NotNull(result);
            Assert.AreEqual(user.UserName, result.Username);
            Assert.AreEqual(user.PhoneNumber, result.PhoneNumber);
            Assert.AreEqual(user.Address, result.Address);
            Assert.AreEqual(user.Bio, result.Bio);
        }

        [Test]
        public void GetProfileInfoAsyncShouldThrowUserNotFoundException()
        {
            var ex = Assert.ThrowsAsync<UserNotFoundException>(async () => await _usersService.GetProfileInfoAsync("invalidId"));
            Assert.AreEqual("User with id invalidId doesnt exist.", ex.Message);
        }

        [Test]
        public async Task GetByIdAsyncShouldReturnUserWhenUserExists()
        {
            var image = new Image { Id = "imageId", Url = "http://example.com/image.jpg", PublicId = "kkkk" };
            _context.Images.Add(image);

            var user = new User { Id = "1", UserName = "TestUser", PhoneNumber = "123", Address = "Test Address", Bio = "Test Bio", ImageId = image.Id };
            _context.Users.Add(user);
            _context.SaveChanges();

            var result = await _usersService.GetByIdAsync("1");

            Assert.NotNull(result);
            Assert.AreEqual(user.Id, result.Id);
        }

        [Test]
        public async Task GetByIdAsyncShouldReturnNullWhenUserDoesNotExist()
        {
            var result = await _usersService.GetByIdAsync("invalidId");
            Assert.IsNull(result);
        }
    }
}