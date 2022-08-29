using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dgPadCms.Models
{
    public class PostTerm
    {
        public int Id { get; set; }

        [Display(Name = "Post")]
        [Range(1, int.MaxValue, ErrorMessage = "You must choose a post")]
        public int PostId { get; set; }


        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }


        [Display(Name = "Term")]
        [Range(1, int.MaxValue, ErrorMessage = "You must choose a term")]
        public int TermId { get; set; }


        [ForeignKey("TermId")]
        public virtual Term Term { get; set; }
    }
}
