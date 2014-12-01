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
    public partial class TituloReceberController : BaseController
    {
		#region Variaveis
        private readonly IRepository<TituloReceber> _tituloReceberRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly ILogger _logger;
        private static readonly Dictionary<string, string> ColunasPesquisaTituloReceber = new Dictionary<string, string>
            {
                {"Data cadastro", "DataCadastro"},
                {"Data emissão", "Emissao"},
                {"Data vencimento", "Vencimento"},
                {"Cliente", "Cliente.NomeFantasia"},
                {"Número", "Numero"},
                {"Situação", "SituacaoTitulo"},
                {"Unidade", "Unidade.NomeFantasia"},
                {"Valor", "Valor"},
            };
        #endregion

        #region Construtores
        public TituloReceberController(ILogger logger, IRepository<TituloReceber> tituloReceberRepository,
            IRepository<Pessoa> pessoaRepository)
        {
            _tituloReceberRepository = tituloReceberRepository;
            _pessoaRepository = pessoaRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index

        [PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index()
        {
            var tituloRecebers = _tituloReceberRepository.Find();

            var model = new PesquisaTituloReceberModel { ModoConsulta = "Listar" };

            model.Grid = tituloRecebers.Select(p => new GridTituloReceberModel
            {
                Id = p.Id.GetValueOrDefault(),
                UnidadeEstocadora = p.Unidade.Unidade.Codigo,
                Numero = p.Numero,
                Plano = p.Plano,
                Cliente = p.Cliente.NomeFantasia,
                Valor = p.Valor,
                SaldoDevedor = p.SaldoDevedor,
                SituacaoTitulo = p.SituacaoTitulo.EnumToString()
            }).ToList();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index(PesquisaTituloReceberModel model)
        {
            var titulos = _tituloReceberRepository.Find();

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

                if (model.Cliente.HasValue)
                {
                    titulos = titulos.Where(p => p.Cliente.Id == model.Cliente);
                    filtros.AppendFormat("Cliente: {0}, ", _pessoaRepository.Get(model.Cliente.Value).Nome);
                }

                if (model.SituacaoTitulo.HasValue)
                {
                    titulos = titulos.Where(p => p.SituacaoTitulo == model.SituacaoTitulo);
                    filtros.AppendFormat("Situação: {0}, ", model.SituacaoTitulo.Value.EnumToString());
                }

                if (model.DataEmissaoInicio.HasValue && model.DataEmissaoFim.HasValue)
                {
                    titulos = titulos.Where(p => p.Emissao.Date >= model.DataEmissaoInicio.Value
                                                             && p.Emissao.Date <= model.DataEmissaoFim.Value);
                    filtros.AppendFormat("Data emissão de '{0}' até '{1}', ",
                                         model.DataEmissaoInicio.Value.ToString("dd/MM/yyyy"),
                                         model.DataEmissaoFim.Value.ToString("dd/MM/yyyy"));
                }

                if (model.DataCadastroInicio.HasValue && model.DataCadastroFim.HasValue)
                {
                    titulos = titulos.Where(p => p.DataCadastro.Date >= model.DataCadastroInicio.Value
                                                             && p.DataCadastro.Date <= model.DataCadastroFim.Value);
                    filtros.AppendFormat("Data cadastro de '{0}' até '{1}', ",
                                         model.DataCadastroInicio.Value.ToString("dd/MM/yyyy"),
                                         model.DataCadastroFim.Value.ToString("dd/MM/yyyy"));
                }

                if (model.DataVencimentoInicio.HasValue && model.DataVencimentoFim.HasValue)
                {
                    titulos = titulos.Where(p => p.Vencimento.Date >= model.DataVencimentoInicio.Value
                                                             && p.Vencimento.Date <= model.DataVencimentoFim.Value);
                    filtros.AppendFormat("Data vencimento de '{0}' até '{1}', ",
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

                    model.Grid = titulos.Select(p => new GridTituloReceberModel
                    {
                        Id = p.Id.GetValueOrDefault(),
                        Numero = p.Numero,
                        Plano = p.Plano,
                        Cliente = p.Cliente.NomeFantasia,
                        Valor = p.Valor,
                        SaldoDevedor = p.SaldoDevedor,
                        SituacaoTitulo = p.SituacaoTitulo.EnumToString()
                    }).ToList();

                    return View(model);
                }

                // Se não é uma listagem, gerar o relatório
                var result = titulos.Fetch(p => p.Cliente).ToList();

                if (!result.Any())
                    return Json(new { Error = "Nenhum item encontrado." });

                #region Montar Relatório

                var report = new ListaTituloReceberReport { DataSource = result };

                if (filtros.Length > 2)
                    report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

                var grupo = report.Groups.First(p => p.Name.Equals("Grupo"));

                if (model.AgruparPor != null)
                {
                    grupo.Groupings.Add("=Fields." + model.AgruparPor);

                    var key = ColunasPesquisaTituloReceber.First(p => p.Value == model.AgruparPor).Key;
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
            var model = new TituloReceberModel
            {
                SituacaoTitulo = SituacaoTitulo.NaoLiquidado,
                SaldoDevedor = 0,
                ValorTotal = 0
            };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(TituloReceberModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<TituloReceber>(model);
                    domain.DataCadastro = domain.DataAlteracao = DateTime.Now;
                    domain.OrigemTituloReceber = OrigemTituloReceber.Cadastro;

                    _tituloReceberRepository.Save(domain);

                    this.AddSuccessMessage("Título a receber cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o título a receber. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _tituloReceberRepository.Get(id);

            if (domain != null)
            {
                if (domain.SituacaoTitulo == SituacaoTitulo.Liquidado ||
                    domain.SituacaoTitulo == SituacaoTitulo.Cancelado)
                {
                    this.AddErrorMessage("Não é possível alterar um título Liquidado ou Cancelado.");
                    return RedirectToAction("Index");
                }

                var model = Mapper.Flat<TituloReceberModel>(domain);
                model.ValorTotal = model.Valor + model.ValorDespesas;

                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o título a receber.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(TituloReceberModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _tituloReceberRepository.Get(model.Id));
                    domain.DataAlteracao = DateTime.Now;

                    _tituloReceberRepository.Update(domain);

                    this.AddSuccessMessage("Título a receber atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o título a receber. Confira se os dados foram informados corretamente: " + exception.Message);
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
					var domain = _tituloReceberRepository.Get(id);
					_tituloReceberRepository.Delete(domain);

                    this.AddSuccessMessage("Título a receber excluído com sucesso");
					return RedirectToAction("Index");
				}
				catch (Exception exception)
				{
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao excluir o título a receber: " + exception.Message);
					_logger.Info(exception.GetMessage());
				}
			}

			return RedirectToAction("Editar", new { id });
        }
        #endregion

        #region Baixar

        public virtual ActionResult Baixar(long id)
        {
            var domain = _tituloReceberRepository.Get(id);

            if (domain != null)
            {
                var model = new BaixaTituloReceberModel
                {
                    Id = domain.Id.GetValueOrDefault(),
                    Banco = domain.Banco.Nome,
                    Cliente = domain.Cliente.NomeFantasia,
                    Historico = domain.Historico,
                    NumeroParcela = string.Format("{0}/{1}", domain.Numero, domain.Parcela),
                    Observacao = domain.Observacao,
                    Plano = domain.Plano.ToString(CultureInfo.InvariantCulture),
                    SituacaoTitulo = domain.SituacaoTitulo.EnumToString(),
                    Unidade = domain.Unidade.NomeFantasia,
                    Valor = domain.Valor,
                    Vencimento = domain.Vencimento.ToString("dd/MM/yyyy")
                };

                foreach (var tituloReceberBaixa in domain.TituloReceberBaixas)
                {
                    var baixaTitulo = new BaixaItemTituloReceberModel
                    {
                        Id = tituloReceberBaixa.Id,
                        Desconto = tituloReceberBaixa.Descontos,
                        Despesa = tituloReceberBaixa.Despesas,
                        Juro = tituloReceberBaixa.Juros,
                        TituloReceberId = tituloReceberBaixa.TituloReceber.Id.GetValueOrDefault(),
                        ValorBaixa = tituloReceberBaixa.ValorBaixa,
                        ValorTotal =
                            tituloReceberBaixa.ValorBaixa + tituloReceberBaixa.Juros + tituloReceberBaixa.Despesas -
                            tituloReceberBaixa.Descontos
                    };
                    model.BaixaTitulos.Add(baixaTitulo);
                }

                return View("Baixar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o título a receber.");
            return RedirectToAction("Index");
        }

        #endregion
		
        #endregion

		#region Métodos
		
        #region PopulateViewData
        protected void PopulateViewData(TituloReceberModel model)
        {
            // Unidade
            var unidades = _pessoaRepository.Find(p => p.Unidade != null && p.Unidade.Ativo).ToList();
            ViewBag.Unidade = unidades.ToSelectList("NomeFantasia", model.Unidade);
        }
        #endregion

        #region PopulateViewDataPesquisa
        protected void PopulateViewDataPesquisa(PesquisaTituloReceberModel model)
        {
            // UnidadeEstocadora
            var unidades = _pessoaRepository.Find(p => p.Unidade != null && p.Unidade.Ativo).ToList();
            ViewBag.Unidade = unidades.ToSelectList("NomeFantasia", model.Unidade);

            ViewBag.OrdenarPor = new SelectList(ColunasPesquisaTituloReceber, "value", "key");
            ViewBag.AgruparPor = new SelectList(ColunasPesquisaTituloReceber, "value", "key");
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var tituloReceber = (TituloReceberModel)model;

            // Validar número único
            if (_tituloReceberRepository.Find(p => p.Numero == tituloReceber.Numero && p.Parcela == tituloReceber.Parcela && p.Unidade.Id == tituloReceber.Unidade && p.Id != tituloReceber.Id).Any())
                ModelState.AddModelError("Numero", "Já existe um título cadastrado com este número e parcela para esta unidade estocadora.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
        }
        #endregion

        #region BaixaTituloAjax

        #region SalveBaixaTitulo
        [HttpPost]
        public virtual ActionResult SalveBaixaTitulo([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<BaixaItemTituloReceberModel> baixaTitulos)
        {
            var baixas = baixaTitulos.ToList();

            if (baixaTitulos != null && ModelState.IsValid)
            {
                try
                {
                    var tituloReceber = _tituloReceberRepository.Get(baixas.First().TituloReceberId);
                    
                    foreach (var baixa in baixas)
                    {
                        tituloReceber.AddTituloReceberBaixa(new TituloReceberBaixa
                        {
                            DataRecebimento = baixa.DataRecebimento,
                            Descontos = baixa.Desconto,
                            Despesas = baixa.Despesa,
                            Historico = tituloReceber.Historico,
                            Juros = baixa.Juro,
                            Observacao = tituloReceber.Observacao,
                            NumeroBaixa = 1,
                            ValorBaixa = baixa.ValorBaixa
                        });
                    }

                    AtualizeTituloReceber(tituloReceber);
                    _tituloReceberRepository.Update(tituloReceber);

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
        public virtual ActionResult AtualizeBaixaTitulo([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<BaixaItemTituloReceberModel> baixaTitulos)
        {
            var baixasAtualizar = baixaTitulos.ToList();

            if (ModelState.IsValid)
            {
                try
                {
                    var tituloReceber = _tituloReceberRepository.Get(baixasAtualizar.First().TituloReceberId);
                    
                    foreach (var baixaAtualizarModel in baixasAtualizar)
                    {
                        var baixaAtualizar = tituloReceber.TituloReceberBaixas.First(x => x.Id == baixaAtualizarModel.Id);
                        
                        baixaAtualizar.DataRecebimento = baixaAtualizarModel.DataRecebimento;
                        baixaAtualizar.Descontos = baixaAtualizarModel.Desconto;
                        baixaAtualizar.Despesas = baixaAtualizarModel.Despesa;
                        baixaAtualizar.Historico = tituloReceber.Historico;
                        baixaAtualizar.Juros = baixaAtualizarModel.Juro;
                        baixaAtualizar.Observacao = tituloReceber.Observacao;
                        baixaAtualizar.ValorBaixa = baixaAtualizarModel.ValorBaixa;
                    }

                    AtualizeTituloReceber(tituloReceber);
                    _tituloReceberRepository.Update(tituloReceber);

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

        public void AtualizeTituloReceber(TituloReceber tituloReceber)
        {
            tituloReceber.DataAlteracao = DateTime.Now;

            var soma = tituloReceber.TituloReceberBaixas.Sum(p => p.ValorBaixa);
            tituloReceber.SaldoDevedor = tituloReceber.Valor - soma;

            if (tituloReceber.TituloReceberBaixas.Count == 0)
            {
                tituloReceber.SituacaoTitulo = SituacaoTitulo.NaoLiquidado;
            }
            else if (Math.Abs(soma - tituloReceber.Valor) < double.Epsilon)
            {
                tituloReceber.SituacaoTitulo = SituacaoTitulo.Liquidado;
            }
            else
            {
                tituloReceber.SituacaoTitulo = SituacaoTitulo.LiquidadoParcial;
            }
        }
        #endregion

        #region ExcluaBaixaTitulo
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult ExcluaBaixaTitulo([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<BaixaItemTituloReceberModel> baixaTitulos)
        {
            var baixasExcluir = baixaTitulos.ToList();
            try
            {
                var tituloReceber = _tituloReceberRepository.Get(baixasExcluir.First().TituloReceberId);
                
                foreach (var baixaExcluirModel in baixasExcluir)
                {
                    var baixaExcluir = tituloReceber.TituloReceberBaixas.First(x => x.Id == baixaExcluirModel.Id);

                    tituloReceber.RemoveTituloReceberBaixa(baixaExcluir);
                }

                AtualizeTituloReceber(tituloReceber);
                _tituloReceberRepository.Update(tituloReceber);

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