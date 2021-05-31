using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ValetParking.WebApi.Controllers
{
    /// <summary>
    /// Base class for controllers
    /// </summary>
    [Produces("application/json")]
   // [Authorize]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
    }
}
