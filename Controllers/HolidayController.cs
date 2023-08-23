using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Telintec_RH.Data;
using Telintec_RH.Models;

namespace Telintec_RH.Controllers
{
    public class HolidayController : Controller
    {
        private readonly AppDbContext _context;

        public HolidayController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int pg = 1, string text = "")
        {

            if (HttpContext.Session.GetString("userAuthenticated") == "true")
            {
                List<Holiday> holidays;
                DateTime now = DateTime.Now;
                if (text != "" && text != null)
                {
                    holidays = _context.Holidays.Include(x => x.Employee).Where(p => p.Employee.fulleName.Contains(text)).ToList();
                   
                }
                else
                {
                    holidays = _context.Holidays.Include(x => x.Employee).ToList();

                }
                foreach (var item in holidays)
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
                int recsCount = holidays.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                this.ViewBag.Pager = pager;
                int recSkip = (pg - 1) * pageSize;
                var data = holidays.Skip(recSkip).Take(pager.PageSize).ToList();
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
        public IActionResult Create(Holiday model)
        {

            if (ModelState.IsValid)
            {
                var data = _context.Employees.Find(model.EmployeeID);
                data.statu = "HOLIDAY";
                _context.Employees.Update(data);
                _context.Holidays.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        public IActionResult Update(int? id)
        {
            if ((HttpContext.Session.GetString("userAuthenticated") == "true"))
            {
                var data = _context.Holidays.Find(id);
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
        public IActionResult Update(Holiday model)
        {
            if (ModelState.IsValid)
            {
                _context.Holidays.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            var hol = _context.Holidays.Find(id);
            if (hol != null)
            {
                _context.Holidays.Remove(hol);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));


        }



    }
}
