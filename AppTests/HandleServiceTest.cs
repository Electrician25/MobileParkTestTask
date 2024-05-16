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
            var apiKey = "3ee625bc20ed43439d9f97d2815e44b5";

            // Act.
            var list = newsHandler.HandleNewsListAsync(prefix, sort, language, year, month, day, apiKey, 0);
            var result = list.Count >= 1;

            // Assert.
            Assert.AreEqual(true, result);
        }


        [TestMethod]
        public void WhenServiceHandling_AndDateIsNotCorrect_ThrowException()
        {
            // Arange.
            var newsHandler = new NewsHandlerService();
            var prefix = "Apple";
            var sort = SortBys.Relevancy;
            var language = Languages.EN;
            var year = 2022;
            var month = 5;
            var day = 10;
            var apiKey = "3ee625bc20ed43439d9f97d2815e44b5";

            // Act, Assert.
            Assert.ThrowsException<TheNewsNotFoundException>(() => newsHandler.HandleNewsListAsync(prefix, sort, language, year, month, day, apiKey, 0));
        }

        [TestMethod]
        public void WhenServiceHandling_AndPrefixAndDateIsNotCorrect_ThrowException()
        {
            // Arange.
            var newsHandler = new NewsHandlerService();
            var prefix = "$@%&#*&@!#@$^&&%&*()";
            var sort = SortBys.Relevancy;
            var language = Languages.EN;
            var year = 2024;
            var month = 5;
            var day = 10;
            var apiKey = "3ee625bc20ed43439d9f97d2815e44b5";

            // Act, Assert.
            Assert.ThrowsException<TheNewsNotFoundException>(() => newsHandler.HandleNewsListAsync(prefix, sort, language, year, month, day, apiKey, 0));
        }
    }
}