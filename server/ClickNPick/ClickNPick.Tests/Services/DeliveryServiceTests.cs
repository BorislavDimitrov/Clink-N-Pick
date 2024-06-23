using AutoFixture;
using AutoFixture.AutoMoq;
using ClickNPick.Application.Abstractions.Repositories;
using ClickNPick.Application.Abstractions.Services;
using ClickNPick.Application.DtoModels.Delivery.Request;
using ClickNPick.Application.Exceptions.Delivery;
using ClickNPick.Application.Exceptions.Identity;
using ClickNPick.Application.Exceptions.Products;
using ClickNPick.Application.Services.Delivery;
using ClickNPick.Application.Services.Products;
using ClickNPick.Application.Services.Users;
using ClickNPick.Domain.Models;
using ClickNPick.Domain.Models.Enums;
using ClickNPick.Infrastructure.Data;
using ClickNPick.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ClickNPick.Tests.Services
{
    public class DeliveryServiceTests
    {
        private IFixture _fixture;
        private IDeliveryService _deliveryService;
        private IRepository<ShipmentRequest> _shipmentRequestsRepository;
        private Mock<IUsersService> _mockedUsersService;
        private Mock<IProductsService> _mockedProductsService;
        private Mock<ICacheService> _mockedCacheService;
        private Mock<HttpClient> _mockedHttpClient;
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
            _mockedCacheService = new Mock<ICacheService>();
            _mockedHttpClient = new Mock<HttpClient>();
            _shipmentRequestsRepository = new Repository<ShipmentRequest>(_context);
            _deliveryService = new DeliveryService(_mockedHttpClient.Object,_mockedCacheService.Object,_shipmentRequestsRepository, _mockedUsersService.Object, _mockedProductsService.Object);
        }


        [Test]
        public async Task CreateShipmentRequestAsyncShouldBeSuccessful()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            var result = await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto);

            Assert.IsNotEmpty(result);
        }

        [Test]
        public async Task CreateShipmentRequestAsyncShouldThrowProductNotFoundException()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            Assert.ThrowsAsync<ProductNotFoundException>(async () => await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto));
        }

        [Test]
        public async Task CreateShipmentRequestAsyncShouldThrowInvalidOperationException()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            newShipmentRequestDto.BuyerId = product.CreatorId;

            Assert.ThrowsAsync<InvalidOperationException>(async () => await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto));
        }

        [Test]
        public async Task CreateShipmentRequestAsyncShouldThrowUserNotFoundExceptionDueToMissingBuyer()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            Assert.ThrowsAsync<UserNotFoundException>(async () => await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto));
        }

        [Test]
        public async Task CreateShipmentRequestAsyncShouldThrowUserNotFoundExceptionDueToMissiSeller()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            Assert.ThrowsAsync<UserNotFoundException>(async () => await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto));
        }

        [Test]
        public async Task AcceptShipmentShouldThrowUserNotFoundException()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            var id = await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto);

            var acceptShipmentRequestDto = _fixture.Create<AcceptShipmentRequestDto>();
            acceptShipmentRequestDto.RequestShipmentId = id;

            Assert.ThrowsAsync<UserNotFoundException>(async () => await _deliveryService.AcceptShipmentAsync(acceptShipmentRequestDto));
        }

        [Test]
        public async Task AcceptShipmentShouldThrowShipmentRequestNotFoundException()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            var id = await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto);

            var acceptShipmentRequestDto = _fixture.Create<AcceptShipmentRequestDto>();
            acceptShipmentRequestDto.UserId = seller.Id;

            Assert.ThrowsAsync<ShipmentRequestNotFoundException>(async () => await _deliveryService.AcceptShipmentAsync(acceptShipmentRequestDto));
        }

        [Test]
        public async Task AcceptShipmentShouldThrowInvalidOperationExceptionDueToUserCantSend()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            var id = await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto);

            var acceptShipmentRequestDto = _fixture.Create<AcceptShipmentRequestDto>();
            acceptShipmentRequestDto.RequestShipmentId = id;
            acceptShipmentRequestDto.UserId = buyer.Id;                    

            Assert.ThrowsAsync<InvalidOperationException>(async () => await _deliveryService.AcceptShipmentAsync(acceptShipmentRequestDto));
        }

        [Test]
        public async Task AcceptShipmentShouldThrowInvalidOperationExceptionDueToStatus()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            var id = await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto);

            var acceptShipmentRequestDto = _fixture.Create<AcceptShipmentRequestDto>();
            acceptShipmentRequestDto.RequestShipmentId = id;
            acceptShipmentRequestDto.UserId = buyer.Id;

            var shipment = await _context.ShipmentRequests.FirstOrDefaultAsync(x => x.Id == id);
            shipment.ShipmentStatus = ShipmentStatus.Accepted;

            Assert.ThrowsAsync<InvalidOperationException>(async () => await _deliveryService.AcceptShipmentAsync(acceptShipmentRequestDto));
        }

        [Test]
        public async Task CancelShipmentRequestAsyncShouldBeSuccesful()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            var id = await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto);

            var cancelShipmentDto = _fixture.Create<CancelShipmentRequestDto>();
            cancelShipmentDto.ShipmentId = id;
            cancelShipmentDto.UserId = seller.Id;

            await _deliveryService.CancelShipmentRequestAsync(cancelShipmentDto);

            var shipment = await _context.ShipmentRequests.FirstOrDefaultAsync(x => x.Id == id);

            Assert.AreEqual("Canceled", shipment.ShipmentStatus.ToString());
        }

        [Test]
        public async Task CancelShipmentRequestAsyncShouldThrowShipmentRequestNotFoundException()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            var id = await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto);

            var cancelShipmentDto = _fixture.Create<CancelShipmentRequestDto>();
            cancelShipmentDto.UserId = seller.Id;         

            Assert.ThrowsAsync<ShipmentRequestNotFoundException>(async () => await _deliveryService.CancelShipmentRequestAsync(cancelShipmentDto));
        }

        [Test]
        public async Task CancelShipmentRequestAsyncShouldThrowInvalidOperationExceptionDueUserCannotCancel()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            var id = await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto);

            var cancelShipmentDto = _fixture.Create<CancelShipmentRequestDto>();
            cancelShipmentDto.ShipmentId = id;
            cancelShipmentDto.UserId = "";

            Assert.ThrowsAsync<InvalidOperationException>(async () => await _deliveryService.CancelShipmentRequestAsync(cancelShipmentDto));
        }

        [Test]
        public async Task CancelShipmentRequestAsyncShouldThrowInvalidOperationExceptionDueToStatusBeingCanceled()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            var id = await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto);

            var cancelShipmentDto = _fixture.Create<CancelShipmentRequestDto>();
            cancelShipmentDto.ShipmentId = id;
            cancelShipmentDto.UserId = seller.Id;

            var shipment = await _context.ShipmentRequests.FirstOrDefaultAsync(x => x.Id == id);
            shipment.ShipmentStatus = ShipmentStatus.Canceled;
            _context.Update(shipment);
            await _context.SaveChangesAsync();

            Assert.ThrowsAsync<InvalidOperationException>(async () => await _deliveryService.CancelShipmentRequestAsync(cancelShipmentDto));
        }

        [Test]
        public async Task DeclineShipmentAsyncShouldBeSuccessful()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            var id = await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto);

            var declineShipmentDto = _fixture.Create<DeclineShipmentRequestDto>();
            declineShipmentDto.ShipmentId = id;
            declineShipmentDto.UserId = seller.Id;

            await _deliveryService.DeclineShipmentAsync(declineShipmentDto);

            var shipment = await _context.ShipmentRequests.FirstOrDefaultAsync(x => x.Id == id);   
            Assert.AreEqual("Declined", shipment.ShipmentStatus.ToString());
        }

        [Test]
        public async Task DeclineShipmentAsyncShouldThrowShipmentRequestNotFoundException()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            var id = await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto);

            var declineShipmentDto = _fixture.Create<DeclineShipmentRequestDto>();
            declineShipmentDto.UserId = seller.Id;

            Assert.ThrowsAsync<ShipmentRequestNotFoundException>(async () => await _deliveryService.DeclineShipmentAsync(declineShipmentDto));
        }

        [Test]
        public async Task DeclineShipmentAsyncShouldThrowInvalidOperationExceptionDueToUserCannotChangeIt()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            var id = await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto);

            var declineShipmentDto = _fixture.Create<DeclineShipmentRequestDto>();
            declineShipmentDto.ShipmentId = id;
            declineShipmentDto.UserId = "some value";

            Assert.ThrowsAsync<InvalidOperationException>(async () => await _deliveryService.DeclineShipmentAsync(declineShipmentDto));
        }

        [Test]
        public async Task DeclineShipmentAsyncShouldThrowInvalidOperationExceptionDueToAlreadyDeclined()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            var id = await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto);

            var declineShipmentDto = _fixture.Create<DeclineShipmentRequestDto>();
            declineShipmentDto.ShipmentId = id;
            declineShipmentDto.UserId = seller.Id;

            var shipment = await _context.ShipmentRequests.FirstOrDefaultAsync(x => x.Id == id);
            shipment.ShipmentStatus = ShipmentStatus.Declined;
            _context.Update(shipment);
            await _context.SaveChangesAsync();

            Assert.ThrowsAsync<InvalidOperationException>(async () => await _deliveryService.DeclineShipmentAsync(declineShipmentDto));
        }

        [Test]
        public async Task GetShipmentDetailsAsyncShouldThrowShipmentRequestNotFoundException()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            var id = await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto);

            var detailsRequestDto = _fixture.Create<ShipmentDetailsRequestDto>();
            detailsRequestDto.UserId = seller.Id;

            Assert.ThrowsAsync<ShipmentRequestNotFoundException>(async () => await _deliveryService.GetShipmentDetailsAsync(detailsRequestDto));
        }

        [Test]
        public async Task GetShipmentDetailsAsyncShouldThrowInvalidOperationExceptionDueToUserCannotViewIt()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            var id = await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto);

            var detailsRequestDto = _fixture.Create<ShipmentDetailsRequestDto>();
            detailsRequestDto.ShipmentId = id;
            detailsRequestDto.UserId = "some value";

            Assert.ThrowsAsync<InvalidOperationException>(async () => await _deliveryService.GetShipmentDetailsAsync(detailsRequestDto));
        }


        [Test]
        public async Task GetShipmentDetailsAsyncShouldThrowInvalidOperationExceptionDueTonotAcceptedStatus()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            var id = await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto);

            var detailsRequestDto = _fixture.Create<ShipmentDetailsRequestDto>();
            detailsRequestDto.ShipmentId = id;
            detailsRequestDto.UserId = seller.Id;

            Assert.ThrowsAsync<InvalidOperationException>(async () => await _deliveryService.GetShipmentDetailsAsync(detailsRequestDto));
        }

        [Test]
        public async Task GetShipmentsToSendAsyncShouldReturnRightList()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id, CategoryId = Guid.NewGuid().ToString(), Description = "d", Title = "t" };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            var id = await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto);

            var shipment = await _context.ShipmentRequests.FirstOrDefaultAsync(x => x.Id == id);
            shipment.Seller = seller;
            shipment.Buyer = buyer;
            shipment.Product = product;
            _context.Update(shipment);
            await _context.SaveChangesAsync();

            var result = await _deliveryService.GetShipmentsToSendAsync(seller.Id);
            Assert.AreEqual(1, result.Shipments.Count);
        }

        [Test]
        public async Task GetShipmentsToSendAsyncShouldThrowUserNotFoundException()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id, CategoryId = Guid.NewGuid().ToString(), Description = "d", Title = "t" };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            var id = await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto);

            var detailsRequestDto = _fixture.Create<ShipmentDetailsRequestDto>();
            detailsRequestDto.ShipmentId = id;

            var result = await _deliveryService.GetShipmentsToSendAsync(seller.Id);
            Assert.ThrowsAsync<UserNotFoundException>(async () => await _deliveryService.GetShipmentsToSendAsync(""));
        }


        [Test]
        public async Task GetShipmentsToReceiveAsyncShouldReturnRightList()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id, CategoryId = Guid.NewGuid().ToString(), Description = "d", Title = "t" };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            var id = await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto);

            var shipment = await _context.ShipmentRequests.FirstOrDefaultAsync(x => x.Id == id);
            shipment.Seller = seller;
            shipment.Buyer = buyer;
            shipment.Product = product;
            _context.Update(shipment);
            await _context.SaveChangesAsync();

            var result = await _deliveryService.GetShipmentsToReceiveAsync(buyer.Id);
            Assert.AreEqual(1, result.Shipments.Count);
        }

        [Test]
        public async Task GetShipmentsToReceiveAsyncShouldThrowUserNotFoundException()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id, CategoryId = Guid.NewGuid().ToString(), Description = "d", Title = "t" };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            var id = await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto);

            Assert.ThrowsAsync<UserNotFoundException>(async () => await _deliveryService.GetShipmentsToReceiveAsync(""));
        }

        [Test]
        public async Task GetByIdAsyncShouldReturnRightRequestShipment()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id, CategoryId = Guid.NewGuid().ToString(), Description = "d", Title = "t" };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            var id = await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto);

            var shipment = await _deliveryService.GetByIdAsync(id);

            Assert.NotNull(shipment);
            Assert.AreEqual(id, shipment.Id);
        }

        [Test]
        public async Task GetByIdAsyncShouldReturnNull()
        {
            var newShipmentRequestDto = _fixture.Create<RequestShipmentRequestDto>();
            newShipmentRequestDto.DeliveryLocation = DeliveryLocation.Office.ToString();

            var buyer = _fixture.Create<User>();
            buyer.Id = newShipmentRequestDto.BuyerId;
            _context.Users.Add(buyer);

            var seller = _fixture.Create<User>();
            seller.Id = Guid.NewGuid().ToString();
            _context.Users.Add(seller);

            var product = new Product() { Id = newShipmentRequestDto.ProductId, CreatorId = seller.Id, CategoryId = Guid.NewGuid().ToString(), Description = "d", Title = "t" };

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.BuyerId))
                .ReturnsAsync(buyer);

            _mockedUsersService
                .Setup(x => x.GetByIdAsync(seller.Id))
                .ReturnsAsync(seller);

            _mockedProductsService
                .Setup(x => x.GetByIdAsync(newShipmentRequestDto.ProductId))
                .ReturnsAsync(product);

            var id = await _deliveryService.CreateShipmentRequestAsync(newShipmentRequestDto);

            var shipment = await _deliveryService.GetByIdAsync("");

            Assert.IsNull(shipment);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
    }
}
