using ST10355869_PROG6212_Part2.Models;
using System.Linq;

namespace ST10355869_PROG6212_Part2.Services
{
    public class EditLecturer
    {
        private readonly ApplicationDbContext _context;

        public EditLecturer(ApplicationDbContext context)
        {
            _context = context;
        }

        public LecturerModel GetLecturerById(int id)
        {
            return _context.Lecturers.FirstOrDefault(l => l.Id == id);
        }

        public void UpdateLecturer(LecturerModel lecturer)
        {
            var existingLecturer = _context.Lecturers.FirstOrDefault(l => l.Id == lecturer.Id);
            if (existingLecturer != null)
            {
                existingLecturer.Name = lecturer.Name;
                existingLecturer.ContactInfo = lecturer.ContactInfo;
                _context.SaveChanges();
            }
        }
    }
}
