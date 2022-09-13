using dgPadCms.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dgPadCms.Models
{
    public class CreatePostViewModel
    {
        public Taxonomy taxonomy { get; set; }
        public SelectList terms { get; set; }
    }
}
