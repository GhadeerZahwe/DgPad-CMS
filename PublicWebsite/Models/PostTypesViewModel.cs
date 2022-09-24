using Common.Models;
using System.Collections.Generic;

namespace dgPadPosts.Models
{
    public class PostTypesViewModel
    {
        public List<Post> posts { get; set; }
        public List<PostType> postTypes { get; set; }
        public List<Term> terms { get; set; }
        public PostType postType { get; set; }
    }
}
