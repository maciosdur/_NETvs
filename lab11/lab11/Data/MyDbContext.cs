using lab11.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; 
using Microsoft.AspNetCore.Identity; 

namespace lab11.Data
{

    public class MyDbContext : IdentityDbContext<IdentityUser>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Elektronika" },
                new Category { Id = 2, Name = "Spożywcze" },
                new Category { Id = 3, Name = "Książki" },
                new Category { Id = 4, Name = "Zabawki" }
            );

            modelBuilder.Entity<Article>()
                .Property(a => a.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}