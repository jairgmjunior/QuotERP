using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.Financeiro;
using Fashion.ERP.Reporting.Financeiro;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Financeiro.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using NHibernate.Linq;
using Ninject.Extensions.Logging;
using Telerik.Reporting;

namespace Fashion.ERP.Web.Areas.Financeiro.Controllers
{
    public partial class TituloPagarController : BaseController
    {
        #region Variaveis
        private readonly IRepository<TituloPagar> _tituloPagarRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<CentroCusto> _centroCustoRepository;
        private readonly IRepository<DespesaReceita> _despesaReceitaRepository;
        private readonly ILogger _logger;
        public const string ChaveCentroCusto = "EB1EE910-68D5-400D-B34E-520344A2B102";
        public const string ChaveDespesaReceita = "30FC485B-1517-4BB7-AAE2-EA022320CAFB";

        private static readonly Dictionary<string, string> ColunasPesquisaTituloPagar = new Dictionary<string, string>
            {
                {"Data cadastro", "DataCadastro"},
                {"Data emissão", "Emissao"},
                {"Data vencimento", "Vencimento"},
                {"Fornecedor", "Fornecedor.NomeFantasia"},
                {"Número", "Numero"},
                {"Situação", "SituacaoTitulo"},
                {"Unidade", "Unidade.NomeFantasia"},
                {"Valor", "Valor"},
            };
        #endregion

        #region Construtores
        public TituloPagarController(ILogger logger, IRepository<TituloPagar> tituloPagarRepository,
            IRepository<Pessoa> pessoaRepository, IRepository<CentroCusto> centroCustoRepository, IRepository<DespesaReceita> despesaReceitaRepository)
        {
            _tituloPagarRepository = tituloPagarRepository;
            _pessoaRepository = pessoaRepository;
            _centroCustoRepository = centroCustoRepository;
            _despesaReceitaRepository = despesaReceitaRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        [PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index()
        {
            var tituloPagars = _tituloPagarRepository.Find();

            var model = new PesquisaTituloPagarModel { ModoConsulta = "Listar" };

            model.Grid = tituloPagars.Select(p => new GridTituloPagarModel
            {
                Id = p.Id.GetValueOrDefault(),
                UnidadeEstocadora = p.Unidade.Unidade.Codigo,
                Numero = p.Numero,
                Parcela = p.Parcela,
                Plano = p.Plano,
                Fornecedor = p.Fornecedor.NomeFantasia,
                Vencimento = p.Vencimento,
                Valor = p.Valor,
                SaldoDevedor = p.SaldoDevedor,
                SituacaoTitulo = p.SituacaoTitulo.EnumToString()
            }).ToList();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index(PesquisaTituloPagarModel model)
        {
            var titulos = _tituloPagarRepository.Find();

            try
            {
                #region Filtros

                var filtros = new StringBuilder();

                if (model.Unidade.HasValue)
                {
                    titulos = titulos.Where(p => p.Unidade.Id == model.Unidade);
                    filtros.AppendFormat("Unidade estocadora: {0}, ",
                                         _pessoaRepository.Get(model.Unidade.Value).Nome);
                }

                if (string.IsNullOrWhiteSpace(model.Numero) == false)
                {
                    titulos = titulos.Where(p => p.Numero == model.Numero);
                    filtros.AppendFormat("Número: {0}, ", model.Numero);
                }

                if (model.Fornecedor.HasValue)
                {
                    titulos = titulos.Where(p => p.Fornecedor.Id == model.Fornecedor);
                    filtros.AppendFormat("Fornecedor: {0}, ", _pessoaRepository.Get(model.Fornecedor.Value).Nome);
                }

                if (model.SituacaoTitulo.HasValue)
                {
                    titulos = titulos.Where(p => p.SituacaoTitulo == model.SituacaoTitulo);
                    filtros.AppendFormat("Situação: {0}, ", model.SituacaoTitulo.Value.EnumToString());
                }

                if (model.DataEmissaoInicio.HasValue && !model.DataEmissaoFim.HasValue)
                {
                    titulos = titulos.Where(p => p.Emissao.Date >= model.DataEmissaoInicio.Value);

                    filtros.AppendFormat("Data de emissão de '{0}', ", model.DataEmissaoInicio.Value.ToString("dd/MM/yyyy"));
                }

                if (!model.DataEmissaoInicio.HasValue && model.DataEmissaoFim.HasValue)
                {
                    titulos = titulos.Where(p => p.Emissao.Date <= model.DataEmissaoFim.Value);

                    filtros.AppendFormat("Data de emissão até '{0}', ", model.DataEmissaoFim.Value.ToString("dd/MM/yyyy"));
                }

                if (model.DataEmissaoInicio.HasValue && model.DataEmissaoFim.HasValue)
                {
                    titulos = titulos.Where(p => p.Emissao.Date >= model.DataEmissaoInicio.Value
                                                             && p.Emissao.Date <= model.DataEmissaoFim.Value);
                    filtros.AppendFormat("Data emissão de '{0}' até '{1}', ",
                                         model.DataEmissaoInicio.Value.ToString("dd/MM/yyyy"),
                                         model.DataEmissaoFim.Value.ToString("dd/MM/yyyy"));
                }

                if (model.DataCadastroInicio.HasValue && !model.DataCadastroFim.HasValue)
                {
                    titulos = titulos.Where(p => p.DataCadastro.Date >= model.DataCadastroInicio.Value);

                    filtros.AppendFormat("Data de cadastro de '{0}', ", model.DataCadastroInicio.Value.ToString("dd/MM/yyyy"));
                }

                if (!model.DataCadastroInicio.HasValue && model.DataCadastroFim.HasValue)
                {
                    titulos = titulos.Where(p => p.DataCadastro.Date <= model.DataCadastroFim.Value);

                    filtros.AppendFormat("Data de cadastro até '{0}', ", model.DataCadastroFim.Value.ToString("dd/MM/yyyy"));
                }

                if (model.DataCadastroInicio.HasValue && model.DataCadastroFim.HasValue)
                {
                    titulos = titulos.Where(p => p.DataCadastro.Date >= model.DataCadastroInicio.Value
                                                             && p.DataCadastro.Date <= model.DataCadastroFim.Value);
                    filtros.AppendFormat("Data de cadastro de '{0}' até '{1}', ",
                                         model.DataCadastroInicio.Value.ToString("dd/MM/yyyy"),
                                         model.DataCadastroFim.Value.ToString("dd/MM/yyyy"));
                }

                if (model.DataVencimentoInicio.HasValue && !model.DataVencimentoFim.HasValue)
                {
                    titulos = titulos.Where(p => p.Vencimento.Date >= model.DataVencimentoInicio.Value);

                    filtros.AppendFormat("Data de vencimento de '{0}', ", model.DataVencimentoInicio.Value.ToString("dd/MM/yyyy"));
                }

                if (!model.DataVencimentoInicio.HasValue && model.DataVencimentoFim.HasValue)
                {
                    titulos = titulos.Where(p => p.Vencimento.Date <= model.DataVencimentoFim.Value);

                    filtros.AppendFormat("Data de vencimento até '{0}', ", model.DataVencimentoFim.Value.ToString("dd/MM/yyyy"));
                }

                if (model.DataVencimentoInicio.HasValue && model.DataVencimentoFim.HasValue)
                {
                    titulos = titulos.Where(p => p.Vencimento.Date >= model.DataVencimentoInicio.Value
                                                             && p.Vencimento.Date <= model.DataVencimentoFim.Value);
                    filtros.AppendFormat("Data de vencimento de '{0}' até '{1}', ",
                                         model.DataVencimentoInicio.Value.ToString("dd/MM/yyyy"),
                                         model.DataVencimentoFim.Value.ToString("dd/MM/yyyy"));
                }

                if (model.ValorInicio.HasValue && model.ValorFim.HasValue)
                {
                    titulos = titulos.Where(p => p.Valor >= model.ValorInicio.Value
                                                             && p.Valor <= model.ValorFim.Value);
                    filtros.AppendFormat("Valor de '{0}' até '{1}', ",
                                         model.ValorInicio.Value.ToString("C2"),
                                         model.ValorFim.Value.ToString("C2"));
                }

                #endregion

                // Verifica se é uma listagem
                if (model.ModoConsulta == "Listar")
                {
                    if (model.OrdenarPor != null)
                        titulos = model.OrdenarEm == "asc"
                                            ? titulos.OrderBy(model.OrdenarPor)
                                            : titulos.OrderByDescending(model.OrdenarPor);

                    model.Grid = titulos.Select(p => new GridTituloPagarModel
                    {
                        Id = p.Id.GetValueOrDefault(),
                        Numero = p.Numero,
                        Parcela = p.Parcela,
                        Plano = p.Plano,
                        Fornecedor = p.Fornecedor.NomeFantasia,
                        Vencimento = p.Vencimento,
                        Valor = p.Valor,
                        SaldoDevedor = p.SaldoDevedor,
                        SituacaoTitulo = p.SituacaoTitulo.EnumToString()
                    }).ToList();

                    return View(model);
                }

                // Se não é uma listagem, gerar o relatório
                var result = titulos
                    .Fetch(p => p.Fornecedor).Fetch(p => p.Banco)
                    .ToList();

                if (!result.Any())
                    return Json(new { Error = "Nenhum item encontrado." });

                #region Montar Relatório

                var report = new ListaTituloPagarReport { DataSource = result };

                if (filtros.Length > 2)
                    report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

                var grupo = report.Groups.First(p => p.Name.Equals("Grupo"));

                if (model.AgruparPor != null)
                {
                    grupo.Groupings.Add("=Fields." + model.AgruparPor);

                    var key = ColunasPesquisaTituloPagar.First(p => p.Value == model.AgruparPor).Key;
                    var titulo = string.Format("= \"{0}: \" + Fields.{1}", key, model.AgruparPor);
                    grupo.GroupHeader.GetTextBox("Titulo").Value = titulo;
                }
                else
                {
                    report.Groups.Remove(grupo);
                }

                if (model.OrdenarPor != null)
                    report.Sortings.Add("=Fields." + model.OrdenarPor,
                                        model.OrdenarEm == "asc" ? SortDirection.Asc : SortDirection.Desc);

                #endregion

                var filename = report.ToByteStream().SaveFile(".pdf");

                return Json(new { Url = filename });
            }
            catch (Exception exception)
            {
                var message = exception.GetMessage();
                _logger.Info(message);

                if (HttpContext.Request.IsAjaxRequest())
                    return Json(new { Error = message });

                ModelState.AddModelError(string.Empty, message);
                return View(model);
            }
        }

        #endregion

        #region Novo

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            TempData.Remove(ChaveCentroCusto);
            TempData.Remove(ChaveDespesaReceita);

            var model = new TituloPagarModel
            {
                SituacaoTitulo = SituacaoTitulo.NaoLiquidado,
                SaldoDevedor = 0
            };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(TituloPagarModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<TituloPagar>(model);
                    domain.DataCadastro = domain.DataAlteracao = DateTime.Now;

                    // Itens do rateio por centro de custo
                    foreach (var centroCustoModel in model.CentroCustos)
                    {
                        var rateioCentroCusto = new RateioCentroCusto
                        {
                            CentroCusto = _centroCustoRepository.Load(Convert.ToInt64(centroCustoModel.CentroCustoId)),
                            Valor = centroCustoModel.ValorCentroCusto
                        };

                        domain.AddRateioCentroCusto(rateioCentroCusto);
                    }

                    // Itens do rateio por tipo de despesa
                    foreach (var despesaReceitaModel in model.DespesaReceitas)
                    {
                        var rateioDespesaReceita = new RateioDespesaReceita
                        {
                            DespesaReceita = _despesaReceitaRepository.Load(Convert.ToInt64(despesaReceitaModel.DespesaReceitaId)),
                            Valor = despesaReceitaModel.ValorDespesaReceita
                        };

                        domain.AddRateioDespesaReceita(rateioDespesaReceita);
                    }

                    _tituloPagarRepository.Save(domain);

                    this.AddSuccessMessage("Título a pagar cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o título a pagar. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }
        #endregion

        #region Editar

        [ImportModelStateFromTempData, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(long id)
        {
            var domain = _tituloPagarRepository.Get(id);

            if (domain != null)
            {

                if (domain.SituacaoTitulo == SituacaoTitulo.Liquidado ||
                    domain.SituacaoTitulo == SituacaoTitulo.Cancelado)
                {
                    this.AddErrorMessage("Não é possível alterar um título Liquidado ou Cancelado.");
                    return RedirectToAction("Index");
                }

                var model = Mapper.Flat<TituloPagarModel>(domain);

                model.CentroCustos = domain.RateioCentroCustos.Select(p => new TituloPagarCentroCustoModel()
                {
                    TituloPagarId = domain.Id,
                    Id = p.Id,
                    CentroCustoId = p.CentroCusto.Id.ToString(),
                    RateioCentroCusto = p.Valor * 100 / domain.Valor,
                    ValorCentroCusto = p.Valor
                }).ToList();

                model.DespesaReceitas = domain.RateioDespesaReceitas.Select(p => new TituloPagarDespesaReceitaModel()
                {
                    TituloPagarId = domain.Id,
                    Id = p.Id,
                    DespesaReceitaId = p.DespesaReceita.Id.ToString(),
                    RateioDespesaReceita = p.Valor * 100 / domain.Valor,
                    ValorDespesaReceita = p.Valor
                }).ToList();

                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o título a pagar.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(TituloPagarModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _tituloPagarRepository.Get(model.Id));
                    domain.DataAlteracao = DateTime.Now;

                    // Itens do rateio por centro de custo
                    domain.ClearRateioCentroCusto();
                    foreach (var centroCustoModel in model.CentroCustos)
                    {
                        var rateioCentroCusto = new RateioCentroCusto
                        {
                            CentroCusto = _centroCustoRepository.Load(Convert.ToInt64(centroCustoModel.CentroCustoId)),
                            Valor = centroCustoModel.ValorCentroCusto
                        };

                        domain.AddRateioCentroCusto(rateioCentroCusto);
                    }

                    // Itens do rateio por tipo de despesa
                    domain.ClearRateioDespesaReceita();
                    foreach (var despesaReceitaModel in model.DespesaReceitas)
                    {
                        var rateioDespesaReceita = new RateioDespesaReceita
                        {
                            DespesaReceita = _despesaReceitaRepository.Load(Convert.ToInt64(despesaReceitaModel.DespesaReceitaId)),
                            Valor = despesaReceitaModel.ValorDespesaReceita
                        };

                        domain.AddRateioDespesaReceita(rateioDespesaReceita);
                    }

                    _tituloPagarRepository.Update(domain);

                    this.AddSuccessMessage("Título a pagar atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o título a pagar. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }
        #endregion

        #region Excluir

        [HttpPost, ValidateAntiForgeryToken, ExportModelStateToTempData]
        public virtual ActionResult Excluir(long? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _tituloPagarRepository.Get(id);
                    _tituloPagarRepository.Delete(domain);

                    this.AddSuccessMessage("Título a pagar excluído com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao excluir o título a pagar: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { id });
        }
        #endregion

        #region Baixar

        public virtual ActionResult Baixar(long id)
        {
            var domain = _tituloPagarRepository.Get(id);

            if (domain != null)
            {
                var model = new BaixaTituloPagarModel
                {
                    Id = domain.Id.GetValueOrDefault(),
                    Banco = domain.Banco.Nome,
                    Fornecedor = domain.Fornecedor.NomeFantasia,
                    Historico = domain.Historico,
                    NumeroParcela = string.Format("{0}/{1}", domain.Numero, domain.Parcela),
                    Observacao = domain.Observacao,
                    Plano = domain.Plano.ToString(CultureInfo.InvariantCulture),
                    SituacaoTitulo = domain.SituacaoTitulo.EnumToString(),
                    Unidade = domain.Unidade.NomeFantasia,
                    Valor = domain.Valor,
                    Vencimento = domain.Vencimento.ToString("dd/MM/yyyy")
                };

                foreach (var tituloPagarBaixa in domain.TituloPagarBaixas)
                {
                    var baixaTitulo = new BaixaItemTituloPagarModel
                    {
                        Id = tituloPagarBaixa.Id,                        
                        DataPagamentoString = tituloPagarBaixa.DataPagamento.ToString("dd/MM/yyyy"),
                        Desconto = tituloPagarBaixa.Descontos,
                        Despesa = tituloPagarBaixa.Despesas,
                        Juro = tituloPagarBaixa.Juros,
                        TituloPagarId = tituloPagarBaixa.TituloPagar.Id.GetValueOrDefault(),
                        ValorBaixa = tituloPagarBaixa.Valor,
                        ValorTotal =
                            tituloPagarBaixa.Valor + tituloPagarBaixa.Juros + tituloPagarBaixa.Despesas -
                            tituloPagarBaixa.Descontos
                    };
                    model.BaixaTitulos.Add(baixaTitulo);
                }

                return View("Baixar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o título a pagar.");
            return RedirectToAction("Index");
        }

        #endregion

        #region Provisionar

        [HttpPost, ValidateAntiForgeryToken, ExportModelStateToTempData]
        public virtual ActionResult Provisionar(long? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var domain = _tituloPagarRepository.Get(id);
                    //_tituloPagarRepository.Delete(domain);

                    this.AddSuccessMessage("Título a pagar provisionado com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao provisionado o título a pagar: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { id });
        }
        #endregion

        #endregion

        #region Métodos

        #region PopulateViewData
        protected void PopulateViewData(TituloPagarModel model)
        {
            // Unidade
            var unidades = _pessoaRepository.Find(p => p.Unidade != null && p.Unidade.Ativo).ToList();
            ViewBag.Unidade = unidades.ToSelectList("NomeFantasia", model.Unidade);

            // Centro de custo
            var centroCustos = _centroCustoRepository.Find(p => p.Ativo).OrderBy(p => p.Nome).ToList();
            ViewBag.CentroCustos = centroCustos.Select(s => new { s.Id, s.Nome });
            ViewBag.CentroCustosDictionary = centroCustos.ToDictionary(k => k.Id, e => e.Nome);

            // DespesaReceita
            var despesaReceitas = _despesaReceitaRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewBag.DespesaReceitas = despesaReceitas.Select(s => new { s.Id, s.Descricao });
            ViewBag.DespesaReceitasDictionary = despesaReceitas.ToDictionary(k => k.Id, e => e.Descricao);
        }
        #endregion

        #region PopulateViewDataPesquisa
        protected void PopulateViewDataPesquisa(PesquisaTituloPagarModel model)
        {
            // UnidadeEstocadora
            var unidades = _pessoaRepository.Find(p => p.Unidade != null && p.Unidade.Ativo).ToList();
            ViewBag.Unidade = unidades.ToSelectList("NomeFantasia", model.Unidade);

            ViewBag.OrdenarPor = new SelectList(ColunasPesquisaTituloPagar, "value", "key");
            ViewBag.AgruparPor = new SelectList(ColunasPesquisaTituloPagar, "value", "key");
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var tituloPagar = (TituloPagarModel)model;

            // Validar número único
            if (_tituloPagarRepository.Find(p => p.Numero == tituloPagar.Numero && p.Parcela == tituloPagar.Parcela && p.Unidade.Id == tituloPagar.Unidade && p.Id != tituloPagar.Id).Any())
                ModelState.AddModelError("Numero", "Já existe um título cadastrado com este número e parcela para esta unidade estocadora.");

            // Centro de custos
            if (!tituloPagar.CentroCustos.Any())
                ModelState.AddModelError("", "É preciso informar o rateio por centro de custo.");
            else if (tituloPagar.Valor != tituloPagar.CentroCustos.Sum(p => p.ValorCentroCusto))
                ModelState.AddModelError("", "A soma do rateio por centro de custo deve ser igual ao valor do título.");

            // Tipo de despesas
            if (!tituloPagar.DespesaReceitas.Any())
                ModelState.AddModelError("", "É preciso informar o rateio por tipo de despesa.");
            else if (tituloPagar.Valor != tituloPagar.DespesaReceitas.Sum(p => p.ValorDespesaReceita))
                ModelState.AddModelError("", "A soma do rateio por tipo de despesa deve ser igual ao valor do título.");

            if (tituloPagar.Prorrogacao.HasValue == false)
                tituloPagar.Prorrogacao = tituloPagar.Vencimento;
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
        }
        #endregion

        #region CentroCusto

        #region LerCentroCustos
        [AjaxOnly, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual ActionResult LerCentroCustos([DataSourceRequest]DataSourceRequest request, long? id)
        {
            //var centroCustos = TempData.Peek(ChaveCentroCusto) as List<TituloPagarCentroCustoModel>;

            //if (id.HasValue)
            //{
            //    var domain = _tituloPagarRepository.Get(id);
            //    var centroCustos = domain.RateioCentroCustos.Select(p => new TituloPagarCentroCustoModel
            //    {
            //        Id = p.Id,
            //        TituloPagarId = p.TituloPagar.Id,
            //        CentroCustoId = p.CentroCusto.Id,
            //        NomeCentroCusto = p.CentroCusto.Nome,
            //        ValorCentroCusto = p.Valor,
            //        RateioCentroCusto = p.Valor * 100 / p.TituloPagar.Valor
            //    });
            //    return Json(centroCustos.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            //}

            //if (centroCustos != null)
            //    return Json(centroCustos.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpPost]
        public virtual ActionResult SalveCentroCusto([DataSourceRequest] DataSourceRequest request, TituloPagarCentroCustoModel centroCusto)
        {
            var results = new List<TituloPagarCentroCustoModel>();

            if (centroCusto != null && ModelState.IsValid)
            {
                try
                {
                    var centroCustos = TempData.Peek(ChaveCentroCusto) as List<TituloPagarCentroCustoModel>;

                    // Adicionar o centro de custo a lista
                    centroCustos.Add(centroCusto);

                    // todo: calcular rateio

                    this.AddSuccessMessage("Centro de custos atualizados com sucesso.");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Ocorreu um erro ao salvar os centros de custos. Confira se os dados foram informados corretamente.");
                    _logger.Info(exception.GetMessage());
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult AtualizeCentroCusto([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<TituloPagarCentroCustoModel> centroCustos)
        {
            //não faz nada, necessário porque a grid do kendo obriga a existência do método para batch updates.
            return Json(centroCustos.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult ExcluaCentroCusto([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<TituloPagarCentroCustoModel> centroCustos)
        {
            //não faz nada, necessário porque a grid do kendo obriga a existência do método para batch updates.
            return Json(centroCustos.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region BaixaTituloAjax

        #region SalveBaixaTitulo
        [HttpPost]
        public virtual ActionResult SalveBaixaTitulo([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<BaixaItemTituloPagarModel> baixaTitulos)
        {
            var baixas = baixaTitulos.ToList();

            if (ModelState.IsValid)
            {
                try
                {
                    var tituloPagar = _tituloPagarRepository.Get(baixas.First().TituloPagarId);
                    foreach (var baixa in baixas)
                    {
                        tituloPagar.AddTituloPagarBaixa(new TituloPagarBaixa
                        {
                            DataAlteracao = tituloPagar.DataAlteracao,
                            DataPagamento = Convert.ToDateTime(baixa.DataPagamentoString),
                            Descontos = baixa.Desconto.GetValueOrDefault(),
                            Despesas = baixa.Despesa.GetValueOrDefault(),
                            Historico = tituloPagar.Historico,
                            Juros = baixa.Juro.GetValueOrDefault(),
                            Observacao = tituloPagar.Observacao,
                            NumeroBaixa = 1,
                            Valor = baixa.ValorBaixa.GetValueOrDefault()
                        });
                    }

                    AtualizeTituloPagar(tituloPagar);
                    _tituloPagarRepository.Update(tituloPagar);

                    this.AddSuccessMessage("Baixa de título atualizado com sucesso.");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Ocorreu um erro ao salvar a baixa de título. Confira se os dados foram informados corretamente.");
                    _logger.Info(exception.GetMessage());
                }
            }

            return Json(baixas.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region AtualizeBaixaTitulo
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult AtualizeBaixaTitulo([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<BaixaItemTituloPagarModel> baixaTitulos)
        {
            var baixasAtualizar = baixaTitulos.ToList();

            if (ModelState.IsValid)
            {
                try
                {
                    var tituloPagar = _tituloPagarRepository.Get(baixasAtualizar.First().TituloPagarId);
                    
                    foreach (var baixaAtualizarModel in baixasAtualizar)
                    {
                        var baixaAtualizar = tituloPagar.TituloPagarBaixas.First(x => x.Id == baixaAtualizarModel.Id);

                        baixaAtualizar.DataAlteracao = tituloPagar.DataAlteracao;
                        baixaAtualizar.DataPagamento = Convert.ToDateTime(baixaAtualizarModel.DataPagamentoString);
                        baixaAtualizar.Descontos = baixaAtualizarModel.Desconto.GetValueOrDefault();
                        baixaAtualizar.Despesas = baixaAtualizarModel.Despesa.GetValueOrDefault();
                        baixaAtualizar.Historico = tituloPagar.Historico;
                        baixaAtualizar.Juros = baixaAtualizarModel.Juro.GetValueOrDefault();
                        baixaAtualizar.Observacao = tituloPagar.Observacao;
                        baixaAtualizar.Valor = baixaAtualizarModel.ValorBaixa.GetValueOrDefault();
                    }

                    AtualizeTituloPagar(tituloPagar);
                    _tituloPagarRepository.Update(tituloPagar);

                    this.AddSuccessMessage("Baixa de título atualizado com sucesso.");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Ocorreu um erro ao atualizar a baixa de título. Confira se os dados foram informados corretamente.");
                    _logger.Info(exception.GetMessage());
                }
            }

            return Json(baixasAtualizar.ToDataSourceResult(request, ModelState));
        }

        public void AtualizeTituloPagar(TituloPagar tituloPagar)
        {
            tituloPagar.DataAlteracao = DateTime.Now;

            var soma = tituloPagar.TituloPagarBaixas.Sum(p => p.Valor);
            tituloPagar.SaldoDevedor = tituloPagar.Valor - soma;

            if (tituloPagar.TituloPagarBaixas.Count == 0)
            {
                tituloPagar.SituacaoTitulo = SituacaoTitulo.NaoLiquidado;
            }
            else if (Math.Abs(soma - tituloPagar.Valor) < double.Epsilon)
            {
                tituloPagar.SituacaoTitulo = SituacaoTitulo.Liquidado;
            }
            else
            {
                tituloPagar.SituacaoTitulo = SituacaoTitulo.LiquidadoParcial;
            }
        }
        #endregion

        #region ExcluaBaixaTitulo
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult ExcluaBaixaTitulo([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<BaixaItemTituloPagarModel> baixaTitulos)
        {
            var baixasExcluir = baixaTitulos.ToList();
            try
            {
                var tituloPagar = _tituloPagarRepository.Get(baixasExcluir.First().TituloPagarId);
                
                foreach (var baixaExcluirModel in baixasExcluir)
                {
                    var baixaExcluir = tituloPagar.TituloPagarBaixas.First(x => x.Id == baixaExcluirModel.Id);

                    tituloPagar.RemoveTituloPagarBaixa(baixaExcluir);
                }

                AtualizeTituloPagar(tituloPagar);
                _tituloPagarRepository.Update(tituloPagar);

                this.AddSuccessMessage("Baixa de título excluído com sucesso.");
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty,
                    "Ocorreu um erro ao excluir a baixa de título. Confira se os dados foram informados corretamente.");
                _logger.Info(exception.GetMessage());
            }

            return Json(baixasExcluir.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #endregion

        #endregion
    }
}