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
        var dto = new RegisterRequestDto();

        dto.Email = this.Email;
        dto.Username = this.Username;
        dto.Password = this.Password;
        dto.ConfirmPassword = this.ConfirmPassword;

        return dto;
    }
}
