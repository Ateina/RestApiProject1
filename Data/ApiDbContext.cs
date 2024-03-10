using Microsoft.EntityFrameworkCore;
using WebApplicationTestProject1.Models;

namespace WebApplicationTestProject1.Data
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=TestApiCourseDb;");
        }
    }
}
