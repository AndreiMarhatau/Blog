using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Blog.Models
{
    public class Db:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Token> Tokens { get; set; }


        public Db(DbContextOptions<Db> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
