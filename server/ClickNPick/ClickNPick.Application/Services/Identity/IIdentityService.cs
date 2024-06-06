using ClickNPick.Application.DtoModels.Users.Request;
using ClickNPick.Application.DtoModels.Users.Response;

namespace ClickNPick.Application.Services.Identity;

public interface IIdentityService
{
     Task CreateUserAsync(RegisterRequestDto model);

     Task ConfirmEmailAsync(EmailConfirmationRequestDto model);

     Task ForgotPasswordSendLinkAsync(ForgotPasswordRequestDto model);

     Task ResetPasswordByLinkAsync(ResetPasswordRequestDto model);

     Task<LoginResponseDto> LoginAsync(LoginRequestDto model);

     Task ChangePasswordAsync(ChangePasswordRequestDto model);
}
