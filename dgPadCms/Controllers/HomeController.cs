using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace dgPadCms.Controllers
{
    [Authorize] 
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //    UserManager<IdentityUser> userManager;
        //    RoleManager<IdentityRole> roleManager;

        //    ApplicationDbContext db;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        //public async Task<IActionResult> Index()
        //{
        //    if (db.Roles.FirstOrDefault(x => x.Name == "aaa") == null)
        //    {
        //        db.Roles.Add(new IdentityRole { Name = "aaa", NormalizedName = "AAA" });
        //        db.SaveChanges();
        //    }
        //    await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    await roleManager.CreateAsync(new IdentityRole { Name = "Publisher" });
        //    await roleManager.CreateAsync(new IdentityRole { Name = "Editor" });
        //    await roleManager.CreateAsync(new IdentityRole { Name = "Manager" });
        //    return View();
        //}  

        //public IActionResult users()
        //{
        //    var users = userManager.Users.ToList();
        //   // userManager.CreateAsync(new IdentityUser { });
        //    return View(users);
        //} 

        //public IActionResult roles()
        //{
        //    var roles = roleManager.Roles.ToList();
        //    //roleManager.CreateAsync(new IdentityRole { });
        //    return View(roles);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddRole(Role model)
        //{
        //    await roleManager.CreateAsync(new IdentityRole { Name = model.roleName });
        //    return RedirectToAction("index");
        //}

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Taxonomies()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
