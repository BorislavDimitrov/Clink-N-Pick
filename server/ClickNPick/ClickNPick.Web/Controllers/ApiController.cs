using Microsoft.AspNetCore.Mvc;

namespace ClickNPick.Web.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class ApiController : ControllerBase
{
}
