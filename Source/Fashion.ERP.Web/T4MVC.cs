﻿// <auto-generated />
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

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
public static class MVC
{
    static readonly AlmoxarifadoClass s_Almoxarifado = new AlmoxarifadoClass();
    public static AlmoxarifadoClass Almoxarifado { get { return s_Almoxarifado; } }
    static readonly ComprasClass s_Compras = new ComprasClass();
    public static ComprasClass Compras { get { return s_Compras; } }
    static readonly ComumClass s_Comum = new ComumClass();
    public static ComumClass Comum { get { return s_Comum; } }
    static readonly DevClass s_Dev = new DevClass();
    public static DevClass Dev { get { return s_Dev; } }
    static readonly EngenhariaProdutoClass s_EngenhariaProduto = new EngenhariaProdutoClass();
    public static EngenhariaProdutoClass EngenhariaProduto { get { return s_EngenhariaProduto; } }
    static readonly FinanceiroClass s_Financeiro = new FinanceiroClass();
    public static FinanceiroClass Financeiro { get { return s_Financeiro; } }
    public static Fashion.ERP.Web.Controllers.ArquivoController Arquivo = new Fashion.ERP.Web.Controllers.T4MVC_ArquivoController();
    public static Fashion.ERP.Web.Controllers.ErrorController Error = new Fashion.ERP.Web.Controllers.T4MVC_ErrorController();
    public static Fashion.ERP.Web.Controllers.HomeController Home = new Fashion.ERP.Web.Controllers.T4MVC_HomeController();
    public static Fashion.ERP.Web.Controllers.LogController Log = new Fashion.ERP.Web.Controllers.T4MVC_LogController();
    public static Fashion.ERP.Web.Controllers.RelatorioController Relatorio = new Fashion.ERP.Web.Controllers.T4MVC_RelatorioController();
    public static T4MVC.SharedController Shared = new T4MVC.SharedController();
    public static T4MVC.XmlSiteMapController XmlSiteMap = new T4MVC.XmlSiteMapController();
}

namespace T4MVC
{
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class AlmoxarifadoClass
    {
        public readonly string Name = "Almoxarifado";
        public Fashion.ERP.Web.Areas.Almoxarifado.Controllers.CategoriaController Categoria = new Fashion.ERP.Web.Areas.Almoxarifado.Controllers.T4MVC_CategoriaController();
        public Fashion.ERP.Web.Areas.Almoxarifado.Controllers.ConsultaController Consulta = new Fashion.ERP.Web.Areas.Almoxarifado.Controllers.T4MVC_ConsultaController();
        public Fashion.ERP.Web.Areas.Almoxarifado.Controllers.DepositoMaterialController DepositoMaterial = new Fashion.ERP.Web.Areas.Almoxarifado.Controllers.T4MVC_DepositoMaterialController();
        public Fashion.ERP.Web.Areas.Almoxarifado.Controllers.EntradaMaterialController EntradaMaterial = new Fashion.ERP.Web.Areas.Almoxarifado.Controllers.T4MVC_EntradaMaterialController();
        public Fashion.ERP.Web.Areas.Almoxarifado.Controllers.FamiliaController Familia = new Fashion.ERP.Web.Areas.Almoxarifado.Controllers.T4MVC_FamiliaController();
        public Fashion.ERP.Web.Areas.Almoxarifado.Controllers.MarcaMaterialController MarcaMaterial = new Fashion.ERP.Web.Areas.Almoxarifado.Controllers.T4MVC_MarcaMaterialController();
        public Fashion.ERP.Web.Areas.Almoxarifado.Controllers.MaterialController Material = new Fashion.ERP.Web.Areas.Almoxarifado.Controllers.T4MVC_MaterialController();
        public Fashion.ERP.Web.Areas.Almoxarifado.Controllers.SaidaMaterialController SaidaMaterial = new Fashion.ERP.Web.Areas.Almoxarifado.Controllers.T4MVC_SaidaMaterialController();
        public Fashion.ERP.Web.Areas.Almoxarifado.Controllers.SubcategoriaController Subcategoria = new Fashion.ERP.Web.Areas.Almoxarifado.Controllers.T4MVC_SubcategoriaController();
        public Fashion.ERP.Web.Areas.Almoxarifado.Controllers.UnidadeMedidaController UnidadeMedida = new Fashion.ERP.Web.Areas.Almoxarifado.Controllers.T4MVC_UnidadeMedidaController();
        public T4MVC.Almoxarifado.SharedController Shared = new T4MVC.Almoxarifado.SharedController();
    }
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class ComprasClass
    {
        public readonly string Name = "Compras";
        public Fashion.ERP.Web.Areas.Compras.Controllers.AutorizacoesController Autorizacoes = new Fashion.ERP.Web.Areas.Compras.Controllers.T4MVC_AutorizacoesController();
        public Fashion.ERP.Web.Areas.Compras.Controllers.MotivoCancelamentoPedidoCompraController MotivoCancelamentoPedidoCompra = new Fashion.ERP.Web.Areas.Compras.Controllers.T4MVC_MotivoCancelamentoPedidoCompraController();
        public Fashion.ERP.Web.Areas.Compras.Controllers.ParametroModuloCompraController ParametroModuloCompra = new Fashion.ERP.Web.Areas.Compras.Controllers.T4MVC_ParametroModuloCompraController();
        public Fashion.ERP.Web.Areas.Compras.Controllers.PedidoCompraCancelamentoController PedidoCompraCancelamento = new Fashion.ERP.Web.Areas.Compras.Controllers.T4MVC_PedidoCompraCancelamentoController();
        public Fashion.ERP.Web.Areas.Compras.Controllers.PedidoCompraController PedidoCompra = new Fashion.ERP.Web.Areas.Compras.Controllers.T4MVC_PedidoCompraController();
        public Fashion.ERP.Web.Areas.Compras.Controllers.RecebimentoCompraController RecebimentoCompra = new Fashion.ERP.Web.Areas.Compras.Controllers.T4MVC_RecebimentoCompraController();
        public Fashion.ERP.Web.Areas.Compras.Controllers.ValidaPedidoCompraController ValidaPedidoCompra = new Fashion.ERP.Web.Areas.Compras.Controllers.T4MVC_ValidaPedidoCompraController();
        public T4MVC.Compras.OrdemEntradaCompraController OrdemEntradaCompra = new T4MVC.Compras.OrdemEntradaCompraController();
        public T4MVC.Compras.SharedController Shared = new T4MVC.Compras.SharedController();
    }
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class ComumClass
    {
        public readonly string Name = "Comum";
        public Fashion.ERP.Web.Areas.Comum.Controllers.AreaInteresseController AreaInteresse = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_AreaInteresseController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.BancoController Banco = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_BancoController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.CentroCustoController CentroCusto = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_CentroCustoController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.ClassificacaoDificuldadeController ClassificacaoDificuldade = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_ClassificacaoDificuldadeController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.ClienteController Cliente = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_ClienteController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.ColecaoController Colecao = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_ColecaoController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.ContatoController Contato = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_ContatoController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.CorController Cor = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_CorController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.DepartamentoProducaoController DepartamentoProducao = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_DepartamentoProducaoController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.DependenteController Dependente = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_DependenteController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.EmpresaController Empresa = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_EmpresaController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.EnderecoController Endereco = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_EnderecoController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.FornecedorController Fornecedor = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_FornecedorController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.FuncionarioController Funcionario = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_FuncionarioController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.GrauDependenciaController GrauDependencia = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_GrauDependenciaController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.InformacaoBancariaController InformacaoBancaria = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_InformacaoBancariaController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.MarcaController Marca = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_MarcaController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.MeioPagamentoController MeioPagamento = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_MeioPagamentoController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.PerfilDeAcessoController PerfilDeAcesso = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_PerfilDeAcessoController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.PrazoController Prazo = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_PrazoController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.PrestadorServicoController PrestadorServico = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_PrestadorServicoController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.ProfissaoController Profissao = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_ProfissaoController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.ReferenciaController Referencia = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_ReferenciaController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.RelatorioController Relatorio = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_RelatorioController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.TamanhoController Tamanho = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_TamanhoController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.TipoFornecedorController TipoFornecedor = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_TipoFornecedorController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.UnidadeController Unidade = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_UnidadeController();
        public Fashion.ERP.Web.Areas.Comum.Controllers.UsuarioController Usuario = new Fashion.ERP.Web.Areas.Comum.Controllers.T4MVC_UsuarioController();
        public T4MVC.Comum.SharedController Shared = new T4MVC.Comum.SharedController();
    }
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class DevClass
    {
        public readonly string Name = "Dev";
        public Fashion.ERP.Web.Areas.Dev.Controllers.MenuBuilderController MenuBuilder = new Fashion.ERP.Web.Areas.Dev.Controllers.T4MVC_MenuBuilderController();
    }
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class EngenhariaProdutoClass
    {
        public readonly string Name = "EngenhariaProduto";
        public Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.AprovarModeloController AprovarModelo = new Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.T4MVC_AprovarModeloController();
        public Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.ArtigoController Artigo = new Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.T4MVC_ArtigoController();
        public Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.BarraController Barra = new Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.T4MVC_BarraController();
        public Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.ClassificacaoController Classificacao = new Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.T4MVC_ClassificacaoController();
        public Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.ComprimentoController Comprimento = new Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.T4MVC_ComprimentoController();
        public Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.GradeController Grade = new Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.T4MVC_GradeController();
        public Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.MaterialComposicaoModeloController MaterialComposicaoModelo = new Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.T4MVC_MaterialComposicaoModeloController();
        public Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.ModeloController Modelo = new Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.T4MVC_ModeloController();
        public Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.NaturezaController Natureza = new Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.T4MVC_NaturezaController();
        public Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.OperacaoProducaoController OperacaoProducao = new Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.T4MVC_OperacaoProducaoController();
        public Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.ProdutoBaseController ProdutoBase = new Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.T4MVC_ProdutoBaseController();
        public Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.RelatorioController Relatorio = new Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.T4MVC_RelatorioController();
        public Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.SegmentoController Segmento = new Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.T4MVC_SegmentoController();
        public Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.SequenciaProducaoController SequenciaProducao = new Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.T4MVC_SequenciaProducaoController();
        public Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.SetorProducaoController SetorProducao = new Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.T4MVC_SetorProducaoController();
    }
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class FinanceiroClass
    {
        public readonly string Name = "Financeiro";
        public Fashion.ERP.Web.Areas.Financeiro.Controllers.ChequeRecebidoController ChequeRecebido = new Fashion.ERP.Web.Areas.Financeiro.Controllers.T4MVC_ChequeRecebidoController();
        public Fashion.ERP.Web.Areas.Financeiro.Controllers.ContaBancariaController ContaBancaria = new Fashion.ERP.Web.Areas.Financeiro.Controllers.T4MVC_ContaBancariaController();
        public Fashion.ERP.Web.Areas.Financeiro.Controllers.DespesaReceitaController DespesaReceita = new Fashion.ERP.Web.Areas.Financeiro.Controllers.T4MVC_DespesaReceitaController();
        public Fashion.ERP.Web.Areas.Financeiro.Controllers.EmitenteController Emitente = new Fashion.ERP.Web.Areas.Financeiro.Controllers.T4MVC_EmitenteController();
        public Fashion.ERP.Web.Areas.Financeiro.Controllers.ExtratoBancarioController ExtratoBancario = new Fashion.ERP.Web.Areas.Financeiro.Controllers.T4MVC_ExtratoBancarioController();
        public Fashion.ERP.Web.Areas.Financeiro.Controllers.RelatorioController Relatorio = new Fashion.ERP.Web.Areas.Financeiro.Controllers.T4MVC_RelatorioController();
    }
}

namespace T4MVC
{
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class Dummy
    {
        private Dummy() { }
        public static Dummy Instance = new Dummy();
    }
}

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal partial class T4MVC_System_Web_Mvc_JsonResult : System.Web.Mvc.JsonResult, IT4MVCActionResult
{
    public T4MVC_System_Web_Mvc_JsonResult(string area, string controller, string action, string protocol = null): base()
    {
        this.InitMVCT4Result(area, controller, action, protocol);
    }
    
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Protocol { get; set; }
    public RouteValueDictionary RouteValueDictionary { get; set; }
}
[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal partial class T4MVC_System_Web_Mvc_ActionResult : System.Web.Mvc.ActionResult, IT4MVCActionResult
{
    public T4MVC_System_Web_Mvc_ActionResult(string area, string controller, string action, string protocol = null): base()
    {
        this.InitMVCT4Result(area, controller, action, protocol);
    }
     
    public override void ExecuteResult(System.Web.Mvc.ControllerContext context) { }
    
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Protocol { get; set; }
    public RouteValueDictionary RouteValueDictionary { get; set; }
}
[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal partial class T4MVC_System_Web_Mvc_ContentResult : System.Web.Mvc.ContentResult, IT4MVCActionResult
{
    public T4MVC_System_Web_Mvc_ContentResult(string area, string controller, string action, string protocol = null): base()
    {
        this.InitMVCT4Result(area, controller, action, protocol);
    }
    
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Protocol { get; set; }
    public RouteValueDictionary RouteValueDictionary { get; set; }
}



namespace Links
{
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static class Scripts {
        private const string URLPATH = "~/Scripts";
        public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
        public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
        public static readonly string bootstrap_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/bootstrap.min.js") ? Url("bootstrap.min.js") : Url("bootstrap.js");
        public static readonly string bootstrap_min_js = Url("bootstrap.min.js");
        public static readonly string jasny_bootstrap_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jasny-bootstrap.min.js") ? Url("jasny-bootstrap.min.js") : Url("jasny-bootstrap.js");
        public static readonly string jasny_bootstrap_min_js = Url("jasny-bootstrap.min.js");
        public static readonly string jquery_1_10_2_intellisense_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery-1.10.2.intellisense.min.js") ? Url("jquery-1.10.2.intellisense.min.js") : Url("jquery-1.10.2.intellisense.js");
        public static readonly string jquery_1_10_2_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery-1.10.2.min.js") ? Url("jquery-1.10.2.min.js") : Url("jquery-1.10.2.js");
        public static readonly string jquery_1_10_2_min_js = Url("jquery-1.10.2.min.js");
        public static readonly string jquery_1_10_2_min_map = Url("jquery-1.10.2.min.map");
        public static readonly string jquery_migrate_1_2_1_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery-migrate-1.2.1.min.js") ? Url("jquery-migrate-1.2.1.min.js") : Url("jquery-migrate-1.2.1.js");
        public static readonly string jquery_migrate_1_2_1_min_js = Url("jquery-migrate-1.2.1.min.js");
        public static readonly string jquery_color_2_1_2_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.color-2.1.2.min.js") ? Url("jquery.color-2.1.2.min.js") : Url("jquery.color-2.1.2.js");
        public static readonly string jquery_color_2_1_2_min_js = Url("jquery.color-2.1.2.min.js");
        public static readonly string jquery_color_svg_names_2_1_2_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.color.svg-names-2.1.2.min.js") ? Url("jquery.color.svg-names-2.1.2.min.js") : Url("jquery.color.svg-names-2.1.2.js");
        public static readonly string jquery_color_svg_names_2_1_2_min_js = Url("jquery.color.svg-names-2.1.2.min.js");
        public static readonly string jquery_form_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.form.min.js") ? Url("jquery.form.min.js") : Url("jquery.form.js");
        public static readonly string jquery_form_min_js = Url("jquery.form.min.js");
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class jquery_globalize {
            private const string URLPATH = "~/Scripts/jquery.globalize";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class cultures {
                private const string URLPATH = "~/Scripts/jquery.globalize/cultures";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string globalize_culture_pt_BR_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/globalize.culture.pt-BR.min.js") ? Url("globalize.culture.pt-BR.min.js") : Url("globalize.culture.pt-BR.js");
            }
        
            public static readonly string globalize_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/globalize.min.js") ? Url("globalize.min.js") : Url("globalize.js");
        }
    
        public static readonly string jquery_Jcrop_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.Jcrop.min.js") ? Url("jquery.Jcrop.min.js") : Url("jquery.Jcrop.js");
        public static readonly string jquery_Jcrop_min_js = Url("jquery.Jcrop.min.js");
        public static readonly string jquery_unobtrusive_ajax_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.unobtrusive-ajax.min.js") ? Url("jquery.unobtrusive-ajax.min.js") : Url("jquery.unobtrusive-ajax.js");
        public static readonly string jquery_unobtrusive_ajax_min_js = Url("jquery.unobtrusive-ajax.min.js");
        public static readonly string jquery_validate_vsdoc_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.validate-vsdoc.min.js") ? Url("jquery.validate-vsdoc.min.js") : Url("jquery.validate-vsdoc.js");
        public static readonly string jquery_validate_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.validate.min.js") ? Url("jquery.validate.min.js") : Url("jquery.validate.js");
        public static readonly string jquery_validate_min_js = Url("jquery.validate.min.js");
        public static readonly string jquery_validate_unobtrusive_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.validate.unobtrusive.min.js") ? Url("jquery.validate.unobtrusive.min.js") : Url("jquery.validate.unobtrusive.js");
        public static readonly string jquery_validate_unobtrusive_min_js = Url("jquery.validate.unobtrusive.min.js");
        public static readonly string json2_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/json2.min.js") ? Url("json2.min.js") : Url("json2.js");
        public static readonly string json2_min_js = Url("json2.min.js");
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class kendo {
            private const string URLPATH = "~/Scripts/kendo";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class cultures {
                private const string URLPATH = "~/Scripts/kendo/cultures";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string kendo_culture_pt_BR_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/kendo.culture.pt-BR.min.js") ? Url("kendo.culture.pt-BR.min.js") : Url("kendo.culture.pt-BR.js");
            }
        
            public static readonly string kendo_aspnetmvc_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/kendo.aspnetmvc.min.js") ? Url("kendo.aspnetmvc.min.js") : Url("kendo.aspnetmvc.js");
            public static readonly string kendo_web_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/kendo.web.min.js") ? Url("kendo.web.min.js") : Url("kendo.web.js");
        }
    
        public static readonly string lightbox_2_6_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/lightbox-2.6.min.js") ? Url("lightbox-2.6.min.js") : Url("lightbox-2.6.js");
        public static readonly string lightbox_2_6_min_js = Url("lightbox-2.6.min.js");
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class localization {
            private const string URLPATH = "~/Scripts/localization";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly string jquery_ui_datepicker_pt_BR_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.ui.datepicker-pt-BR.min.js") ? Url("jquery.ui.datepicker-pt-BR.min.js") : Url("jquery.ui.datepicker-pt-BR.js");
            public static readonly string messages_pt_BR_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/messages_pt_BR.min.js") ? Url("messages_pt_BR.min.js") : Url("messages_pt_BR.js");
            public static readonly string methods_pt_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/methods_pt.min.js") ? Url("methods_pt.min.js") : Url("methods_pt.js");
        }
    
        public static readonly string modernizr_2_6_2_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/modernizr-2.6.2.min.js") ? Url("modernizr-2.6.2.min.js") : Url("modernizr-2.6.2.js");
        public static readonly string pdfobject_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/pdfobject.min.js") ? Url("pdfobject.min.js") : Url("pdfobject.js");
        public static readonly string pdfobject_min_js = Url("pdfobject.min.js");
        public static readonly string respond_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/respond.min.js") ? Url("respond.min.js") : Url("respond.js");
        public static readonly string respond_min_js = Url("respond.min.js");
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static class Content {
        private const string URLPATH = "~/Content";
        public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
        public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class Bootstrap {
            private const string URLPATH = "~/Content/Bootstrap";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly string editor_png = Url("editor.png");
            public static readonly string imagebrowser_png = Url("imagebrowser.png");
            public static readonly string loading_image_gif = Url("loading-image.gif");
            public static readonly string loading_gif = Url("loading.gif");
            public static readonly string slider_h_gif = Url("slider-h.gif");
            public static readonly string slider_v_gif = Url("slider-v.gif");
            public static readonly string sprite_png = Url("sprite.png");
        }
    
        public static readonly string bootstrap_override_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/bootstrap-override.min.css") ? Url("bootstrap-override.min.css") : Url("bootstrap-override.css");
             
        public static readonly string bootstrap_responsive_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/bootstrap-responsive.min.css") ? Url("bootstrap-responsive.min.css") : Url("bootstrap-responsive.css");
             
        public static readonly string bootstrap_responsive_min_css = Url("bootstrap-responsive.min.css");
        public static readonly string bootstrap_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/bootstrap.min.css") ? Url("bootstrap.min.css") : Url("bootstrap.css");
             
        public static readonly string bootstrap_min_css = Url("bootstrap.min.css");
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class images {
            private const string URLPATH = "~/Content/images";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly string ajax_loader_gif = Url("ajax-loader.gif");
            public static readonly string avatar_unknown_jpg = Url("avatar_unknown.jpg");
            public static readonly string bg_login_jpg = Url("bg-login.jpg");
            public static readonly string glyphicons_halflings_white_png = Url("glyphicons-halflings-white.png");
            public static readonly string glyphicons_halflings_png = Url("glyphicons-halflings.png");
            public static readonly string logo_png = Url("logo.png");
            public static readonly string no_image_jpg = Url("no_image.jpg");
            public static readonly string no_image_report_png = Url("no_image_report.png");
        }
    
        public static readonly string jasny_bootstrap_responsive_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jasny-bootstrap-responsive.min.css") ? Url("jasny-bootstrap-responsive.min.css") : Url("jasny-bootstrap-responsive.css");
             
        public static readonly string jasny_bootstrap_responsive_min_css = Url("jasny-bootstrap-responsive.min.css");
        public static readonly string jasny_bootstrap_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jasny-bootstrap.min.css") ? Url("jasny-bootstrap.min.css") : Url("jasny-bootstrap.css");
             
        public static readonly string Jcrop_gif = Url("Jcrop.gif");
        public static readonly string jquery_Jcrop_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.Jcrop.min.css") ? Url("jquery.Jcrop.min.css") : Url("jquery.Jcrop.css");
             
        public static readonly string jquery_Jcrop_min_css = Url("jquery.Jcrop.min.css");
        public static readonly string kendo_bootstrap_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/kendo.bootstrap.min.css") ? Url("kendo.bootstrap.min.css") : Url("kendo.bootstrap.css");
             
        public static readonly string kendo_bootstrap_min_css = Url("kendo.bootstrap.min.css");
        public static readonly string kendo_common_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/kendo.common.min.css") ? Url("kendo.common.min.css") : Url("kendo.common.css");
             
        public static readonly string kendo_common_min_css = Url("kendo.common.min.css");
        public static readonly string layout_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/layout.min.css") ? Url("layout.min.css") : Url("layout.css");
             
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class Lightbox {
            private const string URLPATH = "~/Content/Lightbox";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly string close_png = Url("close.png");
            public static readonly string loading_gif = Url("loading.gif");
            public static readonly string next_png = Url("next.png");
            public static readonly string prev_png = Url("prev.png");
        }
    
        public static readonly string lightbox_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/lightbox.min.css") ? Url("lightbox.min.css") : Url("lightbox.css");
             
        public static readonly string login_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/login.min.css") ? Url("login.min.css") : Url("login.css");
             
        public static readonly string pick_a_color_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/pick-a-color.min.css") ? Url("pick-a-color.min.css") : Url("pick-a-color.css");
             
        public static readonly string Site_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/Site.min.css") ? Url("Site.min.css") : Url("Site.css");
             
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class textures {
            private const string URLPATH = "~/Content/textures";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly string glass_png = Url("glass.png");
            public static readonly string highlight_png = Url("highlight.png");
        }
    
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class themes {
            private const string URLPATH = "~/Content/themes";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class custom_theme {
                private const string URLPATH = "~/Content/themes/custom-theme";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
                public static class images {
                    private const string URLPATH = "~/Content/themes/custom-theme/images";
                    public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                    public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                    public static readonly string ui_bg_flat_0_aaaaaa_40x100_png = Url("ui-bg_flat_0_aaaaaa_40x100.png");
                    public static readonly string ui_bg_glass_55_fbf9ee_1x400_png = Url("ui-bg_glass_55_fbf9ee_1x400.png");
                    public static readonly string ui_bg_glass_65_ffffff_1x400_png = Url("ui-bg_glass_65_ffffff_1x400.png");
                    public static readonly string ui_bg_glass_75_dadada_1x400_png = Url("ui-bg_glass_75_dadada_1x400.png");
                    public static readonly string ui_bg_glass_75_e6e6e6_1x400_png = Url("ui-bg_glass_75_e6e6e6_1x400.png");
                    public static readonly string ui_bg_glass_75_ffffff_1x400_png = Url("ui-bg_glass_75_ffffff_1x400.png");
                    public static readonly string ui_bg_highlight_soft_75_cccccc_1x100_png = Url("ui-bg_highlight-soft_75_cccccc_1x100.png");
                    public static readonly string ui_bg_inset_soft_95_fef1ec_1x100_png = Url("ui-bg_inset-soft_95_fef1ec_1x100.png");
                    public static readonly string ui_icons_222222_256x240_png = Url("ui-icons_222222_256x240.png");
                    public static readonly string ui_icons_2e83ff_256x240_png = Url("ui-icons_2e83ff_256x240.png");
                    public static readonly string ui_icons_454545_256x240_png = Url("ui-icons_454545_256x240.png");
                    public static readonly string ui_icons_888888_256x240_png = Url("ui-icons_888888_256x240.png");
                    public static readonly string ui_icons_cd0a0a_256x240_png = Url("ui-icons_cd0a0a_256x240.png");
                    public static readonly string ui_icons_f6cf3b_256x240_png = Url("ui-icons_f6cf3b_256x240.png");
                }
            
                public static readonly string jquery_ui_1_8_16_custom_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery-ui-1.8.16.custom.min.css") ? Url("jquery-ui-1.8.16.custom.min.css") : Url("jquery-ui-1.8.16.custom.css");
                     
                public static readonly string jquery_ui_1_8_16_ie_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.ui.1.8.16.ie.min.css") ? Url("jquery.ui.1.8.16.ie.min.css") : Url("jquery.ui.1.8.16.ie.css");
                     
            }
        
        }
    
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static partial class Bundles
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static partial class Scripts {}
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static partial class Styles {}
    }
}

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal static class T4MVCHelpers {
    // You can change the ProcessVirtualPath method to modify the path that gets returned to the client.
    // e.g. you can prepend a domain, or append a query string:
    //      return "http://localhost" + path + "?foo=bar";
    private static string ProcessVirtualPathDefault(string virtualPath) {
        // The path that comes in starts with ~/ and must first be made absolute
        string path = VirtualPathUtility.ToAbsolute(virtualPath);
        
        // Add your own modifications here before returning the path
        return path;
    }

    // Calling ProcessVirtualPath through delegate to allow it to be replaced for unit testing
    public static Func<string, string> ProcessVirtualPath = ProcessVirtualPathDefault;

    // Calling T4Extension.TimestampString through delegate to allow it to be replaced for unit testing and other purposes
    public static Func<string, string> TimestampString = System.Web.Mvc.T4Extensions.TimestampString;

    // Logic to determine if the app is running in production or dev environment
    public static bool IsProduction() { 
        return (HttpContext.Current != null && !HttpContext.Current.IsDebuggingEnabled); 
    }
}





#endregion T4MVC
#pragma warning restore 1591


