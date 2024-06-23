using ClickNPick.Application.DtoModels.Categories.Response;

namespace ClickNPick.Web.Models.Categories.Response;

public class CategoryResponseModel
{
    public string Id { get; set; }

    public string Name { get; set; }

    public static CategoryResponseModel FromCategoryDto(CategoryDto dto)
    {
        var model = new CategoryResponseModel();

        model.Id = dto.Id;
        model.Name = dto.Name;
        
        return model;
    }
}
