using Microsoft.AspNetCore.Mvc;
using TestAspDotNet.Model;
using TestAspDotNet.Repository;

namespace TestAspDotNet.WebUI.Controllers
{
    public class CommonController : Controller
    {
        private readonly IAccount _account;
        public CommonController(IAccount account)
        {
                _account = account; 
        }
        public User GetUser(HttpContext context)
        {
            string cookie = context.Request.Cookies["User-access-token"];
            if (cookie != null)
            {
                User user = _account.GetUserInfo(cookie);
                if (user != null) 
                {
                    return user;
                }
            
            }
            return null;
        }
    }
}
