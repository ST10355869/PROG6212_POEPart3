// Kevin Muller
// ST10355869
// Gr 1

// References
//https://htmlcolorcodes.com/
//https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-8.0
//https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/models-data/displaying-a-table-of-database-data-cs
//https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/working-with-sql?view=aspnetcore-8.0&tabs=visual-studio

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using ST10355869_PROG6212_Part2.Data;
using ST10355869_PROG6212_Part2.Services;

namespace ST10355869_PROG6212_Part2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ReportService>();

            // Add Identity services
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            var app = builder.Build();

            // Seed roles and users
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await DatabaseSeeder.SeedRolesAndUsers(services);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
