namespace ClickNPick.Application.DtoModels.Users.Request;

public class ResetPasswordRequestDto
{
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Email { get; set; }
    public string ResetPasswordToken { get; set; }
}
