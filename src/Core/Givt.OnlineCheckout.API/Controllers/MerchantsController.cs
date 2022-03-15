using Microsoft.AspNetCore.Mvc;

namespace Givt.OnlineCheckout.API.Controllers
{
    public class MerchantsController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return Ok();
        }
    }
}