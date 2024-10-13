using Microsoft.EntityFrameworkCore;
using ST10355869_PROG6212_Part2.Models;

namespace ST10355869_PROG6212_Part2.Data
{
    public class AppDbContext : DbContext
    {


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Lecturer> Lecturers { get; set; }

    }
}
