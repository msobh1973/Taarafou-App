using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Taarafou.Posts;   // تأكد أن مساحة الاسم هنا تطابق مساحة اسم PostsContext

namespace Taarafou.Posts   // يمكنك إزالة "Controllers" من المسار لو شئت
{
    /// <summary>
    /// مصنع تصميمي لإنشاء DbContext لاستخدام EF Core CLI (الهجرات).
    /// </summary>
    public class PostsContextFactory : IDesignTimeDbContextFactory<PostsContext>
    {
        public PostsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PostsContext>();
            
            // استخدم SQLite هنا بدلاً من In-Memory
            optionsBuilder.UseSqlite("Data Source=posts.db");

            return new PostsContext(optionsBuilder.Options);
        }
    }
}
