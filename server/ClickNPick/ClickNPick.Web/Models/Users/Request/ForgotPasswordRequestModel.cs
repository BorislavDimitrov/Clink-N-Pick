using ClickNPick.Application.DtoModels.Users.Request;

namespace ClickNPick.Web.Models.Users.Request;

public class ForgotPasswordRequestModel
{
    public string Email { get; set; }

    public ForgotPasswordRequestDto ToForgotPasswordRequestDto()
    {
        return new ForgotPasswordRequestDto
        {
            Email = this.Email,
        };
    }
}
