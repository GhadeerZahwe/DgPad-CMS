
using Common.Data;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace dgPadCms.Areas.Admin.Controllers
{
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
        public async Task<IActionResult> Index()
        {
            var posts = await context.Posts.OrderByDescending(p => p.PostId).Include(x => x.PostType).ToListAsync();
            return View(posts);
        }


        // GET /admin/posts/create
        public async Task<ActionResult> Create(int? postTypeId = null)
        {
            ViewBag.PostTypeId = postTypeId;
            if (!postTypeId.HasValue)
            {
                ViewBag.PostType = await context.PostTypes.ToListAsync();
                return View();
            }

            var postType = await context.PostTypes.FindAsync(postTypeId);
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
            post.CreationDate = DateTime.Now.ToString("dd/MM/yyyy h:mm tt");

            string imageName = "noimage.png";
            if (post.ImageUpload != null)
            {
                string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "media/posts");
                imageName = Guid.NewGuid().ToString() + "_" + post.ImageUpload.FileName;
                string filePath = Path.Combine(uploadsDir, imageName);
                FileStream fs = new FileStream(filePath, FileMode.Create);
                await post.ImageUpload.CopyToAsync(fs);
                fs.Close();
            }

            post.Image = imageName;

            context.Add(post);
            await context.SaveChangesAsync();

            foreach (var id in termIdList)
            {
                PostTerm postTerm = new PostTerm()
                {
                    TermId = id,
                    PostId = post.PostId,
                };

                context.Add(postTerm);
            }
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET /admin/posts/edit/id
        public async Task<ActionResult> Edit(int postId, int postTypeId)
        {

            var post = await context.Posts.Include(x => x.PostType).FirstOrDefaultAsync(x => x.PostId == postId);
            ViewBag.PostType = await context.PostTypes.FindAsync(post.PostTypeId);

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
            post.CreationDate = DateTime.Now.ToString("dd/MM/yyyy h:mm tt") + " (edited)";

            if (post.ImageUpload != null)
            {
                string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "media/posts");

                if (!string.Equals(post.Image, "noimage.png"))
                {
                    string oldImagePath = Path.Combine(uploadsDir, post.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                }
                string imageName = Guid.NewGuid().ToString() + "_" + post.ImageUpload.FileName;
                string filePath = Path.Combine(uploadsDir, imageName);
                FileStream fs = new FileStream(filePath, FileMode.Create);
                await post.ImageUpload.CopyToAsync(fs);
                fs.Close();
                post.Image = imageName;
            }

            context.Update(post);
            await context.SaveChangesAsync();

            foreach (var id in termIdList)
            {
                PostTerm postTerm = new PostTerm()
                {
                    TermId = id,
                    PostId = post.PostId,
                };

                context.Update(postTerm);
            }
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET /admin/posts/delete/id
        public async Task<IActionResult> Delete(int id)
        {
            var post = await context.Posts.FindAsync(id);
            context.Posts.Remove(post);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
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
    }
}
