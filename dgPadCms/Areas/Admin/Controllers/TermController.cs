using dgPadCms.Data;
using dgPadCms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace dgPadCms.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class TermController : Controller
    {
        private readonly ApplicationDbContext context;


        public TermController(ApplicationDbContext context)
        {
            this.context = context;

        }

        // GET /admin/term
        public async Task<IActionResult> Index(int p = 1)
        {
            int pageSize = 6;
            var terms = context.Terms.OrderByDescending(x => x.Id)
                                            .Include(x => x.Taxonomy)
                                            .Skip((p - 1) * pageSize)
                                            .Take(pageSize);

            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)context.Terms.Count() / pageSize);

            return View(await terms.ToListAsync());
        }

        // GET /admin/term/create
        public IActionResult Create()
        {
            ViewBag.TaxonomyId = new SelectList(context.Taxonomies.OrderBy(x => x.Sorting), "Id", "Name");

            return View();
        }


        // POST /admin/term/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Term term)
        {
            ViewBag.TaxonomyId = new SelectList(context.Taxonomies.OrderBy(x => x.Sorting), "Id", "Name");

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


        // GET /admin/term/details/5
        public async Task<IActionResult> Details(int id)
        {
            Term term = await context.Terms.Include(x => x.Taxonomy).FirstOrDefaultAsync(x => x.Id == id);
            if (term == null)
            {
                return NotFound();
            }

            return View(term);
        }


        // GET /admin/term/edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Term term = await context.Terms.FindAsync(id);
            if (term == null)
            {
                return NotFound();
            }

            ViewBag.TaxonomyId = new SelectList(context.Taxonomies.OrderBy(x => x.Sorting), "Id", "Name", term.TaxonomyId);

            return View(term);
        }


        // POST /admin/term/edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Term term)
        {
            ViewBag.TaxonomyId = new SelectList(context.Taxonomies.OrderBy(x => x.Sorting), "Id", "Name", term.TaxonomyId);
          

            if (ModelState.IsValid)
            {
                term.Code = term.Name.ToLower().Replace(" ", "-");

                var code = await context.Terms.Where(x => x.Id != id).FirstOrDefaultAsync(x => x.Code == term.Code);
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


        // POST /admin/term/reorder
        [HttpPost]
        public async Task<IActionResult> Reorder(int[] id)
        {
            int count = 1;

            foreach (var termId in id)
            {
                Term term = await context.Terms.FindAsync(termId);
                term.Sorting = count;
                context.Update(term);
                await context.SaveChangesAsync();
                count++;
            }

            return Ok();
        }
    }
}
