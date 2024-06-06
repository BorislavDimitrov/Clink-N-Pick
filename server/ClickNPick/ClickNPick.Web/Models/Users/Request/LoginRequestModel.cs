using ClickNPick.Application.DtoModels.Users.Request;

namespace ClickNPick.Web.Models.Users.Request;

public class LoginRequestModel
{
    public string Email { get; set; }

    public string Password { get; set; }

    public LoginRequestDto ToLoginRequestDto()
    {
        return new LoginRequestDto
        {
            Email = this.Email,
            Password = this.Password
        };
    }
}
