﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Common.Models
{
    public class Term
    {
        public int TermId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Code { get; set; }

        [Required]
        [Display(Name = "Taxonomy")]
        public int TaxonomyId { get; set; }

        public Taxonomy Taxonomy { get; set; }
        public ICollection<PostTerm> PostTerms { get; set; }

    }
}
