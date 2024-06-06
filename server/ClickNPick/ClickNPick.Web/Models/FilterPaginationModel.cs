using ClickNPick.Application.DtoModels;
using ClickNPick.Application.DtoModels.Products.Request;

namespace ClickNPick.Web.Models;

public class FilterPaginationModel
{
    public string? Search { get; set; }

    public decimal MinPrice { get; set; }

    public decimal MaxPrice { get; set; }

    public List<string> CategoryIds { get; set; } = new List<string>();

    public string? OrderBy { get; set; } = "DateDesc"; 

    public int PageNumber { get; set; }

    public int TotalItems { get; set; }

    public int PageSize { get; private set; } = 9;

    public FilterPaginationDto ToFilterPaginationDto()
    {
        return new FilterPaginationDto
        {
            Search = this.Search,
            MinPrice = this.MinPrice,
            MaxPrice = this.MaxPrice,
            CategoryIds = this.CategoryIds,
            OrderBy = this.OrderBy,
            PageNumber = this.PageNumber,
            TotalItems = this.TotalItems,
        };
    }

    public UserOwnProductsRequestDto ToUserOwnProductsRequestDto()
    {
        return new UserOwnProductsRequestDto
        { 
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
