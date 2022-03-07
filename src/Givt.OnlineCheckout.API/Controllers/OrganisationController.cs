using Microsoft.AspNetCore.Mvc;

namespace Givt.OnlineCheckout.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisationController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetOrganisation()
        {
            
            return new OkResult();
        }
    }
}