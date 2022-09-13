
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Common
{
    public class CreatePostViewModel
    {
            public Taxonomy taxonomy { get; set; }
            public SelectList terms { get; set; }
        
    }
}
