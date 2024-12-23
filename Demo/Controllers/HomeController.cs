using Demo.PL.Language;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Demo.PL.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {

        private readonly IStringLocalizer<SharedResource> sharedLocalizer;

        public HomeController(IStringLocalizer<SharedResource> SharedLocalizer)
        {
            sharedLocalizer = SharedLocalizer;
        }

        public IActionResult Index()
        {
            ViewBag.data = sharedLocalizer["Dashboard"];
            return View();
        }
    }
}
