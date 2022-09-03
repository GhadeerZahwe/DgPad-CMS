using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dgPadCms.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
        
        public string Title { get; set; }
        public string Code { get; set; }
        [DisplayName("CreationDate")]
        public DateTime Date { get; set; }

        public string Detail { get; set; }
        [Required, MinLength(4, ErrorMessage = "Minimum length is 4")]
        public string Summary { get; set; }
        public string Image { get; set; }

        [Display(Name = "PostType")]
        [Range(1, int.MaxValue, ErrorMessage = "You must choose a PostType")]
        public int PostTypeId { get; set; }  
        
   

        [ForeignKey("PostTypeId")]
        public virtual PostType PostType { get; set; } 
        
        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }

        [Display(Name = "Term")]
        [Range(int.MinValue, 5, ErrorMessage = "You must choose a Term")]
        public virtual ICollection<PostTerm> PostTerms { get; set; }
    }
}
