using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Taarafou.Posts
{
    public class PostsContextFactory : IDesignTimeDbContextFactory<PostsContext>
    {
        public PostsContext CreateDbContext(string[] args)
        {
            // خذ سلسلة الاتصال من متغيّر البيئة PostsConnection
            var connectionString = Environment.GetEnvironmentVariable("PostsConnection")
                                   ?? throw new InvalidOperationException("PostsConnection not set");

            var optionsBuilder = new DbContextOptionsBuilder<PostsContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new PostsContext(optionsBuilder.Options);
        }
    }
}
