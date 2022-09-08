
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Common
{
    public class CreatePostViewModel
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
       
        public virtual IList<SelectListItem> Terms { get; set; }
    }
}
