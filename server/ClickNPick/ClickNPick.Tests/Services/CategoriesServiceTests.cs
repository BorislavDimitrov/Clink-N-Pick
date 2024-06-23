using AutoFixture;
using AutoFixture.AutoMoq;
using ClickNPick.Application.Abstractions.Repositories;
using ClickNPick.Application.DtoModels.Categories.Request;
using ClickNPick.Application.Exceptions.Categories;
using ClickNPick.Application.Services.Categories;
using ClickNPick.Domain.Models;
using ClickNPick.Infrastructure.Data;
using ClickNPick.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClickNPick.Tests.Services;

public class CategoriesServiceTests
{

    private IFixture _fixture;
    private ICategoriesService _categoriesService;
    private IRepository<Category> _categoriesRepository;
    private DbContextOptionsBuilder<ClickNPickDbContext> _options;
    private ClickNPickDbContext _context;

    [SetUp]
    [TearDown]
    public void Setup()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _options = new DbContextOptionsBuilder<ClickNPickDbContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString());
        _context = new ClickNPickDbContext(_options.Options);
        _categoriesRepository = new Repository<Category>(_context);
        _categoriesService = new CategoriesService(_categoriesRepository);
    }

    [Test]

    public async Task CreateCategoryShouldBeCreateSuccessfully()
    {
        var newProduct = _fixture.Create<CreateCategoryRequestDto>();
        await _categoriesService.CreateAsync(newProduct);

        Assert.AreEqual(1, _categoriesRepository.AllAsNoTracking().Count());
    }

    [Test]
    public async Task CreateCategoryShouldThrowInvalidOperationException()
    {
        var newCategory = _fixture.Create<CreateCategoryRequestDto>();
        newCategory.Name = "Books";
        var newCategory2 = _fixture.Create<CreateCategoryRequestDto>();
        newCategory2.Name = "Books";
        await _categoriesService.CreateAsync(newCategory);

        Assert.ThrowsAsync<InvalidOperationException>(async () => await _categoriesService.CreateAsync(newCategory2));
    }

    [Test]
    public async Task CreateCategoryShouldReturnRightId()
    {
        var newCategory = _fixture.Create<CreateCategoryRequestDto>();
        newCategory.Name = "Books";

        var id = await _categoriesService.CreateAsync(newCategory);

        var categoryFromDb = await _categoriesRepository
            .AllAsNoTracking()
            .FirstOrDefaultAsync(c => c.Name == newCategory.Name);
        Assert.NotNull(categoryFromDb);
        Assert.AreEqual(categoryFromDb.Id, id);
    }

    [Test]
    public async Task DeleteCategoryShouldBeSuccessful()
    {
        var newCategory = _fixture.Create<CreateCategoryRequestDto>();
        var id = await _categoriesService.CreateAsync(newCategory);
        await _categoriesService.DeleteAsync(id);

        var categories = _categoriesRepository.AllAsNoTracking();
        Assert.AreEqual(0, categories.Count());
    }

    [Test]
    public async Task DeleteCategoryShouldThrowCategoryNotFoundException()
    {
        Assert.ThrowsAsync<CategoryNotFoundException>(async () => await _categoriesService.DeleteAsync("1"));
    }

    [Test]
    public async Task EditShouldBeSuccessful()
    {
        var createCategoryDto = _fixture.Create<CreateCategoryRequestDto>();
        createCategoryDto.Name = "Sports And Equipment";
        var newCategoryId = await _categoriesService.CreateAsync(createCategoryDto);

        var editCategoryDto = _fixture.Create<EditCategoryRequestDto>();
        editCategoryDto.CategoryId = newCategoryId;
        editCategoryDto.Name = "Sports Equipment";

        await _categoriesService.EditAsync(editCategoryDto);
        var editedCategory = await _categoriesRepository
            .AllAsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == "Sports Equipment");
        Assert.IsNotNull(editedCategory);
    }

    [Test]
    public async Task EditShouldThrowCategoryNotFoundException()
    {
        var createCategoryDto = _fixture.Create<CreateCategoryRequestDto>();
        createCategoryDto.Name = "Sports And Equipment";
        var newCategoryId = await _categoriesService.CreateAsync(createCategoryDto);

        var editCategoryDto = _fixture.Create<EditCategoryRequestDto>();
        editCategoryDto.CategoryId = "";
        editCategoryDto.Name = "Sports Equipment";

        Assert.ThrowsAsync<CategoryNotFoundException>(async () => await _categoriesService.EditAsync(editCategoryDto));
    }

    [Test]
    public async Task GetAllShouldRetunAllCategories()
    {
        for (int i = 0; i < 10; i++)
        {
            var createCategoryDto = _fixture.Create<CreateCategoryRequestDto>();
            await _categoriesService.CreateAsync(createCategoryDto);
        }

        var categories = await _categoriesService.GetAllAsync();

        Assert.AreEqual(10, categories.Categories.Count);
    }

    [Test]
    public async Task GetByIdShouldReturnTheRightEntity()
    {
        var createCategoryDto = _fixture.Create<CreateCategoryRequestDto>();
        createCategoryDto.Name = "Sports And Equipment";
        var newCategoryId = await _categoriesService.CreateAsync(createCategoryDto);

        var category = await _categoriesService.GetByIdAsync(newCategoryId);

        Assert.AreEqual(newCategoryId, category.Id);
    }

    [Test]
    public async Task GetByIdShouldReturnNull()
    {
        var createCategoryDto = _fixture.Create<CreateCategoryRequestDto>();
        createCategoryDto.Name = "Sports And Equipment";
        var newCategoryId = await _categoriesService.CreateAsync(createCategoryDto);

        var category = await _categoriesService.GetByIdAsync("");

        Assert.IsNull(category);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }
}
