using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Domain.Producao;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Reporting.Producao;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Producao.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Mvc.ActionsFilters;
using Fashion.Framework.Repository;
using Microsoft.Ajax.Utilities;
using NHibernate.Linq;
using Ninject.Extensions.Logging;
using System.Text;
using Telerik.Reporting;

namespace Fashion.ERP.Web.Areas.Producao.Controllers
{
    public partial class OrdemProducaoController : BaseController
    {
        #region Variaveis
        private readonly IRepository<OrdemProducao> _ordemProducaoRepository;
        private readonly IRepository<DepartamentoProducao> _departamentoProducaoRepository;
        private readonly IRepository<FichaTecnica> _fichaTecnicaRepository;
        private readonly IRepository<Tamanho> _tamanhoRepository;
        private readonly IRepository<Variacao> _variacaoRepository;
        private readonly IRepository<FichaTecnicaVariacaoMatriz> _fichaTecnicaVariacaoMatrizRepository;
        private readonly IRepository<OrdemProducaoFluxoBasico> _ordemProducaoFluxoBasicoRepository;
        private readonly IRepository<Natureza> _naturezaRepository;
        private readonly IRepository<Colecao> _colecaoRepository;
        private readonly IRepository<Classificacao> _classficacaoRepository;
        private readonly IRepository<Artigo> _artigoRepository;
        private readonly ILogger _logger;
        private static readonly Dictionary<string, string> ColunasPesquisaOrdemProducao = new Dictionary<string, string>
            {
                {"Número", "Numero"},
                {"Tag", "Tag"},
                {"Ano", "Ano"},
                {"Data", "DataOrdemProducao"},
                {"Descrição", "FichaTecnica.Descricao"},
                {"Situação", "SituacaoOrdemProducao"},
            };
        #endregion

        #region Construtores
        public OrdemProducaoController(ILogger logger, IRepository<OrdemProducao> ordemProducaoRepository,
            IRepository<DepartamentoProducao> departamentoProducaoRepository, IRepository<FichaTecnica> fichaTecnicaRepository,
            IRepository<Tamanho> tamanhoRepository, IRepository<Variacao> variacaoRepository,
            IRepository<FichaTecnicaVariacaoMatriz> fichaTecnicaVariacaoMatrizRepository, IRepository<OrdemProducaoFluxoBasico> ordemProducaoFluxoBasicoRepository,
            IRepository<Natureza> naturezaRepository, IRepository<Colecao> colecaoRepository, IRepository<Classificacao> classficacaoRepository,
            IRepository<Artigo> artigoRepository)
        {
            _ordemProducaoRepository = ordemProducaoRepository;
            _departamentoProducaoRepository = departamentoProducaoRepository;
            _fichaTecnicaRepository = fichaTecnicaRepository;
            _tamanhoRepository = tamanhoRepository;
            _variacaoRepository = variacaoRepository;
            _fichaTecnicaVariacaoMatrizRepository = fichaTecnicaVariacaoMatrizRepository;
            _ordemProducaoFluxoBasicoRepository = ordemProducaoFluxoBasicoRepository;
            _naturezaRepository = naturezaRepository;
            _colecaoRepository = colecaoRepository;
            _classficacaoRepository = classficacaoRepository;
            _artigoRepository = artigoRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        [PopulateViewData("PopulateViewDataPesquisa"), NoCache]
        public virtual ActionResult Index()
        {
            var ordemProducoes = _ordemProducaoRepository.Find();

            var model = new PesquisaOrdemProducaoModel { ModoConsulta = "Listar" };

            model.Grid = ordemProducoes.Select(p => new GridOrdemProducaoModel
            {
                Id = p.Id.GetValueOrDefault(),
                Numero = p.Numero,
                Tag = p.FichaTecnica.Tag,
                Ano = p.FichaTecnica.Ano,
                Data = p.Data,
                Descricao = p.FichaTecnica.Descricao,
                Situacao = p.SituacaoOrdemProducao.EnumToString(),
                //Foto = p.FichaTecnica.
            }).ToList();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index(PesquisaOrdemProducaoModel model)
        {
            var ordemProducoes = _ordemProducaoRepository.Find();

            try
            {
                #region Filtros

                var filtros = new StringBuilder();

                if (model.Numero.HasValue)
                {
                    ordemProducoes = ordemProducoes.Where(p => p.Numero == model.Numero);
                    filtros.AppendFormat("Número: {0}, ", model.Numero);
                }

                if (model.SituacaoOrdemProducao.HasValue)
                {
                    ordemProducoes = ordemProducoes.Where(p => p.SituacaoOrdemProducao == model.SituacaoOrdemProducao);
                    filtros.AppendFormat("Situação: {0}, ", model.SituacaoOrdemProducao.Value.EnumToString());
                }

                if (model.Tag.IsNullOrWhiteSpace() == false && model.Ano.HasValue)
                {
                    ordemProducoes = ordemProducoes.Where(p => p.FichaTecnica.Tag == model.Tag
                        && p.FichaTecnica.Ano == model.Ano);
                    filtros.AppendFormat("Tag/Ano: {0}/{1}, ", model.Tag, model.Ano);
                }

                if (model.TipoOrdemProducao.HasValue)
                {
                    ordemProducoes = ordemProducoes.Where(p => p.TipoOrdemProducao == model.TipoOrdemProducao);
                    filtros.AppendFormat("Tipo OP: {0}, ", model.TipoOrdemProducao.Value.EnumToString());
                }

                if (model.DataOrdemProducao.HasValue)
                {
                    ordemProducoes = ordemProducoes.Where(p => p.Data.Date == model.DataOrdemProducao.Value);
                    filtros.AppendFormat("Data: {0}", model.DataOrdemProducao.Value.ToString("dd/MM/yyyy"));
                }

                if (model.DataCorte.HasValue)
                {
                    // TODO: ordemProducoes = ordemProducoes.Where(p => p.FichaTecnica.DataCorte.Date == model.DataCorte.Value);
                    filtros.AppendFormat("Data corte: {0}", model.DataCorte.Value.ToString("dd/MM/yyyy"));
                }

                if (model.DataProgramacao.HasValue)
                {
                    ordemProducoes = ordemProducoes.Where(p => p.DataProgramacao.Date == model.DataProgramacao.Value);
                    filtros.AppendFormat("Data Programação: {0}", model.DataProgramacao.Value.ToString("dd/MM/yyyy"));
                }

                if (model.DataPrevisao.HasValue)
                {
                    ordemProducoes = ordemProducoes.Where(p => p.DataPrevisao.Date == model.DataPrevisao.Value);
                    filtros.AppendFormat("Data previsão: {0}", model.DataPrevisao.Value.ToString("dd/MM/yyyy"));
                }

                if (model.Colecao.HasValue)
                {
                    ordemProducoes = ordemProducoes.Where(p => p.FichaTecnica.Colecao.Id == model.Colecao);
                    filtros.AppendFormat("Coleção: {0}, ", _colecaoRepository.Get(model.Colecao.Value).Descricao);
                }

                if (model.Natureza.HasValue)
                {
                    ordemProducoes = ordemProducoes.Where(p => p.FichaTecnica.Natureza.Id == model.Natureza);
                    filtros.AppendFormat("Natureza: {0}, ", _naturezaRepository.Get(model.Natureza.Value).Descricao);
                }

                if (model.Classificacao.HasValue)
                {
                    ordemProducoes = ordemProducoes.Where(p => p.FichaTecnica.Classificacao.Id == model.Classificacao);
                    filtros.AppendFormat("Classificação: {0}, ", _classficacaoRepository.Get(model.Classificacao.Value).Descricao);
                }

                if (model.Artigo.HasValue)
                {
                    ordemProducoes = ordemProducoes.Where(p => p.FichaTecnica.Artigo.Id == model.Artigo);
                    filtros.AppendFormat("Artigo: {0}, ", _artigoRepository.Get(model.Artigo.Value).Descricao);
                }

                #endregion

                // Verifica se é uma listagem
                if (model.ModoConsulta == "Listar")
                {
                    if (model.OrdenarPor != null)
                        ordemProducoes = model.OrdenarEm == "asc"
                                            ? ordemProducoes.OrderBy(model.OrdenarPor)
                                            : ordemProducoes.OrderByDescending(model.OrdenarPor);

                    model.Grid = ordemProducoes.Select(p => new GridOrdemProducaoModel
                    {
                        Id = p.Id.GetValueOrDefault(),
                        Numero = p.Numero,
                        Tag = p.FichaTecnica.Tag,
                        Ano = p.FichaTecnica.Ano,
                        Data = p.Data,
                        Descricao = p.FichaTecnica.Descricao,
                        Situacao = p.SituacaoOrdemProducao.EnumToString(),
                    }).ToList();

                    return View(model);
                }

                // Se não é uma listagem, gerar o relatório
                var result = ordemProducoes
                    .Fetch(p => p.FichaTecnica)
                    .ToList();

                if (!result.Any())
                    return Json(new { Error = "Nenhum item encontrado." });

                #region Montar Relatório

                var report = new ListaOrdemProducaoReport { DataSource = result };

                if (filtros.Length > 2)
                    report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

                var grupo = report.Groups.First(p => p.Name.Equals("Grupo"));

                if (model.AgruparPor != null)
                {
                    grupo.Groupings.Add("=Fields." + model.AgruparPor);

                    var key = ColunasPesquisaOrdemProducao.First(p => p.Value == model.AgruparPor).Key;
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
            var model = new OrdemProducaoModel {Numero = GerarNumero()};

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(OrdemProducaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<OrdemProducao>(model);
                    domain.Numero = GerarNumero();
                    domain.FichaTecnica = _fichaTecnicaRepository.Find(f => f.Tag == model.Tag && f.Ano == model.Ano).FirstOrDefault();

                    // Matriz
                    for (int i = 0; i < model.ItemQuantidades.Count; i++)
                    {
                        var quantidade = model.ItemQuantidades[i];

                        if (quantidade > 0)
                        {
                            var variacaoMatrizId = EncontraVariacaoMatrizId(model.ItemVariacaoMatrizes, model.ItemQuantidades.Count, i);

                            domain.AddOrdemProducaoItem(new OrdemProducaoItem
                            {
                                QuantidadeSolicitada = quantidade,
                                SituacaoOrdemProducao = domain.SituacaoOrdemProducao,
                                FichaTecnicaVariacaoMatriz = _fichaTecnicaVariacaoMatrizRepository.Get(variacaoMatrizId),
                                Tamanho = _tamanhoRepository.Get(model.ItemTamanhos[i])
                            });
                        }
                    }

                    // Fluxo Básico
                    var fluxoBasico = new OrdemProducaoFluxoBasico();
                    foreach (var fluxoDepartamento in model.FluxoDepartamentos)
                        fluxoBasico.AddDepartamentoProducao(_departamentoProducaoRepository.Get(fluxoDepartamento));
                    domain.OrdemProducaoFluxoBasico = _ordemProducaoFluxoBasicoRepository.Save(fluxoBasico);

                    _ordemProducaoRepository.Save(domain);

                    this.AddSuccessMessage("Ordem de produção cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a ordem de produção. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _ordemProducaoRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<OrdemProducaoModel>(domain);

                // Lista os tamanhos da ficha técnica
                foreach (var tamanho in domain.FichaTecnica.FichaTecnicaMatriz.Grade.Tamanhos)
                    model.ItemTamanhos.Add(tamanho.Key.Id.GetValueOrDefault());

                // Lista as variações da ficha técnica
                foreach (var variacaoMatrizes in domain.FichaTecnica.FichaTecnicaMatriz.FichaTecnicaVariacaoMatrizs)
                    model.ItemVariacaoMatrizes.Add(variacaoMatrizes.Id.GetValueOrDefault());

                // Preenche as quantidades, com a OrdemProducaoItem, e com zero quando não houver
                foreach (var itemTamanho in model.ItemTamanhos)
                {
                    foreach (var itemVariacaoMatriz in model.ItemVariacaoMatrizes)
                    {
                        var itemQuantidade = domain.OrdemProducaoItens.SingleOrDefault(p => p.Tamanho.Id == itemTamanho
                                         && p.FichaTecnicaVariacaoMatriz.Id == itemVariacaoMatriz);
                        model.ItemQuantidades.Add(itemQuantidade != null ? itemQuantidade.QuantidadeSolicitada : 0);
                    }
                }

                // Preenche a lista de departamentos
                foreach (var departamentoProducao in domain.OrdemProducaoFluxoBasico.DepartamentoProducoes)
                {
                    model.FluxoDepartamentos.Add(departamentoProducao.Id.GetValueOrDefault());
                }

                // Busca os dados específicos da ficha técnica
                model.Tag = domain.FichaTecnica.Tag;
                model.Ano = domain.FichaTecnica.Ano;
                model.Descricao = domain.FichaTecnica.Descricao;

                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a ordem de produção.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(OrdemProducaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _ordemProducaoRepository.Get(model.Id));

                    // Matriz
                    domain.ClearOrdemProducaoItem();
                    for (int i = 0; i < model.ItemQuantidades.Count; i++)
                    {
                        var quantidade = model.ItemQuantidades[i];

                        if (quantidade > 0)
                        {
                            var variacaoMatrizId = EncontraVariacaoMatrizId(model.ItemVariacaoMatrizes, model.ItemQuantidades.Count, i);

                            domain.AddOrdemProducaoItem(new OrdemProducaoItem
                            {
                                QuantidadeSolicitada = quantidade,
                                SituacaoOrdemProducao = domain.SituacaoOrdemProducao,
                                FichaTecnicaVariacaoMatriz = _fichaTecnicaVariacaoMatrizRepository.Get(variacaoMatrizId),
                                Tamanho = _tamanhoRepository.Get(model.ItemTamanhos[i])
                            });
                        }
                    }

                    // Fluxo Básico
                    var fluxoBasico = new OrdemProducaoFluxoBasico();
                    foreach (var fluxoDepartamento in model.FluxoDepartamentos)
                        fluxoBasico.AddDepartamentoProducao(_departamentoProducaoRepository.Get(fluxoDepartamento));
                    domain.OrdemProducaoFluxoBasico = _ordemProducaoFluxoBasicoRepository.Save(fluxoBasico);

                    _ordemProducaoRepository.Update(domain);

                    this.AddSuccessMessage("Ordem de produção atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a ordem de produção. Confira se os dados foram informados corretamente: " + exception.Message);
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
                    var domain = _ordemProducaoRepository.Get(id);

                    _ordemProducaoRepository.Delete(domain);

                    this.AddSuccessMessage("Ordem de produção excluída com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao excluir a ordem de produção: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { id });
        }
        #endregion

        #endregion

        #region Métodos

        #region PopulateViewData
        protected void PopulateViewData(OrdemProducaoModel model)
        {
            // Departamentos
            var departamentos = _departamentoProducaoRepository.Find().OrderBy(p => p.Nome).ToList();

            var departamentosAtivo = departamentos.Where(p => p.Ativo).ToList();
            ViewData["DepartamentoProducao"] = departamentosAtivo.ToSelectList("Nome");

            ViewBag.DepartamentosDicionario = departamentos.ToDictionary(t => t.Id, t => t.Nome);

            // Tamanhos
            var tamanhos = _tamanhoRepository.Find().ToList();
            ViewBag.TamanhosDicionario = tamanhos.ToDictionary(t => t.Id, t => t.Descricao);

            // Variações
            var variacoes = _variacaoRepository.Find().ToList();
            ViewBag.VariacoesDicionario = variacoes.ToDictionary(t => t.Id, t => t.Nome);
        }
        #endregion

        #region PopulateViewDataPesquisa
        protected void PopulateViewDataPesquisa(PesquisaOrdemProducaoModel model)
        {
            var naturezas = _naturezaRepository.Find(p => p.Ativo).ToList();
            ViewBag.Natureza = naturezas.ToSelectList("Descricao", model.Natureza);

            var colecoes = _colecaoRepository.Find(p => p.Ativo).ToList();
            ViewBag.Colecao = colecoes.ToSelectList("Descricao", model.Colecao);

            var classificacoes = _classficacaoRepository.Find(p => p.Ativo).ToList();
            ViewBag.Classificacao = classificacoes.ToSelectList("Descricao", model.Classificacao);

            var artigos = _artigoRepository.Find(p => p.Ativo).ToList();
            ViewBag.Artigo = artigos.ToSelectList("Descricao", model.Artigo);

            ViewBag.OrdenarPor = new SelectList(ColunasPesquisaOrdemProducao, "value", "key");
            ViewBag.AgruparPor = new SelectList(ColunasPesquisaOrdemProducao, "value", "key");
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var ordemProducao = (OrdemProducaoModel)model;

            if (ordemProducao.ItemQuantidades.All(p => p == 0))
                ModelState.AddModelError(string.Empty, "Pelo menos um item da Matriz Solicitada deve ser preenchida com valor maior que zero.");

            if (ordemProducao.FluxoDepartamentos.Any() == false)
                ModelState.AddModelError(string.Empty, "Informe os departamentos do Fluxo Básico.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
        }
        #endregion

        #region PesquisarMatriz
        [HttpGet, AjaxOnly]
        public virtual ActionResult PesquisarMatriz(string tag, long ano)
        {
            var fichaTecnica = _fichaTecnicaRepository.Find(f => f.Tag == tag && f.Ano == ano).FirstOrDefault();
            var tamanhoIds = new List<long>();
            var tamanhoSiglas = new List<string>();
            var variacaoIds = new List<long>();
            var variacaoNomes = new List<string>();

            if (fichaTecnica != null)
            {
                foreach (var tamanho in fichaTecnica.FichaTecnicaMatriz.Grade.Tamanhos)
                {
                    tamanhoIds.Add(tamanho.Key.Id.GetValueOrDefault());
                    tamanhoSiglas.Add(tamanho.Key.Sigla);
                }

                foreach (var variacaoMatrizes in fichaTecnica.FichaTecnicaMatriz.FichaTecnicaVariacaoMatrizs)
                {
                    variacaoIds.Add(variacaoMatrizes.Id.GetValueOrDefault());
                    variacaoNomes.Add(variacaoMatrizes.Variacao.Nome);
                }

                return Json(new { descricao = fichaTecnica.Descricao, tamanhoIds, tamanhoSiglas, variacaoIds, variacaoNomes }, JsonRequestBehavior.AllowGet);

            }

            return Json(new { erro = "Nenhuma Ficha Técnica encontrada para esta Tag/Ano." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GerarNumero
        private long GerarNumero()
        {
            return _ordemProducaoRepository.Find().Any()
                ? _ordemProducaoRepository.Find().Max(p => p.Numero) + 1
                : 1;
        }
        #endregion

        #region EncontraVariacaoMatrizId
        private long EncontraVariacaoMatrizId(IList<long> itemVariacaoMatrizes, long qtdItens, int indice)
        {
            var registrosPorVariacao = qtdItens / itemVariacaoMatrizes.Count;
            var indiceVariacao = indice / registrosPorVariacao;
            return itemVariacaoMatrizes[(int)indiceVariacao];
        }
        #endregion

        #endregion
    }
}