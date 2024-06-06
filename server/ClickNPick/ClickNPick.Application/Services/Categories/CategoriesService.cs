using ClickNPick.Application.Abstractions.Repositories;
using ClickNPick.Application.DtoModels.Categories.Request;
using ClickNPick.Application.DtoModels.Categories.Response;
using ClickNPick.Application.Exceptions.Categories;
using ClickNPick.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ClickNPick.Application.Services.Categories;

public class CategoriesService : ICategoriesService
{
    private readonly IRepository<Category> _categoriesRepository;

    public CategoriesService(
        IRepository<Category> categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public async Task<string> CreateAsync(CreateCategoryRequestDto model)
    {
        if (_categoriesRepository.AllAsNoTracking().Any(x => x.Name == model.Name))
        {
            throw new InvalidOperationException();
        }

        var newCategory = model.ToCategory();
        
        await _categoriesRepository.AddAsync(newCategory);
        await _categoriesRepository.SaveChangesAsync();

        return newCategory.Id;
    }

    public async Task DeleteAsync(string id)
    {
        var category = await _categoriesRepository.
            All()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (category == null)
        {
            throw new CategoryNotFoundException();
        }

        _categoriesRepository.SoftDelete(category);
        await _categoriesRepository.SaveChangesAsync();
    }

    public async Task EditAsync(EditCategoryRequestDto model)
    {
        var category = await _categoriesRepository
            .All()
            .FirstOrDefaultAsync(x => x.Id == model.CategoryId);

        if (category == null)
        {
            throw new CategoryNotFoundException();
        }

        category.Name = model.Name;

        await _categoriesRepository.SaveChangesAsync();
    }

    public async Task<CategoriesResponseDto> GetAllAsync()
    {
        var categories = await _categoriesRepository
            .AllAsNoTracking()
            .ToListAsync();

        return CategoriesResponseDto.FromCategories(categories);
    }

    public async Task<Category> GetByIdAsync(string categoryId)
        => await _categoriesRepository.All()
            .FirstOrDefaultAsync(x => x.Id == categoryId);
}
