using ClickNPick.Application.DtoModels.Users.Request;
using ClickNPick.Application.DtoModels.Users.Response;
using ClickNPick.Domain.Models;

namespace ClickNPick.Application.Services.Users;

public interface IUsersService
{
    Task<string> EditProfileAsync(EditProfileRequestDto model);

    Task<EditProfileInfoResponseDto> GetUserEditProfile(string userId);

    Task<User> GetByIdAsync(string userId);

    Task<ViewProfileResponseDto> GetProfileInfoAsync(string userId);
}
