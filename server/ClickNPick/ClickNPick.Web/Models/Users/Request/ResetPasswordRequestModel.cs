using ClickNPick.Application.DtoModels.Users.Request;

namespace ClickNPick.Web.Models.Users.Request;

public class ResetPasswordRequestModel
{
    public string Password { get; set; }

    public string ConfirmPassword { get; set; }

    public string Email { get; set; }

    public string ResetPasswordToken { get; set; }

    public ResetPasswordRequestDto ToResetPasswordRequestDto()
    {
        var dto = new ResetPasswordRequestDto();

        dto.Password = this.Password;
        dto.ConfirmPassword = this.ConfirmPassword;
        dto.Email = this.Email;
        dto.ResetPasswordToken = this.ResetPasswordToken;

        return dto;
    }
}
