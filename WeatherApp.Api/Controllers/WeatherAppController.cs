using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WeatherApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherAppController : ControllerBase
    {


        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
