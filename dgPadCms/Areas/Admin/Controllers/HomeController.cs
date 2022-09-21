using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Common.Data;
using Microsoft.EntityFrameworkCore;

namespace dgPadCms.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        readonly ApplicationDbContext context;

        public HomeController(ApplicationDbContext context)
        {
            this.context = context;
        }

       
        public async Task<IActionResult> Index()
        {
            var t = context.Terms.Where(x => x.TermId != null);

            List<Term> terms = new List<Term>();
            foreach (Term item in t)
            {
                int termId = item.TermId;
                Term Trm = context.Terms.First(p => p.TermId == termId);

                terms.Add(new Term
                {
                    TermId = Trm.TermId,
                    Name = Trm.Name,
                    Code = Trm.Code

                });
            }
            ViewBag.Term = terms;
            var postTypes = await context.PostTypes.ToListAsync();
            ViewBag.postTypes = postTypes;
            return View();
        }
        public async Task<IActionResult> Messages()
        {
            var t = context.Terms.Where(x => x.TermId != null);

            List<Term> terms = new List<Term>();
            foreach (Term item in t)
            {
                int termId = item.TermId;
                Term Trm = context.Terms.First(p => p.TermId == termId);

                terms.Add(new Term
                {
                    TermId = Trm.TermId,
                    Name = Trm.Name,
                    Code = Trm.Code

                });
            }
            ViewBag.Term = terms;
            var postTypes = await context.PostTypes.ToListAsync();
            ViewBag.postTypes = postTypes;
            return View(context.Contacts.ToList());
        }
    }
}
