
using System.Collections.Generic;
using System.Web.Mvc;

namespace Common.Models.ViewModels
{
    public class CreatePostViewModel
    {
        public Taxonomy taxonomy { get; set; }
        public SelectList terms { get; set; }
    }
}
