using System;
using Microsoft.EntityFrameworkCore;

namespace Taarafou.Posts
{
    public class PostsContext : DbContext
    {
        public PostsContext(DbContextOptions<PostsContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; } = null!;
    }

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
