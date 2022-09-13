using dgPadCms.Data;
using dgPadCms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace dgPadCms.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class TermsController : Controller
    {
        private readonly ApplicationDbContext context;
        public TermsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET /admin/terms
        public async Task<IActionResult> Index()
        {

            var terms = await context.Terms.OrderByDescending(t => t.TermId).Include(t => t.Taxonomy).ToListAsync();

            return View(terms);
        }

        // GET /admin/terms/create
        public IActionResult Create()
        {
            ViewBag.TaxonomyId = new SelectList(context.Taxonomies.OrderBy(x => x.Name), "TaxonomyId", "Name");
            return View();
        }


        // POST /admin/term/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Term term)
        {
            ViewBag.TaxonomyId = new SelectList(context.Taxonomies.OrderBy(x => x.Name), "TaxonomyId", "Name");

            if (ModelState.IsValid)
            {
                term.Code = term.Name.ToLower().Replace(" ", "-");

                var code = await context.Terms.FirstOrDefaultAsync(x => x.Code == term.Code);
                if (code != null)
                {
                    ModelState.AddModelError("", "The term already exists.");
                    return View(term);
                }

                context.Add(term);
                await context.SaveChangesAsync();

                TempData["Success"] = "The term has been added!";

                return RedirectToAction("Index");
            }

            return View(term);
        }


        // GET /admin/terms/edit/id
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.TaxonomyId = new SelectList(context.Taxonomies.OrderBy(x => x.Name), "TaxonomyId", "Name");

            var term = await context.Terms.FindAsync(id);
            return View(term);
        }
        // POST /admin/term/edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Term term)
        {
            ViewBag.TaxonomyId = new SelectList(context.Taxonomies.OrderBy(x => x.Name), "TaxonomyId", "Name");


            if (ModelState.IsValid)
            {
                term.Code = term.Name.ToLower().Replace(" ", "-");

                var code = await context.Terms.Where(x => x.TaxonomyId != id).FirstOrDefaultAsync(x => x.Code == term.Code);
                if (code != null)
                {
                    ModelState.AddModelError("", "The term already exists.");
                    return View(term);
                }
                context.Update(term);
                await context.SaveChangesAsync();
                TempData["Success"] = "The term has been edited!";

                return RedirectToAction("Index");
            }

            return View(term);
        }
        // GET /admin/term/details/5
        public async Task<IActionResult> Details(int id)
        {
            Term term = await context.Terms.Include(x => x.Taxonomy).FirstOrDefaultAsync(x => x.TaxonomyId == id);
            if (term == null)
            {
                return NotFound();
            }

            return View(term);
        }

        // GET /admin/term/delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Term term = await context.Terms.FindAsync(id);

            if (term == null)
            {
                TempData["Error"] = "The term does not exist!";
            }
            else
            {
                context.Terms.Remove(term);
                await context.SaveChangesAsync();

                TempData["Success"] = "The term has been deleted!";
            }

            return RedirectToAction("Index");
        }
    }
}
    