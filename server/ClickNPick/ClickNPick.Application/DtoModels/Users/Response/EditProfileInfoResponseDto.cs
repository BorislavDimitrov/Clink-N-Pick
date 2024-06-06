using ClickNPick.Domain.Models;

namespace ClickNPick.Application.DtoModels.Users.Response;

public class EditProfileInfoResponseDto
{
    public string Username { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? Bio { get; set; }

    public string ProfileImageUrl { get; set; }

    public static EditProfileInfoResponseDto FromUser (User user)
    {
        return new EditProfileInfoResponseDto
        {
            Username = user.UserName,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
            Bio = user.Bio,
            ProfileImageUrl = user.Image.Url,
        };
    }
}
