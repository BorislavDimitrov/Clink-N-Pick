using ClickNPick.Domain.Models;

namespace ClickNPick.Application.DtoModels.Users.Response;

public class EditProfileInfoResponseDto
{
    public string Username { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string Bio { get; set; }

    public string ProfileImageUrl { get; set; }

    public static EditProfileInfoResponseDto FromUser (User user)
    {
        var dto = new EditProfileInfoResponseDto();

        dto.Username = user.UserName;
        dto.PhoneNumber = user.PhoneNumber;
        dto.Address = user.Address;
        dto.Bio = user.Bio;
        dto.ProfileImageUrl = user.Image.Url;
        
        return dto;
    }
}
