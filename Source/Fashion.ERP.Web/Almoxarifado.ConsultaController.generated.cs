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
namespace Fashion.ERP.Web.Areas.Almoxarifado.Controllers
{
    public partial class ConsultaController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ConsultaController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult CustoMaterial()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CustoMaterial);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult ExtratoItem()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExtratoItem);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ConsultaController Actions { get { return MVC.Almoxarifado.Consulta; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Almoxarifado";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Consulta";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Consulta";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string EstoqueMaterial = "EstoqueMaterial";
            public readonly string CustoMaterial = "CustoMaterial";
            public readonly string ExtratoItem = "ExtratoItem";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string EstoqueMaterial = "EstoqueMaterial";
            public const string CustoMaterial = "CustoMaterial";
            public const string ExtratoItem = "ExtratoItem";
        }


        static readonly ActionParamsClass_EstoqueMaterial s_params_EstoqueMaterial = new ActionParamsClass_EstoqueMaterial();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_EstoqueMaterial EstoqueMaterialParams { get { return s_params_EstoqueMaterial; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_EstoqueMaterial
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_CustoMaterial s_params_CustoMaterial = new ActionParamsClass_CustoMaterial();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_CustoMaterial CustoMaterialParams { get { return s_params_CustoMaterial; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_CustoMaterial
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_ExtratoItem s_params_ExtratoItem = new ActionParamsClass_ExtratoItem();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ExtratoItem ExtratoItemParams { get { return s_params_ExtratoItem; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ExtratoItem
        {
            public readonly string id = "id";
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
                public readonly string CustoMaterial = "CustoMaterial";
                public readonly string EstoqueMaterial = "EstoqueMaterial";
                public readonly string ExtratoItem = "ExtratoItem";
            }
            public readonly string CustoMaterial = "~/Areas/Almoxarifado/Views/Consulta/CustoMaterial.cshtml";
            public readonly string EstoqueMaterial = "~/Areas/Almoxarifado/Views/Consulta/EstoqueMaterial.cshtml";
            public readonly string ExtratoItem = "~/Areas/Almoxarifado/Views/Consulta/ExtratoItem.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ConsultaController : Fashion.ERP.Web.Areas.Almoxarifado.Controllers.ConsultaController
    {
        public T4MVC_ConsultaController() : base(Dummy.Instance) { }

        partial void EstoqueMaterialOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult EstoqueMaterial()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EstoqueMaterial);
            EstoqueMaterialOverride(callInfo);
            return callInfo;
        }

        partial void EstoqueMaterialOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.Almoxarifado.Models.ConsultaEstoqueMaterialModel model);

        public override System.Web.Mvc.ActionResult EstoqueMaterial(Fashion.ERP.Web.Areas.Almoxarifado.Models.ConsultaEstoqueMaterialModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EstoqueMaterial);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            EstoqueMaterialOverride(callInfo, model);
            return callInfo;
        }

        partial void CustoMaterialOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long id);

        public override System.Web.Mvc.ActionResult CustoMaterial(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CustoMaterial);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            CustoMaterialOverride(callInfo, id);
            return callInfo;
        }

        partial void ExtratoItemOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long id);

        public override System.Web.Mvc.ActionResult ExtratoItem(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExtratoItem);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ExtratoItemOverride(callInfo, id);
            return callInfo;
        }

        partial void ExtratoItemOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.Almoxarifado.Models.ExtratoItemModel model);

        public override System.Web.Mvc.ActionResult ExtratoItem(Fashion.ERP.Web.Areas.Almoxarifado.Models.ExtratoItemModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExtratoItem);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ExtratoItemOverride(callInfo, model);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
