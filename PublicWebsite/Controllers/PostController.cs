using Microsoft.AspNetCore.Mvc;
using Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Common.Data;
using Common.Models;

namespace PublicWebsite.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext context;
        public PostController(ApplicationDbContext context)
        {
            this.context = context;
        }


        // GET /admin/posts
        public async Task<IActionResult> Index()
        {
            var posts = await context.Posts.OrderByDescending(p => p.PostId).Include(x => x.PostType).ToListAsync();
            return View(posts);
        }

        // GET /admin/posts/details/5
        public async Task<IActionResult> Details(int id)
        {
            Post post = await context.Posts.Include(x => x.PostType).Include(x => x.PostTerms).FirstOrDefaultAsync(x => x.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET /terms
        public async Task<IActionResult> Term()
        {

            var terms = await context.PostTerms.OrderByDescending(t => t.TermId).Include(t => t.PostId).ToListAsync();

            return View(terms);
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
