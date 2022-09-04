﻿using System.Collections.Generic;
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
        [Required(ErrorMessage = "You must choose a taxonomy")]
        public ICollection<TaxonomyPostType> TaxonomyPostTypes { get; set; } = new HashSet<TaxonomyPostType>();


    }
}
