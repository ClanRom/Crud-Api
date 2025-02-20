using Microsoft.EntityFrameworkCore;

namespace Roman
{
    public class Movies
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class AppDbContext : DbContext
    {
        public DbSet<Movies> Movies { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
