using Common.Data;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace dgPadCms.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TaxonomiesController : Controller
    {
        private readonly ApplicationDbContext context;
        public TaxonomiesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET /admin/taxonomies
        public async Task<IActionResult> Index()
        {
            var taxonomies = await context.Taxonomies.OrderByDescending(p => p.TaxonomyId).ToListAsync();
            return View(taxonomies);
        }

        // GET /admin/taxonomies/create
        public IActionResult Create() => View();

        // POST /admin/taxonomies/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Taxonomy taxonomy)
        {
            if (!ModelState.IsValid)
            {
                return View(taxonomy);
            }

            var name = await context.Taxonomies.FirstOrDefaultAsync(x => x.Name == taxonomy.Name);
           
            if (name != null)
            {
                ModelState.AddModelError("", "This Taxonomy already exist");
                return View(taxonomy);
            }
            if (ModelState.IsValid)
            {
                taxonomy.Code = taxonomy.Name.ToLower().Replace(" ", "-");
             

                var code = await context.Taxonomies.FirstOrDefaultAsync(x => x.Code == taxonomy.Code);
                if (code != null)
                {
                    ModelState.AddModelError("", "The taxonomy already exists.");
                    return View(taxonomy);
                }

                context.Add(taxonomy);
                await context.SaveChangesAsync();

                TempData["Success"] = "The taxonomy has been added!";

                return RedirectToAction("Index");
            }

            context.Add(taxonomy);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        // GET /admin/taxonomies/edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Taxonomy taxonomy = await context.Taxonomies.FindAsync(id);
            if (taxonomy == null)
            {
                return NotFound();
            }

            return View(taxonomy);
        }

        // POST /admin/taxonomies/edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Taxonomy taxonomy)
        {
            if (ModelState.IsValid)
            {
                taxonomy.Code = taxonomy.Name.ToLower().Replace(" ", "-");

                var code = await context.Taxonomies.Where(x => x.TaxonomyId != id).FirstOrDefaultAsync(x => x.Code == taxonomy.Code);
                if (code != null)
                {
                    ModelState.AddModelError("", "The taxonomy already exists.");
                    return View(taxonomy);
                }

                context.Update(taxonomy);
                await context.SaveChangesAsync();

                TempData["Success"] = "The taxonomy has been edited!";

                return RedirectToAction("Index", new { id });
            }

            return View(taxonomy);
        }

        // GET /admin/taxonomies/delete/id
        public async Task<IActionResult> Delete(int id)
        {
            var taxonomy = await context.Taxonomies.FindAsync(id);
            context.Taxonomies.Remove(taxonomy);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
