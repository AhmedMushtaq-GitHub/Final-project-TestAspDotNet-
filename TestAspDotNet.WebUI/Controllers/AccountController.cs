using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using TestAspDotNet.Data;
using TestAspDotNet.Model;
using TestAspDotNet.Repository;
namespace TestAspDotNet.WebUI.Controllers
{

    public class AccountController : Controller
    {

        private readonly Repository.IAccount _account;

        public AccountController(Repository.IAccount account)
        {
            _account = account;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(IFormCollection Data)
        {
            string email = Data["EmailAddress"];
            string password = Data["Password"];
            var dbUser = _account.GetUserForLogin(email, password);
            if (dbUser!=null)
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.UtcNow.AddDays(30);
                Response.Cookies.Append("User-access-token", dbUser.AccessToken, options);
                return Redirect("/Home/Index");
            }
            ViewBag.Error = "Your UserName Or Password is Incorrect";
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();        
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
      
            return View();
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("User-access-token");
            return Redirect("/Home/Index");
        }

    }
}
