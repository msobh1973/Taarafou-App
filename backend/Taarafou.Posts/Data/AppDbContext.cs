using Microsoft.EntityFrameworkCore;

namespace Taarafou.Posts.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<Post> Posts => Set<Post>();
    }
}
