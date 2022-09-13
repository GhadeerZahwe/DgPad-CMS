
using dgPadCms.Data;
using dgPadCms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dgPadCms.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostTypesController : Controller
    {
        private readonly ApplicationDbContext context;
        public PostTypesController(ApplicationDbContext context)
        {
            this.context = context;
        }


        // GET /admin/posttypes
        public async Task<IActionResult> Index()
        {
            var postTypes = await context.PostTypes.ToListAsync();
            return View(postTypes);
        }

        // GET /admin/posttype/detials
        public async Task<IActionResult> Details(int id)
        {
            var postType = await context.PostTypes.FindAsync(id);
            ViewBag.isChecked = await context.TaxonomyPostTypes.Where(x => x.PostTypeId == id).Include(x => x.Taxonomy).ToListAsync();

            return View(postType);
        }

        // GET /admin/posttypes/create
        public async Task<IActionResult> Create()
        { 

            ViewBag.taxonomies = await context.Taxonomies.ToListAsync();

            return View();
        }

        // POST /admin/posttypes/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostType postType, List<int> taxonomyIdList)
        {
            if (!ModelState.IsValid)
            {
                return View(postType);
            }

            if (taxonomyIdList == null) return View(postType);



            context.Add(postType);
            await context.SaveChangesAsync();

            foreach (var taxonomy in taxonomyIdList)
            {
                TaxonomyPostType taxonomyPostType = new TaxonomyPostType()
                {
                    TaxonomyId = taxonomy,
                    PostTypeId = postType.PostTypeId,
                };

                context.Add(taxonomyPostType);
            }

            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET /admin/posttypes/edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var postType = await context.PostTypes.FindAsync(id);
            ViewBag.taxonomies = await context.Taxonomies.ToListAsync();
            ViewBag.isChecked = await context.TaxonomyPostTypes.Where(x => x.PostTypeId == id).ToListAsync();

            return View(postType);
        }

        // POST /admin/posttypes/edit/id
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(PostType postType, List<int> taxonomyIdList)
        {
            if (!ModelState.IsValid)
            {
                return View(postType);
            }
            if (taxonomyIdList == null) return View(postType);

            var oldTax = await context.TaxonomyPostTypes.Where(x => x.PostTypeId == postType.PostTypeId).ToListAsync();
            foreach (var tax in oldTax)
            {
                context.TaxonomyPostTypes.Remove(tax);
            }
            await context.SaveChangesAsync();

            context.Update(postType);
            await context.SaveChangesAsync();

            foreach (var taxonomy in taxonomyIdList)
            {
                TaxonomyPostType taxonomyPostType = new TaxonomyPostType()
                {
                    TaxonomyId = taxonomy,
                    PostTypeId = postType.PostTypeId,
                };

                context.Add(taxonomyPostType);
            }

            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET /admin/posttype/delete/id
        public async Task<IActionResult> Delete(int id)
        {
            var postType = await context.PostTypes.FindAsync(id);
            context.PostTypes.Remove(postType);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
