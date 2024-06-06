using ClickNPick.Application.DtoModels.Products.Response;

namespace ClickNPick.Web.Models.Products.Response;

public class ProductListingResponseModel : FilterPaginationModel
{
    public List<ProductsInListResponseModel> Products { get; set; }

    public static ProductListingResponseModel FromProductListingResponseDto(ProductListingResponseDto dto)
    {
        var products = dto.Products.Select(x => ProductsInListResponseModel.FromProductInListResponseDto(x)).ToList();

        return new ProductListingResponseModel { Products = products, PageNumber = dto.PageNumber, TotalItems = dto.TotalItems };
    }
}
