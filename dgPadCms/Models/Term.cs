using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dgPadCms.Models
{
    public class Term
    {
        public int Id { get; set; }
        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
        public string Name { get; set; }
        public string Code { get; set; }
        [MinLength(4, ErrorMessage = "Minimum length is 4")]
        public string Content { get; set; }
        public int Sorting { get; set; }

        [Display(Name = "Taxonomy")]
        [Range(1, int.MaxValue, ErrorMessage = "You must choose a taxonomy")]
        public int TaxonomyId { get; set; }

        [ForeignKey("TaxonomyId")]
        public virtual Taxonomy Taxonomy { get; set; }

     
        public virtual ICollection<PostTerm> PostTerms { get; set; }
    }
}
