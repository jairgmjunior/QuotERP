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
namespace Fashion.ERP.Web.Areas.Dev.Controllers
{
    public partial class MenuBuilderController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected MenuBuilderController(Dummy d) { }

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


        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public MenuBuilderController Actions { get { return MVC.Dev.MenuBuilder; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Dev";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "MenuBuilder";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "MenuBuilder";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string LerItensMenu = "LerItensMenu";
            public readonly string LerItensDisponiveis = "LerItensDisponiveis";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string LerItensMenu = "LerItensMenu";
            public const string LerItensDisponiveis = "LerItensDisponiveis";
        }


        static readonly ActionParamsClass_Index s_params_Index = new ActionParamsClass_Index();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Index IndexParams { get { return s_params_Index; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Index
        {
            public readonly string id = "id";
            public readonly string text = "text";
            public readonly string parentId = "parentId";
            public readonly string area = "area";
            public readonly string controller = "controller";
            public readonly string action = "action";
            public readonly string exibeNoMenu = "exibeNoMenu";
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
                public readonly string Index = "Index";
            }
            public readonly string Index = "~/Areas/Dev/Views/MenuBuilder/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_MenuBuilderController : Fashion.ERP.Web.Areas.Dev.Controllers.MenuBuilderController
    {
        public T4MVC_MenuBuilderController() : base(Dummy.Instance) { }

        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long[] id, string[] text, long[] parentId, string[] area, string[] controller, string[] action, bool[] exibeNoMenu);

        public override System.Web.Mvc.ActionResult Index(long[] id, string[] text, long[] parentId, string[] area, string[] controller, string[] action, bool[] exibeNoMenu)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "text", text);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "parentId", parentId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "area", area);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "controller", controller);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "action", action);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "exibeNoMenu", exibeNoMenu);
            IndexOverride(callInfo, id, text, parentId, area, controller, action, exibeNoMenu);
            return callInfo;
        }

        partial void LerItensMenuOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult LerItensMenu()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.LerItensMenu);
            LerItensMenuOverride(callInfo);
            return callInfo;
        }

        partial void LerItensDisponiveisOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult LerItensDisponiveis()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.LerItensDisponiveis);
            LerItensDisponiveisOverride(callInfo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
