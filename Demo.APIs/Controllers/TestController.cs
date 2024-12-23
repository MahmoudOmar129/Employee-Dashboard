using Demo.DAL.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.APIs.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [HttpPost]
        [Route("~/api/create")]
        //[Consumes("application/xml")]
        public IActionResult Create(Person person)
        {
            //var doc = JsonExtension.ConverToXml(person);
            return Ok();
        }



    }
}
