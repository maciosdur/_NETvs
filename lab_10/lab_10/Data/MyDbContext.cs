using lab_10.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace lab_10.Data
{
    // Klasa dziedziczy po DbContext - to daje nam całą magię EF
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        // Te właściwości reprezentują tabele w bazie danych
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }

        // Tutaj możemy dodatkowo skonfigurować bazę (np. wartości początkowe)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Przykład: Dodanie kilku kategorii na start (Seed Data)
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Elektronika" },
                new Category { Id = 2, Name = "Spożywcze" },
                new Category { Id = 3, Name = "Książki" },
                new Category { Id = 4, Name = "Zabawki" }
            );
        }
    }
}