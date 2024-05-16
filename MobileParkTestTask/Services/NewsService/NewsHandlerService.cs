using MobileParkTestTask.Entities.FileNewsEntities;
using MobileParkTestTask.Services.NewsService;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;

namespace MobileParkTestTask.Services.News
{
    public class NewsHandlerService
    {
        public List<NewsInfoFile> HandleNewsAsync(string prefix)
        {
            var articlesResponse = GetArticle(prefix);

            var filesList = new List<NewsInfoFile>();

            if (articlesResponse.Status == Statuses.Ok)
            {
                int i = 0;
                foreach (var article in articlesResponse.Articles)
                {
                    if (article.Content != "[Removed]" && article.Description != "[Removed]")
                    {
                        filesList.Add(new NewsInfoFile
                        {
                            Id = i,
                            Content = article.Content,
                            Description = article.Description,
                            VowelLetter = GetVowelLetter(SubstringHandler.GetFirstCharactersOfString(article.Content, prefix))
                        });
                        i++;
                    }
                }
            }

            else if (articlesResponse.Error.Message.Equals("You are trying to request results too far in the past."))
            {
                //throw new ThePastDateException();
            }

            var result = filesList.OrderByDescending(x => x.VowelLetter).ToList();

            return result;
        }

        private ArticlesResult GetArticle(string prefix)
        {
            var newsApiClient = new NewsApiClient("a8c7ec95885a493ea159cec18a7a45a1");
            return newsApiClient.GetEverything(new EverythingRequest
            {
                Q = prefix,
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
                From = new DateTime(2024, 4, 10)
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