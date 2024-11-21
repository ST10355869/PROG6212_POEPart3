using System.ComponentModel.DataAnnotations;

namespace ST10355869_PROG6212_Part2.Models
{
    public class LecturerModel
    {
        public int Id { get; set; }

        [Required]
        public double HoursWorked { get; set; }

        [Required]
        public double HourlyRate {  get; set; }

        public string? AdditionalNotes { get; set; }
         public byte[]? DocumentContent { get; set; }
        public string? DocumentFileName { get; set; }
        public string? DocumentFileType { get; set; }
        public string ClaimStatus { get; set; } 
        public double finalPayment { get; set; }
    }
}
