using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ST10355869_PROG6212_Part2.Models;

namespace ST10355869_PROG6212_Part2.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<LecturerModel> Lecturers { get; set; }
        public DbSet<EditLecturerModel> EditLecturerModels { get; set; }
    }
}
