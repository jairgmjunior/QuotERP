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
namespace Fashion.ERP.Web.Areas.Comum.Controllers
{
    public partial class FuncionarioController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected FuncionarioController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult Pesquisar()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Pesquisar);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult PesquisarComParametros()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PesquisarComParametros);
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
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult VirtualizationComboBox_Read()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.VirtualizationComboBox_Read);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Funcionarios_ValueMapper()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Funcionarios_ValueMapper);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public FuncionarioController Actions { get { return MVC.Comum.Funcionario; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Comum";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Funcionario";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Funcionario";

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
            public readonly string PesquisarComParametros = "PesquisarComParametros";
            public readonly string PesquisarFiltro = "PesquisarFiltro";
            public readonly string PesquisarCodigo = "PesquisarCodigo";
            public readonly string PesquisarId = "PesquisarId";
            public readonly string VirtualizationComboBox_Read = "VirtualizationComboBox_Read";
            public readonly string Funcionarios_ValueMapper = "Funcionarios_ValueMapper";
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
            public const string PesquisarComParametros = "PesquisarComParametros";
            public const string PesquisarFiltro = "PesquisarFiltro";
            public const string PesquisarCodigo = "PesquisarCodigo";
            public const string PesquisarId = "PesquisarId";
            public const string VirtualizationComboBox_Read = "VirtualizationComboBox_Read";
            public const string Funcionarios_ValueMapper = "Funcionarios_ValueMapper";
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
        static readonly ActionParamsClass_Pesquisar s_params_Pesquisar = new ActionParamsClass_Pesquisar();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Pesquisar PesquisarParams { get { return s_params_Pesquisar; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Pesquisar
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_PesquisarComParametros s_params_PesquisarComParametros = new ActionParamsClass_PesquisarComParametros();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_PesquisarComParametros PesquisarComParametrosParams { get { return s_params_PesquisarComParametros; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_PesquisarComParametros
        {
            public readonly string model = "model";
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
            public readonly string funcaoFuncionario = "funcaoFuncionario";
        }
        static readonly ActionParamsClass_PesquisarId s_params_PesquisarId = new ActionParamsClass_PesquisarId();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_PesquisarId PesquisarIdParams { get { return s_params_PesquisarId; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_PesquisarId
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_VirtualizationComboBox_Read s_params_VirtualizationComboBox_Read = new ActionParamsClass_VirtualizationComboBox_Read();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_VirtualizationComboBox_Read VirtualizationComboBox_ReadParams { get { return s_params_VirtualizationComboBox_Read; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_VirtualizationComboBox_Read
        {
            public readonly string request = "request";
            public readonly string funcoes = "funcoes";
        }
        static readonly ActionParamsClass_Funcionarios_ValueMapper s_params_Funcionarios_ValueMapper = new ActionParamsClass_Funcionarios_ValueMapper();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Funcionarios_ValueMapper Funcionarios_ValueMapperParams { get { return s_params_Funcionarios_ValueMapper; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Funcionarios_ValueMapper
        {
            public readonly string values = "values";
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
                public readonly string PesquisarComParametros = "PesquisarComParametros";
            }
            public readonly string _NovoOuEditar = "~/Areas/Comum/Views/Funcionario/_NovoOuEditar.cshtml";
            public readonly string Editar = "~/Areas/Comum/Views/Funcionario/Editar.cshtml";
            public readonly string Index = "~/Areas/Comum/Views/Funcionario/Index.cshtml";
            public readonly string Novo = "~/Areas/Comum/Views/Funcionario/Novo.cshtml";
            public readonly string Pesquisar = "~/Areas/Comum/Views/Funcionario/Pesquisar.cshtml";
            public readonly string PesquisarComParametros = "~/Areas/Comum/Views/Funcionario/PesquisarComParametros.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_FuncionarioController : Fashion.ERP.Web.Areas.Comum.Controllers.FuncionarioController
    {
        public T4MVC_FuncionarioController() : base(Dummy.Instance) { }

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

        partial void NovoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.Comum.Models.NovoFuncionarioModel model);

        public override System.Web.Mvc.ActionResult Novo(Fashion.ERP.Web.Areas.Comum.Models.NovoFuncionarioModel model)
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

        partial void EditarOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.Comum.Models.FuncionarioModel model);

        public override System.Web.Mvc.ActionResult Editar(Fashion.ERP.Web.Areas.Comum.Models.FuncionarioModel model)
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

        partial void PesquisarOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.Comum.Models.PesquisarFuncionarioModel model);

        public override System.Web.Mvc.ActionResult Pesquisar(Fashion.ERP.Web.Areas.Comum.Models.PesquisarFuncionarioModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Pesquisar);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            PesquisarOverride(callInfo, model);
            return callInfo;
        }

        partial void PesquisarComParametrosOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.Comum.Models.PesquisarFuncionarioModel model);

        public override System.Web.Mvc.ActionResult PesquisarComParametros(Fashion.ERP.Web.Areas.Comum.Models.PesquisarFuncionarioModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PesquisarComParametros);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            PesquisarComParametrosOverride(callInfo, model);
            return callInfo;
        }

        partial void PesquisarFiltroOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.Comum.Models.PesquisarFuncionarioModel model);

        public override System.Web.Mvc.ActionResult PesquisarFiltro(Fashion.ERP.Web.Areas.Comum.Models.PesquisarFuncionarioModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PesquisarFiltro);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            PesquisarFiltroOverride(callInfo, model);
            return callInfo;
        }

        partial void PesquisarCodigoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? codigo, string funcaoFuncionario);

        public override System.Web.Mvc.ActionResult PesquisarCodigo(long? codigo, string funcaoFuncionario)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PesquisarCodigo);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "codigo", codigo);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "funcaoFuncionario", funcaoFuncionario);
            PesquisarCodigoOverride(callInfo, codigo, funcaoFuncionario);
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

        partial void VirtualizationComboBox_ReadOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Kendo.Mvc.UI.DataSourceRequest request, string funcoes);

        public override System.Web.Mvc.ActionResult VirtualizationComboBox_Read(Kendo.Mvc.UI.DataSourceRequest request, string funcoes)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.VirtualizationComboBox_Read);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "request", request);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "funcoes", funcoes);
            VirtualizationComboBox_ReadOverride(callInfo, request, funcoes);
            return callInfo;
        }

        partial void Funcionarios_ValueMapperOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long[] values);

        public override System.Web.Mvc.ActionResult Funcionarios_ValueMapper(long[] values)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Funcionarios_ValueMapper);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "values", values);
            Funcionarios_ValueMapperOverride(callInfo, values);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
