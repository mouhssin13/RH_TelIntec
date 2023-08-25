using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Telintec_RH.Data;
using Telintec_RH.Models;

namespace Telintec_RH.Controllers
{
    public class OperationController : Controller
    {
        private readonly AppDbContext _context;

        public OperationController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(DateTime d1 = default(DateTime), DateTime d2 = default(DateTime), int pg = 1)
        {

            if (HttpContext.Session.GetString("userAuthenticated") == "true")
            {
                List<Operation> operations;
                if (d1!= default(DateTime) && d2!= default(DateTime))
                {
                    operations = _context.Operations.Include(x => x.Employee).Where(p => p.Date>= d1 && p.Date <= d2).ToList();

                }
                else
                {
                    operations = _context.Operations.Include(x => x.Employee).ToList();

                }
                const int pageSize = 6;
                if (pg < 1) pg = 1;
                int recsCount = operations.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                this.ViewBag.Pager = pager;
                int recSkip = (pg - 1) * pageSize;
                var data = operations.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                ViewBag.Employees = _context.Employees.OrderBy(x => x.fulleName).ToList();
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
                ViewBag.Employee = _context.Employees.OrderBy(x => x.fulleName).ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Operation model)
        {

            UploadImage(model);
            if (ModelState.IsValid)
            {
                _context.Operations.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Update(int? id)
        {
            if ((HttpContext.Session.GetString("userAuthenticated") == "true"))
            {
                var data = _context.Operations.Find(id);
                ViewBag.Employees = _context.Employees.OrderBy(x => x.fulleName).ToList();
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Operation model)
        {
            UploadImage(model);
            if (ModelState.IsValid)
            {
                _context.Operations.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Details(int? id)
        {
            if ((HttpContext.Session.GetString("userAuthenticated") == "true"))
            {
                var data = _context.Operations.Find(id);
                ViewBag.Employees = _context.Employees.Find(data.EmployeeID);
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public IActionResult Delete(int? id)
        {
            var hol = _context.Operations.Find(id);
            if (hol != null)
            {
                _context.Operations.Remove(hol);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));


        }

        private void UploadImage(Operation model)
        {
            var file = HttpContext.Request.Form.Files;
            var x = model.contrat;
            if (file.Count() > 0)
            {
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var fileStream = new FileStream(Path.Combine(@"wwwroot/", "Contracts", ImageName), FileMode.Create);
                file[0].CopyTo(fileStream);
                model.contrat = ImageName;
                Console.Write(model.contrat);
            }
            else if (model.contrat == null && model.ID == null)
            {
                model.contrat = "Default.png";
            }
            else
            {
                model.contrat = x;
            }
        }


    }
}
