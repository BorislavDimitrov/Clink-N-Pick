using ClickNPick.Application.Services.Categories;
using ClickNPick.Web.Models.Categories.Request;
using Microsoft.AspNetCore.Mvc;

namespace ClickNPick.Web.Controllers.Admin;

[Route("api/administration/[controller]/[action]")]
public class CategoriesController : AdminApiController
{
    private readonly ICategoriesService _categoriesService;

    public CategoriesController(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] string id)
    {
        await _categoriesService.DeleteAsync(id);

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequestModel model)
    {
        var dto = model.ToCreateCategoryRequestDto();
        await _categoriesService.CreateAsync(dto);

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromBody] EditCategoryRequestModel model)
    {
        var dto = model.ToEditCategoryRequestDto();
        await _categoriesService.EditAsync(dto);

        return Ok();
    }
}
