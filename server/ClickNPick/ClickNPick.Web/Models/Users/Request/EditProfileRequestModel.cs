using ClickNPick.Application.DtoModels.Users.Request;
using Microsoft.AspNetCore.Http;

namespace ClickNPick.Web.Models.Users.Request;

public class EditProfileRequestModel
{
    public string Username { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string Bio { get; set; }

    public IFormFile Image { get; set; }

    public EditProfileRequestDto ToEditProfileRequestDto()
    {
        var dto = new EditProfileRequestDto();

        dto.Username = this.Username;
        dto.PhoneNumber = this.PhoneNumber;
        dto.Bio = this.Bio;
        dto.Image = this.Image;
        dto.Address = this.Address;

        return dto;
    }
}
