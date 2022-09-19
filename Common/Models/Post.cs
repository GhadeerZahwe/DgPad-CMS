using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Infrastructure;

namespace Common.Models
{
    public class Post
    {
        public int PostId { get; set; }

        public string PostTitle { get; set; }

        public int PostTypeId { get; set; }

        public string CreationDate { get; set; }

        public string Details { get; set; }

        public string Summary { get; set; }

        public string Image { get; set; }

        public PostType PostType { get; set; }

        public ICollection<PostTerm> PostTerms { get; set; }

        [NotMapped]
        [FileExtention]
        public IFormFile ImageUpload { get; set; }

    }
}
