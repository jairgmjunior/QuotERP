using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.Extensions;
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
using Fashion.ERP.Domain;

namespace Fashion.ERP.Web.Areas.Comum.Controllers
{
    public partial class ClienteController : BaseController
    {
        #region Variaveis
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<Profissao> _profissaoRepository;
        private readonly IRepository<AreaInteresse> _areaInteresseRepository;
        private readonly IRepository<UF> _ufRepository;
        private readonly IRepository<Cidade> _cidadeRepository;
        private readonly IRepository<Arquivo> _arquivoRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public ClienteController(ILogger logger, IRepository<Pessoa> pessoaRepository,
            IRepository<Profissao> profissaoRepository, IRepository<AreaInteresse> areaInteresseRepository,
            IRepository<UF> ufRepository, IRepository<Cidade> cidadeRepository, IRepository<Arquivo> arquivoRepository)
        {
            _pessoaRepository = pessoaRepository;
            _profissaoRepository = profissaoRepository;
            _areaInteresseRepository = areaInteresseRepository;
            _ufRepository = ufRepository;
            _cidadeRepository = cidadeRepository;
            _arquivoRepository = arquivoRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var pessoas = _pessoaRepository.Find(p => p.Cliente != null);

            var list = pessoas.Select(p => new GridClienteModel
            {
                Id = p.Id.GetValueOrDefault(),
                Codigo = p.Cliente.Codigo,
                CpfCnpj = p.CpfCnpj.FormateCpfCnpj(),
                Nome = p.Nome,
                DataCadastro = p.Cliente.DataCadastro,
            }).ToList();

            return View(list);
        }
        #endregion

        #region Novo

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            return View(new NovoClienteModel { EnderecoTipoEndereco = TipoEndereco.Residencial });
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(NovoClienteModel model)
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
                    CadastrarCliente(ref domain);

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

                    domain.Foto = null; // Não tem foto ao incluir
                    _pessoaRepository.Save(domain);

                    this.AddSuccessMessage("Cliente cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o cliente. Confira se os dados foram informados corretamente.");
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
                var model = Mapper.Flat<ClienteModel>(domain);

                if (domain.TipoPessoa == TipoPessoa.Fisica)
                    model.Cpf = domain.CpfCnpj.DesformateCpfCnpj();
                else if (domain.TipoPessoa == TipoPessoa.Juridica)
                    model.Cnpj = domain.CpfCnpj.DesformateCpfCnpj();

                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o cliente.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(ClienteModel model, string filename)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var pessoa = _pessoaRepository.Get(model.Id);

                    // Buscar o nome da foto antes da atualização do Model
                    var nomeFoto = pessoa.Foto != null ? pessoa.Foto.Nome : "";

                    var domain = Mapper.Unflat(model, pessoa);
                    domain.CpfCnpj = model.TipoPessoa == TipoPessoa.Fisica ? model.Cpf.DesformateCpfCnpj()
                                   : model.TipoPessoa == TipoPessoa.Juridica ? model.Cnpj.DesformateCpfCnpj()
                                   : null;
                    CadastrarCliente(ref domain);

                    // Verificar se existe foto
                    if (model.FotoNome != null)
                    {
                        if (domain.Foto == null || nomeFoto != model.FotoNome)
                        {
                            // Se é uma foto nova, salvá-la no disco e depois no BD
                            var arquivo = ArquivoController.SalvarArquivo(model.FotoNome, model.Nome);
                            domain.Foto = _arquivoRepository.Save(arquivo);
                        }
                    }
                    else
                    {
                        // Se a foto foi excluída, remover do DB
                        if (domain.Foto != null && domain.Foto.Id.HasValue)
                        {
                            _arquivoRepository.Delete(domain.Foto);
                        }

                        domain.Foto = null;
                    }

                    _pessoaRepository.Update(domain);

                    this.AddSuccessMessage("Cliente atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao salvar o cliente. Confira se os dados foram informados corretamente.");
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

                    this.AddSuccessMessage("Cliente excluído com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir o cliente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { id });
        }
        #endregion

        #region CadastrarCliente
        /// <summary>
        /// Preenche os dados básicos do cliente.
        /// </summary>
        private void CadastrarCliente(ref Pessoa cliente)
        {
            if (cliente.Id.HasValue == false)
                cliente.DataCadastro = DateTime.Now;

            if (cliente.Cliente.Id.HasValue == false)
            {
                var proximoCodigo = _pessoaRepository.Find().Any(p => p.Cliente != null)
                                            ? _pessoaRepository.Find().Max(p => p.Cliente.Codigo) + 1
                                            : 1;
                cliente.Cliente.DataCadastro = DateTime.Now;
                cliente.Cliente.Codigo = proximoCodigo;
            }
        }
        #endregion

        #region VerificarCpfCnpj
        [AjaxOnly]
        public virtual JsonResult VerificarCpfCnpj(string cpfCnpj)
        {
            bool existePessoa = false, existeCliente = false;
            long pessoaId = 0;
            var pessoa = _pessoaRepository.Get(p => p.CpfCnpj == cpfCnpj.DesformateCpfCnpj());

            if (pessoa != null)
            {
                existePessoa = true;
                pessoaId = pessoa.Id.GetValueOrDefault();
                if (pessoa.Cliente != null)
                    existeCliente = true;
            }

            var result = new { existePessoa, existeCliente, pessoaId };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Pesquisar Cliente

        #region Pesquisar
        [ChildActionOnly, OutputCache(Duration = 3600)]
        public virtual ActionResult Pesquisar()
        {
            PreencheColuna();
            return PartialView();
        }
        #endregion

        #region Pesquisar
        [HttpPost, AjaxOnly]
        public virtual ActionResult PesquisarFiltro(PesquisarModel model)
        {
            var filters = new List<FilterExpression>
            {
                // Apenas clientes
                new FilterExpression("Cliente", ComparisonOperator.IsDifferent, null, LogicOperator.And),
                // Filtro da tela
                model.Filtrar<Pessoa>()
            };

            var clientes = _pessoaRepository.Find(filters.ToArray()).ToList();

            var list = clientes.Select(p => new GridClienteModel
            {
                Id = p.Id.GetValueOrDefault(),
                Codigo = p.Cliente.Codigo,
                CpfCnpj = p.CpfCnpj.FormateCpfCnpj(),
                Nome = p.Nome,
                DataCadastro = p.DataCadastro,
            }).OrderBy(p => p.Nome).ToList();

            return Json(list);
        }
        #endregion

        #region PesquisarCodigo
        [HttpGet, AjaxOnly]
        public virtual ActionResult PesquisarCodigo(long? codigo)
        {
            if (codigo.HasValue)
            {
                var cliente = _pessoaRepository.Find(p => p.Cliente.Codigo == codigo.Value).FirstOrDefault();

                if (cliente != null)
                    return Json(new { cliente.Id, cliente.Cliente.Codigo, cliente.Nome }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { erro = "Nenhum cliente encontrado." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PesquisarId
        [HttpGet, AjaxOnly]
        public virtual ActionResult PesquisarId(long id)
        {
            var cliente = _pessoaRepository.Get(id);

            if (cliente != null)
                return Json(new { cliente.Id, cliente.Cliente.Codigo, cliente.Nome }, JsonRequestBehavior.AllowGet);

            return Json(new { erro = "Nenhum cliente encontrado." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PreencheColuna
        private void PreencheColuna()
        {
            var coluna = new[]
                              {
                                  new { value = "Nome", text = "Nome"},
                                  new { value = "CpfCnpj", text = "Cpf/Cnpj"},
                                  new { value = "Cliente.Codigo", text = "Código"}
                              };
            ViewData["ColunaPesquisa"] = new SelectList(coluna, "value", "text");
        }
        #endregion

        #endregion

        #endregion

        #region Métodos

        #region PopulateViewData
        protected void PopulateViewData(ClienteModel model)
        {
            //var clienteModel = model as ClienteModel;
            var profissoes = _profissaoRepository.Find().OrderBy(o => o.Nome).ToList();
            ViewData["ClienteProfissao"] = profissoes.ToSelectList("Nome", model.ClienteProfissao);

            var areas = _areaInteresseRepository.Find().OrderBy(o => o.Nome).ToList();
            ViewData["ClienteAreaInteresse"] = areas.ToSelectList("Nome", model.ClienteAreaInteresse);

            // Tela de novo cliente
            var novoClienteModel = model as NovoClienteModel;
            if (novoClienteModel != null)
            {
                var ufId = 0L;
                var cidadeId = 0L;

                var cidade = _cidadeRepository.Get(novoClienteModel.EnderecoCidade);
                if (cidade != null)
                {
                    ufId = cidade.UF.Id.GetValueOrDefault();
                    cidadeId = cidade.Id.GetValueOrDefault();
                }

                var ufList = _ufRepository.Find().OrderBy(o => o.Nome).ToList();
                ViewData["Uf"] = ufList.ToSelectList("Nome", ufId);

                var cidadeList = _cidadeRepository.Find(p => p.UF.Id == ufId).ToList().OrderBy(o => o.Nome).ToList();
                ViewData["EnderecoCidade"] = cidadeList.ToSelectList("Nome", cidadeId);
            }
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var cliente = model as ClienteModel;

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