namespace ClickNPick.Application.DtoModels.Products.Request;

public class UserOwnProductsRequestDto : FilterPaginationDto
{
    public string UserId { get; set; }
}
