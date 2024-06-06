using ClickNPick.Application.DtoModels.Users.Request;
using Microsoft.AspNetCore.Http;

namespace ClickNPick.Web.Models.Users.Request;

public class EditProfileRequestModel
{
    public string Username { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? Bio { get; set; }

    public IFormFile? Image { get; set; }

    public EditProfileRequestDto ToEditProfileRequestDto()
    {
        return new EditProfileRequestDto
        {
            Username = this.Username,
            PhoneNumber = this.PhoneNumber,
            Bio = this.Bio,
            Image = this.Image,
            Address = this.Address,
        };
    }
}
