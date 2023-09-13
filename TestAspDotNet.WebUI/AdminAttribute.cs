using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using TestAspDotNet.Data;

namespace TestAspDotNet.WebUI
{
    public class AdminAttribute : Attribute, IActionFilter
    {
       
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string accesstoken = context.HttpContext.Request.Cookies["User-access-token"];
            if (!string.IsNullOrEmpty(accesstoken))
            {
                TestAspDotNetDbContext db = context.HttpContext.RequestServices.GetRequiredService<TestAspDotNetDbContext>();
                var test = db.Users.Where(x => x.AccessToken.Equals(accesstoken) && x.UserRole.Name.Equals("Admin")).Any();
                if (!test)
                {
                    context.Result = new RedirectResult("/Account/Login");
                }
            }
            else
            {
                context.Result = new RedirectResult("/Account/Login");
            }
       
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
