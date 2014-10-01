using System.Web.Mvc;

namespace Fashion.ERP.Web.Areas.Compras
{
    public class ComprasAreaRegistration : AreaRegistration 
    {
        public override string AreaName { get  { return "Compras"; } }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "Compras_default",
                url: "Compras/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Fashion.ERP.Web.Areas.Compras.Controllers" }
            );
        }
    }
}