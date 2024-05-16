using Microsoft.AspNetCore.Mvc;
using MobileParkTestTask.Entities.FileNewsEntities;
using MobileParkTestTask.Services.News;
using NewsAPI.Constants;

namespace MobileParkTestTask.Controllers
{
    [ApiController]
    [Route("/api/{controller}/")]
    public class NewsController(NewsHandlerService newsGetter) : ControllerBase
    {
        [HttpGet("Get/{prefix}/{sortBy}")]
        public List<NewsInfoFile> GetNewsForUserAsync(string prefix, SortBys sortBy)
        {
            return newsGetter.HandleNewsAsync(prefix, sortBy);
        }
    }
}