using System.Web.Mvc;

namespace ClickNPick.Web.Controllers.Admin;

[Authorize(Roles = "Administrator")]

public abstract class AdminApiController : ApiController
{
}
