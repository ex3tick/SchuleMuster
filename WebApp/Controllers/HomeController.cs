using Microsoft.AspNetCore.Mvc;
using WebApp.Model;
using WebApp.SqlDal;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    
    {
       
        private readonly Services _services;

        public HomeController(IConfiguration configuration)
        {
            _services = new Services(configuration);
        }
       
      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
        public IActionResult Index()
        {
           
            
            LoginTransferObjekt login = new LoginTransferObjekt();
            bool isAdmin = HttpContext.Session.GetString("isAdmin") == "True";
            string username = HttpContext.Session.GetString("username");
            if(username != null)
            {
                login = _services.Islogedin(username,isAdmin);
               
                return View(login);
            }
            
            return View(login);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginTransferObjekt login)
        {
           
            if (_services.Login(login))
            {
                HttpContext.Session.SetString("username", login.person.Username);
                HttpContext.Session.SetString("isAdmin", login.person.IsAdmin.ToString());
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(LoginTransferObjekt login)
        {
              if (_services.Register(login))
                {
                 return RedirectToAction("Login");
                }
                return RedirectToAction("Register");
        }
      
    }
}
