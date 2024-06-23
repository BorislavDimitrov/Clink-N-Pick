using ClickNPick.Application.DtoModels.Users.Response;

namespace ClickNPick.Web.Models.Users.Response;

public class LoginResponseModel
{
    public string Token { get; set; }

    public string UserImageUrl { get; set; }

    public static LoginResponseModel FromLoginResponseDto(LoginResponseDto dto)
    {
        var model = new LoginResponseModel();

        model.Token = dto.Token;
        model.UserImageUrl = dto.UserImageUrl;

        return model;
    }
}
