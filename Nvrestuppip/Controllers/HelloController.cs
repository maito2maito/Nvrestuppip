using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Nvrestuppip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        [HttpGet] 
        public ActionResult sayHello()
        {
            return Ok("Hello World");
        }


        [HttpGet("/shout/{feed}")]
        public ActionResult shoutsomething(string feed)
        {
            return Ok(feed + "!!!");
        }

    }
}
