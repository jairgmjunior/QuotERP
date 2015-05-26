using System.Web.Mvc;
using Fashion.ERP.Web.Helpers;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto
{
    public class EngenhariaProdutoAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "EngenhariaProduto"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            // Modelo
            context.MapRoute(
                name: "EngenhariaProduto_modelo_novo",
                url: "EngenhariaProduto/Modelo/Novo",
                defaults: new { controller = "Modelo", action = "Novo" },
                namespaces: new[] { "Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers" }
            );

            context.MapRoute(
                name: "EngenhariaProduto_modelo",
                url: "EngenhariaProduto/Modelo/{modeloId}/{action}/{id}",
                constraints: new { modeloId = @"\d+" },
                defaults: new { controller = "Modelo", action = "Detalhar", id = UrlParameter.Optional },
                namespaces: new[] { "Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers" }
            );

            context.MapRoute(
                name: "EngenhariaProduto_seq_materialcomposicao",
                url: "EngenhariaProduto/{controller}/{action}/{modeloId}",
                constraints: new { modeloId = @"\d+", controller = new RouteConstraintIsEqual(new []{ "MaterialComposicaoModelo", "SequenciaProducao", "ModeloMaterialConsumo"}) },
                defaults: new {},
                namespaces: new[] { "Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers" }
            );

            context.MapRoute(
                name: "EngenhariaProduto_default",
                url: "EngenhariaProduto/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers" }
            );
        }
    }
}
