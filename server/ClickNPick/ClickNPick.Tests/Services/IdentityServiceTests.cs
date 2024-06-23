using ClickNPick.Application.Abstractions.Repositories;
using ClickNPick.Application.Abstractions.Services;
using ClickNPick.Application.DtoModels.Users.Request;
using ClickNPick.Application.Exceptions.General;
using ClickNPick.Application.Exceptions.Identity;
using ClickNPick.Application.Services.Identity;
using ClickNPick.Application.Services.Images;
using ClickNPick.Domain.Models;
using ClickNPick.Infrastructure.Data;
using ClickNPick.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Text;

namespace ClickNPick.Tests.Services
{
    public class IdentityServiceTests
    {
        private Mock<UserManager<User>> _mockedUserManager;
        private IRepository<User> _usersRepository;
        private Mock<ITokenGeneratorService> _mockedTokenGeneratorService;
        private Mock<IEmailSender> _mockedEmailSender;
        private Mock<IImagesService> _mockedImagesService;
        private IdentityService _identityService;
        private DbContextOptionsBuilder<ClickNPickDbContext> _options;
        private ClickNPickDbContext _context;

        [SetUp]
        public void Setup()
        {
            _mockedUserManager = new Mock<UserManager<User>>(
                new Mock<IUserStore<User>>().Object, null, null, null, null, null, null, null, null
            );
            _options = new DbContextOptionsBuilder<ClickNPickDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());
            _context = new ClickNPickDbContext(_options.Options);
            _usersRepository = new Repository<User>(_context);
            _mockedTokenGeneratorService = new Mock<ITokenGeneratorService>();
            _mockedEmailSender = new Mock<IEmailSender>();
            _mockedImagesService = new Mock<IImagesService>();

            _identityService = new IdentityService(
                _mockedUserManager.Object,
                _usersRepository,
                _mockedTokenGeneratorService.Object,
                _mockedEmailSender.Object,
                _mockedImagesService.Object
            );
        }

        [Test]
        public async Task CreateUserAsyncShouldCreateUserAndSendEmail()
        {
            var registerRequest = new RegisterRequestDto
            {
                Email = "test@example.com",
                Username = "testuser",
                Password = "Password123!"
            };

            var imageId = "testImageId";
            var newImage = new Image { Id = imageId };

            _mockedUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _mockedUserManager.Setup(x => x.GetUserIdAsync(It.IsAny<User>()))
                .ReturnsAsync("testUserId");
            _mockedUserManager.Setup(x => x.GenerateEmailConfirmationTokenAsync(It.IsAny<User>()))
                .ReturnsAsync("testToken");

            _mockedImagesService.Setup(x => x.CreateImageAsync(It.IsAny<IFormFile>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(imageId);
            _mockedImagesService.Setup(x => x.GetImageByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(newImage);

            _mockedEmailSender.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            _mockedUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            await _identityService.CreateUserAsync(registerRequest);

            _mockedUserManager.Verify(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
            _mockedEmailSender.Verify(x => x.SendEmailAsync(registerRequest.Email, "Confirm your email", It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task CreateUserAsyncShouldDeleteImageIfUserCreationFails()
        {
            var registerRequest = new RegisterRequestDto
            {
                Email = "test@example.com",
                Username = "testuser",
                Password = "Password123!"
            };

            var imageId = "testImageId";
            var newImage = new Image { Id = imageId };

            _mockedUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "User creation failed" }));

            _mockedImagesService.Setup(x => x.CreateImageAsync(It.IsAny<IFormFile>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(imageId);
            _mockedImagesService.Setup(x => x.GetImageByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(newImage);

            _mockedUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            var ex = Assert.ThrowsAsync<OperationFailedException>(async () =>
                await _identityService.CreateUserAsync(registerRequest));
            Assert.That(ex.Message, Is.EqualTo("The creation of user failed."));
            _mockedImagesService.Verify(x => x.DeleteImageAsync(imageId), Times.Once);
        }

        [Test]
        public void CreateUserAsyncShouldHandleImageCreationFailure()
        {
            var registerRequest = new RegisterRequestDto
            {
                Email = "test@example.com",
                Username = "testuser",
                Password = "Password123!"
            };

            _mockedImagesService.Setup(x => x.CreateImageAsync(It.IsAny<IFormFile>(), It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("Image creation failed"));

            _mockedUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            Assert.ThrowsAsync<Exception>(async () =>
                await _identityService.CreateUserAsync(registerRequest));
        }

        [Test]
        public async Task CreateUserAsyncShouldHandleEmailSendingFailure()
        {
            var registerRequest = new RegisterRequestDto
            {
                Email = "test@example.com",
                Username = "testuser",
                Password = "Password123!"
            };

            var imageId = "testImageId";
            var newImage = new Image { Id = imageId };

            _mockedUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _mockedUserManager.Setup(x => x.GetUserIdAsync(It.IsAny<User>()))
                .ReturnsAsync("testUserId");
            _mockedUserManager.Setup(x => x.GenerateEmailConfirmationTokenAsync(It.IsAny<User>()))
                .ReturnsAsync("testToken");

            _mockedImagesService.Setup(x => x.CreateImageAsync(It.IsAny<IFormFile>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(imageId);
            _mockedImagesService.Setup(x => x.GetImageByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(newImage);

            _mockedEmailSender.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("Email sending failed"));

            _mockedUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _identityService.CreateUserAsync(registerRequest));
            Assert.That(ex.Message, Is.EqualTo("Email sending failed"));
        }

        [Test]
        public async Task ConfirmEmailAsync_ShouldSucceed()
        {
            var userId = "testUserId";
            var token = "testToken";
            var user = new User { Id = userId, Email = "test@example.com", EmailConfirmed = false };

            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var encodedUserId = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(userId));

            var model = new EmailConfirmationRequestDto
            {
                EmailConfirmationToken = encodedToken,
                UserId = encodedUserId
            };

            _mockedUserManager.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync(user);
            _mockedUserManager.Setup(x => x.ConfirmEmailAsync(user, token)).ReturnsAsync(IdentityResult.Success);

            await _identityService.ConfirmEmailAsync(model);

            _mockedUserManager.Verify(x => x.FindByIdAsync(userId), Times.Once);
            _mockedUserManager.Verify(x => x.ConfirmEmailAsync(user, token), Times.Once);
        }

        [Test]
        public void ConfirmEmailAsyncShouldThrowUserNotFoundException()
        {
            var userId = "nonexistentUserId";
            var token = "testToken";

            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var encodedUserId = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(userId));

            var model = new EmailConfirmationRequestDto
            {
                EmailConfirmationToken = encodedToken,
                UserId = encodedUserId
            };

            _mockedUserManager.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync((User)null);

            var ex = Assert.ThrowsAsync<UserNotFoundException>(async () => await _identityService.ConfirmEmailAsync(model));
            Assert.That(ex.Message, Is.EqualTo($"User with id {userId} doesnt exist."));
        }

        [Test]
        public void ConfirmEmailAsyncShouldThrowInvalidOperationExceptionIfEmailAlreadyConfirmed()
        {
            var userId = "testUserId";
            var token = "testToken";
            var user = new User { Id = userId, Email = "test@example.com", EmailConfirmed = true };

            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var encodedUserId = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(userId));

            var model = new EmailConfirmationRequestDto
            {
                EmailConfirmationToken = encodedToken,
                UserId = encodedUserId
            };

            _mockedUserManager.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync(user);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _identityService.ConfirmEmailAsync(model));
            Assert.That(ex.Message, Is.EqualTo($"The email {user.Email} is already confirmed."));
        }

        [Test]
        public void ConfirmEmailAsyncShouldThrowOperationFailedExceptionIfEmailConfirmationFails()
        {
            var userId = "testUserId";
            var token = "testToken";
            var user = new User { Id = userId, Email = "test@example.com", EmailConfirmed = false };

            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var encodedUserId = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(userId));

            var model = new EmailConfirmationRequestDto
            {
                EmailConfirmationToken = encodedToken,
                UserId = encodedUserId
            };

            _mockedUserManager.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync(user);
            _mockedUserManager.Setup(x => x.ConfirmEmailAsync(user, token)).ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Confirmation failed" }));

            var ex = Assert.ThrowsAsync<OperationFailedException>(async () => await _identityService.ConfirmEmailAsync(model));
            Assert.That(ex.Message, Is.EqualTo($"The confirmation of email {user.Email} failed."));
        }

        [Test]
        public void ForgotPasswordSendLinkAsync_ShouldThrowUserNotFoundException()
        {
            // Arrange
            var model = new ForgotPasswordRequestDto { Email = "test@example.com" };
            _mockedUserManager.Setup(x => x.FindByEmailAsync(model.Email)).ReturnsAsync((User)null);

            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(async () =>
                await _identityService.ForgotPasswordSendLinkAsync(model));
        }

        [Test]
        public void ForgotPasswordSendLinkAsync_ShouldThrowInvalidOperationExceptionIfEmailNotConfirmed()
        {
            // Arrange
            var model = new ForgotPasswordRequestDto { Email = "test@example.com" };
            var user = new User { Email = model.Email, EmailConfirmed = false };
            _mockedUserManager.Setup(x => x.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            _mockedUserManager.Setup(x => x.IsEmailConfirmedAsync(user)).ReturnsAsync(false);

            // Act & Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _identityService.ForgotPasswordSendLinkAsync(model));
            Assert.That(ex.Message, Is.EqualTo($"The email {model.Email} is not confirmed yet."));
        }

        [Test]
        public async Task ForgotPasswordSendLinkAsyncShouldSucceed()
        {
            var model = new ForgotPasswordRequestDto { Email = "test@example.com" };
            var user = new User { Email = model.Email, UserName = "testuser", EmailConfirmed = true };
            var token = "testToken";

            _mockedUserManager.Setup(x => x.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            _mockedUserManager.Setup(x => x.IsEmailConfirmedAsync(user)).ReturnsAsync(true);
            _mockedUserManager.Setup(x => x.GeneratePasswordResetTokenAsync(user)).ReturnsAsync(token);
            _mockedEmailSender.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);

            await _identityService.ForgotPasswordSendLinkAsync(model);

            _mockedUserManager.Verify(x => x.FindByEmailAsync(model.Email), Times.Once);
            _mockedUserManager.Verify(x => x.IsEmailConfirmedAsync(user), Times.Once);
            _mockedUserManager.Verify(x => x.GeneratePasswordResetTokenAsync(user), Times.Once);
            _mockedEmailSender.Verify(x => x.SendEmailAsync(user.Email, "Forgot password", It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ForgotPasswordSendLinkAsyncShouldHandleEmailSendingFailure()
        {
            var model = new ForgotPasswordRequestDto { Email = "test@example.com" };
            var user = new User { Email = model.Email, UserName = "testuser", EmailConfirmed = true };
            var token = "testToken";

            _mockedUserManager.Setup(x => x.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            _mockedUserManager.Setup(x => x.IsEmailConfirmedAsync(user)).ReturnsAsync(true);
            _mockedUserManager.Setup(x => x.GeneratePasswordResetTokenAsync(user)).ReturnsAsync(token);
            _mockedEmailSender.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception("Email sending failed"));

            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _identityService.ForgotPasswordSendLinkAsync(model));
            Assert.That(ex.Message, Is.EqualTo("Email sending failed"));
        }

        [Test]
        public async Task ResetPasswordByLinkAsyncShouldSucceed()
        {
            var model = new ResetPasswordRequestDto
            {
                ResetPasswordToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes("testToken")),
                Email = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes("testuser@example.com")),
                Password = "NewPassword123!"
            };

            var user = new User { Email = "testuser@example.com" };

            _mockedUserManager.Setup(x => x.FindByEmailAsync("testuser@example.com")).ReturnsAsync(user);
            _mockedUserManager.Setup(x => x.ResetPasswordAsync(user, "testToken", "NewPassword123!")).ReturnsAsync(IdentityResult.Success);

            await _identityService.ResetPasswordByLinkAsync(model);

            _mockedUserManager.Verify(x => x.FindByEmailAsync("testuser@example.com"), Times.Once);
            _mockedUserManager.Verify(x => x.ResetPasswordAsync(user, "testToken", "NewPassword123!"), Times.Once);
        }

        [Test]
        public void ResetPasswordByLinkAsyncShouldThrowOperationFailedExceptionIfResetFails()
        {
            var model = new ResetPasswordRequestDto
            {
                ResetPasswordToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes("testToken")),
                Email = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes("testuser@example.com")),
                Password = "NewPassword123!"
            };

            var user = new User { Email = "testuser@example.com" };

            _mockedUserManager.Setup(x => x.FindByEmailAsync("testuser@example.com")).ReturnsAsync(user);
            _mockedUserManager.Setup(x => x.ResetPasswordAsync(user, "testToken", "NewPassword123!")).ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Reset failed" }));

            var ex = Assert.ThrowsAsync<OperationFailedException>(async () =>
                await _identityService.ResetPasswordByLinkAsync(model));
            Assert.That(ex.Message, Is.EqualTo("The reseting of the password failed."));
        }

        [Test]
        public void LoginAsyncShouldThrowInvalidOperationExceptionIfEmailNotConfirmed()
        {
            // Arrange
            var model = new LoginRequestDto { Email = "test@example.com", Password = "Password123!" };
            var user = new User { Email = "test@example.com", EmailConfirmed = false };
            _mockedUserManager.Setup(x => x.FindByEmailAsync(model.Email)).ReturnsAsync(user);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _identityService.LoginAsync(model));
            Assert.That(ex.Message, Is.EqualTo($"The email {model.Email} is not confirmed."));
        }

        [Test]
        public void LoginAsyncShouldThrowArgumentExceptionIfPasswordIncorrect()
        {
            var model = new LoginRequestDto { Email = "test@example.com", Password = "WrongPassword123!" };
            var user = new User { Email = "test@example.com", EmailConfirmed = true };
            _mockedUserManager.Setup(x => x.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            _mockedUserManager.Setup(x => x.CheckPasswordAsync(user, model.Password)).ReturnsAsync(false);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await _identityService.LoginAsync(model));
            Assert.That(ex.Message, Is.EqualTo("The provided password is not right."));
        }

        [Test]
        public async Task LoginAsyncShouldSucceed()
        {
            var model = new LoginRequestDto { Email = "test@example.com", Password = "Password123!" };
            var user = new User { Email = "test@example.com", EmailConfirmed = true, ImageId = "testImageId" };
            var token = "testToken";
            var imageUrl = "http://example.com/image.jpg";

            _mockedUserManager.Setup(x => x.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            _mockedUserManager.Setup(x => x.CheckPasswordAsync(user, model.Password)).ReturnsAsync(true);
            _mockedTokenGeneratorService.Setup(x => x.GenerateToken(user)).ReturnsAsync(token);
            _mockedImagesService.Setup(x => x.GetImageByIdAsync(user.ImageId)).ReturnsAsync(new Image { Url = imageUrl });

            var result = await _identityService.LoginAsync(model);

            Assert.NotNull(result);
            Assert.AreEqual(token, result.Token);
            Assert.AreEqual(imageUrl, result.UserImageUrl);

            _mockedUserManager.Verify(x => x.FindByEmailAsync(model.Email), Times.Once);
            _mockedUserManager.Verify(x => x.CheckPasswordAsync(user, model.Password), Times.Once);
            _mockedTokenGeneratorService.Verify(x => x.GenerateToken(user), Times.Once);
            _mockedImagesService.Verify(x => x.GetImageByIdAsync(user.ImageId), Times.Once);
        }

        [Test]
        public void ChangePasswordAsyncShouldThrowInvalidOperationExceptionIfOldPasswordSameAsNew()
        {
            var model = new ChangePasswordRequestDto
            {
                UserId = "testUserId",
                OldPassword = "Password123!",
                NewPassword = "Password123!" // Same as old password
            };
            var user = new User { Id = "testUserId" };
            _mockedUserManager.Setup(x => x.FindByIdAsync(model.UserId)).ReturnsAsync(user);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _identityService.ChangePasswordAsync(model));
            Assert.That(ex.Message, Is.EqualTo("The new password cannot be same as the old one."));
        }

        [Test]
        public void ChangePasswordAsyncShouldThrowOperationFailedExceptionIfChangeFails()
        {
            var model = new ChangePasswordRequestDto
            {
                UserId = "testUserId",
                OldPassword = "OldPassword123!",
                NewPassword = "NewPassword123!"
            };
            var user = new User { Id = "testUserId" };
            _mockedUserManager.Setup(x => x.FindByIdAsync(model.UserId)).ReturnsAsync(user);
            _mockedUserManager.Setup(x => x.ChangePasswordAsync(user, model.OldPassword, model.NewPassword))
                              .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Change failed" }));

            var ex = Assert.ThrowsAsync<OperationFailedException>(async () =>
                await _identityService.ChangePasswordAsync(model));
            Assert.That(ex.Message, Is.EqualTo("The changing of the password failed."));
        }

        [Test]
        public async Task ChangePasswordAsyncShouldSucceed()
        {

            var model = new ChangePasswordRequestDto
            {
                UserId = "testUserId",
                OldPassword = "OldPassword123!",
                NewPassword = "NewPassword123!"
            };
            var user = new User { Id = "testUserId" };
            _mockedUserManager.Setup(x => x.FindByIdAsync(model.UserId)).ReturnsAsync(user);
            _mockedUserManager.Setup(x => x.ChangePasswordAsync(user, model.OldPassword, model.NewPassword))
                              .ReturnsAsync(IdentityResult.Success);

            await _identityService.ChangePasswordAsync(model);

            _mockedUserManager.Verify(x => x.FindByIdAsync(model.UserId), Times.Once);
            _mockedUserManager.Verify(x => x.ChangePasswordAsync(user, model.OldPassword, model.NewPassword), Times.Once);
        }       

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
    }
}
