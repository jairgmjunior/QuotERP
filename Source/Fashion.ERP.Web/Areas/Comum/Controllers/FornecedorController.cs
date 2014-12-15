using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Comum.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Fashion.Framework.UnitOfWork.DinamicFilter;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Comum.Controllers
{
    public partial class FornecedorController : BaseController
    {
		#region Variaveis
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<TipoFornecedor> _tipoFornecedorRepository;
        private readonly IRepository<UF> _ufRepository;
        private readonly IRepository<Cidade> _cidadeRepository;
        private readonly IRepository<ReferenciaExterna> _referenciaExternaRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public FornecedorController(ILogger logger, IRepository<Pessoa> pessoaRepository,
            IRepository<TipoFornecedor> tipoFornecedorRepository, IRepository<UF> ufRepository,
            IRepository<Cidade> cidadeRepository, IRepository<ReferenciaExterna> referenciaExternaRepository)
        {
            _pessoaRepository = pessoaRepository;
            _tipoFornecedorRepository = tipoFornecedorRepository;
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
            var fornecedores = _pessoaRepository.Find(p => p.Fornecedor != null);

            var list = fornecedores.Select(p => new GridFornecedorModel
            {
                Id = p.Id.GetValueOrDefault(),
                Codigo = p.Fornecedor.Codigo,
                CpfCnpj = p.CpfCnpj,
                DataCadastro = p.Fornecedor.DataCadastro,
                Nome = p.Nome,
                TipoFornecedor = p.Fornecedor.TipoFornecedor.Descricao,
                Ativo = p.Fornecedor.Ativo
            }).ToList();

            return View(list);
        }
        #endregion

        #region Novo

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            return View(new NovoFornecedorModel { EnderecoTipoEndereco = TipoEndereco.Residencial });
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(NovoFornecedorModel model)
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
                    domain.CpfCnpj = model.TipoPessoa == TipoPessoa.Fisica ? model.Cpf
                                   : model.TipoPessoa == TipoPessoa.Juridica ? model.Cnpj
                                   : null;

                    CadastrarFornecedor(ref domain);

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

                    this.AddSuccessMessage("Fornecedor cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o fornecedor. Confira se os dados foram informados corretamente.");
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
                var model = Mapper.Flat<FornecedorModel>(domain);
                
                if (domain.TipoPessoa == TipoPessoa.Fisica)
                    model.Cpf = domain.CpfCnpj;
                else if (domain.TipoPessoa == TipoPessoa.Juridica)
                    model.Cnpj = domain.CpfCnpj;

                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o fornecedor.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(FornecedorModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _pessoaRepository.Get(model.Id));
                    domain.CpfCnpj = model.TipoPessoa == TipoPessoa.Fisica ? model.Cpf
                                   : model.TipoPessoa == TipoPessoa.Juridica ? model.Cnpj
                                   : null;
                    
                    
                    CadastrarFornecedor(ref domain);
                    _pessoaRepository.Update(domain);

                    this.AddSuccessMessage("Fornecedor atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o fornecedor. Confira se os dados foram informados corretamente.");
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

                    this.AddSuccessMessage("Fornecedor excluído com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir o Fornecedor: " + exception.Message);
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
                    var situacao = !domain.Fornecedor.Ativo;

                    domain.Fornecedor.Ativo = situacao;
                    _pessoaRepository.Update(domain);
                    this.AddSuccessMessage("Fornecedor {0} com sucesso", situacao ? "ativada" : "inativada");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação do fornecedor: " + exception.Message);
                _logger.Info(exception.GetMessage());
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region CadastrarFornecedor
        /// <summary>
        /// Preenche os dados básicos do fornecedor.
        /// </summary>
        private void CadastrarFornecedor(ref Pessoa fornecedor)
        {
            if (fornecedor.Id.HasValue == false)
                fornecedor.DataCadastro = DateTime.Now;

            if (fornecedor.Fornecedor.Id.HasValue == false)
            {
                var proximoCodigo = _pessoaRepository.Find().Any(p => p.Fornecedor != null)
                                            ? _pessoaRepository.Find().Max(p => p.Fornecedor.Codigo) + 1
                                            : 1;
                fornecedor.Fornecedor.DataCadastro = DateTime.Now;
                fornecedor.Fornecedor.Ativo = true;
                fornecedor.Fornecedor.Codigo = proximoCodigo;
            }
        }
        #endregion

        #region VerificarCpfCnpj
        [AjaxOnly]
        public virtual JsonResult VerificarCpfCnpj(string cpfCnpj)
        {
            bool existePessoa = false, existeFornecedor = false;
            long pessoaId = 0;
            var pessoa = _pessoaRepository.Get(p => p.CpfCnpj == cpfCnpj);

            if (pessoa != null)
            {
                existePessoa = true;
                pessoaId = pessoa.Id.GetValueOrDefault();
                if (pessoa.Fornecedor != null)
                    existeFornecedor = true;
            }

            var result = new { existePessoa, existeFornecedor, pessoaId };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Pesquisar Fornecedor

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
                new FilterExpression("Fornecedor", ComparisonOperator.IsDifferent, null, LogicOperator.And),
                // Filtro da tela
                model.Filtrar<Pessoa>()
            };

            var fornecedores = _pessoaRepository.Find(filters.ToArray()).ToList();

            var list = fornecedores.Select(p => new GridFornecedorModel
            {
                Id = p.Id.GetValueOrDefault(),
                Codigo = p.Fornecedor.Codigo,
                CpfCnpj = p.CpfCnpj,
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
                var fornecedor = _pessoaRepository.Find(p => p.Fornecedor.Codigo == codigo.Value).FirstOrDefault();

                if (fornecedor != null)
                    return Json(new { fornecedor.Id, fornecedor.Fornecedor.Codigo, fornecedor.Nome }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { erro = "Nenhum fornecedor encontrado." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PesquisarId
        [HttpGet, AjaxOnly]
        public virtual ActionResult PesquisarId(long id)
        {
            var fornecedor = _pessoaRepository.Get(id);

            if (fornecedor != null)
                return Json(new { fornecedor.Id, fornecedor.Fornecedor.Codigo, fornecedor.Nome }, JsonRequestBehavior.AllowGet);

            return Json(new { erro = "Nenhum fornecedor encontrado." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PreencheColuna
        private void PreencheColuna()
        {
            var coluna = new[]
                              {
                                  new { value = "Nome", text = "Nome"},
                                  new { value = "CpfCnpj", text = "Cpf/Cnpj"},
                                  new { value = "Fornecedor.Codigo", text = "Código"}
                              };
            ViewData["ColunaPesquisa"] = new SelectList(coluna, "value", "text");
        }
        #endregion

        #endregion

        #endregion

        #region Métodos

        #region PopulateViewData
        protected void PopulateViewData(FornecedorModel model)
        {
            var tipoFornecedores = _tipoFornecedorRepository.Find().OrderBy(o => o.Descricao).ToList();
            ViewData["FornecedorTipoFornecedor"] = tipoFornecedores.ToSelectList("Descricao", model.FornecedorTipoFornecedor);

            // Tela de novo Fornecedor
            var novoFornecedorModel = model as NovoFornecedorModel;
            if (novoFornecedorModel != null)
            {
                var ufId = 0L;
                var cidadeId = 0L;

                var cidade = _cidadeRepository.Get(novoFornecedorModel.EnderecoCidade);
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
            var fornecedor = model as FornecedorModel;

        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            // Referência externa
            if (_referenciaExternaRepository.Find().Any(p => p.Fornecedor.Id == id))
                ModelState.AddModelError("", "Não é possível excluir este fornecedor, pois existe uma referência externa cadastrada com ele.");
        }
        #endregion

        #endregion
    }
}