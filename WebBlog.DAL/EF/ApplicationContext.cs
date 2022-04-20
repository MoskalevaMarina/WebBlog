using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.DAL.Entities;

namespace WebBlog.DAL.EF
{
  public  class ApplicationContext : DbContext
    {
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Comment> Comments => Set<Comment>();

        public ApplicationContext(DbContextOptions ff) : base(ff)
        {
        }
        // => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=.\\bd.db");
        }
    }

}
