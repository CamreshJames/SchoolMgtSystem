using System.ComponentModel.DataAnnotations;

namespace SchoolMS.Models
{
    public class Courses
    {
        // Properties for the Course entity
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public List<string> Prerequisite { get; set; }

        [Required]
        public string Lecturer { get; set; }

        [Required]
        public string LecturerUserName { get;}

        [Required]
        public string ProgramName { get; set; }
    }
}
