namespace MobileParkTestTask.Services.NewsService
{
    public static class SubstringHandlerService
    {
        public static string GetFirstMentionOfPrefix(string text, string prefix)
        {
            try
            {
                return text.Substring(text.IndexOf(prefix, 0));
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}