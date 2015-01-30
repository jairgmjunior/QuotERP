using System.Web.Mvc;

namespace Fashion.ERP.Web.Areas.Producao
{
    public class ProducaoAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Producao"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Producao_default",
                url: "Producao/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Fashion.ERP.Web.Areas.Producao.Controllers" }
            );
        }
    }
}
