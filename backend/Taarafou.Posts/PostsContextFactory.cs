using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Taarafou.Posts
{
    public class PostsContextFactory : IDesignTimeDbContextFactory<PostsContext>
    {
        public PostsContext CreateDbContext(string[] args)
        {
            // حاول أولاً PostsConnection (إذا أضفته يدوياً)، ثم SQLAZURECONNSTR_PostsConnection
            var connectionString =
                Environment.GetEnvironmentVariable("PostsConnection")
                ?? Environment.GetEnvironmentVariable("SQLAZURECONNSTR_PostsConnection")
                ?? throw new InvalidOperationException(
                       "Connection string not set: 'PostsConnection' أو 'SQLAZURECONNSTR_PostsConnection'");

            var optionsBuilder = new DbContextOptionsBuilder<PostsContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new PostsContext(optionsBuilder.Options);
        }
    }
}
