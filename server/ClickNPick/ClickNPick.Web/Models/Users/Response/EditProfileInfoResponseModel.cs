using ClickNPick.Application.DtoModels.Users.Response;

namespace ClickNPick.Web.Models.Users.Response;

public class EditProfileInfoResponseModel
{
    public string Username { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Bio { get; set; }

    public string? Address { get; set; }

    public string ProfileImageUrl { get; set; }

    public static EditProfileInfoResponseModel FromEditProfileInfoResponseModel(EditProfileInfoResponseDto dto)
    {
        return new EditProfileInfoResponseModel
        {
            Username = dto.Username,
            PhoneNumber = dto.PhoneNumber,
            Bio = dto.Bio,
            ProfileImageUrl = dto.ProfileImageUrl,
            Address = dto.Address
        };
    }
}
