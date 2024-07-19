using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiUkraineCities.Controllers
{
    [Authorize(Roles = "LogisticManager")]
    [Route("api/[controller]")]
    [ApiController]
    public class LogisticManagerController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("You have accessed the Logistic Manager controller.");
        }
    }
}
