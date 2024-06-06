using ClickNPick.Application.DtoModels.Categories.Request;

namespace ClickNPick.Web.Models.Categories.Request;

public class CreateCategoryRequestModel
{
    public string Name { get; set; }

    public CreateCategoryRequestDto ToCreateCategoryRequestDto()
    {
        return new CreateCategoryRequestDto
        {
            Name = Name,
        };
    }
}
