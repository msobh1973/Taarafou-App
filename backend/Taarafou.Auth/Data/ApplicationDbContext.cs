using Microsoft.EntityFrameworkCore;
using Taarafou.Auth.Data.Entities;

namespace Taarafou.Auth.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
