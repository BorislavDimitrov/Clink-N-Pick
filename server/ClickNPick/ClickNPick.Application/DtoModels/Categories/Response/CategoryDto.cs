using ClickNPick.Domain.Models;

namespace ClickNPick.Application.DtoModels.Categories.Response;

public class CategoryDto
{
    public string Id { get; set; }

    public string Name { get; set; }

    public static CategoryDto FromCategory (Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
        };
    }
}
