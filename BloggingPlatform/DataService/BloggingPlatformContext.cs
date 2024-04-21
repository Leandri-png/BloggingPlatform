using Microsoft.EntityFrameworkCore;
using BloggingPlatform.DataService.Models;

namespace BloggingPlatform.DataService
{
    public class BloggingPlatformContext : DbContext
    {
        public BloggingPlatformContext(DbContextOptions<BloggingPlatformContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Post { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customize model if needed
        }
    }
}
