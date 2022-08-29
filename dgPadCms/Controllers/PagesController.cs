using dgPadCms.Data;
using dgPadCms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace dgPadCms.Controllers
{ 
   
    public class PagesController : Controller
    {

        private readonly ApplicationDbContext context;

        public PagesController(ApplicationDbContext context)
        {
            this.context = context;

        }

        // GET / or /code
        public async Task<IActionResult> PostType (string code)
        {
            if (code == null)
            {
                return View(await context.PostType.Where(x => x.Code == "home").FirstOrDefaultAsync());
            }

            PostType posttype = await context.PostType.Where(x => x.Code == code).FirstOrDefaultAsync();

            if (posttype == null)
            {
                return NotFound();
            }

            return View(posttype);
        }
    }
}
