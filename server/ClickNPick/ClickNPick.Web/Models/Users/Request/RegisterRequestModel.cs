using ClickNPick.Application.DtoModels.Users.Request;

namespace ClickNPick.Web.Models.Users.Request;

public class RegisterRequestModel
{
    public string Email { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }

    public RegisterRequestDto ToRegisterRequestDto()
    {
        return new RegisterRequestDto
        {
            Email = this.Email,
            Username = this.Username,
            Password = this.Password,
            ConfirmPassword = this.ConfirmPassword
        };
    }
}
