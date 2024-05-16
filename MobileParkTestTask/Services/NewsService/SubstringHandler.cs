namespace MobileParkTestTask.Services.NewsService
{
    public static class SubstringHandler
    {
        public static string GetFirstMentionOfPrefix(string text, string index)
        {
            try
            {
                return text.Substring(text.IndexOf(index, 0));
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}