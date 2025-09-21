using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShelfSpace_Razor.Models;

namespace ShelfSpace_Razor.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) :
            base(options) { }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
               new Category { Id = 1, Name = "Action", DisplayOrder = 10 },
               new Category { Id = 2, Name = "Rom Com", DisplayOrder = 20 },
               new Category { Id = 3, Name = "Horror", DisplayOrder = 30 }
            );
        }
    }
}
