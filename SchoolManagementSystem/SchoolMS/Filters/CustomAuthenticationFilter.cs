using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMS.Filters
{
    public class CustomAuthenticationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Check if the user is authenticated
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // User is not authenticated, redirect to the login page
                filterContext.Result = new RedirectResult("~/Admin/Login");
            }

            base.OnActionExecuting(filterContext);
        }
    }

}
