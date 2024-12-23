using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
