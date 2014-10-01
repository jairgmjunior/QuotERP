using System.Web.Mvc;

namespace Fashion.ERP.Web.Areas.Almoxarifado
{
    public class AlmoxarifadoAreaRegistration : AreaRegistration
    {
        public override string AreaName { get { return "Almoxarifado"; } }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Almoxarifado_default",
                url: "Almoxarifado/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Fashion.ERP.Web.Areas.Almoxarifado.Controllers" }
            );
        }
    }
}
