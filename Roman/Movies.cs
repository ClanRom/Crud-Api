using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Roman
{
    public class Movies
    {
        public int Id { get; set; }
        [MaxLength(1000)]
        [Required]
        public string Title { get; set; }
        [MaxLength(5000)]
        [Required]
        public string Description { get; set; }
    }

    public class AppDbContext : DbContext
    {
        public DbSet<Movies> Movies { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
