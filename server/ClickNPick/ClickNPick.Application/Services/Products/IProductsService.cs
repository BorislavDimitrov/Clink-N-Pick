using ClickNPick.Application.DtoModels;
using ClickNPick.Application.DtoModels.Products.Request;
using ClickNPick.Application.DtoModels.Products.Response;

namespace ClickNPick.Application.Services.Products;

public interface IProductsService
{
     Task<string> CreateProductAsync(CreateProductRequestDto model);

     Task EditProductAsync(EditProductRequestDto model);

     Task<bool> IsProductMadeByUserAsync(string productId, string userId);

     Task<ProductDetailsResponseDto> GetDetailsAsync(string productId);

     Task DeleteAsync(DeleteProductRequestDto model);

     Task<ProductListingResponseDto> GetUserOwnProductsAsync(UserOwnProductsRequestDto model);

     Task PromoteAsync(PromoteProductRequestDto model);

     Task<ProductEditDetailsResponseDto> GetEditDetailsAsync(GetProductEditDetailsRequestDto model);

     Task<ProductListingResponseDto> SearchAsync(FilterPaginationDto model);

    Task<ProductListingResponseDto> GetUserProductsAsync(UserProductsRequestDto model);
}
