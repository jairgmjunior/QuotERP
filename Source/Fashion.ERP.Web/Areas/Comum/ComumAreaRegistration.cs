using System.Web.Mvc;

namespace Fashion.ERP.Web.Areas.Comum
{
    public class ComumAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Comum"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Comum_default",
                url: "Comum/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Fashion.ERP.Web.Areas.Comum.Controllers" }
            );
        }
    }
}
