using ClickNPick.Application.Services.Users;
using ClickNPick.Web.Extensions;
using ClickNPick.Web.Models.Users.Request;
using ClickNPick.Web.Models.Users.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClickNPick.Web.Controllers;

public class UsersController : ApiController
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> EditProfile()
    {
        var userId = HttpContext.User.GetId();
        var result = await _usersService.GetUserEditProfile(userId);
        var response = EditProfileInfoResponseModel.FromEditProfileInfoResponseModel(result);

        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    [RequestSizeLimit(20 * 1024 * 1024)]
    public async Task<IActionResult> EditProfile([FromForm] EditProfileRequestModel model)
    {
        var dto = model.ToEditProfileRequestDto();
        var userId = HttpContext.User.GetId();
        dto.UserId = userId;

        var response = await _usersService.EditProfileAsync(dto);

        return Ok(new { profileImageUrl = response });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ViewProfile([FromRoute] string id)
    {
        var result = await _usersService.GetProfileInfoAsync(id);
        var response = ViewProfileResponseModel.FromViewProfileResponseDto(result);
        return Ok(response);
    }
}
