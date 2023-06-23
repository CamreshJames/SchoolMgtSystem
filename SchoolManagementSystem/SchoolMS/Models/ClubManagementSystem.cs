using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolMS.Models
{
    public class Club
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClubId { get; set; }

        [Required(ErrorMessage = "Club name is required.")]
        public string Name { get; set; }

        [Display(Name = "Number of Members")]
        public int NumberOfMembers { get; set; }

        public string Patron { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Display(Name = "Chair Person")]
        public string ChairPerson { get; set; }
    }

    public class ClubMember
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClubMemberId { get; set; }

        [Required(ErrorMessage = "Student name is required.")]
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string ClubName { get; set; }
    }
}
