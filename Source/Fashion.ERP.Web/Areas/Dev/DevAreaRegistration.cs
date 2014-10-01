using System.Web.Mvc;

namespace Fashion.ERP.Web.Areas.Dev
{
#if DEBUG // Essa área só funciona em modo Debug
    public class DevAreaRegistration : AreaRegistration
    {
        public override string AreaName { get { return "Dev"; } }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Dev_default",
                url: "Dev/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Fashion.ERP.Web.Areas.Dev.Controllers" }
            );
        }
    }
#endif
}
