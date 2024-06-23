using ClickNPick.Application.DtoModels.Comments.Request;
using ClickNPick.Application.Services.Comments;
using ClickNPick.Web.Extensions;
using ClickNPick.Web.Models.Comments.Request;
using ClickNPick.Web.Models.Comments.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClickNPick.Web.Controllers;

public class CommentsController : ApiController
{
    private readonly ICommentsService _commentsService;

    public CommentsController(ICommentsService commentsService)
    {
        _commentsService = commentsService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCommentRequestModel model)
    {
        var dto = model.ToCreateCommentRequestDto();
        var userId = HttpContext.User.GetId();
        dto.CreatorId = userId;

        var result = await _commentsService.CreateAsync(dto);
        var response = CreateCommentResponseModel.FromCreateCommentResponseDto(result);

        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Edit([FromBody] EditCommentRequestModel model)
    {           
        var userId = HttpContext.User.GetId();
        var dto = model.ToEditCommentRequestDto();
        dto.UserId = userId;

        await _commentsService.EditAsync(dto);

        return Ok();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] string id)
    {
        var userId = HttpContext.User.GetId();

        var request = new DeleteCommentRequestDto() { CommentId = id, UserId = userId };

        await _commentsService.DeleteAsync(request);

        return Ok();
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetForProduct([FromRoute] string productId)
    {
        var result = await _commentsService.GetForProductAsync(productId);
        var response = CommentListingResponseModel.FromCommentListingResponseDto(result);

        return Ok(response);
    }
}
