// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers
{
    public partial class ModeloAprovacaoController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ModeloAprovacaoController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult ObtenhaListaGridModeloAprovacaoModel()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ObtenhaListaGridModeloAprovacaoModel);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult CriarFichaTecnica()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CriarFichaTecnica);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ModeloAprovacaoController Actions { get { return MVC.EngenhariaProduto.ModeloAprovacao; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "EngenhariaProduto";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "ModeloAprovacao";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "ModeloAprovacao";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string ObtenhaListaGridModeloAprovacaoModel = "ObtenhaListaGridModeloAprovacaoModel";
            public readonly string CriarFichaTecnica = "CriarFichaTecnica";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string ObtenhaListaGridModeloAprovacaoModel = "ObtenhaListaGridModeloAprovacaoModel";
            public const string CriarFichaTecnica = "CriarFichaTecnica";
        }


        static readonly ActionParamsClass_Index s_params_Index = new ActionParamsClass_Index();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Index IndexParams { get { return s_params_Index; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Index
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_ObtenhaListaGridModeloAprovacaoModel s_params_ObtenhaListaGridModeloAprovacaoModel = new ActionParamsClass_ObtenhaListaGridModeloAprovacaoModel();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ObtenhaListaGridModeloAprovacaoModel ObtenhaListaGridModeloAprovacaoModelParams { get { return s_params_ObtenhaListaGridModeloAprovacaoModel; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ObtenhaListaGridModeloAprovacaoModel
        {
            public readonly string request = "request";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_CriarFichaTecnica s_params_CriarFichaTecnica = new ActionParamsClass_CriarFichaTecnica();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_CriarFichaTecnica CriarFichaTecnicaParams { get { return s_params_CriarFichaTecnica; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_CriarFichaTecnica
        {
            public readonly string ids = "ids";
            public readonly string model = "model";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string CriarFichaTecnica = "CriarFichaTecnica";
                public readonly string Index = "Index";
            }
            public readonly string CriarFichaTecnica = "~/Areas/EngenhariaProduto/Views/ModeloAprovacao/CriarFichaTecnica.cshtml";
            public readonly string Index = "~/Areas/EngenhariaProduto/Views/ModeloAprovacao/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ModeloAprovacaoController : Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.ModeloAprovacaoController
    {
        public T4MVC_ModeloAprovacaoController() : base(Dummy.Instance) { }

        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.EngenhariaProduto.Models.PesquisaModeloAprovacaoModel model);

        public override System.Web.Mvc.ActionResult Index(Fashion.ERP.Web.Areas.EngenhariaProduto.Models.PesquisaModeloAprovacaoModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            IndexOverride(callInfo, model);
            return callInfo;
        }

        partial void ObtenhaListaGridModeloAprovacaoModelOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Kendo.Mvc.UI.DataSourceRequest request, Fashion.ERP.Web.Areas.EngenhariaProduto.Models.PesquisaModeloAprovacaoModel model);

        public override System.Web.Mvc.ActionResult ObtenhaListaGridModeloAprovacaoModel(Kendo.Mvc.UI.DataSourceRequest request, Fashion.ERP.Web.Areas.EngenhariaProduto.Models.PesquisaModeloAprovacaoModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ObtenhaListaGridModeloAprovacaoModel);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "request", request);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ObtenhaListaGridModeloAprovacaoModelOverride(callInfo, request, model);
            return callInfo;
        }

        partial void CriarFichaTecnicaOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Collections.Generic.IEnumerable<long> ids);

        public override System.Web.Mvc.ActionResult CriarFichaTecnica(System.Collections.Generic.IEnumerable<long> ids)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CriarFichaTecnica);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "ids", ids);
            CriarFichaTecnicaOverride(callInfo, ids);
            return callInfo;
        }

        partial void CriarFichaTecnicaOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.EngenhariaProduto.Models.CriacaoFichaTecnicaModel model);

        public override System.Web.Mvc.ActionResult CriarFichaTecnica(Fashion.ERP.Web.Areas.EngenhariaProduto.Models.CriacaoFichaTecnicaModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CriarFichaTecnica);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            CriarFichaTecnicaOverride(callInfo, model);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
