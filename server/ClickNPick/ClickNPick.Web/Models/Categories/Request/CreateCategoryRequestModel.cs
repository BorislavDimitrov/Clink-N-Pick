using ClickNPick.Application.DtoModels.Categories.Request;

namespace ClickNPick.Web.Models.Categories.Request;

public class CreateCategoryRequestModel
{
    public string Name { get; set; }

    public CreateCategoryRequestDto ToCreateCategoryRequestDto()
    {
        var dto = new CreateCategoryRequestDto();
        
        dto.Name = Name;

        return dto;
    }
}
