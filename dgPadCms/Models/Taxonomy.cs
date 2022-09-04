using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dgPadCms.Models
{
    public class Taxonomy
    {
        public int Id { get; set; }
        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
        [RegularExpression(@"^[a-zA-Z-]+$", ErrorMessage = "Only letters are allowed")]
        public string Name { get; set; }
        public string Code { get; set; }
        public int Sorting { get; set; }

        public ICollection<TaxonomyPostType> TaxonomyPostTypes { get; set; }

    }
}
