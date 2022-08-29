using dgPadCms.Data;
using dgPadCms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace dgPadCms.Controllers
{
    [Authorize] 
    public class PostTypesController : Controller
    {

        private readonly ApplicationDbContext context;


        public PostTypesController(ApplicationDbContext context)
        {
            this.context = context;

        }

        // GET /posttypes
        public async Task<IActionResult> Index(int p = 1)
        {
            int pageSize = 6;
            var posttypes = context.PostType.OrderByDescending(x => x.Id)
                                            .Skip((p - 1) * pageSize)
                                            .Take(pageSize);

            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)context.PostType.Count() / pageSize);

            return View(await posttypes.ToListAsync());
        }

        // GET /posttypes/taxonomy
        public async Task<IActionResult> PostTypesByTaxonomy(string taxonomyCode, int p = 1)
        {
            Taxonomy taxonomy = await context.Taxonomies.Where(x => x.Code == taxonomyCode).FirstOrDefaultAsync();
            if (taxonomy == null) return RedirectToAction("Index");

            int pageSize = 6;
            var posttypes = context.PostType.OrderByDescending(x => x.Id)
                                            .Where(x => x.TaxonomyId == taxonomy.Id)
                                            .Skip((p - 1) * pageSize)
                                            .Take(pageSize);

            ViewBag.PageNumber = p;

            ViewBag.PageRange = pageSize;

            ViewBag.TotalPages = (int)Math.Ceiling((decimal)context.PostType.Where(x => x.TaxonomyId == taxonomy.Id).Count() / pageSize);

            ViewBag.TaxonomyName = taxonomy.Name;

            ViewBag.TaxonomyCode = taxonomyCode;

            return View(await posttypes.ToListAsync());
        }
    }
}
