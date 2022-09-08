
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common
{
    public class CreatePostTypeViewModel
    {
        public int Id { get; set; }
        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
        public string Title { get; set; }
        public string Code { get; set; }

        public IList<SelectListItem> Taxonomies { get; set; }
    }
}
