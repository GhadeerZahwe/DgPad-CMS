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
            var t = context.Terms.Where(x => x.TermId != null);
            List<PostTerm> postterms = new List<PostTerm>();
            List<Term> terms = new List<Term>();
            foreach (Term item in t)
            {
                int termId = item.TermId;
                Term Trm= context.Terms.First(p => p.TermId == termId);

                terms.Add(new Term
             {
                  TermId = Trm.TermId,
                  Name=Trm.Name,
                  Code=Trm.Code

                     });
            }
            ViewBag.Term = terms;

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

        //// GET /terms
        //public async Task<IActionResult> Term(int? TermId = null)
        //{
        //    ViewBag.TermId = TermId;
        //    if (!TermId.HasValue)
        //    {
        //        ViewBag.PostTerms = await context.PostTerms.ToListAsync();
        //        return View();
        //    }
        //    var post = await context.Posts.FindAsync(TermId);
        //    ViewBag.Post = post;
        //    //var terms = await context.PostTerms.OrderByDescending(t => t.TermId).Include(t => t.PostId).ToListAsync();

        //    return View();
        //}

        ////Just List Posts
        //public async Task<IActionResult> Index()
        //{
        //    var postype_tar = services.CheckPostType();
        //    var target_tar = services.CheckAllTerm();
        //    List<PostType> ptypes = new List<PostType>();


        //    foreach (PostType i in postype_tar)
        //    {
        //        int pid = i.PostTypeId;

        //        PostType ptype = services.getPostTypeId(pid);

        //        ptypes.Add(new PostType
        //        {
        //            Title = ptype.Title,
        //            PostTypeId = ptype.PostTypeId,
        //            TaxonomyId = ptype.TaxonomyId

        //        });

        //    }
        //    ViewBag.Ptypes = ptypes;


        //    var pos = services.OrderPost();

        //    return View(pos.ToList());


        //}

//        //Inviews
//        Foreach(PostType pp in ViewBag.PostType)
//        {
//            Pp.Name;
//            Pp.Id
//}

    }
}
