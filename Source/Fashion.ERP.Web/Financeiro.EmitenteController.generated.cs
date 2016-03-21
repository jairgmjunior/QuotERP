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
namespace Fashion.ERP.Web.Areas.Financeiro.Controllers
{
    public partial class EmitenteController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected EmitenteController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult Detalhe()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Detalhe);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult NovoOuEditar()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.NovoOuEditar);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult PesquisarId()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PesquisarId);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public EmitenteController Actions { get { return MVC.Financeiro.Emitente; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Financeiro";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Emitente";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Emitente";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Detalhe = "Detalhe";
            public readonly string NovoOuEditar = "NovoOuEditar";
            public readonly string PesquisarId = "PesquisarId";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Detalhe = "Detalhe";
            public const string NovoOuEditar = "NovoOuEditar";
            public const string PesquisarId = "PesquisarId";
        }


        static readonly ActionParamsClass_Detalhe s_params_Detalhe = new ActionParamsClass_Detalhe();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Detalhe DetalheParams { get { return s_params_Detalhe; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Detalhe
        {
            public readonly string banco = "banco";
            public readonly string agencia = "agencia";
            public readonly string conta = "conta";
        }
        static readonly ActionParamsClass_NovoOuEditar s_params_NovoOuEditar = new ActionParamsClass_NovoOuEditar();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_NovoOuEditar NovoOuEditarParams { get { return s_params_NovoOuEditar; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_NovoOuEditar
        {
            public readonly string banco = "banco";
            public readonly string agencia = "agencia";
            public readonly string conta = "conta";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_PesquisarId s_params_PesquisarId = new ActionParamsClass_PesquisarId();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_PesquisarId PesquisarIdParams { get { return s_params_PesquisarId; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_PesquisarId
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
                public readonly string Detalhe = "Detalhe";
                public readonly string NovoOuEditar = "NovoOuEditar";
            }
            public readonly string Detalhe = "~/Areas/Financeiro/Views/Emitente/Detalhe.cshtml";
            public readonly string NovoOuEditar = "~/Areas/Financeiro/Views/Emitente/NovoOuEditar.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_EmitenteController : Fashion.ERP.Web.Areas.Financeiro.Controllers.EmitenteController
    {
        public T4MVC_EmitenteController() : base(Dummy.Instance) { }

        partial void DetalheOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? banco, string agencia, string conta);

        public override System.Web.Mvc.ActionResult Detalhe(long? banco, string agencia, string conta)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Detalhe);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "banco", banco);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "agencia", agencia);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "conta", conta);
            DetalheOverride(callInfo, banco, agencia, conta);
            return callInfo;
        }

        partial void NovoOuEditarOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? banco, string agencia, string conta);

        public override System.Web.Mvc.ActionResult NovoOuEditar(long? banco, string agencia, string conta)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.NovoOuEditar);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "banco", banco);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "agencia", agencia);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "conta", conta);
            NovoOuEditarOverride(callInfo, banco, agencia, conta);
            return callInfo;
        }

        partial void NovoOuEditarOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.Financeiro.Models.EmitenteModel model);

        public override System.Web.Mvc.ActionResult NovoOuEditar(Fashion.ERP.Web.Areas.Financeiro.Models.EmitenteModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.NovoOuEditar);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            NovoOuEditarOverride(callInfo, model);
            return callInfo;
        }

        partial void PesquisarIdOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long id);

        public override System.Web.Mvc.ActionResult PesquisarId(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PesquisarId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            PesquisarIdOverride(callInfo, id);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
