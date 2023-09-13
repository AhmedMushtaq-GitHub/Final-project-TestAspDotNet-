using Microsoft.AspNetCore.Mvc;
using TestAspDotNet.Repository;

namespace TestAspDotNet.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }
        [Admin]
        [HttpGet]
        public IActionResult Roles()
        {
           return View(_user.GetRoles());
        }
    }
}
