using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10355869_PROG6212_Part2.Data;
using ST10355869_PROG6212_Part2.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ST10355869_PROG6212_Part2.Controllers
{
    public class ManagerController : Controller
    {
        private readonly AppDbContext _context;

        public ManagerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Admin()
        {
            var claims = await _context.Lecturers.ToListAsync();

            foreach (var claim in claims)
            {
                if (claim.HoursWorked >= 1 && claim.HoursWorked <= 50 
                    && claim.HourlyRate >= 1 && claim.HourlyRate <= 100 
                    && (claim.HoursWorked * claim.HourlyRate) <= 5000)
                {
                    claim.ClaimStatus = "Approved";
                }
                else
                {
                    claim.ClaimStatus = "Rejected";
                }

                _context.Lecturers.Update(claim);
            }

            await _context.SaveChangesAsync();

            return View(claims);
        }

        [HttpPost]
        public async Task<IActionResult> Admin(IFormCollection form, int id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer == null) return NotFound();

            string fileType = lecturer.DocumentFileType ?? "N/A";

            if (form.ContainsKey("Approvebtn"))
            {
                lecturer.ClaimStatus = "Approved";
            }
            else if (form.ContainsKey("Rejectbtn"))
            {
                lecturer.ClaimStatus = "Rejected";
            }

            _context.Lecturers.Update(lecturer);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Admin)); // Redirect to refresh the page
        }

        public async Task<IActionResult> DownloadDocument(int id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer == null || lecturer.DocumentContent == null)
            {
                return NotFound();
            }

            return File(lecturer.DocumentContent, lecturer.DocumentFileType, lecturer.DocumentFileName);
        }
    }
}
