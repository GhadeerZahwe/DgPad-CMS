using Common.Models;
using System.Collections.Generic;

namespace dgPadPosts.Models
{
    public class IndexViewModel
    {
        public List<Post> carousel { get; set; }
        public List<Post> posts { get; set; }
        public List<PostsOfPostTypes> postsOfPostTypes { get; set; }
    }
}
