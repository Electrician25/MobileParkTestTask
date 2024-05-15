using MobileParkTestTask.Entities.FileNewsEntities;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;

namespace MobileParkTestTask.Services.News
{
    public class NewsHandlerService
    {
        public List<NewsInfoFile> HandleNewsAsync()
        {
            var articlesResponse = GetArticle();

            var result = new List<NewsInfoFile>();

            if (articlesResponse.Status == Statuses.Ok)
            {
                int i = 0;
                foreach (var article in articlesResponse.Articles)
                {
                    i++;
                    if (article.Content != "[Removed]" && article.Description != "[Removed]")
                    {
                        result.Add(new NewsInfoFile
                        {
                            Id = i,
                            Content = article.Content,
                            Description = article.Description,
                            VowelLetter = GetVowelLetter(article.Description + "" + article.Content)
                        });
                    }
                }
            }

            return result;
        }

        private ArticlesResult GetArticle()
        {
            var newsApiClient = new NewsApiClient("a8c7ec95885a493ea159cec18a7a45a1");
            return newsApiClient.GetEverything(new EverythingRequest
            {
                Q = "Goold Apple",
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
                From = new DateTime(2024, 4, 15)
            });
        }

        private int GetVowelLetter(string text)
        {
            var vowelCounter = 0;
            var vowels = new List<char>()
            {
                'a', 'e', 'i', 'o', 'u', 'y', 'у',
                'е', 'ы', 'а', 'о', 'э', 'я', 'и', 'ю'
            };

            foreach (char c in text.ToLower())
            {
                if (vowels.Contains(c))
                {
                    vowelCounter++;
                }
            }

            return vowelCounter;
        }
    }
}