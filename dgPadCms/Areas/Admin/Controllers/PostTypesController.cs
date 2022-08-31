using dgPadCms.Data;
using dgPadCms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace dgPadCms.Areas.Admin.Controllers
{
   
     [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class PostTypesController : Controller
    {
        private readonly ApplicationDbContext context;
     

        public PostTypesController(ApplicationDbContext context)
        {
            this.context = context;
       
        }

        // GET /admin/posttypes
        public async Task<IActionResult> Index(int p = 1)
        {
            int pageSize = 6;
            var posttypes = context.PostType.OrderByDescending(x => x.Id)
                                            .Include(x => x.Taxonomy)
                                            .Skip((p - 1) * pageSize)
                                            .Take(pageSize);

            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)context.PostType.Count() / pageSize);

            return View(await posttypes.ToListAsync());
        }

        // GET /admin/posttypes/create
        public IActionResult Create()
        {
            ViewBag.TaxonomyId = new SelectList(context.Taxonomies.OrderBy(x => x.Sorting), "Id", "Name");

            return View();
        }


        // POST /admin/posttypes/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostType posttype)
        {
            ViewBag.TaxonomyId = new SelectList(context.Taxonomies.OrderBy(x => x.Sorting), "Id", "Name");

            if (ModelState.IsValid)
            {
                posttype.Code = posttype.Title.ToLower().Replace(" ", "-");

                var code = await context.PostType.FirstOrDefaultAsync(x => x.Code == posttype.Code);
                if (code != null)
                {
                    ModelState.AddModelError("", "The post type already exists.");
                    return View(posttype);
                }

                context.Add(posttype);
                await context.SaveChangesAsync();

                TempData["Success"] = "The post type has been added!";

                return RedirectToAction("Index");
            }

            return View(posttype);
        }


        // GET /admin/posttypes/details/5
        public async Task<IActionResult> Details(int id)
        {
            PostType posttype = await context.PostType.Include(x => x.Taxonomy).FirstOrDefaultAsync(x => x.Id == id);
            if (posttype == null)
            {
                return NotFound();
            }

            return View(posttype);
        }


        // GET /admin/posttypes/edit/5
        public async Task<IActionResult> Edit(int id)
        {
            PostType posttype = await context.PostType.FindAsync(id);
            if (posttype == null)
            {
                return NotFound();
            }

            ViewBag.TaxonomyId = new SelectList(context.Taxonomies.OrderBy(x => x.Sorting), "Id", "Name", posttype.TaxonomyId);

            return View(posttype);
        }


        // POST /admin/posttypes/edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PostType posttype)
        {
            ViewBag.TaxonomyId = new SelectList(context.Taxonomies.OrderBy(x => x.Sorting), "Id", "Name", posttype.TaxonomyId);

            if (ModelState.IsValid)
            {
                posttype.Code = posttype.Title.ToLower().Replace(" ", "-");

                var code = await context.PostType.Where(x => x.Id != id).FirstOrDefaultAsync(x => x.Code == posttype.Code);
                if (code != null)
                {
                    ModelState.AddModelError("", "The posttype already exists.");
                    return View(posttype);
                }
                 context.Update(posttype);
            await context.SaveChangesAsync();
                TempData["Success"] = "The post type has been edited!";

                return RedirectToAction("Index");
            }
          
            return View(posttype);
        }

        // GET /admin/posttypes/delete/5
        public async Task<IActionResult> Delete(int id)
        {
            PostType posttype = await context.PostType.FindAsync(id);

            if (posttype == null)
            {
                TempData["Error"] = "The post type does not exist!";
            }
            else
            {
                context.PostType.Remove(posttype);
                await context.SaveChangesAsync();

                TempData["Success"] = "The post type has been deleted!";
            }

            return RedirectToAction("Index");
        }
    }
}
