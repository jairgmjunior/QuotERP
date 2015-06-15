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
    public partial class ModeloMaterialConsumoController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ModeloMaterialConsumoController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult ModeloMaterialConsumo()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ModeloMaterialConsumo);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ModeloMaterialConsumoController Actions { get { return MVC.EngenhariaProduto.ModeloMaterialConsumo; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "EngenhariaProduto";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "ModeloMaterialConsumo";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "ModeloMaterialConsumo";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string ModeloMaterialConsumo = "ModeloMaterialConsumo";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string ModeloMaterialConsumo = "ModeloMaterialConsumo";
        }


        static readonly ActionParamsClass_ModeloMaterialConsumo s_params_ModeloMaterialConsumo = new ActionParamsClass_ModeloMaterialConsumo();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ModeloMaterialConsumo ModeloMaterialConsumoParams { get { return s_params_ModeloMaterialConsumo; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ModeloMaterialConsumo
        {
            public readonly string modeloId = "modeloId";
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
                public readonly string ModeloMaterialConsumo = "ModeloMaterialConsumo";
            }
            public readonly string ModeloMaterialConsumo = "~/Areas/EngenhariaProduto/Views/ModeloMaterialConsumo/ModeloMaterialConsumo.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ModeloMaterialConsumoController : Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.ModeloMaterialConsumoController
    {
        public T4MVC_ModeloMaterialConsumoController() : base(Dummy.Instance) { }

        partial void ModeloMaterialConsumoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long modeloId);

        public override System.Web.Mvc.ActionResult ModeloMaterialConsumo(long modeloId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ModeloMaterialConsumo);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "modeloId", modeloId);
            ModeloMaterialConsumoOverride(callInfo, modeloId);
            return callInfo;
        }

        partial void ModeloMaterialConsumoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.EngenhariaProduto.Models.ModeloMaterialConsumoModel model);

        public override System.Web.Mvc.ActionResult ModeloMaterialConsumo(Fashion.ERP.Web.Areas.EngenhariaProduto.Models.ModeloMaterialConsumoModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ModeloMaterialConsumo);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ModeloMaterialConsumoOverride(callInfo, model);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591