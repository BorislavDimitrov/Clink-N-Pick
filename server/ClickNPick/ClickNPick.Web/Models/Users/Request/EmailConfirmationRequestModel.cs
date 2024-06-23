using ClickNPick.Application.DtoModels.Users.Request;

namespace ClickNPick.Web.Models.Users.Request;

public class EmailConfirmationRequestModel
{
    public string UserId { get; set; }

    public string EmailConfirmationToken { get; set; }

    public EmailConfirmationRequestDto ToEmailConfirmationRequestDto()
    {
        var dto = new EmailConfirmationRequestDto();

        dto.UserId = this.UserId;
        dto.EmailConfirmationToken = this.EmailConfirmationToken;

        return dto;
    }
}
