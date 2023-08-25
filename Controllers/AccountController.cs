using Microsoft.AspNetCore.Mvc;
using System.Text;
using Telintec_RH.Data;
using Telintec_RH.Models;

namespace Telintec_RH.Controllers
{
    public class AccountController : Controller
    {

        private readonly AppDbContext _context;

        public AccountController(AppDbContext context) {
            _context = context;
            }

       public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Account model)
        {
            if (!ModelState.IsValid)
            {
                if (VerifierPW(model.userName, model.password) == false)
                {

                    HttpContext.Session.SetString("error", "account not found");
                    return View();
                }
                else
                {
                    var res = _context.Accounts.Where(m => m.userName == model.userName).SingleOrDefault();
                    HttpContext.Session.SetString("userAuthenticated", "true");
                    HttpContext.Session.SetString("userId",
                    Convert.ToString(res.ID));
                    HttpContext.Session.SetString("userFirstName",
                    res.firstName);
                    HttpContext.Session.SetString("userLastName",
                    res.lastName);
                    HttpContext.Session.SetString("userTel",
                    res.Tel);
                    HttpContext.Session.SetString("userName",
                    model.userName);
                    HttpContext.Session.SetString("role",
                    res.role);

                    if (res.role == "ADMIN")
                    {
                        return RedirectToAction(nameof(Index));

                    }
                    else
                    {
                        return RedirectToAction("Index", "Employee");
                    }
                }
            }
            return View();
        }

        public IActionResult Register()
        {

            if (HttpContext.Session.GetString("userAuthenticated") == "true" && HttpContext.Session.GetString("role") == "ADMIN")
            {
                return View();
            }
            else
            {
                return RedirectToAction(nameof(Login));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Account model)
        {
            if (ModelState.IsValid)
            {
                var Result = _context.Accounts.Where(m => m.userName == model.userName).SingleOrDefault();

                if (Result == null)
                {
                    model.password = Encrypt(model.password);
                    _context.Accounts.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    ViewBag.msg = "user_name already used ";
                    return View();
                }

            }
            return View();
        }
        public IActionResult Index(int pg = 1, string text = "")
        {
            
            if (HttpContext.Session.GetString("userAuthenticated") == "true" && HttpContext.Session.GetString("role") == "ADMIN")
            {
                List<Account> accounts;
                if (text != "" && text != null)
                {
                    accounts = _context.Accounts.Where(p => p.userName.Contains(text)).ToList();
                    
                }
                else
                {
                    accounts = _context.Accounts.ToList();
                    
                }
                const int pageSize = 6;
                if (pg < 1) pg = 1;
                int recsCount = accounts.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                this.ViewBag.Pager = pager;
                int recSkip = (pg - 1) * pageSize;
                var data = accounts.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                return View(data);
                
            }
            else
            {
                return RedirectToAction(nameof(Login));
            }
        }

        public IActionResult Update(int? id)
        {
            var Result = _context.Accounts.Find(id);
            if (HttpContext.Session.GetString("userAuthenticated") == "true" && HttpContext.Session.GetString("role") == "ADMIN")
            {
                return View(Result);
            }
            else
            {
                return RedirectToAction(nameof(Login));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Account model)
        {
            if (ModelState.IsValid)
            {
                var pw = Encrypt(model.password);
                model.password = pw;
                _context.Accounts.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

  
        public IActionResult Delete(int? id)
        {
            var acc = _context.Accounts.Find(id);
            if (acc != null)
            {
                _context.Accounts.Remove(acc);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
            

        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
        public bool VerifierUtilisateurExiste(string username)
        {

            var Result = _context.Accounts.Where(m => m.userName == username).SingleOrDefault();
            if (Result == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool VerifierPW(string username, string pass)
        {

            if (VerifierUtilisateurExiste(username) == false)
            {
                return false;
            }
            else
            {
                string pw = Encrypt(pass);
                var Result = _context.Accounts.Where(m => m.password == pw).Where(m => m.userName == username).SingleOrDefault();
                if (Result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static string Encrypt(string pass)
        {
            if (pass != null)
            {
                byte[] storePassword = ASCIIEncoding.UTF8.GetBytes(pass);
                string EncyptedPassword = Convert.ToBase64String(storePassword);
                return EncyptedPassword;
            }
            else
            {
                return null;
            }

        }

        public static string Decrypt(string pass)
        {
            if (string.IsNullOrEmpty(pass))
            {
                return null;
            }
            else
            {
                byte[] encryptedPass = Convert.FromBase64String(pass);
                string decryptedPass = ASCIIEncoding.ASCII.GetString(encryptedPass);
                return decryptedPass;
            }
        }


    }
}
