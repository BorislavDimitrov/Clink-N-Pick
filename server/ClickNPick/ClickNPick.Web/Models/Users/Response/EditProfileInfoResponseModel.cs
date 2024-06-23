using ClickNPick.Application.DtoModels.Users.Response;

namespace ClickNPick.Web.Models.Users.Response;

public class EditProfileInfoResponseModel
{
    public string Username { get; set; }

    public string PhoneNumber { get; set; }

    public string Bio { get; set; }

    public string Address { get; set; }

    public string ProfileImageUrl { get; set; }

    public static EditProfileInfoResponseModel FromEditProfileInfoResponseModel(EditProfileInfoResponseDto dto)
    {
        var model = new EditProfileInfoResponseModel();

        model.Username = dto.Username;
        model.PhoneNumber = dto.PhoneNumber;
        model.Bio = dto.Bio;
        model.ProfileImageUrl = dto.ProfileImageUrl;
        model.Address = dto.Address;

        return model;
    }
}
