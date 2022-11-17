using LoginAndRegistration.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace LoginAndRegistration.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyDbContext _db;
        public AccountController(MyDbContext db)
        {
            _db = db;
        }
        public IActionResult Records()
        {     
                return View(_db.userAccounts.ToList());    
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                _db.userAccounts.Add(account);
                _db.SaveChanges();
                
                ModelState.Clear();
                ViewBag.Message = account.FirstName + " " + account.LastName + "successful registered.";
            }
            return View();
        }
        public IActionResult Login()
        {
           return View();          
        }
        [HttpPost]
        public IActionResult Login(UserAccount user)
        {
                var usr = _db.userAccounts.Single(u => u.UserName == user.UserName && u.Password == user.Password);
                if(usr != null)
                {
                    HttpContext.Session.SetString("UserID", usr.UserID.ToString());
                    HttpContext.Session.SetString("UserName", usr.UserID.ToString());
                    return RedirectToAction("LoggedIn");

                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is wrong.");
                }
            
            return View();
        }
        public ActionResult LoggedIn()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
} 
