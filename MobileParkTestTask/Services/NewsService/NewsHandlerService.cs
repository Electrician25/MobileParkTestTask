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
        public List<NewsInfoFile> HandleNewsAsync(string prefix, SortBys sortBy)
        {
            var articlesResponse = GetArticle(prefix, sortBy);
            var filesList = new List<NewsInfoFile>();

            if (articlesResponse.Error != null)
            {
                ThrowIfNotValid(articlesResponse.Error.Message);
            }

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
                            VowelLetter = GetVowelLetter(SubstringHandler.GetFirstMentionOfPrefix(article.Content, prefix))
                        });
                        i++;
                    }
                }
            }

            return filesList.OrderByDescending(x => x.VowelLetter).ToList();
        }

        private ArticlesResult GetArticle(string prefix, SortBys sortBy)
        {
            var newsApiClient = new NewsApiClient("a8c7ec95885a493ea159cec18a7a45a1");
            return newsApiClient.GetEverything(new EverythingRequest
            {
                Q = prefix,
                SortBy = sortBy,
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

        private void ThrowIfNotValid(string message)
        {
            if (message.Contains("You are trying to request results too far in the past."))
            {
                throw new ThePastDateException();
            }
        }
    }
}