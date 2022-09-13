using System;
using System.Collections.Generic;

namespace dgPadCms.Models
{
    public class Post
    {
        public int PostId { get; set; }

        public string PostTitle { get; set; }

        public int PostTypeId { get; set; }

        public string CreationDate { get; set; }

        public string Details { get; set; }

        public string Summary { get; set; }

        public PostType PostType { get; set; }

        public ICollection<PostTerm> PostTerms { get; set; }

    }
}
