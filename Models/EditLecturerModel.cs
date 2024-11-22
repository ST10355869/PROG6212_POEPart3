using System.ComponentModel.DataAnnotations;

namespace ST10355869_PROG6212_Part2.Models
{
    public class EditLecturerModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Phone]
        public string ContactNumber { get; set; }
    }
}
