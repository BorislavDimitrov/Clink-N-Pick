using ClickNPick.Application.DtoModels.Products.Request;

namespace ClickNPick.Web.Models.Products.Request;

public class UserProductsRequestModel : FilterPaginationModel
{
    public string UserId { get; set; }

    public UserProductsRequestDto ToUserProductsRequestDto()
    {
        var dto = new UserProductsRequestDto();

        dto.UserId = this.UserId;
        dto.Search = this.Search;
        dto.MinPrice = this.MinPrice;
        dto.MaxPrice = this.MaxPrice;
        dto.CategoryIds = this.CategoryIds;
        dto.OrderBy = this.OrderBy;
        dto.PageNumber = this.PageNumber;
        dto.TotalItems = this.TotalItems;

        return dto;
    }
}
