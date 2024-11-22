using ST10355869_PROG6212_Part2.Models;
using ST10355869_PROG6212_Part2.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ST10355869_PROG6212_Part2.Services
{
    public class EditLecturer
    {
        private readonly AppDbContext _context;

        public EditLecturer(AppDbContext context)
        {
            _context = context;
        }

        // Get all lecturers for editing
        public Task<List<EditLecturerModel>> GetAllAsync()
        {
            return _context.EditLecturerModels.ToListAsync(); // This fetches from the EditLecturerModels table
        }

        // Get a lecturer by ID for editing
        public Task<EditLecturerModel> GetByIdAsync(int id)
        {
            return _context.EditLecturerModels
                .Where(l => l.Id == id)
                .FirstOrDefaultAsync();
        }

        // Update lecturer data in the EditLecturerModels table
        public async Task UpdateAsync(EditLecturerModel updatedLecturer)
        {
            var lecturer = await _context.EditLecturerModels.FindAsync(updatedLecturer.Id);
            if (lecturer != null)
            {
                lecturer.Name = updatedLecturer.Name;
                lecturer.Surname = updatedLecturer.Surname;
                lecturer.Address = updatedLecturer.Address;
                lecturer.ContactNumber = updatedLecturer.ContactNumber;

                _context.EditLecturerModels.Update(lecturer);
                await _context.SaveChangesAsync();
            }
        }

        // Add a new lecturer to the EditLecturerModels table
        public async Task AddAsync(EditLecturerModel newLecturer)
        {
            _context.EditLecturerModels.Add(newLecturer);
            await _context.SaveChangesAsync();
        }

        // Seed initial data to the EditLecturerModels table
        public async Task SeedEditLecturerModelsAsync()
        {
            if (!_context.EditLecturerModels.Any())
            {
                var initialLecturers = new List<EditLecturerModel>
                {
                    new EditLecturerModel {Name = "John", Surname = "Doe", Address = "123 Main St", ContactNumber = "555-1234" },
                };

                _context.EditLecturerModels.AddRange(initialLecturers);
                await _context.SaveChangesAsync();
            }
        }
    }
}
