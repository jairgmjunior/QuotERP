using System;
using System.Collections.Specialized;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Fashion.ERP.Web.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class,
        AllowMultiple = false, Inherited = true)]
    public class ValidateJsonAntiForgeryTokenAttribute :
        FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var httpContext = filterContext.HttpContext;
            var request = httpContext.Request;
 
            var form = new NameValueCollection(request.Form);
            if (request.Headers["__RequestVerificationToken"] != null) {
              form["__RequestVerificationToken"] 
                = request.Headers["__RequestVerificationToken"];
            }
            
            var cookie = httpContext.Request.Cookies[AntiForgeryConfig.CookieName];
            AntiForgery.Validate(cookie != null ? cookie.Value : null, form["__RequestVerificationToken"]);
        }
    }
}