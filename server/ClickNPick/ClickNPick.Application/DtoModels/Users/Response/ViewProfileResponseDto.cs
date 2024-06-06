using ClickNPick.Domain.Models;

namespace ClickNPick.Application.DtoModels.Users.Response;

public class ViewProfileResponseDto
{
    public string Username { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string Email { get; set; }

    public string Bio { get; set; }

    public string ProfileImageUrl { get; set; }

    public static ViewProfileResponseDto FromUser(User user)
    {
        return new ViewProfileResponseDto
        {
            Username = user.UserName,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
            Email = user.Email,
            Bio = user.Bio,
            ProfileImageUrl = user.Image.Url
        };
    }
}
