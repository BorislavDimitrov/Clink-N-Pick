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
        return new ResetPasswordRequestDto
        {
            Password = this.Password,
            ConfirmPassword = this.ConfirmPassword,
            Email = this.Email,
            ResetPasswordToken = this.ResetPasswordToken
        };
    }
}
