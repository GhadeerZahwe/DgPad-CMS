﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Taxonomy
    {
        public int TaxonomyId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Code { get; set; }

        public ICollection<Term> Terms { get; set; }

        public ICollection<TaxonomyPostType> TaxonomyPostTypes { get; set; }

    }
}
