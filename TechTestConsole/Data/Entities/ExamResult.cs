using System.ComponentModel.DataAnnotations;

namespace TechTestConsole.Data.Entities
{
    public class ExamResult
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }

        [Required]
        public double Grade { get; set; }
        public double? AdjustedGrade { get; set; }

        public Student Student { get; set; }
        public Subject Subject { get; set; }
    }
}
