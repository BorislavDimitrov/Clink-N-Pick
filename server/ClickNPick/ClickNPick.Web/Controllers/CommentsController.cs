using ClickNPick.Application.Services.Comments;
using ClickNPick.Web.Extensions;
using ClickNPick.Web.Models.Comments.Request;
using ClickNPick.Web.Models.Comments.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClickNPick.Web.Controllers
{
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
    }
}
