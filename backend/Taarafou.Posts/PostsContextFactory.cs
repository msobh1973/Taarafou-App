using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Taarafou.Posts;  // مساحة الاسم التي تضم PostsContext

namespace Taarafou.Posts.Controllers
{
    // هذا المصنع يُستخدم من EF Core CLI لإنشاء DbContext في وقت التصميم (الهجرات)
    public class PostsContextFactory : IDesignTimeDbContextFactory<PostsContext>
    {
        public PostsContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<PostsContext>()
                .UseSqlite("Data Source=posts.db")
                .Options;

            return new PostsContext(options);
        }
    }
}
