using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
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
using FluentNHibernate.Conventions;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Comum.Controllers
{
    public partial class EmpresaController : BaseController
    {
        #region Variaveis
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<UF> _ufRepository;
        private readonly IRepository<Cidade> _cidadeRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public EmpresaController(ILogger logger, IRepository<Pessoa> pessoaRepository,
            IRepository<UF> ufRepository, IRepository<Cidade> cidadeRepository)
        {
            _pessoaRepository = pessoaRepository;
            _ufRepository = ufRepository;
            _cidadeRepository = cidadeRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var empresas = _pessoaRepository.Find(p => p.Empresa != null);

            var list = empresas.Select(p => new GridEmpresaModel
            {
                Id = p.Id.GetValueOrDefault(),
                Codigo = p.Empresa.Codigo,
                CpfCnpj = p.CpfCnpj,
                Nome = p.Nome,
                Ativo = p.Empresa.Ativo
            }).ToList();

            return View(list);
        }
        #endregion

        #region Novo

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            return View(new NovoEmpresaModel{ EnderecoTipoEndereco = TipoEndereco.Residencial }); //todo qual o tipo de endereço de pessoa!?!?
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(NovoEmpresaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var pessoa = Mapper.Unflat<Pessoa>(model);
                    pessoa.CpfCnpj = model.Cnpj;
                    pessoa.DataNascimento = model.DataFundacao;

                    CadastrarEmpresa(ref pessoa);

                    // Adicionar o endereço
                    pessoa.AddEndereco(new Endereco
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
                    pessoa.AddContato(new Contato
                    {
                        TipoContato = model.ContatoTipoContato,
                        Nome = model.ContatoNome,
                        Telefone = model.ContatoTelefone,
                        Operadora = model.ContatoOperadora,
                        Email = model.ContatoEmail
                    });
                    
                    _pessoaRepository.Save(pessoa);
                    
                    this.AddSuccessMessage("Empresa cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a Empresa. Confira se os dados foram informados corretamente.");
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
                var model = Mapper.Flat<EmpresaModel>(domain);
                model.Cnpj = domain.CpfCnpj;
                model.DataFundacao = domain.DataNascimento;

                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a pessoa.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(EmpresaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _pessoaRepository.Get(model.Id));
                    domain.CpfCnpj = model.Cnpj;
                    domain.DataNascimento = model.DataFundacao;

                    CadastrarEmpresa(ref domain);
                    _pessoaRepository.Update(domain);

                    this.AddSuccessMessage("Empresa atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a pessoa. Confira se os dados foram informados corretamente.");
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

                    this.AddSuccessMessage("Empresa excluído com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir a pessoa: " + exception.Message);
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
                    var situacao = !domain.Empresa.Ativo;

                    domain.Empresa.Ativo = situacao;
                    _pessoaRepository.Update(domain);
                    this.AddSuccessMessage("Empresa {0} com sucesso", situacao ? "ativada" : "inativada");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação da pessoa: " + exception.Message);
                _logger.Info(exception.GetMessage());
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region VerificarCpfCnpj
        [AjaxOnly]
        public virtual JsonResult VerificarCpfCnpj(string cpfCnpj)
        {
            bool existePessoa = false, existeEmpresa = false;
            long pessoaId = 0;
            var pessoa = _pessoaRepository.Get(p => p.CpfCnpj == cpfCnpj);

            if (pessoa != null)
            {
                existePessoa = true;
                pessoaId = pessoa.Id.GetValueOrDefault();
                if (pessoa.Empresa != null)
                    existeEmpresa = true;
            }

            var result = new { existePessoa, existeEmpresa, pessoaId };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Pesquisar Empresa

        #region Pesquisar
        [ChildActionOnly]
        public virtual ActionResult Pesquisar(PesquisarModel model)
        {
            PreencheColuna();
            return PartialView(model);
        }
        #endregion

        #region PesquisarFiltro
        [HttpPost, AjaxOnly]
        public virtual ActionResult PesquisarFiltro(PesquisarModel model)
        {
            var filters = new List<FilterExpression>
            {
                // Apenas empresas
                new FilterExpression("Empresa", ComparisonOperator.IsDifferent, null, LogicOperator.And),
                // Filtro da tela
                model.Filtrar<Pessoa>()
            };

            var pessoas = _pessoaRepository.Find(filters.ToArray()).ToList();
            
            var list = pessoas.Select(p => new GridEmpresaModel
            {
                Id = p.Id.GetValueOrDefault(),
                Codigo = p.Empresa.Codigo,
                CpfCnpj = p.CpfCnpj,
                Nome = p.Nome
            }).ToList();

            return Json(list);
        }
        #endregion

        #region PesquisarCodigo
        [HttpGet, AjaxOnly]
        public virtual ActionResult PesquisarCodigo(long? codigo)
        {
            if (codigo.HasValue)
            {
                var query = _pessoaRepository.Find(p => p.Empresa.Codigo == codigo.Value);
                
                var pessoa = query.FirstOrDefault();

                if (pessoa != null)
                    return Json(new { pessoa.Id, pessoa.Empresa.Codigo, pessoa.Nome }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { erro = "Nenhuma pessoa encontrada." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PesquisarId
        [HttpGet, AjaxOnly]
        public virtual ActionResult PesquisarId(long id)
        {
            var pessoa = _pessoaRepository.Get(id);

            if (pessoa != null)
                return Json(new { pessoa.Id, pessoa.Empresa.Codigo, pessoa.Nome }, JsonRequestBehavior.AllowGet);

            return Json(new { erro = "Nenhuma pessoa encontrada." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PreencheColuna
        private void PreencheColuna()
        {
            var coluna = new[]
                              {
                                  new { value = "Nome", text = "Nome"},
                                  new { value = "CpfCnpj", text = "Cpf/Cnpj"},
                                  new { value = "Empresa.Codigo", text = "Código"}
                              };
            ViewData["Coluna"] = new SelectList(coluna, "value", "text");
        }
        #endregion

        #endregion

        #endregion

        #region Métodos

        #region CadastrarEmpresa
        /// <summary>
        /// Preenche os dados básicos da empresa.
        /// </summary>
        private void CadastrarEmpresa(ref Pessoa pessoa)
        {
            if (pessoa.Id.HasValue == false)
                pessoa.DataCadastro = DateTime.Now;

            if(pessoa.Empresa == null)
                pessoa.Empresa = new Empresa();

            if (pessoa.Empresa.Id.HasValue == false)
            {
                var proximoCodigo = _pessoaRepository.Find().Any(p => p.Empresa != null)
                                            ? _pessoaRepository.Find().Max(p => p.Empresa.Codigo) + 1
                                            : 1;
                pessoa.Empresa.DataCadastro = DateTime.Now;
                pessoa.Empresa.Ativo = true;
                pessoa.Empresa.Codigo = proximoCodigo;
            }
        }
        #endregion

        #region PopulateViewData
        protected void PopulateViewData(EmpresaModel model)
        {
            // Tela de nova pessoa
            var novoEmpresaModel = model as NovoEmpresaModel;
            if (novoEmpresaModel != null)
            {
                // Preencher e selecionar os combos de UF e Cidade
                var ufId = 0L;
                var cidadeId = 0L;

                var cidade = _cidadeRepository.Get(novoEmpresaModel.EnderecoCidade);
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
            var empresa = (EmpresaModel)model;

            var ufForm = Request.Form["Uf"];

            if (ufForm == null || ufForm.IsEmpty())
                return;

            var uf = _ufRepository.Get(Convert.ToInt64(ufForm));
            if (uf != null)
                if (empresa.InscricaoEstadual.IsInscricaoEstadual(uf.Sigla) == false)
                    ModelState.AddModelError("InscricaoEstadual", "Inscrição estadual inválida para a UF " + uf.Nome + ".");
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