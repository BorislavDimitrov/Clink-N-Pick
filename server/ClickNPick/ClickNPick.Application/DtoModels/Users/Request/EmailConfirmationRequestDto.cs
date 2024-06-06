namespace ClickNPick.Application.DtoModels.Users.Request;

public class EmailConfirmationRequestDto
{
    public string UserId { get; set; }

    public string EmailConfirmationToken { get; set; }
}
