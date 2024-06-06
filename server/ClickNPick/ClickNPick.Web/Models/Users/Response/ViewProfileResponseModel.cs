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
        return new ViewProfileResponseModel
        {
            Username = dto.Username,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            Address = dto.Address,
            Bio = dto.Bio,
            ProfileImageUrl = dto.ProfileImageUrl,
        };
    }
}
