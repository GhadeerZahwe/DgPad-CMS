using dgPadCms.Data;
using dgPadCms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> Index(int p = 1)
        {
            int pageSize = 6;

            var posts = context.Posts.OrderByDescending(x => x.Id)
                                            .Include(x => x.PostType)
                                            .Include(x => x.PostTerms)
                                            .Skip((p - 1) * pageSize)
                                            .Take(pageSize);

            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)context.Posts.Count() / pageSize);
            return View(await posts.ToListAsync());
        }


        // GET /admin/posts/create
        public IActionResult Create()
        {
            ViewBag.PostTypeId = new SelectList(context.PostType, "Id", "Title");
            ViewBag.TermId = new SelectList(context.Terms, "Id", "Name");
            return View();
        }

        // POST /admin/posts/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {
            ViewBag.PostTypeId = new SelectList(context.PostType, "Id", "Title");
            ViewBag.TermId = new SelectList(context.Terms, "Id", "Name");

            if (ModelState.IsValid)
            {
                post.Code = post.Title.ToLower().Replace(" ", "-");

                var code = await context.Posts.FirstOrDefaultAsync(x => x.Code == post.Code);
                if (code != null)
                {
                    ModelState.AddModelError("", "The post already exists.");
                    return View(post);
                }
           
                //foreach (var item in )
                //    post.PostTerms.Add(new PostTerm()
                //    {
                //        TermId = int.Parse(item)
                //    }) ;

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

                TempData["Success"] = "The post has been added!";

                return RedirectToAction("Index");
            }

            return View(post);
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

        // GET /admin/products/edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Post post = await context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            ViewBag.PostTypeId = new SelectList(context.PostType, "Id", "Title", post.PostTypeId);
            ViewBag.TermId = new SelectList(context.Terms, "Id", "Name",post.PostTerms);

            return View(post);
        }

        // POST /admin/posts/edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Post post)
        {
            ViewBag.CategoryId = new SelectList(context.PostType, "Id", "Title", post.PostTypeId);
            ViewBag.TermId = new SelectList(context.Terms, "Id", "Name", post.PostTerms);

            if (ModelState.IsValid)
            {
                post.Code = post.Title.ToLower().Replace(" ", "-");

                var slug = await context.Posts.Where(x => x.Id != id).FirstOrDefaultAsync(x => x.Code == post.Code);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The post already exists.");
                    return View(post);
                }

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

                TempData["Success"] = "The post has been edited!";

                return RedirectToAction("Index");
            }

            return View(post);
        }


        // GET /admin/posts/delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Post post = await context.Posts.FindAsync(id);

            if (post == null)
            {
                TempData["Error"] = "The post does not exist!";
            }
            else
            {
                if (!string.Equals(post.Image, "noimage.png"))
                {
                    string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "media/posts");
                    string oldImagePath = Path.Combine(uploadsDir, post.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                context.Posts.Remove(post);
                await context.SaveChangesAsync();

                TempData["Success"] = "The post has been deleted!";
            }

            return RedirectToAction("Index");
        }
    }
}
