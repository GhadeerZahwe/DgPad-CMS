using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{

    public class ContactUS
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        [StringLength(15)]
        [DisplayName("Your Name")]
        public string Name { get; set; }
        [Required]
        public string Message { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
    }
}
