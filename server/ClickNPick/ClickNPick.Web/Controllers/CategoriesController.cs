using ClickNPick.Application.Services.Categories;
using ClickNPick.Web.Models.Categories.Response;
using Microsoft.AspNetCore.Mvc;

namespace ClickNPick.Web.Controllers;

public class CategoriesController : ApiController
{
    private readonly ICategoriesService _categoriesService;

    public CategoriesController(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _categoriesService.GetAllAsync();
        var response = CategoriesResponseModel.FromCategoriesResponseDto(result);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] string id)
    {
        var response = await _categoriesService.GetByIdAsync(id);
        return Ok(response);
    }
}
