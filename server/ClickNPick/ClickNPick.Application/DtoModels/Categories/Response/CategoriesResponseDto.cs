using ClickNPick.Domain.Models;

namespace ClickNPick.Application.DtoModels.Categories.Response;

public class CategoriesResponseDto
{
    public List<CategoryDto> Categories { get; set; }

    public static CategoriesResponseDto FromCategories(IEnumerable<Category> categories)
    {
        var categoryDtos =  categories.Select( x => CategoryDto.FromCategory(x) ).ToList();

        return new CategoriesResponseDto { Categories = categoryDtos };
    }
}
