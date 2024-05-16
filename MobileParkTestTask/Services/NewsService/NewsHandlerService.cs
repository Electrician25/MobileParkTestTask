using MobileParkTestTask.Entities.FileNewsEntities;
using MobileParkTestTask.Exceptions;
using MobileParkTestTask.Services.NewsService;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;

namespace MobileParkTestTask.Services.News
{
    public class NewsHandlerService
    {
        private readonly NewsInfoFile _newsInfoFile = new NewsInfoFile();

        public List<NewsInfoFile> HandleNewsListAsync(
            string prefix,
            SortBys sortBy,
            Languages language,
            int year,
            int month,
            int day,
            string apiKey)
        {
            var articlesResponse = GetArticle(prefix, sortBy, language, year, month, day, apiKey);
            var filesList = new List<NewsInfoFile>();

            if (articlesResponse.Error != null || articlesResponse.Articles == null)
            {
                ThrowIfNotValid(articlesResponse);
            }

            if (articlesResponse.Status == Statuses.Ok)
            {
                int i = _newsInfoFile.Id;
                foreach (var article in articlesResponse.Articles)
                {
                    if (article.Content != "[Removed]" && article.Description != "[Removed]")
                    {
                        filesList.Add(new NewsInfoFile
                        {
                            Id = i,
                            Content = article.Content,
                            Description = article.Description,
                            VowelLetter = GetVowels(SubstringHandlerService.GetFirstMentionOfPrefix(article.Content, prefix))
                        });
                        i++;
                    }
                }
            }

            return filesList.OrderByDescending(x => x.VowelLetter).ToList();
        }

        private ArticlesResult GetArticle
            (string prefix,
            SortBys sortBy,
            Languages language,
            int year,
            int month,
            int day,
            string apiKey)
        {
            var newsApiClient = new NewsApiClient(apiKey);
            return newsApiClient.GetEverything(new EverythingRequest
            {
                Q = prefix,
                SortBy = sortBy,
                Language = language,
                From = new DateTime(year, month, day)
            });
        }

        private int GetVowels(string text)
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

        private void ThrowIfNotValid(ArticlesResult message)
        {
            if (message.Articles == null)
            {
                throw new TheNewsNotFoundException();
            }
        }
    }
}