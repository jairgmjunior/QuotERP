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
namespace Fashion.ERP.Web.Areas.Producao.Controllers
{
    public partial class RelatorioProgramacaoProducaoController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RelatorioProgramacaoProducaoController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult Imprimir()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Imprimir);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public RelatorioProgramacaoProducaoController Actions { get { return MVC.Producao.RelatorioProgramacaoProducao; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Producao";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "RelatorioProgramacaoProducao";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "RelatorioProgramacaoProducao";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Imprimir = "Imprimir";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Imprimir = "Imprimir";
        }


        static readonly ActionParamsClass_Imprimir s_params_Imprimir = new ActionParamsClass_Imprimir();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Imprimir ImprimirParams { get { return s_params_Imprimir; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Imprimir
        {
            public readonly string id = "id";
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
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_RelatorioProgramacaoProducaoController : Fashion.ERP.Web.Areas.Producao.Controllers.RelatorioProgramacaoProducaoController
    {
        public T4MVC_RelatorioProgramacaoProducaoController() : base(Dummy.Instance) { }

        partial void ImprimirOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long id);

        public override System.Web.Mvc.ActionResult Imprimir(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Imprimir);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ImprimirOverride(callInfo, id);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
