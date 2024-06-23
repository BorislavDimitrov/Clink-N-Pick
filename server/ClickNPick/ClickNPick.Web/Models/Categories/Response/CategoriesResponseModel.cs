using ClickNPick.Application.DtoModels.Categories.Response;

namespace ClickNPick.Web.Models.Categories.Response;

public class CategoriesResponseModel
{
    public List<CategoryResponseModel> Categories { get; set; }

    public static CategoriesResponseModel FromCategoriesResponseDto(CategoriesResponseDto dto)
    {
        var categories = dto.Categories.Select(x => CategoryResponseModel.FromCategoryDto(x)).ToList();

        return new CategoriesResponseModel { Categories = categories };      
    }
}
