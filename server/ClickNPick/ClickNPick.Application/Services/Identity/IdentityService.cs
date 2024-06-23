using ClickNPick.Application.Abstractions.Repositories;
using ClickNPick.Application.Abstractions.Services;
using ClickNPick.Application.DtoModels.Users.Request;
using ClickNPick.Application.DtoModels.Users.Response;
using ClickNPick.Application.Exceptions.General;
using ClickNPick.Application.Exceptions.Identity;
using ClickNPick.Application.Helpers;
using ClickNPick.Application.Services.Images;
using ClickNPick.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Text;

namespace ClickNPick.Application.Services.Identity;

public class IdentityService : IIdentityService
{
    private const int AvatarImageWidth = 200;
    private const int AvatarImageHeight = 200;

    private readonly UserManager<User> _userManager;
    private readonly IRepository<User> _usersRepository;
    private readonly ITokenGeneratorService _tokenGeneratorService;
    private readonly IEmailSender _emailSender;
    private readonly IImagesService _imagesService;

    public IdentityService(
        UserManager<User> userManager,
        IRepository<User> usersRepository,
        ITokenGeneratorService tokenGeneratorService,
        IEmailSender emailSender,
        IImagesService imagesService
       )
    {
        _userManager = userManager;
        _usersRepository = usersRepository;
        _tokenGeneratorService = tokenGeneratorService;
        _emailSender = emailSender;
        _imagesService = imagesService;
    }

    public async Task CreateUserAsync(RegisterRequestDto model)
    {          
        if(await IsEmailUsedAsync(model.Email) == true)
        {
            throw new EmailAlreadyUsedException($"Email {model.Email} is already used by other user.");
        }

        var byteArray = await this.GenerateDefaultAvatarImage(model.Username, AvatarImageWidth, AvatarImageHeight, "Arial", 40, FontStyle.Bold);
        var stream = new MemoryStream(byteArray);
        IFormFile fileImage = new FormFile(stream, 0, byteArray.Length, "name", "fileName");

        var imageId = await _imagesService.CreateImageAsync(fileImage, AvatarImageWidth, AvatarImageHeight);
        var newImage = await _imagesService.GetImageByIdAsync(imageId);

        var newUser = new User
        {
            Email = model.Email,
            UserName = model.Username,
            ImageId = newImage.Id,
        };      

        var result = await _userManager.CreateAsync(newUser, model.Password);

        if (result.Succeeded == false)
        {
            await _imagesService.DeleteImageAsync(imageId);
            throw new OperationFailedException("The creation of user failed.");
        }

        var userId = await _userManager.GetUserIdAsync(newUser);
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        var encodedUserId = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(userId));

        var callbackUrl = $"http://localhost:3000/Identity/ConfirmEmail/{encodedUserId}/{encodedToken}";
        var bodyContent = EmailContentHelper.FormatConfirmEmailText(newUser.UserName, callbackUrl);
        await _emailSender.SendEmailAsync(newUser.Email, "Confirm your email",
        bodyContent);

    }

    public async Task ConfirmEmailAsync(EmailConfirmationRequestDto model)
    {
        var decodedTokenBytes = WebEncoders.Base64UrlDecode(model.EmailConfirmationToken);
        var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

        var decodedUserIdBytes = WebEncoders.Base64UrlDecode(model.UserId);
        var decodedUserId = Encoding.UTF8.GetString(decodedUserIdBytes);

        var user = await _userManager.FindByIdAsync(decodedUserId);

        if (user == null)
        {
           throw new UserNotFoundException($"User with id {decodedUserId} doesnt exist.");
        }

        if (user.EmailConfirmed == true)
        {
            throw new InvalidOperationException($"The email {user.Email} is already confirmed.");
        }

        var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

        if (result.Succeeded == false)
        {
            throw new OperationFailedException($"The confirmation of email {user.Email} failed.");
        }
    }

    public async Task ForgotPasswordSendLinkAsync(ForgotPasswordRequestDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        if (await _userManager.IsEmailConfirmedAsync(user) == false)
        {
            throw new InvalidOperationException($"The email {model.Email} is not confirmed yet.");
        }

          var token = await _userManager.GeneratePasswordResetTokenAsync(user);

          var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
          var encodedEmail = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(model.Email));

          var callbackUrl = $"http://localhost:3000/Identity/ForgotPasswordChange?email={encodedEmail}&token={encodedToken}";
          var bodyContent = EmailContentHelper.FormatResetPasswordEmailText(user.UserName, callbackUrl);
          await _emailSender.SendEmailAsync(user.Email, "Forgot password",
                   bodyContent);           
    }

    public async Task ResetPasswordByLinkAsync(ResetPasswordRequestDto model)
    {
        var decodedTokenBytes = WebEncoders.Base64UrlDecode(model.ResetPasswordToken);
        var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

        var decodedEmailBytes = WebEncoders.Base64UrlDecode(model.Email);
        var decodedEmail = Encoding.UTF8.GetString(decodedEmailBytes);

        var user = await _userManager.FindByEmailAsync(decodedEmail);

        if (user == null)
        {
            throw new UserNotFoundException($"User with email {decodedEmail} doesnt exist.");
        }        

        var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.Password);

        if (result.Succeeded == false)
        {
            throw new OperationFailedException("The reseting of the password failed.");
        }
    }  

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        if (user.EmailConfirmed == false)
        {
            throw new InvalidOperationException($"The email {model.Email} is not confirmed.");
        }


        if (await _userManager.CheckPasswordAsync(user, model.Password) == false)
        {
            throw new ArgumentException("The provided password is not right.");
        }

        var token = await _tokenGeneratorService.GenerateToken(user);

        var userImage = await _imagesService.GetImageByIdAsync(user.ImageId);

        var response = new LoginResponseDto { Token = token, UserImageUrl = userImage.Url};    

        return response;
    }

    public async Task ChangePasswordAsync(ChangePasswordRequestDto model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        if (model.OldPassword == model.NewPassword)
        {
            throw new InvalidOperationException("The new password cannot be same as the old one.");
        }

        var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

        if (result.Succeeded == false)
        {
            throw new OperationFailedException("The changing of the password failed.");
        }
    }

    public async Task<bool> IsEmailUsedAsync(string email)
        => await _usersRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Email == email) == null ? false : true;

    private async Task<byte[]> GenerateDefaultAvatarImage(string text, int width, int height, string fontName, int emSize, FontStyle fontStyle)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentException($"'{nameof(text)}' cannot be null or empty.", nameof(text));
        }

        if (string.IsNullOrEmpty(fontName))
        {
            throw new ArgumentException($"'{nameof(fontName)}' cannot be null or empty.", nameof(fontName));
        }

        var backgroundColors = new List<string> { "3C79B2", "FF8F88", "6FB9FF", "C0CC44", "AFB28C" };

        var backgroundColor = backgroundColors[new Random().Next(0, backgroundColors.Count - 1)];

        var bmp = new Bitmap(width, height);
        var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

        var font = new Font(fontName, emSize, fontStyle, GraphicsUnit.Pixel);
        var graphics = Graphics.FromImage(bmp);

        graphics.Clear((Color)new ColorConverter().ConvertFromString("#" + backgroundColor));

        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

        graphics.DrawString(text, font, new SolidBrush(Color.WhiteSmoke), new RectangleF(0, 0, width, height), sf);
        graphics.Flush();

        await using var ms = new MemoryStream();
        bmp.Save(ms, ImageFormat.Jpeg);

        return ms.ToArray();                    
    }
}
