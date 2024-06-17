using ClickNPick.Application.DtoModels.Comments.Request;
using ClickNPick.Application.DtoModels.Comments.Response;

namespace ClickNPick.Application.Services.Comments
{
    public interface ICommentsService
    {
        Task<CreateCommentResponseDto> CreateAsync(CreateCommentRequestDto model);
    }
}
