using System.ComponentModel.DataAnnotations;

namespace SchoolMS.Models
{
    public class Admin
    {
        [Key] public int Id { get; set; }

        [Required] public string Name { get; set; }
        [Required] public string Username { get; set; }
        [Required] public string Email { get; set; }
        [Required] public Int64 Phone { get; set; }
        [Required] public string Password { get; set; }
    }
}
