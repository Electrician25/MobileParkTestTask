using Microsoft.AspNetCore.Mvc;
using MobileParkTestTask.Services.News;

namespace MobileParkTestTask.Controllers
{
    [ApiController]
    [Route("/{controller}/")]
    public class NewsController(NewsHandlerService newsGetter) : ControllerBase
    {
        [HttpGet("Get")]
        public void GetNewsForUserAsync()
        {
            newsGetter.HandleNewsAsync();
        }
    }
}