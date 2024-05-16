using MobileParkTestTask.Controllers;
using MobileParkTestTask.Exceptions;
using MobileParkTestTask.Services.News;
using NewsAPI.Constants;

namespace AppTests
{
    [TestClass]
    public class HandleServiceTest
    {
        [TestMethod]
        public void WhenServiceHandling_AndPrefixAndDateIsCorrect_ThenShouldBeTrue()
        {
            // Arange.
            var newsHandler = new NewsHandlerService();
            var prefix = "Apple";
            var sort = SortBys.Relevancy;
            var language = Languages.EN;
            var year = 2024;
            var month = 5;
            var day = 15;
            var apiKey = "a8c7ec95885a493ea159cec18a7a45a1";

            // Act.
            var list = newsHandler.HandleNewsListAsync(prefix, sort, language, year, month, day, apiKey);
            var result = list.Count >= 1;

            // Assert.
            Assert.AreEqual(true, result);
        }


        [TestMethod]
        public void WhenServiceHandling_AndDateIsNotCorrect_ThrowException()
        {
            // Arange.
            var newsHandler = new NewsHandlerService();
            var controller = new NewsController(newsHandler);
            var prefix = "Apple";
            var sort = SortBys.Relevancy;
            var language = Languages.EN;
            var year = 2022;
            var month = 5;
            var day = 10;
            var apiKey = "a8c7ec95885a493ea159cec18a7a45a1";

            // Act, Assert.
            Assert.ThrowsException<ThePastDateException>(() => newsHandler.HandleNewsListAsync(prefix, sort, language, year, month, day, apiKey));
        }

        [TestMethod]
        public void WhenServiceHandling_AndPrefixAndDateIsNotCorrect_ThrowException()
        {
            // Arange.
            var newsHandler = new NewsHandlerService();
            var controller = new NewsController(newsHandler);
            var prefix = "$@%&#*&@!#@$^&&%&*()";
            var sort = SortBys.Relevancy;
            var language = Languages.EN;
            var year = 2024;
            var month = 5;
            var day = 10;
            var apiKey = "a8c7ec95885a493ea159cec18a7a45a1";

            // Act, Assert.
            Assert.ThrowsException<TheNewsNotFoundException>(() => newsHandler.HandleNewsListAsync(prefix, sort, language, year, month, day, apiKey));
        }

    }
}