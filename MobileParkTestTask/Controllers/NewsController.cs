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
        [HttpGet("Get/{prefix}/{sortBy}/{language}/{year}/{month}/{day}/{apiKey}")]
        public List<NewsInfoFile> GetNewsForUserAsync(
            string prefix,
            SortBys sortBy,
            Languages language,
            int year,
            int month,
            int day,
            string apiKey)
        {
            return newsGetter.HandleNewsListAsync(prefix, sortBy, language, year, month, day, apiKey);
        }
    }
}