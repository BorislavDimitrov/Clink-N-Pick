using ClickNPick.Application.DtoModels.Comments.Request;
using ClickNPick.Application.DtoModels.Comments.Response;
using ClickNPick.Domain.Models;

namespace ClickNPick.Application.Services.Comments
{
    public interface ICommentsService
    {
        Task<CreateCommentResponseDto> CreateAsync(CreateCommentRequestDto model);

        Task<CommentListingResponseDto> GetForProductAsync(string productId);

        Task DeleteAsync(DeleteCommentRequestDto model);

        Task EditAsync(EditCommentRequestDto model);

        Task<bool> IsCommentCreatedByUser(string commentId, string userId);

        public Task<Comment?> GetByIdAsync(string commentId);
    }
}
