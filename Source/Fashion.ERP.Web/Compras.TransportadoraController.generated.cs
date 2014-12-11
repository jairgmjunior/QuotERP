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
namespace Fashion.ERP.Web.Areas.Compras.Controllers
{
    public partial class TransportadoraController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected TransportadoraController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult Editar()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Editar);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Excluir()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Excluir);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult EditarSituacao()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditarSituacao);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult VerificarCpfCnpj()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.VerificarCpfCnpj);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult PesquisarFiltro()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PesquisarFiltro);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult PesquisarCodigo()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PesquisarCodigo);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult PesquisarId()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PesquisarId);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public TransportadoraController Actions { get { return MVC.Compras.Transportadora; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Compras";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Transportadora";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Transportadora";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string Novo = "Novo";
            public readonly string Editar = "Editar";
            public readonly string Excluir = "Excluir";
            public readonly string EditarSituacao = "EditarSituacao";
            public readonly string VerificarCpfCnpj = "VerificarCpfCnpj";
            public readonly string Pesquisar = "Pesquisar";
            public readonly string PesquisarFiltro = "PesquisarFiltro";
            public readonly string PesquisarCodigo = "PesquisarCodigo";
            public readonly string PesquisarId = "PesquisarId";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string Novo = "Novo";
            public const string Editar = "Editar";
            public const string Excluir = "Excluir";
            public const string EditarSituacao = "EditarSituacao";
            public const string VerificarCpfCnpj = "VerificarCpfCnpj";
            public const string Pesquisar = "Pesquisar";
            public const string PesquisarFiltro = "PesquisarFiltro";
            public const string PesquisarCodigo = "PesquisarCodigo";
            public const string PesquisarId = "PesquisarId";
        }


        static readonly ActionParamsClass_Novo s_params_Novo = new ActionParamsClass_Novo();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Novo NovoParams { get { return s_params_Novo; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Novo
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Editar s_params_Editar = new ActionParamsClass_Editar();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Editar EditarParams { get { return s_params_Editar; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Editar
        {
            public readonly string id = "id";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Excluir s_params_Excluir = new ActionParamsClass_Excluir();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Excluir ExcluirParams { get { return s_params_Excluir; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Excluir
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_EditarSituacao s_params_EditarSituacao = new ActionParamsClass_EditarSituacao();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_EditarSituacao EditarSituacaoParams { get { return s_params_EditarSituacao; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_EditarSituacao
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_VerificarCpfCnpj s_params_VerificarCpfCnpj = new ActionParamsClass_VerificarCpfCnpj();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_VerificarCpfCnpj VerificarCpfCnpjParams { get { return s_params_VerificarCpfCnpj; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_VerificarCpfCnpj
        {
            public readonly string cpfCnpj = "cpfCnpj";
        }
        static readonly ActionParamsClass_PesquisarFiltro s_params_PesquisarFiltro = new ActionParamsClass_PesquisarFiltro();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_PesquisarFiltro PesquisarFiltroParams { get { return s_params_PesquisarFiltro; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_PesquisarFiltro
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_PesquisarCodigo s_params_PesquisarCodigo = new ActionParamsClass_PesquisarCodigo();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_PesquisarCodigo PesquisarCodigoParams { get { return s_params_PesquisarCodigo; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_PesquisarCodigo
        {
            public readonly string codigo = "codigo";
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
                public readonly string _NovoOuEditar = "_NovoOuEditar";
                public readonly string Editar = "Editar";
                public readonly string Index = "Index";
                public readonly string Novo = "Novo";
                public readonly string Pesquisar = "Pesquisar";
            }
            public readonly string _NovoOuEditar = "~/Areas/Compras/Views/Transportadora/_NovoOuEditar.cshtml";
            public readonly string Editar = "~/Areas/Compras/Views/Transportadora/Editar.cshtml";
            public readonly string Index = "~/Areas/Compras/Views/Transportadora/Index.cshtml";
            public readonly string Novo = "~/Areas/Compras/Views/Transportadora/Novo.cshtml";
            public readonly string Pesquisar = "~/Areas/Compras/Views/Transportadora/Pesquisar.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_TransportadoraController : Fashion.ERP.Web.Areas.Compras.Controllers.TransportadoraController
    {
        public T4MVC_TransportadoraController() : base(Dummy.Instance) { }

        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        partial void NovoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult Novo()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Novo);
            NovoOverride(callInfo);
            return callInfo;
        }

        partial void NovoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.Compras.Models.NovoTransportadoraModel model);

        public override System.Web.Mvc.ActionResult Novo(Fashion.ERP.Web.Areas.Compras.Models.NovoTransportadoraModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Novo);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            NovoOverride(callInfo, model);
            return callInfo;
        }

        partial void EditarOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long id);

        public override System.Web.Mvc.ActionResult Editar(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Editar);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            EditarOverride(callInfo, id);
            return callInfo;
        }

        partial void EditarOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.Compras.Models.TransportadoraModel model);

        public override System.Web.Mvc.ActionResult Editar(Fashion.ERP.Web.Areas.Compras.Models.TransportadoraModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Editar);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            EditarOverride(callInfo, model);
            return callInfo;
        }

        partial void ExcluirOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? id);

        public override System.Web.Mvc.ActionResult Excluir(long? id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Excluir);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ExcluirOverride(callInfo, id);
            return callInfo;
        }

        partial void EditarSituacaoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long id);

        public override System.Web.Mvc.ActionResult EditarSituacao(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditarSituacao);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            EditarSituacaoOverride(callInfo, id);
            return callInfo;
        }

        partial void VerificarCpfCnpjOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, string cpfCnpj);

        public override System.Web.Mvc.JsonResult VerificarCpfCnpj(string cpfCnpj)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.VerificarCpfCnpj);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "cpfCnpj", cpfCnpj);
            VerificarCpfCnpjOverride(callInfo, cpfCnpj);
            return callInfo;
        }

        partial void PesquisarOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult Pesquisar()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Pesquisar);
            PesquisarOverride(callInfo);
            return callInfo;
        }

        partial void PesquisarFiltroOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Models.PesquisarModel model);

        public override System.Web.Mvc.ActionResult PesquisarFiltro(Fashion.ERP.Web.Models.PesquisarModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PesquisarFiltro);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            PesquisarFiltroOverride(callInfo, model);
            return callInfo;
        }

        partial void PesquisarCodigoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? codigo);

        public override System.Web.Mvc.ActionResult PesquisarCodigo(long? codigo)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PesquisarCodigo);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "codigo", codigo);
            PesquisarCodigoOverride(callInfo, codigo);
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