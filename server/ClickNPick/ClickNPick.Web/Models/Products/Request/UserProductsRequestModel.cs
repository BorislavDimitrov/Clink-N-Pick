using ClickNPick.Application.DtoModels.Products.Request;

namespace ClickNPick.Web.Models.Products.Request;

public class UserProductsRequestModel : FilterPaginationModel
{
    public string UserId { get; set; }

    public UserProductsRequestDto ToUserProductsRequestDto()
    {
        return new UserProductsRequestDto
        {
            UserId = this.UserId,
            Search = this.Search,
            MinPrice = this.MinPrice,
            MaxPrice = this.MaxPrice,
            CategoryIds = this.CategoryIds,
            OrderBy = this.OrderBy,
            PageNumber = this.PageNumber,
            TotalItems = this.TotalItems,
        };
    }
}
