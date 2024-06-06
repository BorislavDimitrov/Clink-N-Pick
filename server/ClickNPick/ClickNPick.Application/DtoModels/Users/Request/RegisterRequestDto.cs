namespace ClickNPick.Application.DtoModels.Users.Request;

public class RegisterRequestDto
{
    public string Email { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }
}
