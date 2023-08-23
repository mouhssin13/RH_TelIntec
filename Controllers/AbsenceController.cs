using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Telintec_RH.Data;
using Telintec_RH.Models;

namespace Telintec_RH.Controllers
{
    public class AbsenceController : Controller
    {
        private readonly AppDbContext _context;

        public AbsenceController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int pg = 1, string text = "")
        {

            if (HttpContext.Session.GetString("userAuthenticated") == "true")
            {
                List<Absence> absences;
                DateTime now = DateTime.Now;
                if (text != "" && text != null)
                {
                    absences = _context.Absences.Include(x => x.Employee).Where(p => p.Employee.fulleName.Contains(text)).ToList();

                }
                else
                {
                    absences = _context.Absences.Include(x => x.Employee).ToList();

                }
                foreach (var item in absences)
                {
                    if (item.Date_Fin < now.Date)
                    {
                        var x = _context.Employees.Find(item.EmployeeID);
                        x.statu = "WORKING";
                        _context.Employees.Update(x);
                        _context.SaveChanges();
                    }
                }
                const int pageSize = 6;
                if (pg < 1) pg = 1;
                int recsCount = absences.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                this.ViewBag.Pager = pager;
                int recSkip = (pg - 1) * pageSize;
                var data = absences.Skip(recSkip).Take(pager.PageSize).ToList();
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
        public IActionResult Create(Absence model)
        {

            UploadImage(model);
            if (ModelState.IsValid)
            {
                var data = _context.Employees.Find(model.EmployeeID);
                data.statu = "ABSENT";
                _context.Employees.Update(data);
                _context.Absences.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Update(int? id)
        {
            if ((HttpContext.Session.GetString("userAuthenticated") == "true"))
            {
                var data = _context.Absences.Find(id);
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
        public IActionResult Update(Absence model)
        {
            UploadImage(model);
            if (ModelState.IsValid)
            {
                _context.Absences.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Details(int? id)
        {
            if ((HttpContext.Session.GetString("userAuthenticated") == "true"))
            {
                var data = _context.Absences.Find(id);
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
            var hol = _context.Absences.Find(id);
            if (hol != null)
            {
                _context.Absences.Remove(hol);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));


        }
        private void UploadImage(Absence model)
        {
            var file = HttpContext.Request.Form.Files;
            var x = model.reason;
            if (file.Count() > 0)
            {
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var fileStream = new FileStream(Path.Combine(@"wwwroot/", "Reasons", ImageName), FileMode.Create);
                file[0].CopyTo(fileStream);
                model.reason = ImageName;
                Console.Write(model.reason);
            }
            else if (model.reason == null && model.ID == null)
            {
                model.reason = "Default.png";
            }
            else
            {
                model.reason = x;
            }
        }
    }
}
