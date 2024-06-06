using ClickNPick.Application.DtoModels.Users.Request;

namespace ClickNPick.Web.Models.Users.Request;

public class ChangePasswordRequestModel
{
    public string OldPassword { get; set; }

    public string NewPassword { get; set; }

    public ChangePasswordRequestDto ToChangePasswordRequestDto()
    {
        return new ChangePasswordRequestDto
        {
            OldPassword = this.OldPassword,
            NewPassword = this.NewPassword
        };
    }
}
