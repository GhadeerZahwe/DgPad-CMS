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

        // GET /admin/products
        public async Task<IActionResult> Index(int p = 1)
        {
            int pageSize = 6;
            var posts = context.Posts.OrderByDescending(x => x.Id)
                                            .Include(x => x.PostType)
                                            .Skip((p - 1) * pageSize)
                                            .Take(pageSize);

            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)context.Posts.Count() / pageSize);

            return View(await posts.ToListAsync());
        }


        // GET /admin/products/create
        public IActionResult Create()
        {
            ViewBag.PostTypeId = new SelectList(context.PostType, "Id", "Title");

            return View();
        }

        // POST /admin/products/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {
            ViewBag.PostTypeId = new SelectList(context.PostType, "Id", "Title");

            if (ModelState.IsValid)
            {
                post.Code = post.Title.ToLower().Replace(" ", "-");

                var code = await context.Posts.FirstOrDefaultAsync(x => x.Code == post.Code);
                if (code != null)
                {
                    ModelState.AddModelError("", "The post already exists.");
                    return View(post);
                }

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


        // GET /admin/products/details/5
        public async Task<IActionResult> Details(int id)
        {
            Post post = await context.Posts.Include(x => x.PostType).FirstOrDefaultAsync(x => x.Id == id);
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

            return View(post);
        }

        // POST /admin/products/edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Post post)
        {
            ViewBag.CategoryId = new SelectList(context.PostType, "Id", "Title", post.PostTypeId);

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
