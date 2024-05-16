using Microsoft.EntityFrameworkCore;
using MobileParkTestTask.Entities.FileNewsEntities;

namespace MobileParkTestTask.Data
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<NewsInfoFile> FileNews => Set<NewsInfoFile>();
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}