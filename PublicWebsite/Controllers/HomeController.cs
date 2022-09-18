using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Common.Data;

namespace PublicWebsite.Controllers
{
    public class HomeController : Controller
    {
        readonly ApplicationDbContext db;

        public HomeController(ApplicationDbContext context)
        {
            db = context;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Messages()
        {

            return View(db.Contacts.ToList());
        }

        public IActionResult Contact()
        {

            return View();
        }
        [HttpPost]
        public IActionResult SaveContact(ContactUS model)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Contact", model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

   
    }
}
