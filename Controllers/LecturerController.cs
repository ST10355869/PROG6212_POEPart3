using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10355869_PROG6212_Part2.Data;
using ST10355869_PROG6212_Part2.Models;
using ST10355869_PROG6212_Part2.Controllers;

namespace ST10355869_PROG6212_Part2.Controllers
{
    public class LecturerController : Controller
    {
        private readonly AppDbContext _context;
        public LecturerController(AppDbContext context)
        {
            
            _context = context;
        }

        [HttpGet]
        public IActionResult SubmitClaim()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SubmitClaim(IFormCollection form, IFormFile fileUpload)
        {
            if (double.TryParse(form["hourlyRates"], out double hourlyRate) &&
                double.TryParse(form["hoursDone"], out double hoursWorked))
            {
                var lecturer = new LecturerModel
                {
                    HourlyRate = hourlyRate,
                    HoursWorked = hoursWorked,
                    AdditionalNotes = form["additionalNotes"],
                    ClaimStatus = "Pending"

                };

                if (fileUpload != null && fileUpload.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await fileUpload.CopyToAsync(memoryStream);
                        lecturer.DocumentContent = memoryStream.ToArray();
                        lecturer.DocumentFileName = fileUpload.FileName;

                    }
                }

                _context.Lecturers.Add(lecturer);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(SubmitClaim));
            }

            return View();
        }

        public IActionResult ClaimStatus()
        {
            var claims = _context.Lecturers.ToList();
            return View(claims);
        }

    }
}
