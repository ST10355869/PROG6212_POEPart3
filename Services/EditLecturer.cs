using ST10355869_PROG6212_Part2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ST10355869_PROG6212_Part2.Services
{
    public class EditLecturer
    {
        private readonly List<EditLecturerModel> _testLecturers;

        public EditLecturer()
        {
            // Initialize with some test data
            _testLecturers = new List<EditLecturerModel>
            {
                new EditLecturerModel { Id = 1, Name = "John", Surname = "Doe", Address = "123 Main St", ContactNumber = "555-1234" },
            };
        }

        public Task<List<EditLecturerModel>> GetAllAsync()
        {
            return Task.FromResult(_testLecturers);
        }

        public Task<EditLecturerModel> GetByIdAsync(int id)
        {
            var lecturer = _testLecturers.FirstOrDefault(l => l.Id == id);
            return Task.FromResult(lecturer);
        }

        public Task UpdateAsync(EditLecturerModel updatedLecturer)
        {
            var lecturer = _testLecturers.FirstOrDefault(l => l.Id == updatedLecturer.Id);
            if (lecturer != null)
            {
                lecturer.Name = updatedLecturer.Name;
                lecturer.Surname = updatedLecturer.Surname;
                lecturer.Address = updatedLecturer.Address;
                lecturer.ContactNumber = updatedLecturer.ContactNumber;
            }
            return Task.CompletedTask;
        }
    }
}