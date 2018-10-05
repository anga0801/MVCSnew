using System.Web;
using System.Web.Mvc;

namespace MVCSuppliers.Custom
{
    public class SecurityFilter : ActionFilterAttribute
    {
        private readonly int _Role;
        public SecurityFilter(int role)
        {
            _Role = role;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;

            if ((session["Role"] == null || ((session["Role"] != null) && (int)session["Role"] > (_Role))))
            {
                filterContext.Result = new RedirectResult("/Account/Login", false);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}