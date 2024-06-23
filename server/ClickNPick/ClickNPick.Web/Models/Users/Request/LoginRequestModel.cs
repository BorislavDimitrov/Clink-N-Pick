using ClickNPick.Application.DtoModels.Users.Request;

namespace ClickNPick.Web.Models.Users.Request;

public class LoginRequestModel
{
    public string Email { get; set; }

    public string Password { get; set; }

    public LoginRequestDto ToLoginRequestDto()
    {
        var dto = new LoginRequestDto();

        dto.Email = this.Email;
        dto.Password = this.Password;

        return dto;
    }
}
