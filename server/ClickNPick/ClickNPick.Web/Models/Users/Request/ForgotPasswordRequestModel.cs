using ClickNPick.Application.DtoModels.Users.Request;

namespace ClickNPick.Web.Models.Users.Request;

public class ForgotPasswordRequestModel
{
    public string Email { get; set; }

    public ForgotPasswordRequestDto ToForgotPasswordRequestDto()
    {
        var dto = new ForgotPasswordRequestDto();

        dto.Email = this.Email;

        return dto;
    }
}
