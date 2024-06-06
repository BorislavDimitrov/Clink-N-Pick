using ClickNPick.Application.DtoModels.Products.Request;
using ClickNPick.Application.Services.Products;
using ClickNPick.Web.Extensions;
using ClickNPick.Web.Models;
using ClickNPick.Web.Models.Products.Request;
using ClickNPick.Web.Models.Products.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClickNPick.Web.Controllers;

public class ProductsController : ApiController
{
    private readonly IProductsService _productsService;

    public ProductsController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    [Authorize]
    [HttpPost]
    [RequestSizeLimit(20 * 1024 * 1024)]
    public async Task<IActionResult> Create([FromForm] CreateProductRequestModel model)
    {
        var dto = model.ToCreateProductRequestDto();
        var userId = HttpContext.User.GetId();
        dto.CreatorId = userId;

        await _productsService.CreateProductAsync(dto);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details([FromRoute] string id)
    {
        var result = await _productsService.GetDetailsAsync(id);
        var response = ProductDetailsResponseModel.FromProductDetailsResponseDto(result);
        return Ok(response);
    }

    [Authorize()]
    [HttpPost]
    [RequestSizeLimit(20 * 1024 * 1024)]
    public async Task<IActionResult> Edit([FromForm] EditProductRequestModel model)
    {
        var dto = model.ToEditProductRequestDto();
        var userId = HttpContext.User.GetId();
        dto.UserId = userId;

        await _productsService.EditProductAsync(dto);
        return Ok();
    }

    [Authorize]
    [HttpGet("GetEditDetails/{id}")]

    public async Task<IActionResult> Edit(string id)
    {
        var dto = new GetProductEditDetailsRequestDto();
        var userId = HttpContext.User.GetId();
        dto.UserId = userId;
        dto.ProductId = id;

        var result = await _productsService.GetEditDetailsAsync(dto);
        var response = ProductEditDetailsResponseModel.FromProductEditDetailsResponseDto(result);
        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] string id)
    {
        var userId = HttpContext.User.GetId();

        var request = new DeleteProductRequestDto() { ProductId = id, UserId = userId };

        await _productsService.DeleteAsync(request);
        return Ok();
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> MyProducts([FromQuery] FilterPaginationModel model)
    {
        var userId = HttpContext.User.GetId();
        var dto = model.ToUserOwnProductsRequestDto();
        dto.UserId = userId;

        var result = await _productsService.GetUserOwnProductsAsync(dto);
        var response = ProductListingResponseModel.FromProductListingResponseDto(result);

        return Ok(response);
    }


    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] FilterPaginationModel model)
    {
        var dto = model.ToFilterPaginationDto();

        var result = await _productsService.SearchAsync(dto);
        var response = ProductListingResponseModel.FromProductListingResponseDto(result);

        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Promote(PromoteProductRequestModel model)
    {
        var userId = HttpContext.User.GetId();
        var dto = model.ToPromoteProductRequestDto();
        dto.UserId = userId;

        await _productsService.PromoteAsync(dto);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> UserProducts([FromQuery] UserProductsRequestModel model)
    {
        var dto = model.ToUserProductsRequestDto();

        var result = await _productsService.GetUserProductsAsync(dto);
        var response = ProductListingResponseModel.FromProductListingResponseDto(result);

        return Ok(response);
    }
}

