using System.Web.Mvc;

namespace Fashion.ERP.Web.Areas.Financeiro
{
    public class FinanceiroAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Financeiro"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Financeiro_default",
                url: "Financeiro/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Fashion.ERP.Web.Areas.Financeiro.Controllers" }
            );
        }
    }
}
