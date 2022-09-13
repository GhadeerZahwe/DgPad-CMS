using Microsoft.AspNetCore.Mvc;
using Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace PublicWebsite.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext context;
        public PostController(ApplicationDbContext context)
        {
            this.context = context;

        }
        ////Just List Posts
        //public IActionResult Index()
        //{
        //    var postype_tar = context.CheckPostType();
        //    var target_tar = context.CheckAllTerm();
        //    List<PostType> ptypes = new List<PostType>();


        //    foreach (PostType i in postype_tar)
        //    {
        //        int pid = i.Id;

        //        PostType ptype = context.getPostTypeId(pid);

        //        ptypes.Add(new PostType
        //        {
        //            Title = ptype.Title,
        //            TaxonomyPostTypes = ptype.TaxonomyPostTypes

        //        });

        //    }
        //    ViewBag.Ptypes = ptypes;


        //    var pos = context.OrderPost();

        //    return View(pos.ToList());


        //}
        //public IActionResult Index(int id)
        //{
        //    PostType p = context.PostType.Find(id);
        //    ViewBag.PostType = p.Title;
        //    ViewData["PostType"] = p.Title;
        //    var result = context.Posts.Where(x => x.PostTypeId == id).OrderByDescending(x => x.Date).ToList();
        //    return View(result);
        //    //var result = context.PostType.OrderByDescending(x => x.Id)(x => x.Sorting).ToListAsync());
        //    //return View(result);
        //}

    }
}
