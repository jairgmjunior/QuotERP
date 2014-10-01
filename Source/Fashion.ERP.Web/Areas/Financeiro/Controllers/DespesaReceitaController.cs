using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Financeiro;
using Fashion.ERP.Reporting.Financeiro;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Web.Areas.Financeiro.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Financeiro.Controllers
{
    public partial class DespesaReceitaController : BaseController
    {
        #region Variaveis
        private readonly IRepository<DespesaReceita> _despesaReceitaRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public DespesaReceitaController(ILogger logger, IRepository<DespesaReceita> despesaReceitaRepository)
        {
            _despesaReceitaRepository = despesaReceitaRepository;
            DespesaReceita teste = _despesaReceitaRepository.Get(1);
            _logger = logger;
            
            PreecheColunasPesquisa();
        }
        #endregion

        #region Views
        
        #region Index
        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Index()
        {
            var despesaReceita = _despesaReceitaRepository.Find();

            var model = new PesquisaDespesaReceitaModel() { ModoConsulta = "Listar" };

            model.Grid = despesaReceita.Select(p => new GridDespesaReceitaModel
            {
                Id = p.Id.GetValueOrDefault(),
                Descricao = p.Descricao,
                TipoDespesaReceita = p.TipoDespesaReceita.EnunToString(),
                Ativo = p.Ativo
            }).ToList(); //.OrderBy(o => o.Descricao)

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Index(PesquisaDespesaReceitaModel model)
        {
            var despesasReceitas = _despesaReceitaRepository.Find();

            try
            {
                #region Filtros
                var filtros = new StringBuilder();
                
                despesasReceitas = FiltreParaListagem(model, despesasReceitas, filtros);

                #endregion

                // Verifica se é uma listagem
                if (model.ModoConsulta == "Listar")
                {
                    if (model.OrdenarPor != null)
                        despesasReceitas = model.OrdenarEm == "asc"
                            ? despesasReceitas.OrderBy(model.OrdenarPor)
                            : despesasReceitas.OrderByDescending(model.OrdenarPor);
                    else
                        despesasReceitas = despesasReceitas.OrderByDescending(m => m.Descricao);

                    model.Grid = despesasReceitas.Select(p => new GridDespesaReceitaModel()
                    {
                        Id = p.Id.GetValueOrDefault(),
                        Descricao = p.Descricao,
                        TipoDespesaReceita = p.TipoDespesaReceita.EnunToString(),
                        Ativo = p.Ativo
                    }).ToList();

                    return View(model);
                }

                // Se não é uma listagem, gerar o relatório

                //if (!string.IsNullOrWhiteSpace(model.Descricao))
                //    despesasReceitas = despesasReceitas.Where(p => p.Descricao == model.Descricao);

                //if (model.TipoDespesaReceita.HasValue)
                //    despesasReceitas = despesasReceitas.Where(p => p.TipoDespesaReceita == model.TipoDespesaReceita);

                //if (model.Ativo.HasValue)
                //    despesasReceitas = despesasReceitas.Where(p => p.Ativo == model.Ativo);

                var result = despesasReceitas.ToList();

                if (!result.Any())
                    return Json(new { Error = "Nenhum item encontrado." });

                #region Montar Relatório
                //Report report = null;

                var report = new ListaDespesaReceitaReport { DataSource = result };

                MonteRelatorio(model, filtros, report);

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

        private IQueryable<DespesaReceita> FiltreParaListagem(PesquisaDespesaReceitaModel model, IQueryable<DespesaReceita> despesasReceitas,
            StringBuilder filtros)
        {
            if (!string.IsNullOrWhiteSpace(model.Descricao))
            {
                despesasReceitas = despesasReceitas.Where(p => p.Descricao.Contains(model.Descricao));
                filtros.AppendFormat("Descrição: {0}, ", model.Descricao);
            }

            if (model.TipoDespesaReceita.HasValue)
            {
                despesasReceitas = despesasReceitas.Where(p => p.TipoDespesaReceita == model.TipoDespesaReceita);
                filtros.AppendFormat("Tipo de despesa/receita: {0}, ", model.TipoDespesaReceita);
            }

            if (model.Ativo.HasValue)
            {
                despesasReceitas = despesasReceitas.Where(p => p.Ativo == model.Ativo);
                filtros.AppendFormat("Ativo: {0}, ", model.Ativo.Value.ToSimNao());
            }
            return despesasReceitas;
        }

        #endregion
        
        #region Novo

        public virtual ActionResult Novo()
        {
            return View(new DespesaReceitaModel());
        }
        
        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(DespesaReceitaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<DespesaReceita>(model);
                    domain.Ativo = true;
                    _despesaReceitaRepository.Save(domain);

                    this.AddSuccessMessage("Despesa/receita cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a despesa/receita. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }
        #endregion

        #region Editar

        //[ImportModelStateFromTempData]
        public virtual ActionResult Editar(long id)
        {
            var domain = _despesaReceitaRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<DespesaReceitaModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a despesa/receita.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(DespesaReceitaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _despesaReceitaRepository.Get(model.Id));

                    _despesaReceitaRepository.Update(domain);

                    this.AddSuccessMessage("Despesa/receita atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a despesa/receita. Confira se os dados foram informados corretamente: " + exception.Message);
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
                    var domain = _despesaReceitaRepository.Get(id);
                    _despesaReceitaRepository.Delete(domain);

                    this.AddSuccessMessage("Despesa/receita excluída com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir a Despesa/receita: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { id });
        }
        #endregion

        #region EditarSituacao
        [HttpPost]
        public virtual ActionResult EditarSituacao(long id)
        {
            try
            {
                var domain = _despesaReceitaRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _despesaReceitaRepository.Update(domain);
                    this.AddSuccessMessage("Despesa/receita {0} com sucesso", situacao ? "ativado" : "inativado");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação da despesa/receita: " + exception.Message);
                _logger.Info(exception.GetMessage());
            }

            return RedirectToAction("Index");
        }
        #endregion

        #endregion

        #region Métodos

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var despesaReceita = model as DespesaReceitaModel;

            // Verificar duplicado
            if (_despesaReceitaRepository.Find(p => p.Descricao == despesaReceita.Descricao && p.Id != despesaReceita.Id).Any())
                ModelState.AddModelError("Descricao", "Já existe uma despesa/receita cadastrada com esta descrição.");
        }
        #endregion

        #endregion

        #region PreecheColunasPesquisa
        private void PreecheColunasPesquisa()
        {
            _colunasPesquisa = new Dictionary<string, string>
                           {
                               {"Descrição", "Descricao"},
                               {"Tipo despesa/receita", "TipoDespesaReceita"}
                           };
        }
        #endregion

        #region PopulateViewData
        protected void PopulateViewData(PesquisaDespesaReceitaModel model)
        {
            if (ValueProvider.GetValue("action").AttemptedValue == "Index")
            {
                ViewBag.AgruparPor = new SelectList(_colunasPesquisa, "value", "key");
                ViewBag.OrdenarPor = new SelectList(_colunasPesquisa, "value", "key");
            }
        }
        #endregion
    }
}