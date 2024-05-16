using Microsoft.AspNetCore.Mvc;
using MobileParkTestTask.Entities.FileNewsEntities;
using MobileParkTestTask.Services.News;

namespace MobileParkTestTask.Controllers
{
    [ApiController]
    [Route("/api/{controller}/")]
    public class NewsController(NewsHandlerService newsGetter) : ControllerBase
    {
        [HttpGet("Get/{prefix}")]
        public List<NewsInfoFile> GetNewsForUserAsync(string prefix)
        {
            return newsGetter.HandleNewsAsync(prefix);
        }
    }
}