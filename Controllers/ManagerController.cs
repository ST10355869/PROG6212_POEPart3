using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10355869_PROG6212_Part2.Data;

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
        public IActionResult Admin()
        {
            var claims = _context.Lecturers.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Admin(IFormCollection form, int id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer == null) return NotFound();

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

            return RedirectToAction(nameof(Index)); // Redirect to refresh the page
        }
    }
}
