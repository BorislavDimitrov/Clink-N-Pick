using ClickNPick.Application.DtoModels.Categories.Request;
using ClickNPick.Application.DtoModels.Categories.Response;
using ClickNPick.Domain.Models;

namespace ClickNPick.Application.Services.Categories;

public interface ICategoriesService
{
    Task<string> CreateAsync(CreateCategoryRequestDto model);

    Task DeleteAsync(string id);

    Task<CategoriesResponseDto> GetAllAsync();

    Task<Category> GetByIdAsync(string categoryId);

    Task EditAsync(EditCategoryRequestDto model);
}
