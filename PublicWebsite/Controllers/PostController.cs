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


        
        public async Task<IActionResult> Index()
        {
            var t = context.Terms.Where(x => x.TermId != null);

            List<Term> terms = new List<Term>();
            foreach (Term item in t)
            {
                int termId = item.TermId;
                Term Trm = context.Terms.First(p => p.TermId == termId);

                terms.Add(new Term
                {
                    TermId = Trm.TermId,
                    Name = Trm.Name,
                    Code = Trm.Code

                });
            }
            ViewBag.Term = terms;
            ViewBag.MyTerm = terms;
            var postTypes = await context.PostTypes.ToListAsync();
            ViewBag.postTypes = postTypes;

            var posts = await context.Posts.OrderByDescending(p => p.PostId)
                .Include(x => x.PostType)
                .Include(x => x.PostTerms)
                .ToListAsync();
            return View(posts);
        }

       
        public async Task<IActionResult> Details(int id)
        {
            Post post = await context.Posts.Include(x => x.PostType)
                .Include(x => x.PostTerms)
                .FirstOrDefaultAsync(x => x.PostId == id);
            if (post == null)
            {
                return NotFound();
            }
            var t = context.Terms.Where(x => x.TermId != null);
            List<Term> terms = new List<Term>();
            foreach (Term item in t)
            {
                int termId = item.TermId;
                Term Trm = context.Terms.First(p => p.TermId == termId);

                terms.Add(new Term
                {
                    TermId = Trm.TermId,
                    Name = Trm.Name,
                    Code = Trm.Code

                });
            }
            ViewBag.Term = terms;
            ViewBag.MyTerm = terms;
            var postTypes = await context.PostTypes.ToListAsync();
            ViewBag.postTypes = postTypes;

            return View(post);
        }


        public async Task<IActionResult> Terms(int id)
        {
            var t = context.Terms.
                Where(x => x.TermId != null);

            List<Term> terms = new List<Term>();
            foreach (Term item in t)
            {
                int termId = item.TermId;
                Term Trm = context.Terms.First(p => p.TermId == termId);

                terms.Add(new Term
                {
                    TermId = Trm.TermId,
                    Name = Trm.Name,
                    Code = Trm.Code

                });
            }
            ViewBag.Term = terms;
            var tt = context.Terms.
            Where(x => x.TermId == id);

            List<Term> tterms = new List<Term>();
            foreach (Term item in tt)
            {
                int termId = item.TermId;
                Term Trm = context.Terms.First(p => p.TermId == termId);

                tterms.Add(new Term
                {
                    TermId = Trm.TermId,
                    Name = Trm.Name,
                    Code = Trm.Code

                });
            }
            ViewBag.MyTerm = tterms;
            var postTypes = await context.PostTypes.ToListAsync();
            ViewBag.postTypes = postTypes;
            var posts = await context.PostTerms
                .Where(t => t.TermId == id)
                .Include(p => p.Post)
                .Include(pt => pt.Post.PostType)
                .Include(pt => pt.Post.PostTerms)
                .Select(x => new Post
                {
                    PostId = x.Post.PostId,
                    PostTitle = x.Post.PostTitle,
                    PostTypeId = x.Post.PostTypeId,
                    CreationDate = x.Post.CreationDate,
                    Details = x.Post.Details,
                    Summary = x.Post.Summary,
                    Image = x.Post.Image,
                    PostType = x.Post.PostType,
                    PostTerms = x.Post.PostTerms
                })
                .Where(p => p.PostTerms.Count() == 1)
                .OrderByDescending(p => p.PostId)
                .ToListAsync();


            return View("Index", posts);
        }


        // GET /admin/posts/posttypes/5
        public async Task<IActionResult> PostTypes(int id)
        {
            var t = context.Terms.Where(x => x.TermId != null);

            List<Term> terms = new List<Term>();
            foreach (Term item in t)
            {
                int termId = item.TermId;
                Term Trm = context.Terms.First(p => p.TermId == termId);

                terms.Add(new Term
                {
                    TermId = Trm.TermId,
                    Name = Trm.Name,
                    Code = Trm.Code

                });
            }
            ViewBag.Term = terms;
            ViewBag.MyTerm = terms;
            var postTypes = await context.PostTypes.ToListAsync();
            ViewBag.postTypes = postTypes;
            var posts = await context.Posts
                .Where(t => t.PostTypeId == id)
                .Include(pt => pt.PostType)
                .Include(x => x.PostTerms)
                .OrderByDescending(p => p.PostId).ToListAsync();

            return View("Index", posts);
        }
    }
}

        //public async Task<IActionResult> PostType(int id)
        //{

        //    var postType = await context.PostTypes.FindAsync(id);

        //    var postTypesTaxonomies = await context.TaxonomyPostTypes
        //        .Where(x => x.PostTypeId == id)
        //        .ToListAsync();

        //    List<int> taxonomiesId = new List<int>();

        //    foreach (var i in postTypesTaxonomies)
        //    {
        //        taxonomiesId.Add(i.TaxonomyId);
        //    }

        //    var terms = await context.Terms
        //        .Where(x => taxonomiesId.Contains(x.TaxonomyId))
        //        .ToListAsync();

        //    return View(new PostTypesViewModel()
        //    {
        //        posts = await context.Posts
        //        .Where(x => x.PostTypeId == id)
        //        .OrderByDescending(x => x.PostId)
        //        .ToListAsync(),
        //        postType = postType,
        //        terms = terms,
        //        postTypes = await context.PostTypes
        //        .OrderByDescending(x => x.PostTypeId)
        //        .ToListAsync(),
        //    });
        //}


      