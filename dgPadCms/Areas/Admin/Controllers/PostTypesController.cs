using dgPadCms.Data;
using dgPadCms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
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
        public async Task<IActionResult> Index(int p = 1)
        {
            int pageSize = 6;
            var posttypes = context.PostType.OrderByDescending(x => x.Id)
                                            .Include(x => x.TaxonomyPostTypes)
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

            var taxonomies = context.Taxonomies.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();
            CreatePostTypeViewModel vm = new CreatePostTypeViewModel();
            vm.Taxonomies = taxonomies;
            return View(vm);
        }


        // POST /admin/posttypes/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatePostTypeViewModel vm)
        {
            var posttype = new PostType
            {
                Title = vm.Title,
                Code = vm.Code
            };

            var selectedTaxonomies = vm.Taxonomies.Where(x => x.Selected).Select(y => y.Value).ToList();
            foreach (var item in selectedTaxonomies)
            {
                posttype.TaxonomyPostTypes.Add(new TaxonomyPostType()
                {
                    TaxonomyId = int.Parse(item)
                });
            }
            context.PostType.Add(posttype);
            context.SaveChanges();
            TempData["Success"] = "The post type has been added!";
            return RedirectToAction("Index");
            //    if (code != null)
            //    {
            //        ModelState.AddModelError("", "The post type already exists.");
            //        return View(posttype);
            //    }

            //    context.Add(posttype);
            //    await context.SaveChangesAsync();



            //    return RedirectToAction("Index");
            //}

            //return View(posttype);
        }


        // GET /admin/posttypes/details/5
        // GET: Student/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var posttype = await context.PostType.Include(x => x.TaxonomyPostTypes).ThenInclude(y => y.Taxonomy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posttype == null)
            {
                return NotFound();
            }

            return View(posttype);
        }


        // GET /admin/posttypes/edit/5
        public async Task<IActionResult> Edit(int id)
        {


            var posttype = await context.PostType.Include(x => x.TaxonomyPostTypes).Where(y => y.Id == id)
                .FirstOrDefaultAsync();
            if (posttype == null)
            {
                return NotFound();
            }
            var selectedIds = posttype.TaxonomyPostTypes.Select(x => x.TaxonomyId).ToList();
            var items = context.Taxonomies.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = selectedIds.Contains(x.Id)
            }).ToList();
            CreatePostTypeViewModel vm = new CreatePostTypeViewModel();
            vm.Title = posttype.Title;
            vm.Code = posttype.Code;
            vm.Taxonomies = items;
            return View(vm);
        }


        // POST /admin/posttypes/edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CreatePostTypeViewModel vm)
        {
            //    ViewBag.TaxonomyId = new SelectList(context.Taxonomies.OrderBy(x => x.Sorting), "Id", "Name", posttype.TaxonomyId);
            var posttype = context.PostType.Find(vm.Id);
            posttype.Title = vm.Title;
            posttype.Code = vm.Code;
            var posttypeById = context.PostType.Include(x => x.TaxonomyPostTypes).FirstOrDefault(y => y.Id == vm.Id);
            var existingIds = posttypeById.TaxonomyPostTypes.Select(x => x.TaxonomyId).ToList();
            var selectedIds = vm.Taxonomies.Where(x => x.Selected).Select(y => y.Value).Select(int.Parse).ToList();
            var toAdd = selectedIds.Except(existingIds);
            var toRemove = existingIds.Except(selectedIds);
            posttype.TaxonomyPostTypes = posttype.TaxonomyPostTypes.Where(x => !toRemove.Contains(x.TaxonomyId)).ToList();
            foreach (var item in toAdd)
            {
                posttype.TaxonomyPostTypes.Add(new TaxonomyPostType()
                {
                    TaxonomyId = item
                });
            }
            context.PostType.Update(posttype);
            context.SaveChangesAsync();
            TempData["Success"] = "The post type has been edited!";
            return RedirectToAction(nameof(Index));

        }
        //        return RedirectToAction("Index");
        //    }

        //    return View(posttype);
        //}

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
