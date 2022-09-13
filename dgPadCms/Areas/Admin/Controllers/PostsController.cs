using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace dgPadCms.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public PostsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET /admin/posts
        // GET /admin/posts
        public async Task<IActionResult> Index()
        {
            var posts = await context.Posts.OrderByDescending(p => p.Id).Include(x => x.PostType).ToListAsync();
            return View(posts);
        }

        // GET /admin/posts/create
        public async Task<ActionResult> Create(int? postTypeId = null)
        {
            ViewBag.PostTypeId = postTypeId;
            if (!postTypeId.HasValue)
            {
                ViewBag.PostType = await context.PostType.ToListAsync();
                return View();
            }

            var postType = await context.PostType.FindAsync(postTypeId);
            ViewBag.PostType = postType;

            var postTypesTaxonomies = await context.TaxonomyPostTypes
                .Where(x => x.PostTypeId == postTypeId)
                .ToListAsync();

            List<int> taxonomiesId = new List<int>();

            foreach (var i in postTypesTaxonomies)
            {
                taxonomiesId.Add(i.TaxonomyId);
            }

            var terms = await context.Terms
                .Where(x => taxonomiesId.Contains(x.TaxonomyId))
                .ToListAsync();

            ViewBag.Terms = terms;


            return View();
        }

      
        // POST /admin/posts/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post, List<int> termIdList)
        {
           
            foreach (var id in termIdList)
            {
                PostTerm postTerm = new PostTerm()
                {
                    TermId = id,
                    PostId = post.Id,
                };

                context.Add(postTerm);
            }
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // POST /admin/posts/create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Post post)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        post.Code = post.Title.ToLower().Replace(" ", "-");

        //        var code = await context.Posts.FirstOrDefaultAsync(x => x.Code == post.Code);
        //        if (code != null)
        //        {
        //            ModelState.AddModelError("", "The post already exists.");
        //            return View(post);
        //        }

        //        //foreach (var item in )
        //        //    post.PostTerms.Add(new PostTerm()
        //        //    {
        //        //        TermId = int.Parse(item)
        //        //    }) ;

        //        string imageName = "noimage.png";
        //        if (post.ImageUpload != null)
        //        {
        //            string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "media/posts");
        //            imageName = Guid.NewGuid().ToString() + "_" + post.ImageUpload.FileName;
        //            string filePath = Path.Combine(uploadsDir, imageName);
        //            FileStream fs = new FileStream(filePath, FileMode.Create);
        //            await post.ImageUpload.CopyToAsync(fs);
        //            fs.Close();
        //        }

        //        post.Image = imageName;

        //        context.Add(post);
        //        await context.SaveChangesAsync();

        //        TempData["Success"] = "The post has been added!";

        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.PostTypeId = new SelectList(context.PostType, "Id", "Title");
        //    ViewBag.TermId = new SelectList(context.Terms, "Id", "Name");

        //    return View(post);
        //}
        // GET /admin/posts/edit/id
        public async Task<ActionResult> Edit(int postId, int postTypeId)
        {

            var post = await context.Posts.Include(x => x.PostType).FirstOrDefaultAsync(x => x.Id == postId);
            ViewBag.PostType = await context.PostType.FindAsync(post.PostTypeId);

            var postTypesTaxonomies = await context.TaxonomyPostTypes
                .Where(x => x.PostTypeId == postId)
                .ToListAsync();

            List<int> taxonomiesId = new List<int>();

            foreach (var i in postTypesTaxonomies)
            {
                taxonomiesId.Add(i.TaxonomyId);
            }

            var terms = await context.Terms
                .Where(x => taxonomiesId.Contains(x.TaxonomyId))
                .ToListAsync();

            ViewBag.Terms = terms;


            return View(post);
        }
        // POST /admin/posts/edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Post post, List<int> termIdList)
        {
            

            foreach (var id in termIdList)
            {
                PostTerm postTerm = new PostTerm()
                {
                    TermId = id,
                    PostId = post.Id,
                };

                context.Update(postTerm);
            }
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET /admin/posttype/delete/id
        public async Task<IActionResult> Delete(int id)
        {
            var posts = await context.Posts.FindAsync(id);
            context.Posts.Remove(posts);
            await context.SaveChangesAsync();
            TempData["Success"] = "The post has been deleted!";
            return RedirectToAction("Index");
        }

        // GET /admin/posts/details/5
        public async Task<IActionResult> Details(int id)
        {
            Post post = await context.Posts.Include(x => x.PostType).Include(x => x.PostTerms).FirstOrDefaultAsync(x => x.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }
    }
}
