using System.Web.Mvc;
using System.Web.Routing;

namespace Fashion.ERP.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.IgnoreRoute("{*staticfile}", new { staticfile = @".*\.(css|js|gif|jpg|jpeg)(/.*)?" });//Uploads/Files/tuu2tnzt.jpeg
            routes.IgnoreRoute("{*allphp}", new { allphp = @".*\.php(/.*)?" });//sys/cmd.php
            routes.IgnoreRoute("{*allcgi}", new { allcgi = @".*\.cgi(/.*)?" });//cgi-bin/index.cgi
            routes.IgnoreRoute("{*allasp}", new { allasp = @".*\.asp(/.*)?" });//ic.asp
            routes.IgnoreRoute("{*allaspx}", new { allaspx = @".*\.aspx(/.*)?" });
            routes.IgnoreRoute("{*allcgibin}", new { allcgibin = @".*cgi-bin(/.*)?" });//cgi-bin/common/attr
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Fashion.ERP.Web.Controllers" }
            );
        }
    }
}