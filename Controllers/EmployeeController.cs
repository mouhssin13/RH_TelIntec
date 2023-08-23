using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Telintec_RH.Data;
using Telintec_RH.Models;

namespace Telintec_RH.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int pg = 1, string text = "")
        {

            if (HttpContext.Session.GetString("userAuthenticated") == "true")
            {
                List<Employee> employees;
                if (text != "" && text != null)
                {

                    employees = _context.Employees.Where(p => p.fulleName.Contains(text)).Include(x => x.Departement).ToList();

                }
                else
                {
                    employees = _context.Employees.ToList();

                }
                const int pageSize = 6;
                if (pg < 1) pg = 1;
                int recsCount = employees.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                this.ViewBag.Pager = pager;
                int recSkip = (pg - 1) * pageSize;
                var data = employees.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                ViewBag.Departments = _context.Departements.OrderBy(x => x.nom).ToList();
                return View(data);

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public IActionResult Create()
        {
            if ((HttpContext.Session.GetString("userAuthenticated") == "true"))
            {
                ViewBag.Departments = _context.Departements.OrderBy(x => x.nom).ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public IActionResult Update(int? id)
        {
            if ((HttpContext.Session.GetString("userAuthenticated") == "true"))
            {
                var data = _context.Employees.Find(id);
                ViewBag.Departments = _context.Departements.OrderBy(x => x.nom).ToList();
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee model)
        {
            UploadImage(model);

            if (ModelState.IsValid)
            {
                _context.Employees.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Employee model)
        {
            UploadImage(model);
            if (ModelState.IsValid)
            {
                _context.Employees.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            var emp = _context.Employees.Find(id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));


        }


        private void UploadImage(Employee model)
        {
            var file = HttpContext.Request.Form.Files;
            var x = model.image;
            if (file.Count() > 0)
            {
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var fileStream = new FileStream(Path.Combine(@"wwwroot/", "Images", ImageName), FileMode.Create);
                file[0].CopyTo(fileStream);
                model.image = ImageName;
                Console.Write(model.image);
            }
            else if (model.image == null && model.ID == null)
            {
                model.image = "Default.png";
            }
            else
            {
                model.image = x;
            }
        }

    }
}
