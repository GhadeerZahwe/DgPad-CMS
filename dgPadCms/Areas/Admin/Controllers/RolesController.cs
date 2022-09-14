
using Common.Data;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dgPadCms.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly ILogger<RolesController> _logger;
        readonly UserManager<IdentityUser> userManager;
        readonly RoleManager<IdentityRole> roleManager;
        readonly ApplicationDbContext db;

        public RolesController(ILogger<RolesController> logger, UserManager<IdentityUser> user,
                 RoleManager<IdentityRole> role,
             ApplicationDbContext identity)
        {
            _logger = logger;
            userManager = user;
            roleManager = role;
            db = identity;
        }


        // GET: RolesController
        public async Task<IActionResult> Index()
        {
            if (db.Roles.FirstOrDefault(x => x.Name == "Admin") == null)
            {
                db.Roles.Add(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
                db.SaveChanges();
            }
            await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            await roleManager.CreateAsync(new IdentityRole { Name = "Publisher" });
            await roleManager.CreateAsync(new IdentityRole { Name = "Editor" });
            await roleManager.CreateAsync(new IdentityRole { Name = "Manager" });
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> AddRole(Role model)
        {
            await roleManager.CreateAsync(new IdentityRole { Name = model.roleName });
            return RedirectToAction("roles");
        }

        public IActionResult users()
        {
            var users = userManager.Users.ToList();
            // userManager.CreateAsync(new IdentityUser { });
            return View(users);
        }

        public IActionResult roles()
        {
            var roles = roleManager.Roles.ToList();
            //roleManager.CreateAsync(new IdentityRole { });
            return View(roles);
        }

        public async Task<IActionResult> UserRoles()
        {
            var users = userManager.Users.ToList();
            //list of users with roles
            List<UserRoles> result = new List<UserRoles>();

            foreach (var item in users)
            {
                //Get roles
                var roles = await userManager.GetRolesAsync(item);
                //add the user with his role
                result.Add(new UserRoles { user = item, userRoles = (List<string>)roles });
            }
            ViewBag.allRoles = roleManager.Roles.ToList();
            return View(result);

        }

        //Adding role to user
        public async Task<IActionResult> addroletouser(string userid, string rolename)
        {
            var user = await userManager.FindByIdAsync(userid);
            var result = await userManager.AddToRoleAsync(user, rolename);
            //if the role isn't added to the user, it means the role is set to this user so we remove it.Else, add the role.
            if (!result.Succeeded)
            {
                await userManager.RemoveFromRoleAsync(user, rolename);
            }

            return RedirectToAction("UserRoles");
        }



        // GET: RolesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RolesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RolesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RolesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RolesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RolesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RolesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
