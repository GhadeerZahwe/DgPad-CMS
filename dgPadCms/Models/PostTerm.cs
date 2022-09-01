using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dgPadCms.Models
{
    public class PostTerm
    {
        [Display(Name = "Post")]
        [Range(1, int.MaxValue, ErrorMessage = "You must choose a post")]
        public int PostId { get; set; }

        [ForeignKey("PostId")]
        public Post Post { get; set; }

        [Display(Name = "Term")]
        [Range(1, int.MaxValue, ErrorMessage = "You must choose a term")]
        public int TermId { get; set; }

        [ForeignKey("TermId")]
        public Term Term { get; set; }

    }
}
