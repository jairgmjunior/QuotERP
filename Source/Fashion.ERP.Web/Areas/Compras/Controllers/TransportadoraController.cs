using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Extensions;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Fashion.Framework.UnitOfWork.DinamicFilter;
using Ninject.Extensions.Logging;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Areas.Compras.Models;
using Fashion.ERP.Domain.Compras;

namespace Fashion.ERP.Web.Areas.Compras.Controllers
{
    public partial class TransportadoraController : BaseController
    {
		#region Variaveis
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<UF> _ufRepository;
        private readonly IRepository<Cidade> _cidadeRepository;
        private readonly IRepository<ReferenciaExterna> _referenciaExternaRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public TransportadoraController(ILogger logger, IRepository<Pessoa> pessoaRepository,IRepository<UF> ufRepository,
            IRepository<Cidade> cidadeRepository, IRepository<ReferenciaExterna> referenciaExternaRepository)
        {
            _pessoaRepository = pessoaRepository;

            _ufRepository = ufRepository;
            _cidadeRepository = cidadeRepository;
            _referenciaExternaRepository = referenciaExternaRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var transportadoras = _pessoaRepository.Find(p => p.Transportadora != null);

            var list = transportadoras.Select(p => new GridTransportadoraModel
            {
                Id = p.Id.GetValueOrDefault(),
                Codigo = p.Transportadora.Codigo,
                CpfCnpj = p.CpfCnpj.FormateCpfCnpj(),
                DataCadastro = p.Transportadora.DataCadastro,
                Nome = p.Nome,
                Ativo = p.Transportadora.Ativo
            }).ToList();

            return View(list);
        }
        #endregion

        #region Novo

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            return View(new NovoTransportadoraModel { EnderecoTipoEndereco = TipoEndereco.Residencial });
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(NovoTransportadoraModel model)
        {
            // Validar IE
            if (model.TipoPessoa == TipoPessoa.Juridica)
            {
                var uf = _ufRepository.Get(Convert.ToInt64(Request.Form["Uf"]));
                if (uf != null)
                    if (model.InscricaoEstadual.IsInscricaoEstadual(uf.Sigla) == false)
                        ModelState.AddModelError("InscricaoEstadual", "Inscrição estadual inválida para a UF " + uf.Nome + ".");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<Pessoa>(model);
                    domain.CpfCnpj = model.TipoPessoa == TipoPessoa.Fisica ? model.Cpf.DesformateCpfCnpj()
                                   : model.TipoPessoa == TipoPessoa.Juridica ? model.Cnpj.DesformateCpfCnpj()
                                   : null;

                    if (domain.Transportadora == null)
                        domain.Transportadora = new Transportadora();

                    CadastrarTransportadora(ref domain);

                    // Adicionar o endereço
                    domain.AddEndereco(new Endereco
                    {
                        TipoEndereco = model.EnderecoTipoEndereco,
                        Logradouro = model.EnderecoLogradouro,
                        Numero = model.EnderecoNumero,
                        Complemento = model.EnderecoComplemento,
                        Bairro = model.EnderecoBairro,
                        Cep = model.EnderecoCep,
                        Cidade = new Cidade { Id = model.EnderecoCidade }
                    });
                    // Adicionar o contato
                    domain.AddContato(new Contato
                    {
                        TipoContato = model.ContatoTipoContato,
                        Nome = model.ContatoNome,
                        Telefone = model.ContatoTelefone,
                        Operadora = model.ContatoOperadora,
                        Email = model.ContatoEmail
                    });

                    _pessoaRepository.Save(domain);

                    this.AddSuccessMessage("Transportadora cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o transportadora. Confira se os dados foram informados corretamente.");
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
            var domain = _pessoaRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<TransportadoraModel>(domain);
                
                if (domain.TipoPessoa == TipoPessoa.Fisica)
                    model.Cpf = domain.CpfCnpj.FormateCpfCnpj();
                else if (domain.TipoPessoa == TipoPessoa.Juridica)
                    model.Cnpj = domain.CpfCnpj.FormateCpfCnpj();

                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o transportadora.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(TransportadoraModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _pessoaRepository.Get(model.Id));
                    domain.CpfCnpj = model.TipoPessoa == TipoPessoa.Fisica ? model.Cpf.DesformateCpfCnpj()
                                   : model.TipoPessoa == TipoPessoa.Juridica ? model.Cnpj.DesformateCpfCnpj()
                                   : null;
                    if (domain.Transportadora == null)
                        domain.Transportadora = new Transportadora();

                    CadastrarTransportadora(ref domain);
                    _pessoaRepository.Update(domain);

                    this.AddSuccessMessage("Transportadora atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o transportadora. Confira se os dados foram informados corretamente.");
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
                    var domain = _pessoaRepository.Get(id);
                    _pessoaRepository.Delete(domain);

                    this.AddSuccessMessage("Transportadora excluída com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir o Transportadora: " + exception.Message);
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
                var domain = _pessoaRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Transportadora.Ativo;

                    domain.Transportadora.Ativo = situacao;
                    _pessoaRepository.Update(domain);
                    this.AddSuccessMessage("Transportadora {0} com sucesso", situacao ? "ativada" : "inativada");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação do transportadora: " + exception.Message);
                _logger.Info(exception.GetMessage());
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region CadastrarTransportadora
        /// <summary>
        /// Preenche os dados básicos da transportadora.
        /// </summary>
        private void CadastrarTransportadora(ref Pessoa transportadora)
        {
            if (transportadora.Id.HasValue == false)
                transportadora.DataCadastro = DateTime.Now;

            if (transportadora.Transportadora.Id.HasValue == false)
            {
                var proximoCodigo = _pessoaRepository.Find().Any(p => p.Transportadora != null)
                                            ? _pessoaRepository.Find().Max(p => p.Transportadora.Codigo) + 1
                                            : 1;
                transportadora.Transportadora.DataCadastro = DateTime.Now;
                transportadora.Transportadora.Ativo = true;
                transportadora.Transportadora.Codigo = proximoCodigo;
            }
        }
        #endregion

        #region VerificarCpfCnpj
        [AjaxOnly]
        public virtual JsonResult VerificarCpfCnpj(string cpfCnpj)
        {
            bool existePessoa = false, existeTransportadora = false;
            long pessoaId = 0;
            var pessoa = _pessoaRepository.Get(p => p.CpfCnpj == cpfCnpj.DesformateCpfCnpj());

            if (pessoa != null)
            {
                existePessoa = true;
                pessoaId = pessoa.Id.GetValueOrDefault();
                if (pessoa.Transportadora != null)
                    existeTransportadora = true;
            }

            var result = new { existePessoa, existeTransportadora, pessoaId };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Pesquisar Transportadora

        #region Pesquisar
        [ChildActionOnly, OutputCache(Duration = 3600)]
        public virtual ActionResult Pesquisar()
        {
            PreencheColuna();
            return PartialView();
        }
        #endregion

        #region PesquisarFiltro
        [HttpPost, AjaxOnly]
        public virtual ActionResult PesquisarFiltro(PesquisarModel model)
        {
            var filters = new List<FilterExpression>
            {
                // Apenas funcionários
                new FilterExpression("Transportadora", ComparisonOperator.IsDifferent, null, LogicOperator.And),
                // Filtro da tela
                model.Filtrar<Pessoa>()
            };

            var transportadoras = _pessoaRepository.Find(filters.ToArray()).ToList();

            var list = transportadoras.Select(p => new GridTransportadoraModel
            {
                Id = p.Id.GetValueOrDefault(),
                Codigo = p.Transportadora.Codigo,
                CpfCnpj = p.CpfCnpj.FormateCpfCnpj(),
                Nome = p.Nome,
                DataCadastro = p.DataCadastro,
            }).OrderBy(p=> p.Nome).ToList();

            return Json(list);
        }
        #endregion

        #region PesquisarCodigo
        [HttpGet, AjaxOnly]
        public virtual ActionResult PesquisarCodigo(long? codigo)
        {
            if (codigo.HasValue)
            {
                var transportadora = _pessoaRepository.Find(p => p.Transportadora.Codigo == codigo.Value).FirstOrDefault();

                if (transportadora != null)
                    return Json(new { transportadora.Id, transportadora.Transportadora.Codigo, transportadora.Nome }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { erro = "Nenhuma transportadora encontrado." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PesquisarId
        [HttpGet, AjaxOnly]
        public virtual ActionResult PesquisarId(long id)
        {
            var transportadora = _pessoaRepository.Get(id);

            if (transportadora != null)
                return Json(new { transportadora.Id, transportadora.Transportadora.Codigo, transportadora.Nome }, JsonRequestBehavior.AllowGet);

            return Json(new { erro = "Nenhuma transportadora encontrado." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PreencheColuna
        private void PreencheColuna()
        {
            var coluna = new[]
                              {
                                  new { value = "Nome", text = "Nome"},
                                  new { value = "CpfCnpj", text = "Cpf/Cnpj"},
                                  new { value = "Transportadora.Codigo", text = "Código"}
                              };
            ViewData["ColunaPesquisa"] = new SelectList(coluna, "value", "text");
        }
        #endregion

        #endregion

        #endregion

        #region Métodos

        #region PopulateViewData
        protected void PopulateViewData(TransportadoraModel model)
        {
            
            // Tela de novo Transportadora
            var novoTransportadoraModel = model as NovoTransportadoraModel;
            if (novoTransportadoraModel != null)
            {
                var ufId = 0L;
                var cidadeId = 0L;

                var cidade = _cidadeRepository.Get(novoTransportadoraModel.EnderecoCidade);
                if (cidade != null)
                {
                    ufId = cidade.UF.Id.GetValueOrDefault();
                    cidadeId = cidade.Id.GetValueOrDefault();
                }

                var ufs = _ufRepository.Find().OrderBy(o => o.Nome).ToList();
                ViewData["Uf"] = ufs.ToSelectList("Nome", ufId);

                var cidades = _cidadeRepository.Find(p => p.UF.Id == ufId).OrderBy(o => o.Nome).ToList();
                ViewData["EnderecoCidade"] = cidades.ToSelectList("Nome", cidadeId);
            }
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var transportadora = model as TransportadoraModel;

        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            // Referência externa
            //if (_referenciaExternaRepository.Find().Any(p => p.Transportadora.Id == id))
            //    ModelState.AddModelError("", "Não é possível excluir esta transportadora, pois existe uma referência externa cadastrada com ela.");
        }
        #endregion

        #endregion
    }
}