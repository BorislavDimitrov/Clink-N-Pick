using ClickNPick.Application.DtoModels.Users.Request;

namespace ClickNPick.Web.Models.Users.Request;

public class EmailConfirmationRequestModel
{
    public string UserId { get; set; }

    public string EmailConfirmationToken { get; set; }

    public EmailConfirmationRequestDto ToEmailConfirmationRequestDto()
    {
        return new EmailConfirmationRequestDto
        {
            UserId = this.UserId,
            EmailConfirmationToken = this.EmailConfirmationToken
        };
    }
}
