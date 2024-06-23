using ClickNPick.Application.Abstractions.Repositories;
using ClickNPick.Application.DtoModels.Users.Request;
using ClickNPick.Application.DtoModels.Users.Response;
using ClickNPick.Application.Exceptions.Identity;
using ClickNPick.Application.Services.Images;
using ClickNPick.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ClickNPick.Application.Services.Users;

public class UsersService : IUsersService
{
    private const int AvatarImageWidth = 200;
    private const int AvatarImageHeight = 200;

    private readonly IImagesService _imagesService;
    private readonly IRepository<User> _usersRepository;

    public UsersService(
        IImagesService imagesService,
        IRepository<User> usersRepository
        )
    {
        _imagesService = imagesService;
        _usersRepository = usersRepository;
    }

    public async Task<string> EditProfileAsync(EditProfileRequestDto model)
    {
        var user = await GetByIdAsync(model.UserId);

        if (user == null)
        {
            throw new UserNotFoundException($"User with id {model.UserId} doesnt exist.");
        }

         user.UserName = model.Username;
         user.PhoneNumber = model.PhoneNumber;
         user.Address = model.Address;
         user.Bio = model.Bio;

        if (model.Image != null)
        {
            await _imagesService.DeleteImageAsync(user.ImageId);
            var imageId = await _imagesService.CreateImageAsync(model.Image, AvatarImageWidth, AvatarImageHeight);
            user.ImageId = imageId;
        }

        await _usersRepository.SaveChangesAsync();

        var image = await _imagesService.GetImageByIdAsync(user.ImageId);

        return image.Url;
    }

    public async Task<EditProfileInfoResponseDto> GetUserEditProfile(string userId)
    {
        var user = await _usersRepository
            .AllAsNoTracking()
            .Include(x => x.Image)
            .FirstOrDefaultAsync(x => x.Id == userId );

        if (user == null)
        {
            throw new UserNotFoundException($"User with id {userId} doesnt exist.");
        }
        
        return EditProfileInfoResponseDto.FromUser(user);
    }

    public async Task<ViewProfileResponseDto> GetProfileInfoAsync(string userId)
    {
        var user = await _usersRepository
            .AllAsNoTracking()
            .Include(x => x.Image)
            .FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
        {
            throw new UserNotFoundException($"User with id {userId} doesnt exist.");
        }

        return ViewProfileResponseDto.FromUser(user);
    }

    public async Task<User> GetByIdAsync(string userId)
    => await _usersRepository.All().FirstOrDefaultAsync(x => x.Id == userId);
}
