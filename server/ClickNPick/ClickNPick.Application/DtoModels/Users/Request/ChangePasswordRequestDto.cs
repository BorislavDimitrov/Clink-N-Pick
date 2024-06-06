namespace ClickNPick.Application.DtoModels.Users.Request;

public class ChangePasswordRequestDto
{
    public string OldPassword { get; set; }

    public string NewPassword { get; set; }

    public string UserId { get; set; }
}
