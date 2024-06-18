using ClickNPick.Application.Abstractions.Repositories;
using ClickNPick.Application.DtoModels.Comments.Request;
using ClickNPick.Application.DtoModels.Comments.Response;
using ClickNPick.Application.Exceptions.Comments;
using ClickNPick.Application.Exceptions.Identity;
using ClickNPick.Application.Exceptions.Products;
using ClickNPick.Application.Services.Products;
using ClickNPick.Application.Services.Users;
using ClickNPick.Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ClickNPick.Application.Services.Comments
{
    public class CommentsService : ICommentsService
    {
        private readonly IRepository<Comment> _commentsRepository;
        private readonly IUsersService _usersService;
        private readonly IProductsService _productsService;

        public CommentsService(
            IRepository<Comment> commentsRepository,
            IUsersService usersService,
            IProductsService productsService)
        {
            _commentsRepository = commentsRepository;
            _usersService = usersService;
            _productsService = productsService;
        }

        public async Task<CreateCommentResponseDto> CreateAsync(CreateCommentRequestDto model)
        {
            var user = await _usersService.GetByIdAsync(model.CreatorId);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var product = await _productsService.GetByIdAsync(model.ProductId);

            if (product == null)
            {
                throw new ProductNotFoundException();
            }

            if (model.ParentId != null)
            {
                var parentComment = await GetByIdAsync(model.ParentId);

                if (parentComment == null)
                {
                    throw new CommentNotFoundException();
                }
            }

            var newComment = model.ToComment();
            await _commentsRepository.AddAsync(newComment);
            await _commentsRepository.SaveChangesAsync();

            var commentResponse = await _commentsRepository
                .AllAsNoTracking()
                .Include(x => x.Creator)
                .Include(x => x.Creator.Image)
                .FirstOrDefaultAsync(x => x.Id == newComment.Id);

            return CreateCommentResponseDto.FromComment(commentResponse);
        }

        public async Task<CommentListingResponseDto> GetForProductAsync(string productId)
        {
            var product = await _productsService.GetByIdAsync(productId);

            if (product == null)
            {
                throw new ProductNotFoundException();
            }

            var comments = await _commentsRepository
                .All()
                .Where(x => x.ProductId == productId)
                .OrderByDescending(x => x.CreatedOn)
                .Include(x => x.Creator)
                .Include(x => x.Creator.Image)
                .ToListAsync();

            return CommentListingResponseDto.FromComments(comments);
        }

        public async Task DeleteAsync(DeleteCommentRequestDto model)
        {
            var user = await _usersService.GetByIdAsync(model.UserId);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var comment = await GetByIdAsync(model.CommentId);

            if (comment == null)
            {
                throw new CommentNotFoundException();
            }

            if (await IsCommentCreatedByUser(model.CommentId, model.UserId) == false)
            {
                throw new InvalidOperationException();
            }

            if ((DateTime.UtcNow - comment.CreatedOn).TotalMinutes > 5)
            {
                throw new InvalidOperationException();
            }

            _commentsRepository.SoftDelete(comment);
            await _commentsRepository.SaveChangesAsync();
        }

        public async Task EditAsync(EditCommentRequestDto model)
        {
            var user = await _usersService.GetByIdAsync(model.UserId);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var comment = await GetByIdAsync(model.CommentId);

            if (comment == null)
            {
                throw new CommentNotFoundException();
            }

            if (await IsCommentCreatedByUser(model.CommentId, model.UserId) == false)
            {
                throw new InvalidOperationException();
            }

            comment.Content = model.Content;
            await _commentsRepository.SaveChangesAsync();
        }

        public async Task<bool> IsCommentCreatedByUser(string commentId, string userId)
            => await _commentsRepository
            .AllAsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == commentId && x.CreatorId == userId) == null ? false : true;

        public async Task<Comment?> GetByIdAsync(string commentId)
            => await _commentsRepository
            .All()
            .FirstOrDefaultAsync(x => x.Id == commentId);
    }
}
