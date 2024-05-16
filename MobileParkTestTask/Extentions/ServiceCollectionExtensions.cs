using MobileParkTestTask.Services.News;

namespace MobileParkTestTask.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCategoryCrudServices(this IServiceCollection services)
        {
            services.AddTransient<NewsHandlerService>();

            return services;
        }
    }
}