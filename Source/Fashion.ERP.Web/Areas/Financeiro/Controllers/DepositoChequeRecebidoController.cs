using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.Financeiro;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Financeiro.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using FluentNHibernate.Conventions;
using FluentNHibernate.Utils;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.Ajax.Utilities;
using NHibernate.Linq;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Financeiro.Controllers
{
    public partial class DepositoChequeRecebidoController : BaseController
    {
		#region Variaveis
        private readonly IRepository<ChequeRecebido> _chequeRecebidoRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<ContaBancaria> _contaBancariaRepository;
        private readonly IRepository<ExtratoBancario> _extratoBancarioRepository;
        private readonly ILogger _logger;
        public const string ChaveCheque = "0D835C94-AAB2-4994-87BA-F54D840E9974";
        #endregion

        #region Construtores
        public DepositoChequeRecebidoController(ILogger logger, IRepository<ChequeRecebido> chequeRecebidoRepository,
            IRepository<Pessoa> pessoaRepository, IRepository<ContaBancaria> contaBancariaRepository,
            IRepository<ExtratoBancario> extratoBancarioRepository)
        {
            _chequeRecebidoRepository = chequeRecebidoRepository;
            _pessoaRepository = pessoaRepository;
            _contaBancariaRepository = contaBancariaRepository;
            _extratoBancarioRepository = extratoBancarioRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Index()
        {
            return View(new DepositoChequeRecebidoModel());
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Index(DepositoChequeRecebidoModel model)
        {
            if (model.Cheques.IsNullOrEmpty())
                ModelState.AddModelError(string.Empty, "Selecione pelo menos um cheque.");

            if (ModelState.IsValid)
            {
                try
                {
                    var cheques = model.Cheques.Select(p => _chequeRecebidoRepository.Get(p)).ToList();

                    // Fazer um lançamento do depósito no Extrato Bancário
                    var extrato = new ExtratoBancario
                    {
                        TipoLancamento = TipoLancamento.Credito,
                        Emissao = model.DataDeposito.GetValueOrDefault(),
                        Descricao = string.Format("CONF. DEP. PERIODO {0:dd/MM/yyyy} A {1:dd/MM/yyyy}", model.VencimentoDe, model.VencimentoAte),
                        Valor = cheques.Sum(c => c.Valor),
                        ContaBancaria = _contaBancariaRepository.Get(model.ContaBancaria)
                    };

                    _extratoBancarioRepository.Save(extrato);
                    
                    // Atualizar a <<SituacaoChequeRecebido>> de cada cheque depositado para <<Depositado>>.
                    foreach (var chequeRecebido in cheques)
                    {
                        // Criar ocorrência
                        chequeRecebido.AddOcorrenciaCompensacao(new OcorrenciaCompensacao
                        {
                            Data = DateTime.Now,
                            ChequeSituacao = ChequeSituacao.Depositado,
                            Historico = string.Format("Depositado em {0} por {1}",
                                            DateTime.Now.ToString("F"), HttpContext.User.Identity.Name)
                        });

                        _chequeRecebidoRepository.Update(chequeRecebido);
                    }

                    this.AddSuccessMessage("Cheque(s) depositado(s) com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o cheque. Confira se os dados foram informados corretamente.");
                    _logger.Info(exception.GetMessage());
                }
            }
            
            return View(model);
        }
        #endregion

        #region Cheques

        #region LerCheques
        [AjaxOnly, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual ActionResult LerCheques([DataSourceRequest]DataSourceRequest request)
        {
            var cheques = TempData.Peek(ChaveCheque) as List<GridDepositoChequeRecebido>;

            if (cheques != null)
                return Json(cheques.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PesquisaCheque
        [HttpPost]
        public virtual ActionResult PesquisaCheque(long? unidade, DateTime? vencimentoDe, DateTime? vencimentoAte, ChequeSituacao? naoDepositado, ChequeSituacao? devolvido, ChequeSituacao? custodia)
        {
            var query = _chequeRecebidoRepository.Find();

            if (unidade.HasValue)
                query = query.Where(p => p.Unidade.Id == unidade);

            if (vencimentoDe.HasValue)
                query = query.Where(p => p.DataVencimento >= vencimentoDe.Value);

            if (vencimentoAte.HasValue)
                query = query.Where(p => p.DataVencimento <= vencimentoAte.Value);

            var cheques = new List<GridDepositoChequeRecebido>();

            foreach (var chequeRecebido in query.ToList())
            {
                var ultimaOcorrencia = chequeRecebido.UltimaOcorrenciaCompensacao();

                cheques.Add(new GridDepositoChequeRecebido
                {
                    Id = chequeRecebido.Id.GetValueOrDefault(),
                    Unidade = chequeRecebido.Unidade.NomeFantasia,
                    Emitente = chequeRecebido.Emitente.Nome1,
                    NumeroCheque = chequeRecebido.NumeroCheque,
                    Banco = chequeRecebido.Banco.Nome,
                    Valor = chequeRecebido.Valor,
                    DataVencimento = chequeRecebido.DataVencimento,
                    Situacao = ultimaOcorrencia.ChequeSituacao,
                    CodigoCompensacaoCheque = ultimaOcorrencia.CompensacaoCheque != null ? ultimaOcorrencia.CompensacaoCheque.Codigo : 0,
                    Depositar = false
                });
            }

            var situacoes = new List<ChequeSituacao>();
            if (naoDepositado.HasValue) situacoes.Add(naoDepositado.Value);
            if (devolvido.HasValue) situacoes.Add(devolvido.Value);
            if (custodia.HasValue) situacoes.Add(custodia.Value);

            if (situacoes.IsNullOrEmpty() == false)
            {
                cheques = cheques.Where(p => p.CodigoCompensacaoCheque != 12 && situacoes.Contains(p.Situacao)).ToList();
            }
            else
            {
                cheques = cheques.Where(p => p.CodigoCompensacaoCheque != 12 
                    && (p.Situacao == ChequeSituacao.NaoDepositado || p.Situacao == ChequeSituacao.Devolvido || p.Situacao == ChequeSituacao.Custodia)).ToList();
            }

            TempData[ChaveCheque] = cheques;

            return Content(string.Empty);
        }
        #endregion

        #endregion

        #region PesquisarAgenciaContaPorBanco
        [HttpGet, AjaxOnly]
        public virtual ActionResult PesquisarAgenciaContaPorBanco(long id /* Banco.Id */)
        {
            var contas = _contaBancariaRepository.Find(p => p.Banco.Id == id)
                .Select(c => new { c.Id, c.Agencia, c.Conta }).ToList();

            return Json(contas, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

		#region Métodos
		
        #region PopulateViewData
        protected void PopulateViewData(DepositoChequeRecebidoModel model)
        {
            var unidades = _pessoaRepository.Find(p => p.Unidade != null && p.Unidade.Ativo).ToList();
            ViewData["Unidade"] = unidades.ToSelectList("NomeFantasia", model.Unidade);

            var bancos = _contaBancariaRepository.Find().Select(s => s.Banco).Distinct().ToList();
            ViewData["Banco"] = bancos.ToSelectList("Nome", model.Banco);
        }
        #endregion

        #endregion
    }
}