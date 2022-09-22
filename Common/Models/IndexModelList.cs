using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class IndexModelList
    {
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Term> Terms { get; set; }

        public IndexModelList() { }
    }
}
