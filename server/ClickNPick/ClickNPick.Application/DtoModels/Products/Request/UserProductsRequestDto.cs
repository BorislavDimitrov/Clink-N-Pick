namespace ClickNPick.Application.DtoModels.Products.Request;

public class UserProductsRequestDto : FilterPaginationDto
{
    public string UserId { get; set; }
}
