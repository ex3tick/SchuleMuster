using Microsoft.AspNetCore.Mvc;
using WebApp.Model;
using WebApp.SqlDal;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private UserDAL _userDal = new UserDAL();
        public IActionResult Index()
        {
           
            
            LoginTransferObjekt login = new LoginTransferObjekt();
            login.Users = _userDal.GetAllUsers();
            if(HttpContext.Session.GetString("username") != null)
            {
                login = new LoginTransferObjekt();
                login.person = new Person();
                login.person.Username = HttpContext.Session.GetString("username");
                login.loggedIn = true;
                return View(login);
            }
            
            return View(login);
        }
        
        
        public IActionResult Everyone()
        {
            return View();
        }
        
        public IActionResult Privacy()
        {
            if( HttpContext.Session.GetString("username") == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginTransferObjekt login)
        {
           
            if (_userDal.CheckLogin(login))
            {
                HttpContext.Session.SetString("username", login.person.Username);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }
        
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Register(LoginTransferObjekt login, string wPw)
        {
            if (login.person.Password != wPw)
            {
                return RedirectToAction("Register");
            }
            if (_userDal.Register(login))
            {
                
                return RedirectToAction("Login");
            }
            return RedirectToAction("Register");
        }
      
    }
}
