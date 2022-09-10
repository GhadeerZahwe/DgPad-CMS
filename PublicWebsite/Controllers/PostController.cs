using Microsoft.AspNetCore.Mvc;
using Common;
using System.Linq;

namespace PublicWebsite.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext context;
        public PostController(ApplicationDbContext context)
        {
            this.context = context;

        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = context.PostType.ToList();
            return View(result);
        }

        //public IActionResult Index(int id)
        //{
        //    PostType p = context.PostType.Find(id);
        //    ViewBag.PostType = p.Title;
        //    ViewData["PostType"] = p.Title;
        //    var result = context.Posts.Where(x => x.PostTypeId == id).OrderByDescending(x => x.Date).ToList();
        //    return View(result);
        //    //var result = context.PostType.OrderByDescending(x => x.Id)(x => x.Sorting).ToListAsync());
        //    //return View(result);
        //}

    }
}
