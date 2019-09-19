using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }


        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DatabaseContext() { }
    }
}
