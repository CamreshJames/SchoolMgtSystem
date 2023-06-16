using System.ComponentModel.DataAnnotations;

namespace SchoolMS.Models
{
    public class PendingLecturer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string School { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public List<string> Qualifications { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Specialization { get; set; }

        [Required]
        public List<string> CoursesTaught { get; set; }

        [Required]
        public string OfficeLocation { get; set; }

        [Required]
        public string OfficeHours { get; set; }

        [Required]
        public string Nationality { get; set; }
        
        [Required]
        public string NationalId{ get; set; }
    }

}
