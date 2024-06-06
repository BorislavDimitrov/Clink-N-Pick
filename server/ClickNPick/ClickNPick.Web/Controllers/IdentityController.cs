using ClickNPick.Application.Services.Identity;
using ClickNPick.Web.Extensions;
using ClickNPick.Web.Models.Users.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClickNPick.Web.Controllers;

public class IdentityController : ApiController
{

    private readonly IIdentityService _identityService;

    public IdentityController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequestModel model)
    {
        var dto = model.ToRegisterRequestDto();

        await _identityService.CreateUserAsync(dto);

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestModel model)
    {
        var dto = model.ToLoginRequestDto();

        var response = await _identityService.LoginAsync(dto);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmEmail(EmailConfirmationRequestModel model)
    {
        var dto = model.ToEmailConfirmationRequestDto();
        await _identityService.ConfirmEmailAsync(dto);

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPasswordSend(ForgotPasswordRequestModel model)
    {
        var dto = model.ToForgotPasswordRequestDto();
        await _identityService.ForgotPasswordSendLinkAsync(dto);

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPasswordChange(ResetPasswordRequestModel model)
    {
        var dto = model.ToResetPasswordRequestDto();
        await _identityService.ResetPasswordByLinkAsync(dto);

        return Ok();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequestModel model)
    {
        var dto = model.ToChangePasswordRequestDto();
        var userId = HttpContext.User.GetId();
        dto.UserId = userId;

        await _identityService.ChangePasswordAsync(dto);

        return Ok();
    }
}
