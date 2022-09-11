using Microsoft.AspNetCore.Mvc;
using Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace PublicWebsite.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext context;
        public PostController(ApplicationDbContext context)
        {
            this.context = context;

        }

        //// GET /admin/posts
        //public async Task<IActionResult> Index(int p = 1)
        //{
        //    int pageSize = 6;
        //    var posts = context.Posts.OrderByDescending(x => x.Id)
        //                                    .Include(x => x.PostType)
        //                                    .Skip((p - 1) * pageSize)
        //                                    .Take(pageSize);

        //    ViewBag.PageNumber = p;
        //    ViewBag.PageRange = pageSize;
        //    ViewBag.TotalPages = (int)Math.Ceiling((decimal)context.Posts.Count() / pageSize);

        //    return View(await posts.ToListAsync());
        //}
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
