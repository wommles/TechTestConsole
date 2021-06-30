using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechTestConsole.Data.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(20)]
        public string Postcode { get; set; }

        public ICollection<Subject> Subjects { get; set; }
        public ICollection<ExamResult> ExamResults { get; set; }
    }
}
