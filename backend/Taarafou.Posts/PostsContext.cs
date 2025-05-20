using Microsoft.EntityFrameworkCore;

namespace Taarafou.Posts
{
    public class PostsContext : DbContext
    {
        public PostsContext(DbContextOptions<PostsContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
    }
}
