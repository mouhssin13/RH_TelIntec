using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using Telintec_RH.Data;
using Telintec_RH.Models;

namespace Telintec_RH.Controllers
{
    public class DepartementController : Controller
    {
        private readonly AppDbContext _context;

        public DepartementController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int pg = 1, string text = "")
        {

            if (HttpContext.Session.GetString("userAuthenticated") == "true")
            {
                List<Departement> departements;
                if (text != "" && text != null)
                {
                    departements = _context.Departements.Where(p => p.nom.Contains(text)).ToList();

                }
                else
                {
                    departements = _context.Departements.ToList();

                }
                const int pageSize = 6; 
                if (pg < 1) pg = 1;
                int recsCount = departements.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                this.ViewBag.Pager = pager;
                int recSkip = (pg - 1) * pageSize;
                var data = departements.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                return View(data);

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

     public IActionResult Create()
        {
            if((HttpContext.Session.GetString("userAuthenticated") == "true"))
                {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Departement model)
        {
            if (ModelState.IsValid)
            {
                _context.Departements.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

     public IActionResult Update(int? id)
        {

            if ((HttpContext.Session.GetString("userAuthenticated") == "true"))
            {
                var data = _context.Departements.Find(id);
                if (data != null)
                {
                    return View(data);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
                
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Departement model)
        {
           
            if (ModelState.IsValid)
            {
                _context.Departements.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            var dep = _context.Departements.Find(id);
            if (dep != null)
            {
                _context.Departements.Remove(dep);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));


        }




    }
}
