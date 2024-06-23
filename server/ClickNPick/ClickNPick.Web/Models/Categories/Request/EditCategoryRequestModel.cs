using ClickNPick.Application.DtoModels.Categories.Request;

namespace ClickNPick.Web.Models.Categories.Request;

public class EditCategoryRequestModel
{
    public string CategoryId { get; set; }

    public string Name { get; set; }

    public EditCategoryRequestDto ToEditCategoryRequestDto()
    {
        var dto = new EditCategoryRequestDto();

        dto.CategoryId = CategoryId;
        dto.Name = Name;

        return dto;
    }
}
