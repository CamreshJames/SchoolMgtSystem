using System.ComponentModel.DataAnnotations;

namespace SchoolMS.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string StudyProgram { get; set; }

        public string StudentId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string GuardianName { get; set; }

        [Required]
        public string GuardianPhone { get; set; }

        [Required]
        public string Nationality { get; set; }

        [Required]
        public string NationalId { get; set; }

        [Required]
        public string County { get; set; }

        [Required]
        public string Address { get; set; }

        public string ProfileImage { get; set; }

        public string Password { get; set; }
    }

}
