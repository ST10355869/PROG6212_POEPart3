﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10355869_PROG6212_Part2.Data;
using ST10355869_PROG6212_Part2.Models;
using ST10355869_PROG6212_Part2.Services;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ST10355869_PROG6212_Part2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManagerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ReportService _reportService;
        private readonly EditLecturer _editLecturerService;

        public ManagerController(AppDbContext context, ReportService reportService, EditLecturer editLecturerService)
        {
            _context = context;
            _reportService = reportService;
            _editLecturerService = editLecturerService;
        }
        [HttpGet]
        public async Task<IActionResult> EditLecturer(int id)
        {
            var lecturer = await _editLecturerService.GetByIdAsync(id);
            if (lecturer == null)
            {
                return NotFound();
            }
            return View(lecturer);
        }

        [HttpPost]
        public async Task<IActionResult> EditLecturer(EditLecturerModel lecturer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _editLecturerService.UpdateAsync(lecturer);
                    TempData["SuccessMessage"] = "Lecturer details updated successfully!";
                    return RedirectToAction(nameof(Admin)); // Redirect to Admin page or another suitable page
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the lecturer.");
                }
            }
            return View(lecturer);
        }




        [HttpGet]
        public async Task<IActionResult> GenerateReport()
        {
            var reportBytes = await _reportService.GenerateApprovedClaimsReport();
            return File(reportBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ApprovedClaimsReport.xlsx");
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
