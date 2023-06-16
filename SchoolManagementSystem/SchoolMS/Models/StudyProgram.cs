

using System.ComponentModel.DataAnnotations;

namespace SchoolMS.Models
{
    public class StudyProgram
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        public string Description { get; set; }

        public string Coordinator { get; set; }

        public string Duration { get; set; }

        public string Requirements { get; set; }

        public string Prerequisite { get; set; }

        public string DegreeType { get; set; }

        [Required]
        public string School { get; set; }
    }

}
