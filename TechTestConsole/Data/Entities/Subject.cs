using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechTestConsole.Data.Entities
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
