using Common.Models;
using System.Collections.Generic;

namespace dgPadPosts.Models
{
    public class PostsOfPostTypes
    {
        public PostType postType { get; set; }
        public List<Post> posts { get; set; }
    }
}
