using ClickNPick.Application.DtoModels.Categories.Request;

namespace ClickNPick.Web.Models.Categories.Request;

public class EditCategoryRequestModel
{
    public string CategoryId { get; set; }

    public string Name { get; set; }

    public EditCategoryRequestDto ToEditCategoryRequestDto()
    {
        return new EditCategoryRequestDto
        {
            CategoryId = CategoryId,
            Name = Name,
        };
    }
}
