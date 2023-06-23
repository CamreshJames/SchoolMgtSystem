using System.ComponentModel.DataAnnotations;
namespace SchoolMS.Models
{
    public class NoticeBoard
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Receiver { get; set; }

        [Required]
        public string Sender { get; set; }
    }


}
