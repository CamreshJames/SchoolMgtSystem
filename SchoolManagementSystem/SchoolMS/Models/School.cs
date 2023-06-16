using System.ComponentModel.DataAnnotations;

namespace SchoolMS.Models
{
    public class School
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string ChairPerson { get; set; }
    }


}
