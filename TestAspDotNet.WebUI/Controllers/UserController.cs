using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestAspDotNet.Model;
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
        [Admin]
        [HttpGet]
        public IActionResult AddUpdateRole(int id=0) 
        {
            return View(_user.GetRole(id));
        }
        [Admin]
        [HttpPost]
        public IActionResult AddUpdateRole(UserRole userRole)
        {
            _user.AddUpdateRole(userRole);
            return RedirectToAction("Roles");
        }
        
        [HttpGet]
        public IActionResult DeleteRole(int id) 
        {
            _user.DeleteRole(id);
            return RedirectToAction("Roles");
        }
        //-----------Users-------
        [Admin]
        [HttpGet]
        public IActionResult Users()
        {
            return View(_user.GetUsers());
        }
        [Admin]
        [HttpGet]
        public IActionResult AddUpdateUser(int id=0)
        {
            if (id == 0)
            {
                return RedirectToAction("Register" , "Account");
            }
            ViewBag.AllRoles = new SelectList(_user.GetRolesList().ToList(),"Id" , "Name");
            return View(_user.GetUser(id));
        }
        [Admin]
        [HttpPost]
        public IActionResult AddUpdateUser(User user)
        {

            _user.AddUpdateUser(user);
            return RedirectToAction("Users");
        }
        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            _user.DeleteUser(id);
            return RedirectToAction("Users");
        }
    }
}
