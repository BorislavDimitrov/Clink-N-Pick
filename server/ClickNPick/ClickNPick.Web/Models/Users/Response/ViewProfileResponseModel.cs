using ClickNPick.Application.DtoModels.Users.Response;

namespace ClickNPick.Web.Models.Users.Response;

public class ViewProfileResponseModel
{
    public string Username { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string Email { get; set; }

    public string Bio { get; set; }

    public string ProfileImageUrl { get; set; }

    public static ViewProfileResponseModel FromViewProfileResponseDto(ViewProfileResponseDto dto)
    {
        var model = new ViewProfileResponseModel();

        model.Username = dto.Username;
        model.PhoneNumber = dto.PhoneNumber;
        model.Email = dto.Email;
        model.Address = dto.Address;
        model.Bio = dto.Bio;
        model.ProfileImageUrl = dto.ProfileImageUrl;

        return model;
    }
}
