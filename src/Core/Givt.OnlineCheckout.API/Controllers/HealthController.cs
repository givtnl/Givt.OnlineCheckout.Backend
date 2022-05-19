using Microsoft.AspNetCore.Mvc;

namespace Givt.OnlineCheckout.API.Controllers
{
    [Route("/api/health")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        // Health check
        public IActionResult Check()
        {            
            return Ok();
        }
    }
}
