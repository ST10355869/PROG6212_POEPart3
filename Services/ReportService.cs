using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using ST10355869_PROG6212_Part2.Data;
using ST10355869_PROG6212_Part2.Models;

namespace ST10355869_PROG6212_Part2.Services
{
    public class ReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<byte[]> GenerateApprovedClaimsReport()
        {
            var approvedClaims = await _context.Lecturers
                .Where(l => l.ClaimStatus == "Approved")
                .ToListAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Approved Claims");

                worksheet.Cells[1, 1].Value = "Lecturer ID";
                worksheet.Cells[1, 2].Value = "Total Payment";

                for (int i = 0; i < approvedClaims.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = approvedClaims[i].Id;
                    worksheet.Cells[i + 2, 2].Value = approvedClaims[i].finalPayment;
                }

                return package.GetAsByteArray();
            }
        }
    }
}
