using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.Financeiro;
using Fashion.ERP.Web.Areas.Comum.Models;
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
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Financeiro.Controllers
{
    public partial class ChequeRecebidoController : BaseController
    {
        #region Variaveis

        private readonly ChequeSituacao[] _situacaoDevolucao = {ChequeSituacao.NaoDepositado, ChequeSituacao.Devolvido};

        private readonly IRepository<ChequeRecebido> _chequeRecebidoRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<OcorrenciaCompensacao> _ocorrenciaChequeRecebidoRepository;
        private readonly IRepository<BaixaChequeRecebido> _baixaChequeRecebidoRepository;
        private readonly IRepository<MeioPagamento> _meioPagamentoRepository;
        private readonly IRepository<Banco> _bancoRepository;
        private readonly IRepository<Emitente> _emitenteRepository;
        private readonly IRepository<CompensacaoCheque> _compensacaoChequeRepository;
        private readonly ILogger _logger;
        public const string ChaveFuncionario = "31E3CE18-5D49-4287-9FCF-4AF909423B8C";
        public const string ChavePrestador = "CFE169A4-FEFD-46EE-9795-39D8C8CE432A";
        #endregion

        #region Construtores
        public ChequeRecebidoController(ILogger logger, IRepository<ChequeRecebido> chequeRecebidoRepository,
            IRepository<Pessoa> pessoaRepository, IRepository<OcorrenciaCompensacao> ocorrenciaChequeRecebidoRepository,
            IRepository<BaixaChequeRecebido> baixaChequeRecebidoRepository, IRepository<MeioPagamento> meioPagamentoRepository,
            IRepository<Banco> bancoRepository, IRepository<Emitente> emitenteRepository,
            IRepository<CompensacaoCheque> compensacaoChequeRepository)
        {
            _chequeRecebidoRepository = chequeRecebidoRepository;
            _pessoaRepository = pessoaRepository;
            _ocorrenciaChequeRecebidoRepository = ocorrenciaChequeRecebidoRepository;
            _baixaChequeRecebidoRepository = baixaChequeRecebidoRepository;
            _meioPagamentoRepository = meioPagamentoRepository;
            _bancoRepository = bancoRepository;
            _emitenteRepository = emitenteRepository;
            _compensacaoChequeRepository = compensacaoChequeRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Index()
        {
            var chequeRecebidos = _chequeRecebidoRepository.Find();
            
            var model = new PesquisaChequeRecebidoModel {Grid = new List<GridChequeRecebidoModel>()};

            foreach (var chequeRecebido in chequeRecebidos)
            {
                var situacaoCheque = chequeRecebido.UltimaOcorrenciaCompensacao().ChequeSituacao;

                model.Grid.Add(new GridChequeRecebidoModel
                {
                    Id = chequeRecebido.Id.GetValueOrDefault(),
                    Emitente = chequeRecebido.Emitente.Nome1,
                    NumeroCheque = chequeRecebido.NumeroCheque,
                    Valor = chequeRecebido.Valor,
                    Saldo = chequeRecebido.Saldo,
                    Vencimento = chequeRecebido.DataVencimento,
                    Banco = chequeRecebido.Banco.Codigo.ToString("D3"),
                    Emissao = chequeRecebido.DataEmissao,
                    SituacaoCheque = situacaoCheque.EnumToString(),
                    PodeDevolver = _situacaoDevolucao.Contains(situacaoCheque)
                });
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Index(PesquisaChequeRecebidoModel model)
        {
            var chequeRecebidos = _chequeRecebidoRepository.Find();

            try
            {
                if (!string.IsNullOrWhiteSpace(model.Emitente))
                    chequeRecebidos = chequeRecebidos
                        .Where(p => p.Emitente.Nome1.Contains(model.Emitente) || p.Emitente.Nome2.Contains(model.Emitente));

                if (!string.IsNullOrWhiteSpace(model.Cliente))
                    chequeRecebidos = chequeRecebidos.Where(p => p.Cliente.Nome.Contains(model.Cliente));

                if (!string.IsNullOrWhiteSpace(model.NumeroCheque))
                    chequeRecebidos = chequeRecebidos.Where(p => p.NumeroCheque == model.NumeroCheque);

                if (model.Unidade.HasValue)
                    chequeRecebidos = chequeRecebidos.Where(p => p.Unidade.Id == model.Unidade);

                if (model.VencimentoDe.HasValue)
                    chequeRecebidos = chequeRecebidos.Where(p => p.DataVencimento >= model.VencimentoDe.Value);

                if (model.VencimentoAte.HasValue)
                    chequeRecebidos = chequeRecebidos.Where(p => p.DataVencimento <= model.VencimentoAte.Value);

                if (model.Compensado.HasValue)
                    chequeRecebidos = chequeRecebidos.Where(p => p.Compensado == (model.Compensado.Value == SimNao.Sim));
            }
            catch (Exception exception)
            {
                _logger.Info(exception.GetMessage());
                ModelState.AddModelError(string.Empty, exception.Message);
            }

            model.Grid = new List<GridChequeRecebidoModel>();

            foreach (var chequeRecebido in chequeRecebidos)
            {
                var situacaoCheque = chequeRecebido.UltimaOcorrenciaCompensacao().ChequeSituacao;

                model.Grid.Add(new GridChequeRecebidoModel
                {
                    Id = chequeRecebido.Id.GetValueOrDefault(),
                    Emitente = chequeRecebido.Emitente.Nome1,
                    NumeroCheque = chequeRecebido.NumeroCheque,
                    Valor = chequeRecebido.Valor,
                    Saldo = chequeRecebido.Saldo,
                    Vencimento = chequeRecebido.DataVencimento,
                    Banco = chequeRecebido.Banco.Codigo.ToString("D3"),
                    Emissao = chequeRecebido.DataEmissao,
                    SituacaoCheque = situacaoCheque.EnumToString(),
                    PodeDevolver = _situacaoDevolucao.Contains(situacaoCheque)
                });
            }

            return View(model);
        }

        #endregion

        #region Novo

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            TempData.Remove(ChaveFuncionario);
            TempData.Remove(ChavePrestador);

            var model = new ChequeRecebidoModel
            {
                Situacao = SituacaoTitulo.NaoLiquidado.EnumToString()
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(ChequeRecebidoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Funcionários
                    var listaFuncionarios = new List<Pessoa>();
                    var funcionarios = TempData.Peek(ChaveFuncionario) as List<GridFuncionarioModel>;

                    if (funcionarios != null)
                        listaFuncionarios.AddRange(funcionarios.Select(funcionario => _pessoaRepository.Load(funcionario.Id)));

                    // Prestadores de serviço
                    var listaPrestadores = new List<Pessoa>();
                    var prestadores = TempData.Peek(ChavePrestador) as List<GridPrestadorServicoModel>;

                    if (prestadores != null)
                        listaPrestadores.AddRange(prestadores.Select(prestador => _pessoaRepository.Load(prestador.Id)));

                    for (int i = 0; i < model.Bancos.Count; i++)
                    {
                        var idx = i;

                        var domain = Mapper.Unflat<ChequeRecebido>(model);
                        domain.Saldo = domain.Valor;

                        domain.AddFuncionario(listaFuncionarios.ToArray());
                        domain.AddPrestadorServico(listaPrestadores.ToArray());

                        // Criar ocorrência
                        domain.AddOcorrenciaCompensacao(new OcorrenciaCompensacao
                        {
                            Data = DateTime.Now,
                            ChequeSituacao = ChequeSituacao.NaoDepositado,
                            Historico = string.Format("Cadastrado em {0} por {1}",
                                            DateTime.Now.ToString("F"), HttpContext.User.Identity.Name)
                        });

                        domain.Cmc7 = model.Cmc7s[idx];
                        domain.Banco = _bancoRepository.Load(model.IdBancos[idx]);
                        domain.Agencia = model.Agencias[idx];
                        domain.Conta = model.Contas[idx];
                        domain.NumeroCheque = model.Cheques[idx];
                        domain.DataVencimento = model.Vencimentos[idx];
                        domain.Emitente = _emitenteRepository.Load(model.IdEmitentes[idx]);
                        domain.Praca = model.Pracas[idx];
                        domain.Valor = model.Valores[idx];

                        // Salvar cada cheque
                        _chequeRecebidoRepository.Save(domain);
                    }

                    this.AddSuccessMessage("Cheque(s) cadastrado(s) com sucesso.");
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

        #region Editar

        [ImportModelStateFromTempData, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(long id)
        {
            var domain = _chequeRecebidoRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<ChequeRecebidoModel>(domain);

                TempData[ChaveFuncionario] = new List<GridFuncionarioModel>(
                    domain.Funcionarios.Select(f => new GridFuncionarioModel
                    {
                        Id = f.Id.GetValueOrDefault(),
                        Nome = f.Nome,
                        Codigo = f.Funcionario.Codigo,
                        CpfCnpj = f.CpfCnpj,
                        DataCadastro = f.DataCadastro,
                        FuncaoFuncionario = f.Funcionario.FuncaoFuncionario.EnumToString()
                    }));

                TempData[ChavePrestador] = new List<GridPrestadorServicoModel>(
                    domain.PrestadorServicos.Select(f => new GridPrestadorServicoModel
                    {
                        Id = f.Id.GetValueOrDefault(),
                        Nome = f.Nome,
                        Codigo = f.PrestadorServico.Codigo,
                        CpfCnpj = f.CpfCnpj
                    }));

                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o cheque.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(ChequeRecebidoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _chequeRecebidoRepository.Get(model.Id));

                    // Funcionários
                    var funcionarios = TempData.Peek(ChaveFuncionario) as List<GridFuncionarioModel>;

                    if (funcionarios != null)
                        foreach (var funcionario in funcionarios)
                            domain.AddFuncionario(_pessoaRepository.Load(funcionario.Id));

                    // Prestadores de serviço
                    var prestadores = TempData.Peek(ChavePrestador) as List<GridPrestadorServicoModel>;

                    if (prestadores != null)
                        foreach (var prestador in prestadores)
                            domain.AddPrestadorServico(_pessoaRepository.Load(prestador.Id));

                    // Criar ocorrência
                    var historico = string.Format("Cadastrado em {0} por {1}",
                                        DateTime.Now.ToString("F"), HttpContext.User.Identity.Name);
                    var ocorrencia = new OcorrenciaCompensacao
                    {
                        Data = DateTime.Now,
                        ChequeSituacao = ChequeSituacao.NaoDepositado,
                        Historico = historico
                    };

                    //ocorrencia = _ocorrenciaChequeRecebidoRepository.Save(ocorrencia);
                    domain.AddOcorrenciaCompensacao(ocorrencia);

                    //_chequeRecebidoRepository.Merge(domain);
                    _chequeRecebidoRepository.Update(domain);

                    this.AddSuccessMessage("Cheque atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o cheque. Confira se os dados foram informados corretamente.");
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
                    var domain = _chequeRecebidoRepository.Get(id);

                    // Nâo excluir se houver ocorrência
                    if (domain.OcorrenciaCompensacoes.Any())
                    {
                        ModelState.AddModelError(string.Empty, "Não é possível excluir cheque, pois existem ocorrências para este cheque.");
                        return RedirectToAction("Index");
                    }

                    _chequeRecebidoRepository.Delete(domain);

                    this.AddSuccessMessage("Cheque excluído com sucesso");

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir o cheque: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { id });
        }
        #endregion

        #region Baixa
        [Api]
        public virtual ActionResult Baixa(long? id, long? chequeRecebido)
        {
            ViewData["MeioPagamento"] = new SelectList(_meioPagamentoRepository.Find(), "Id", "Descricao");

            // Por enquanto não permitir alteração da baixa
            //if (id.HasValue)
            //{
            //    var baixaCheque = _baixaChequeRecebidoRepository.Get(id);

            //    var model = new BaixaChequeRecebidoModel
            //    {
            //        Id = baixaCheque.Id,
            //        ChequeRecebido = baixaCheque.ChequeRecebido.Id.GetValueOrDefault(),
            //        Valor = baixaCheque.ChequeRecebido.Saldo,
            //        Data = baixaCheque.Data,
            //        Historico = baixaCheque.Historico,
            //        Observacao = baixaCheque.Observacao,
            //        TaxaJuros = baixaCheque.TaxaJuros,
            //        ValorTotal = baixaCheque.ChequeRecebido.Saldo,
            //        ValorDesconto = baixaCheque.ValorDesconto,
            //        ValorJuros = baixaCheque.ValorJuros
            //    };

            //    ViewData["Recebimentos"] = baixaCheque.RecebimentoChequeRecebidos;
            //    return View(model);
            //}

            if (chequeRecebido.HasValue)
            {
                var cheque = Framework.UnitOfWork.StatelessSession.Current.Get<ChequeRecebido>(chequeRecebido);

                var model = new BaixaChequeRecebidoModel
                {
                    ChequeRecebido = chequeRecebido.Value,
                    Valor = cheque.Saldo,
                    ValorTotal = cheque.Saldo,
                    Data = DateTime.Now
                };
                return View(model);
            }

            return View();
        }

        [HttpPost, Api]
        public virtual ActionResult Baixa(BaixaChequeRecebidoModel model, long[] meioPagamentoId, double[] valorRecebimentoChequeRecebido)
        {
            var recebimentos = meioPagamentoId.Select((id, index) => new RecebimentoChequeRecebido
            {
                MeioPagamento = _meioPagamentoRepository.Get(id),
                Valor = valorRecebimentoChequeRecebido[index]
            }).ToArray();

            if (ModelState.IsValid)
            {
                try
                {
                    // Atualizar o saldo do cheque
                    var chequeRecebido = _chequeRecebidoRepository.Get(model.ChequeRecebido);

                    var baixaChequeRecebido = Mapper.Unflat<BaixaChequeRecebido>(model);

                    //domain.Valor = valorRecebimentoChequeRecebido.Sum();
                    if (baixaChequeRecebido.Valor > chequeRecebido.Saldo)
                    {
                        ModelState.AddModelError("Valor", "O valor da baixa não pode ser maior que o saldo do cheque. Saldo: " + chequeRecebido.Saldo.ToString("C"));
                        return View(model);
                    }

                    for (int i = 0; i < meioPagamentoId.Length; i++)
                        baixaChequeRecebido.AddRecebimentoChequeRecebido(recebimentos);

                    baixaChequeRecebido.ChequeRecebido = chequeRecebido;
                    _baixaChequeRecebidoRepository.SaveOrUpdate(baixaChequeRecebido);

                    // Diminuir o saldo do cheque
                    chequeRecebido.Saldo -= model.Valor;

                    // Criar ocorrência
                    var historico = string.Format("{0} em {1} por {2} no valor de {3:C}.",
                                        model.Id.HasValue ? "Atualizado" : "Baixado",
                                        DateTime.Now.ToString("F"), HttpContext.User.Identity.Name, model.Valor);
                    var ocorrencia = new OcorrenciaCompensacao
                    {
                        Data = DateTime.Now,
                        ChequeSituacao = chequeRecebido.Saldo > 0 ? ChequeSituacao.QuitadoParcial : ChequeSituacao.QuitadoTotal,
                        Historico = historico
                    };

                    chequeRecebido.AddOcorrenciaCompensacao(ocorrencia);
                    _chequeRecebidoRepository.Update(chequeRecebido);

                    this.AddSuccessMessage("Cheque baixado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a baixa de cheque. Confira se os dados foram informados corretamente.");
                    _logger.Info(exception.GetMessage());
                }
            }

            ViewData["MeioPagamento"] = new SelectList(_meioPagamentoRepository.Find(), "Id", "Descricao");
            ViewData["Recebimentos"] = recebimentos;

            return View(model);
        }

        #endregion

        #region Devolucao
        [PopulateViewData("PopulateDevolucaoViewData")]
        public virtual ActionResult Devolucao(long? id)
        {
            var domain = _chequeRecebidoRepository.Get(id);

            if (domain != null)
            {
                var model = new DevolucaoChequeRecebidoModel
                {
                    Unidade = domain.Unidade.NomeFantasia,
                    Cliente = domain.Cliente.Nome,
                    Banco = domain.Banco.Nome,
                    Agencia = domain.Agencia,
                    Conta = domain.Conta,
                    NumeroCheque = domain.NumeroCheque,
                    SituacaoCheque = domain.UltimaOcorrenciaCompensacao().ChequeSituacao.EnumToString(),
                    Emitente = domain.Emitente.Nome1,
                    DataEmissao = domain.DataEmissao,
                    DataVencimento = domain.DataVencimento,
                    DataProrrogacao = domain.DataProrrogacao,
                    Valor = domain.Valor
                };

                return View(model);
            }

            this.AddErrorMessage("Não foi possível encontrar o cheque.");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [PopulateViewData("PopulateDevolucaoViewData")]
        public virtual ActionResult Devolucao(DevolucaoChequeRecebidoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var chequeRecebido = _chequeRecebidoRepository.Get(model.Id);
                    var situacaoCheque = chequeRecebido.UltimaOcorrenciaCompensacao().ChequeSituacao;

                    // Valida situação
                    if (_situacaoDevolucao.Contains(situacaoCheque) == false)
                    {
                        this.AddErrorMessage("A devolução de cheque é permitida apenas para cheques na situação <<Não Depositado>> ou <<Devolvido>>.");
                        return RedirectToAction("Index");
                    }

                    // Valida se a situação escolhida é devolução
                    if (model.SituacaoChequeRecebido != ChequeSituacao.Devolvido)
                    {
                        this.AddErrorMessage("A nova situação do cheque só pode ser <<Devolvido>>.");
                        return View(model);
                    }

                    // Criar ocorrência
                    var ocorrencia = new OcorrenciaCompensacao
                    {
                        Data = DateTime.Now,
                        ChequeSituacao = model.SituacaoChequeRecebido,
                        Historico = string.Format("Devolvido em {0} por {1}",
                                        DateTime.Now.ToString("F"), HttpContext.User.Identity.Name)
                    };

                    chequeRecebido.AddOcorrenciaCompensacao(ocorrencia);

                    _chequeRecebidoRepository.Save(chequeRecebido);

                    this.AddSuccessMessage("Cheque devolvido com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a devolução de cheque. Confira se os dados foram informados corretamente.");
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }

        #endregion

        #region ExcluirBaixa

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult ExcluirBaixa(long id)
        {
            var domain = _baixaChequeRecebidoRepository.Get(id);

            try
            {
                if (domain != null)
                {
                    this.AddSuccessMessage("Baixa de cheque excluída com sucesso");
                    _baixaChequeRecebidoRepository.Delete(domain);

                    var chequeRecebido = _chequeRecebidoRepository.Get(domain.ChequeRecebido);
                    chequeRecebido.Saldo += domain.Valor;
                    _chequeRecebidoRepository.Update(chequeRecebido);

                    var historico = string.Format("Baixa de cheque excluída em {0} por {1}.",
                                                  DateTime.Now.ToString("F"), HttpContext.User.Identity.Name);

                    var ocorrencia = new OcorrenciaCompensacao
                    {
                        Data = DateTime.Now,
                        ChequeSituacao = chequeRecebido.Saldo > 0 ? ChequeSituacao.QuitadoParcial : ChequeSituacao.QuitadoTotal,
                        Historico = historico,
                        ChequeRecebido = chequeRecebido
                    };
                    _ocorrenciaChequeRecebidoRepository.Save(ocorrencia);
                }
                else
                {
                    this.AddErrorMessage("Não foi possível excluir a baixa de cheque. O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao excluir a baixa de cheque: " + exception.Message);
                _logger.Info(exception.GetMessage());
            }

            var url = domain != null
                          ? string.Format("Editar/{0}#complemento", domain.ChequeRecebido.Id)
                          : "Index";
            return Redirect(url);
        }
        #endregion

        #region Funcionarios

        #region LerFuncionarios
        [AjaxOnly, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual ActionResult LerFuncionarios([DataSourceRequest]DataSourceRequest request)
        {
            var funcionarios = TempData.Peek(ChaveFuncionario) as List<GridFuncionarioModel>;

            if (funcionarios != null)
                return Json(funcionarios.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region AdicionarFuncionario
        [HttpPost]
        public virtual ActionResult AdicionarFuncionario(long codigo)
        {
            var funcionarios = TempData.Peek(ChaveFuncionario) as List<GridFuncionarioModel>
                                ?? new List<GridFuncionarioModel>();

            var funcionario = _pessoaRepository.Find(p => p.Funcionario.Codigo == codigo)
                .Select(p => new GridFuncionarioModel
                {
                    Id = p.Id.GetValueOrDefault(),
                    Codigo = p.Funcionario.Codigo,
                    CpfCnpj = p.CpfCnpj,
                    Nome = p.Nome,
                    DataCadastro = p.DataCadastro,
                    FuncaoFuncionario = p.Funcionario.FuncaoFuncionario.EnumToString()
                }).FirstOrDefault();

            if (funcionario != null && funcionarios.All(f => f.Id != funcionario.Id))
            {
                funcionarios.Add(funcionario);
                TempData[ChaveFuncionario] = funcionarios;
            }

            return Content(string.Empty);
        }
        #endregion

        #region RemoverFuncionario
        [HttpPost]
        public virtual ActionResult RemoverFuncionario(long id)
        {
            var funcionarios = TempData.Peek(ChaveFuncionario) as List<GridFuncionarioModel>;

            if (funcionarios != null)
            {
                var funcionario = funcionarios.FirstOrDefault(p => p.Id == id);

                if (funcionario != null)
                    funcionarios.Remove(funcionario);
            }

            return Content(string.Empty);
        }
        #endregion

        #endregion

        #region Prestadores

        #region LerPrestadores
        [AjaxOnly, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual ActionResult LerPrestadores([DataSourceRequest]DataSourceRequest request)
        {
            var prestadores = TempData.Peek(ChavePrestador) as List<GridPrestadorServicoModel>;

            if (prestadores != null)
                return Json(prestadores.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region AdicionarPrestador
        [HttpPost]
        public virtual ActionResult AdicionarPrestador(long codigo)
        {
            var prestadores = TempData.Peek(ChavePrestador) as List<GridPrestadorServicoModel>
                                ?? new List<GridPrestadorServicoModel>();

            var prestador = _pessoaRepository.Find(p => p.PrestadorServico.Codigo == codigo)
                .Select(p => new GridPrestadorServicoModel
                {
                    Id = p.Id.GetValueOrDefault(),
                    Codigo = p.PrestadorServico.Codigo,
                    CpfCnpj = p.CpfCnpj,
                    Nome = p.Nome,
                }).FirstOrDefault();

            if (prestador != null && prestadores.All(f => f.Id != prestador.Id))
            {
                prestadores.Add(prestador);
                TempData[ChavePrestador] = prestadores;
            }

            return Content(string.Empty);
        }
        #endregion

        #region RemoverPrestador
        [HttpPost]
        public virtual ActionResult RemoverPrestador(long id)
        {
            var prestadores = TempData.Peek(ChavePrestador) as List<GridPrestadorServicoModel>;

            if (prestadores != null)
            {
                var prestador = prestadores.FirstOrDefault(p => p.Id == id);

                if (prestador != null)
                    prestadores.Remove(prestador);
            }

            return Content(string.Empty);
        }
        #endregion

        #endregion

        #region LerOcorrencias

        /// <summary>
        /// Retorna as ocorrências como o tipo GridOcorrenciaCompensacaoModel, em formato json.
        /// </summary>
        /// <param name="request"> </param>
        /// <param name="id">Id do ChequeRecebido.</param>
        [AjaxOnly, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual ActionResult LerOcorrencias([DataSourceRequest]DataSourceRequest request, long id)
        {
            var ocorrencias = _chequeRecebidoRepository.Get(id).OcorrenciaCompensacoes
                .Select(o => new GridOcorrenciaCompensacaoModel
                {
                    Id = o.Id.GetValueOrDefault(),
                    Data = o.Data.ToString("dd/MM/yyyy"),
                    ChequeSituacao = o.ChequeSituacao.EnumToString()
                });

            return Json(ocorrencias.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region LerBaixasRealizadas

        /// <summary>
        /// Retorna as ocorrências como o tipo GridBaixaChequeRecebidoModel, em formato json.
        /// </summary>
        /// <param name="request"> </param>
        /// <param name="id">Id do ChequeRecebido</param>
        [AjaxOnly, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual ActionResult LerBaixasRealizadas([DataSourceRequest]DataSourceRequest request, long id)
        {
            var baixas = _chequeRecebidoRepository.Get(id).BaixaChequeRecebidos
                .Select(b => new GridBaixaChequeRecebidoModel
                {
                    Id = b.Id.GetValueOrDefault(),
                    Data = b.Data.ToString("dd/MM/yyyy"),
                    ValorJuros = b.ValorJuros.ToString("F") + "%",
                    ValorDesconto = b.ValorDesconto.ToString("C"),
                    Valor = b.Valor.ToString("C")
                });

            return Json(baixas.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

        #region Métodos

        #region OnPopulateViewData
        protected void PopulateViewData(IChequeRecebidoDropdownModel model)
        {
            var unidades = _pessoaRepository.Find(p => p.Unidade != null && p.Unidade.Ativo).ToList();
            ViewData["Unidade"] = unidades.ToSelectList("Nome", model.Unidade);
        }
        #endregion

        #region PopulateDevolucaoViewData
        protected void PopulateDevolucaoViewData(DevolucaoChequeRecebidoModel model)
        {
            var compensacaoCheques = _compensacaoChequeRepository.Find().ToList();
            ViewData["CompensacaoCheque"] = compensacaoCheques.ToSelectList("Descricao");
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var chequeRecebido = model as ChequeRecebidoModel;

        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
        }
        #endregion

        #endregion
    }
}