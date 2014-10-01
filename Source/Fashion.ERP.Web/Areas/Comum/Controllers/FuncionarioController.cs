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
    public partial class FuncionarioController : BaseController
    {
		#region Variaveis
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<UF> _ufRepository;
        private readonly IRepository<Cidade> _cidadeRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public FuncionarioController(ILogger logger, IRepository<Pessoa> pessoaRepository,
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
            var funcionarios = _pessoaRepository.Find(p => p.Funcionario != null);

            var list = funcionarios.Select(p => new GridFuncionarioModel
            {
                Id = p.Id.GetValueOrDefault(),
                Codigo = p.Funcionario.Codigo,
                CpfCnpj = p.CpfCnpj,
                Nome = p.Nome,
                DataCadastro = p.Funcionario.DataCadastro,
                FuncaoFuncionario = p.Funcionario.FuncaoFuncionario.EnumToString(),
                Ativo = p.Funcionario.Ativo
            }).ToList();

            return View(list);
        }
        #endregion

        #region Novo

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            return View(new NovoFuncionarioModel { EnderecoTipoEndereco = TipoEndereco.Residencial });
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(NovoFuncionarioModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<Pessoa>(model);
                    domain.CpfCnpj = model.Cpf;
                    CadastrarFuncionario(ref domain);

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

                    this.AddSuccessMessage("Funcionário cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o Funcionário. Confira se os dados foram informados corretamente.");
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
                var model = Mapper.Flat<FuncionarioModel>(domain);
                model.Cpf = domain.CpfCnpj;

                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o funcionário.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(FuncionarioModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _pessoaRepository.Get(model.Id));
                    domain.CpfCnpj = model.Cpf;
                    CadastrarFuncionario(ref domain);
                    _pessoaRepository.Update(domain);

                    this.AddSuccessMessage("Funcionário atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o funcionário. Confira se os dados foram informados corretamente.");
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

                    this.AddSuccessMessage("Funcionário excluído com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir o funcionário: " + exception.Message);
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
                    var situacao = !domain.Funcionario.Ativo;

                    domain.Funcionario.Ativo = situacao;
                    _pessoaRepository.Update(domain);
                    this.AddSuccessMessage("Funcionário {0} com sucesso", situacao ? "ativada" : "inativada");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação do funcionário: " + exception.Message);
                _logger.Info(exception.GetMessage());
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region VerificarCpfCnpj
        [AjaxOnly]
        public virtual JsonResult VerificarCpfCnpj(string cpfCnpj)
        {
            bool existePessoa = false, existeFuncionario = false;
            long pessoaId = 0;
            var pessoa = _pessoaRepository.Get(p => p.CpfCnpj == cpfCnpj);

            if (pessoa != null)
            {
                existePessoa = true;
                pessoaId = pessoa.Id.GetValueOrDefault();
                if (pessoa.Funcionario != null)
                    existeFuncionario = true;
            }

            var result = new { existePessoa, existeFuncionario, pessoaId };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Pesquisar Funcionario

        #region Pesquisar
        [ChildActionOnly]
        public virtual ActionResult Pesquisar(PesquisarFuncionarioModel model)
        {
            PreencheColuna();
            return PartialView(model);
        }
        #endregion

        #region PesquisarFiltro
        [HttpPost, AjaxOnly]
        public virtual ActionResult PesquisarFiltro(PesquisarFuncionarioModel model)
        {
            var filters = new List<FilterExpression>
            {
                // Apenas funcionários
                new FilterExpression("Funcionario", ComparisonOperator.IsDifferent, null, LogicOperator.And),
                // Filtro da tela
                model.Filtrar<Pessoa>()
            };

            var funcionarios = _pessoaRepository.Find(filters.ToArray()).ToList();

            if (model.FuncaoFuncionario != null)
                funcionarios = funcionarios.Where(p => model.FuncaoFuncionario.Contains(p.Funcionario.FuncaoFuncionario)).ToList();

            var list = funcionarios.Select(p => new GridFuncionarioModel
            {
                Id = p.Id.GetValueOrDefault(),
                Codigo = p.Funcionario.Codigo,
                CpfCnpj = p.CpfCnpj,
                Nome = p.Nome
            }).ToList();

            return Json(list);
        }
        #endregion

        #region PesquisarCodigo
        [HttpGet, AjaxOnly]
        public virtual ActionResult PesquisarCodigo(long? codigo, string funcaoFuncionario)
        {
            if (codigo.HasValue)
            {
                var query = _pessoaRepository.Find(p => p.Funcionario.Codigo == codigo.Value);

                if (string.IsNullOrEmpty(funcaoFuncionario) == false)
                {
                    var funcoes = funcaoFuncionario.Split(',').Select(p => (FuncaoFuncionario)Enum.Parse(typeof(FuncaoFuncionario), p)).ToList();
                    query = query.Where(p => funcoes.Contains(p.Funcionario.FuncaoFuncionario));
                }

                var cliente = query.FirstOrDefault();

                if (cliente != null)
                    return Json(new { cliente.Id, cliente.Funcionario.Codigo, cliente.Nome }, JsonRequestBehavior.AllowGet); 
            }

            return Json(new { erro = "Nenhum funcionário encontrado." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PesquisarId
        [HttpGet, AjaxOnly]
        public virtual ActionResult PesquisarId(long id)
        {
            var cliente = _pessoaRepository.Get(id);

            if (cliente != null)
                return Json(new { cliente.Id, cliente.Funcionario.Codigo, cliente.Nome }, JsonRequestBehavior.AllowGet);

            return Json(new { erro = "Nenhum funcionário encontrado." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PreencheColuna
        private void PreencheColuna()
        {
            var coluna = new[]
                              {
                                  new { value = "Nome", text = "Nome"},
                                  new { value = "CpfCnpj", text = "Cpf/Cnpj"},
                                  new { value = "Funcionario.Codigo", text = "Código"}
                              };
            ViewData["Coluna"] = new SelectList(coluna, "value", "text");
        }
        #endregion

        #endregion

        #endregion

        #region Métodos

        #region CadastrarFuncionario
        /// <summary>
        /// Preenche os dados básicos do funcionario.
        /// </summary>
        private void CadastrarFuncionario(ref Pessoa funcionario)
        {
            if (funcionario.Id.HasValue == false)
                funcionario.DataCadastro = DateTime.Now;

            if (funcionario.Funcionario.Id.HasValue == false)
            {
                var proximoCodigo = _pessoaRepository.Find().Any(p => p.Funcionario != null)
                                            ? _pessoaRepository.Find().Max(p => p.Funcionario.Codigo) + 1
                                            : 1;
                funcionario.Funcionario.DataCadastro = DateTime.Now;
                funcionario.Funcionario.Ativo = true;
                funcionario.Funcionario.Codigo = proximoCodigo;
            }
        }
        #endregion

        #region PopulateViewData
        protected void PopulateViewData(FuncionarioModel model)
        {
            // Tela de novo funcionário
            var novoFuncionarioModel = model as NovoFuncionarioModel;
            if (novoFuncionarioModel != null)
            {
                // Preencher e selecionar os combos de UF e Cidade
                var ufId = 0L;
                var cidadeId = 0L;

                var cidade = _cidadeRepository.Get(novoFuncionarioModel.EnderecoCidade);
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
            var funcionario = (FuncionarioModel)model;
            
            var ufForm = Request.Form["Uf"];

            if (ufForm == null || ufForm.IsEmpty())
                return; 

            var uf = _ufRepository.Get(Convert.ToInt64(ufForm));
            if (uf != null)
                if (funcionario.InscricaoEstadual.IsInscricaoEstadual(uf.Sigla) == false)
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