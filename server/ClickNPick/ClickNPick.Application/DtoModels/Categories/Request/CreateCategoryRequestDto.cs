using ClickNPick.Domain.Models;

namespace ClickNPick.Application.DtoModels.Categories.Request;

public class CreateCategoryRequestDto
{
    public string Name { get; set; }

    public Category ToCategory()
    {
        var category = new Category();
        category.Name = Name;

        return category;
    }      
}
