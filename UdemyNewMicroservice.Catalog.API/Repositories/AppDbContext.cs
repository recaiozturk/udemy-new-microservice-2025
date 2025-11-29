using Microsoft.EntityFrameworkCore;
using UdemyNewMicroservice.Catalog.API.Features.Categories;
using UdemyNewMicroservice.Catalog.API.Features.Courses;

namespace UdemyNewMicroservice.Catalog.API.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly); //bu assemblydeki tüm entity configurationları uygula demek
        }
    }
}
