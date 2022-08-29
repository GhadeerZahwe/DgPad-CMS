using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dgPadCms.Models
{
    public class PostType
    {
        public int Id { get; set; }
        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
        public string Title { get; set; }
        public string Code { get; set; }

        [Display(Name = "Taxonomy")]
        [Range(1, int.MaxValue, ErrorMessage = "You must choose a taxonomy")]
        public int TaxonomyId { get; set; }


        [ForeignKey("TaxonomyId")]
        public virtual Taxonomy Taxonomy { get; set; }

     
    }
}
