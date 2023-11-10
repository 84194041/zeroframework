using Microsoft.AspNetCore.Mvc;

namespace ZeroFramework.ReverseProxy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("Index")]
        public async Task<string> Index()
        {
            return await Task.FromResult("Welcome Use ReverseProxy");
        }
    }
}
