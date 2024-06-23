using Microsoft.AspNetCore.Http;

namespace ClickNPick.Application.DtoModels.Users.Request;

public class EditProfileRequestDto
{
    public string Username { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string Bio { get; set; }

    public IFormFile Image { get; set; }

    public string UserId { get; set; }
}
