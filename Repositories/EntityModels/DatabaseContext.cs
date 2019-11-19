using Microsoft.EntityFrameworkCore;

namespace EntityModels
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<Like> Likes { get; set; }


        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { Database.EnsureCreated(); }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex("Login", "Email", "Name", "Surname");
            builder.Entity<Token>()
                .HasIndex("StrToken");
            builder.Entity<Post>()
                .HasIndex("AuthorId");
            builder.Entity<Comment>()
                .HasIndex("AuthorId");
        }

        public DatabaseContext() { }
    }
}