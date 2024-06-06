using ClickNPick.Domain.Models;

namespace ClickNPick.Application.DtoModels.Products.Response;

public class ProductListingResponseDto : FilterPaginationDto
{
    public List<ProductInListResponseDto> Products { get; set; }

    public static ProductListingResponseDto FromProducts(IEnumerable<Product> products)
    {
        var productsDto = products.Select(x => ProductInListResponseDto.FromProduct(x)).ToList();

        return new ProductListingResponseDto { Products =  productsDto };
    }
}
