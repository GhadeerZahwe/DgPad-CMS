﻿namespace Common.Models
{
    public class PostTerm
    {
        public int TermId { get; set; }
        public int PostId { get; set; }

        public Term Term { get; set; }
        public Post Post { get; set; }
    }
}
