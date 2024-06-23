using AutoFixture;
using AutoFixture.AutoMoq;
using ClickNPick.Application.Abstractions.Repositories;
using ClickNPick.Application.DtoModels.Comments.Request;
using ClickNPick.Application.Exceptions.Comments;
using ClickNPick.Application.Exceptions.Identity;
using ClickNPick.Application.Exceptions.Products;
using ClickNPick.Application.Services.Comments;
using ClickNPick.Application.Services.Products;
using ClickNPick.Application.Services.Users;
using ClickNPick.Domain.Models;
using ClickNPick.Infrastructure.Data;
using ClickNPick.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ClickNPick.Tests.Services
{
    public class CommentsServicesTests
    {
        private IFixture _fixture;
        private ICommentsService _commentsService;
        private IRepository<Comment> _commentsRepository;
        private Mock<IUsersService> _mockedUsersService;
        private Mock<IProductsService> _mockedProductsService;
        private DbContextOptionsBuilder<ClickNPickDbContext> _options;
        private ClickNPickDbContext _context;

        [SetUp]
        [TearDown]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _options = new DbContextOptionsBuilder<ClickNPickDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());
            _context = new ClickNPickDbContext(_options.Options);
            _mockedUsersService = new Mock<IUsersService>();
            _mockedProductsService = new Mock<IProductsService>();
            _commentsRepository = new Repository<Comment>(_context);
            _commentsService = new CommentsService(_commentsRepository, _mockedUsersService.Object, _mockedProductsService.Object);
        }

        [Test]

        public async Task CreateCommentShouldBeCreateSuccessfully()
        {           
            var newComment = _fixture.Create<CreateCommentRequestDto>();
            newComment.ParentId = null;
           
            var newUser = _fixture.Create<User>();
            newUser.Id = newComment.CreatorId;
            _context.Users.Add(newUser);

            var product = new Product() { Id = newComment.ProductId };
            product.Id = newComment.ProductId;

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newComment.CreatorId))
                .ReturnsAsync(newUser);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newComment.ProductId))
                .ReturnsAsync(product);

            var result = await _commentsService.CreateAsync(newComment);

            Assert.AreEqual(result.ParentId, newComment.ParentId);
        }

        [Test]
        public void CreateCommentShouldThrowUserNotFoundException()
        {
            var newComment = _fixture.Create<CreateCommentRequestDto>();

            Assert.ThrowsAsync<UserNotFoundException>(async () => await _commentsService.CreateAsync(newComment));
        }

        [Test]
        public void CreateCommentShouldThrowProductNotFoundException()
        {
            var newComment = _fixture.Create<CreateCommentRequestDto>();
            newComment.ParentId = null;

            var user = new User { Id = newComment.CreatorId };
            user.Id = newComment.CreatorId;

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newComment.CreatorId))
                .ReturnsAsync(user);

             Assert.ThrowsAsync<ProductNotFoundException>(async () => await _commentsService.CreateAsync(newComment));
        }

        [Test]
        public async Task GetForProductAsyncShouldReturnRightComment()
        {
            var newComment = _fixture.Create<CreateCommentRequestDto>();
            newComment.ParentId = null;

            var newUser = _fixture.Create<User>();
            newUser.Id = newComment.CreatorId;
            _context.Users.Add(newUser);

            var product = new Product() { Id = newComment.ProductId };
            product.Id = newComment.ProductId;

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newComment.CreatorId))
                .ReturnsAsync(newUser);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newComment.ProductId))
                .ReturnsAsync(product);

            var result = await _commentsService.CreateAsync(newComment);

            var response = await _commentsService.GetForProductAsync(product.Id);

            Assert.AreEqual(1, response.Comments.Count);
        }

        [Test]
        public async Task GetForProductAsyncShouldThrowProductNotFoundException()
        {           
             Assert.ThrowsAsync<ProductNotFoundException>(async () => await _commentsService.GetForProductAsync(""));
        }

        [Test]
        public async Task DeleteShouldBeSuccessful()
        {                  
            var newComment = _fixture.Create<CreateCommentRequestDto>();
            newComment.ParentId = null;

            var newUser = _fixture.Create<User>();
            newUser.Id = newComment.CreatorId;
            _context.Users.Add(newUser);

            var product = new Product() { Id = newComment.ProductId };
            product.Id = newComment.ProductId;

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newComment.CreatorId))
                .ReturnsAsync(newUser);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newComment.ProductId))
                .ReturnsAsync(product);

            var result = await _commentsService.CreateAsync(newComment);

            var deleteDto = new DeleteCommentRequestDto() { UserId = newUser.Id, CommentId = result.Id };

            await _commentsService.DeleteAsync(deleteDto);

            Assert.AreEqual(null, await _context.Comments.FirstOrDefaultAsync(x => x.Id == result.Id && x.IsDeleted == false));
        }

        [Test]
        public async Task DeleteShouldThrowUserNotFoundException()
        {
            var newComment = _fixture.Create<CreateCommentRequestDto>();
            newComment.ParentId = null;

            var newUser = _fixture.Create<User>();
            newUser.Id = newComment.CreatorId;
            _context.Users.Add(newUser);

            var product = new Product() { Id = newComment.ProductId };
            product.Id = newComment.ProductId;

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newComment.CreatorId))
                .ReturnsAsync(newUser);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newComment.ProductId))
                .ReturnsAsync(product);

            var result = await _commentsService.CreateAsync(newComment);

            var deleteDto = new DeleteCommentRequestDto() { UserId = "", CommentId = result.Id };

            Assert.ThrowsAsync<UserNotFoundException>(async () => await _commentsService.DeleteAsync(deleteDto));
        }

        [Test]
        public async Task DeleteShouldThrowInvalidOperationExceptioDueToInvalidCreator()
        {
            var newComment = _fixture.Create<CreateCommentRequestDto>();
            newComment.ParentId = null;

            var newUser = _fixture.Create<User>();
            newUser.Id = newComment.CreatorId;
            _context.Users.Add(newUser);

            var newUser2 = _fixture.Create<User>();
            newUser2.Id = Guid.NewGuid().ToString();
            _context.Users.Add(newUser2);

            var product = new Product() { Id = newComment.ProductId };
            product.Id = newComment.ProductId;

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newComment.CreatorId))
                .ReturnsAsync(newUser);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newComment.ProductId))
                .ReturnsAsync(product);

            var result = await _commentsService.CreateAsync(newComment);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newUser2.Id))
                .ReturnsAsync(newUser2);

            var deleteDto = new DeleteCommentRequestDto() { UserId = newUser2.Id, CommentId = result.Id };

            Assert.ThrowsAsync<InvalidOperationException>(async () => await _commentsService.DeleteAsync(deleteDto));
        }

        [Test]
        public async Task DeleteShouldThrowInvalidOperationExceptionDueToFiveMinutesPassed()
        {
            var newComment = _fixture.Create<CreateCommentRequestDto>();
            newComment.ParentId = null;

            var newUser = _fixture.Create<User>();
            newUser.Id = newComment.CreatorId;
            _context.Users.Add(newUser);

            var product = new Product() { Id = newComment.ProductId };
            product.Id = newComment.ProductId;

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newComment.CreatorId))
                .ReturnsAsync(newUser);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newComment.ProductId))
                .ReturnsAsync(product);

            var result = await _commentsService.CreateAsync(newComment);

            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == result.Id);
            comment.CreatedOn = comment.CreatedOn.AddDays(1);
            _context.Comments.Update(comment);

            var deleteDto = new DeleteCommentRequestDto() { UserId = newUser.Id, CommentId = result.Id };

            Assert.ThrowsAsync<InvalidOperationException>(async () => await _commentsService.DeleteAsync(deleteDto));
        }

        [Test]
        public async Task EditShouldBeSuccessful()
        {
            var newComment = _fixture.Create<CreateCommentRequestDto>();
            newComment.ParentId = null;

            var newUser = _fixture.Create<User>();
            newUser.Id = newComment.CreatorId;
            _context.Users.Add(newUser);

            var product = new Product() { Id = newComment.ProductId };
            product.Id = newComment.ProductId;

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newComment.CreatorId))
                .ReturnsAsync(newUser);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newComment.ProductId))
                .ReturnsAsync(product);

            var result = await _commentsService.CreateAsync(newComment);

            var editDto = new EditCommentRequestDto() { UserId = newUser.Id, CommentId = result.Id, Content = "EDITED" };

            await _commentsService.EditAsync(editDto);

            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == result.Id);

            Assert.AreEqual("EDITED", comment.Content);
        }


        [Test]
        public async Task EditShouldThrowUserNotFoundException()
        {
            var newComment = _fixture.Create<CreateCommentRequestDto>();
            newComment.ParentId = null;

            var newUser = _fixture.Create<User>();
            newUser.Id = newComment.CreatorId;
            _context.Users.Add(newUser);

            var product = new Product() { Id = newComment.ProductId };
            product.Id = newComment.ProductId;

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newComment.CreatorId))
                .ReturnsAsync(newUser);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newComment.ProductId))
                .ReturnsAsync(product);

            var result = await _commentsService.CreateAsync(newComment);

            var editDto = new EditCommentRequestDto() { UserId = "", CommentId = result.Id, Content = "EDITED" };

            Assert.ThrowsAsync<UserNotFoundException>(async () => await _commentsService.EditAsync(editDto));
        }


        [Test]
        public async Task EditShouldThrowCommentNotFoundException()
        {
            var newComment = _fixture.Create<CreateCommentRequestDto>();
            newComment.ParentId = null;

            var newUser = _fixture.Create<User>();
            newUser.Id = newComment.CreatorId;
            _context.Users.Add(newUser);

            var product = new Product() { Id = newComment.ProductId };
            product.Id = newComment.ProductId;

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newComment.CreatorId))
                .ReturnsAsync(newUser);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newComment.ProductId))
                .ReturnsAsync(product);

            var result = await _commentsService.CreateAsync(newComment);


            var editDto = new EditCommentRequestDto() { UserId = newUser.Id, CommentId = "", Content = "EDITED" };

            Assert.ThrowsAsync<CommentNotFoundException>(async () => await _commentsService.EditAsync(editDto));
        }

            [Test]
        public async Task EditShouldThrowInvalidOperationException()
        {
            var newComment = _fixture.Create<CreateCommentRequestDto>();
            newComment.ParentId = null;

            var newUser = _fixture.Create<User>();
            newUser.Id = newComment.CreatorId;
            _context.Users.Add(newUser);

            var newUser2 = _fixture.Create<User>();
            newUser2.Id = Guid.NewGuid().ToString();
            _context.Users.Add(newUser2);

            var product = new Product() { Id = newComment.ProductId };
            product.Id = newComment.ProductId;

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newComment.CreatorId))
                .ReturnsAsync(newUser);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newComment.ProductId))
                .ReturnsAsync(product);

            var result = await _commentsService.CreateAsync(newComment);

            var editDto = new EditCommentRequestDto() { UserId = newUser2.Id, CommentId = result.Id, Content = "EDITED" };

            _mockedUsersService
             .Setup(x => x.GetByIdAsync(newUser2.Id))
             .ReturnsAsync(newUser2);

            Assert.ThrowsAsync<InvalidOperationException>(async () => await _commentsService.EditAsync(editDto));
        }

        [Test]
        public async Task IsCommentCreatedByUserShuldReturnTrue()
        {
            var newComment = _fixture.Create<CreateCommentRequestDto>();
            newComment.ParentId = null;

            var newUser = _fixture.Create<User>();
            newUser.Id = newComment.CreatorId;
            _context.Users.Add(newUser);

            var product = new Product() { Id = newComment.ProductId };
            product.Id = newComment.ProductId;

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newComment.CreatorId))
                .ReturnsAsync(newUser);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newComment.ProductId))
                .ReturnsAsync(product);

            var result = await _commentsService.CreateAsync(newComment);

             
            Assert.AreEqual(true, await _commentsService.IsCommentCreatedByUser(result.Id, newUser.Id));
        }

        [Test]
        public async Task IsCommentCreatedByUserShuldReturnFalse()
        {
            var newComment = _fixture.Create<CreateCommentRequestDto>();
            newComment.ParentId = null;

            var newUser = _fixture.Create<User>();
            newUser.Id = newComment.CreatorId;
            _context.Users.Add(newUser);

            var product = new Product() { Id = newComment.ProductId };
            product.Id = newComment.ProductId;

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newComment.CreatorId))
                .ReturnsAsync(newUser);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newComment.ProductId))
                .ReturnsAsync(product);

            var result = await _commentsService.CreateAsync(newComment);


            Assert.AreEqual(false, await _commentsService.IsCommentCreatedByUser(result.Id, ""));
        }

        [Test]
        public async Task GetByIdAsyncShouldReturnTheRightComment()
        {
            var newComment = _fixture.Create<CreateCommentRequestDto>();
            newComment.ParentId = null;

            var newUser = _fixture.Create<User>();
            newUser.Id = newComment.CreatorId;
            _context.Users.Add(newUser);

            var product = new Product() { Id = newComment.ProductId };
            product.Id = newComment.ProductId;

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newComment.CreatorId))
                .ReturnsAsync(newUser);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newComment.ProductId))
                .ReturnsAsync(product);

            var result = await _commentsService.CreateAsync(newComment);
            var comment = await _commentsService.GetByIdAsync(result.Id);

            Assert.AreEqual(result.Content, comment.Content);
        }

        [Test]
        public async Task GetByIdAsyncShouldReturnNull()
        {
            var newComment = _fixture.Create<CreateCommentRequestDto>();
            newComment.ParentId = null;

            var newUser = _fixture.Create<User>();
            newUser.Id = newComment.CreatorId;
            _context.Users.Add(newUser);

            var product = new Product() { Id = newComment.ProductId };
            product.Id = newComment.ProductId;

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newComment.CreatorId))
                .ReturnsAsync(newUser);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newComment.ProductId))
                .ReturnsAsync(product);

            var result = await _commentsService.CreateAsync(newComment);

            Assert.AreEqual(null, await _commentsService.GetByIdAsync(""));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
    }
}
