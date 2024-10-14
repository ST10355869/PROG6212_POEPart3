using Microsoft.AspNetCore.Mvc;
using ST10355869_PROG6212_Part2.Models;
using ST10355869_PROG6212_Part2.Data;
using System.Diagnostics;

namespace ST10355869_PROG6212_Part2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
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
                    AdditionalNotes = form["additionalNotes"]

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

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}