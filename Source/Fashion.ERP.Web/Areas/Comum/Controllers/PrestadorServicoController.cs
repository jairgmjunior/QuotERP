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

namespace Fashion.ERP.Web.Areas.Comum.Controllers
{
    public partial class PrestadorServicoController : BaseController
    {
		#region Variaveis
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<UF> _ufRepository;
        private readonly IRepository<Cidade> _cidadeRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public PrestadorServicoController(ILogger logger, IRepository<Pessoa> pessoaRepository,
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
            var prestadorServicos = _pessoaRepository.Find(p => p.PrestadorServico != null);

            var list = prestadorServicos.Select(p => new GridPrestadorServicoModel
            {
                Id = p.Id.GetValueOrDefault(),
                Codigo = p.PrestadorServico.Codigo,
                Nome = p.Nome,
                CpfCnpj = p.CpfCnpj.FormateCpfCnpj(),
                DataCadastro = p.PrestadorServico.DataCadastro,
                Ativo = p.PrestadorServico.Ativo
            }).ToList();

            return View(list);
        }
        #endregion

        #region Novo
        
        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            return View(new NovoPrestadorServicoModel { EnderecoTipoEndereco = TipoEndereco.Residencial });
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(NovoPrestadorServicoModel model)
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
                    CadastrarPrestadorServico(ref domain);

                    domain.PrestadorServico.AddTipoPrestadorServico(model.PrestadorServicoTipoPrestadorServicos);

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

                    this.AddSuccessMessage("Prestador de servico cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o prestador de servico. Confira se os dados foram informados corretamente.");
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
                var model = Mapper.Flat<PrestadorServicoModel>(domain);

                if (domain.TipoPessoa == TipoPessoa.Fisica)
                    model.Cpf = domain.CpfCnpj.FormateCpfCnpj();
                else if (domain.TipoPessoa == TipoPessoa.Juridica)
                    model.Cnpj = domain.CpfCnpj.FormateCpfCnpj();

                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o prestador de servico.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(PrestadorServicoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _pessoaRepository.Get(model.Id));

                    domain.CpfCnpj = model.TipoPessoa == TipoPessoa.Fisica ? model.Cpf.DesformateCpfCnpj()
                                   : model.TipoPessoa == TipoPessoa.Juridica ? model.Cnpj.DesformateCpfCnpj()
                                   : null;

                    domain.PrestadorServico.AddTipoPrestadorServico(model.PrestadorServicoTipoPrestadorServicos);
                    CadastrarPrestadorServico(ref domain);
                    _pessoaRepository.Update(domain);

                    this.AddSuccessMessage("Prestador de servico atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o prestador de servico. Confira se os dados foram informados corretamente.");
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

                    this.AddSuccessMessage("Prestador de servico excluído com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("",
                                             "Ocorreu um erro ao excluir o prestador de servico: " + exception.Message);
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
                    var situacao = !domain.PrestadorServico.Ativo;

                    domain.PrestadorServico.Ativo = situacao;
                    _pessoaRepository.Update(domain);
                    this.AddSuccessMessage("Prestador de serviço {0} com sucesso", situacao ? "ativada" : "inativada");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação do prestador de serviço: " + exception.Message);
                _logger.Info(exception.GetMessage());
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region CadastrarPrestadorServico
        /// <summary>
        /// Preenche os dados básicos do prestador de servico.
        /// </summary>
        private void CadastrarPrestadorServico(ref Pessoa prestadorServico)
        {
            if (prestadorServico.Id.HasValue == false)
                prestadorServico.DataCadastro = DateTime.Now;

            if (prestadorServico.PrestadorServico.Id.HasValue == false)
            {
                var proximoCodigo = _pessoaRepository.Find().Any(p => p.PrestadorServico != null)
                                            ? _pessoaRepository.Find().Max(p => p.PrestadorServico.Codigo) + 1
                                            : 1;
                prestadorServico.PrestadorServico.DataCadastro = DateTime.Now;
                prestadorServico.PrestadorServico.Ativo = true;
                prestadorServico.PrestadorServico.Codigo = proximoCodigo;
            }
        }
        #endregion

        #region VerificarCpfCnpj
        [AjaxOnly]
        public virtual JsonResult VerificarCpfCnpj(string cpfCnpj)
        {
            bool existePessoa = false, existePrestadorServico = false;
            long pessoaId = 0;
            var pessoa = _pessoaRepository.Get(p => p.CpfCnpj == cpfCnpj.DesformateCpfCnpj());

            if (pessoa != null)
            {
                existePessoa = true;
                pessoaId = pessoa.Id.GetValueOrDefault();
                if (pessoa.PrestadorServico != null)
                    existePrestadorServico = true;
            }

            var result = new { existePessoa, existePrestadorServico, pessoaId };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Pesquisar PrestadorServico

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
                new FilterExpression("PrestadorServico", ComparisonOperator.IsDifferent, null, LogicOperator.And),
                // Filtro da tela
                model.Filtrar<Pessoa>()
            };

            var prestadores = _pessoaRepository.Find(filters.ToArray()).ToList();

            var list = prestadores.Select(p => new GridPrestadorServicoModel
            {
                Id = p.Id.GetValueOrDefault(),
                Codigo = p.PrestadorServico.Codigo,
                CpfCnpj = p.CpfCnpj.FormateCpfCnpj(),
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
                var cliente = _pessoaRepository.Find(p => p.PrestadorServico.Codigo == codigo.Value).FirstOrDefault();

                if (cliente != null)
                    return Json(new { cliente.Id, cliente.PrestadorServico.Codigo, cliente.Nome }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { erro = "Nenhum prestador de serviço encontrado." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PesquisarId
        [HttpGet, AjaxOnly]
        public virtual ActionResult PesquisarId(long id)
        {
            var cliente = _pessoaRepository.Get(id);

            if (cliente != null)
                return Json(new { cliente.Id, cliente.PrestadorServico.Codigo, cliente.Nome }, JsonRequestBehavior.AllowGet);

            return Json(new { erro = "Nenhum prestador de serviço encontrado." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PreencheColuna
        private void PreencheColuna()
        {
            var coluna = new[]
                              {
                                  new { value = "Nome", text = "Nome"},
                                  new { value = "CpfCnpj", text = "Cpf/Cnpj"},
                                  new { value = "PrestadorServico.Codigo", text = "Código"}
                              };
            ViewData["ColunaPesquisa"] = new SelectList(coluna, "value", "text");
        }
        #endregion

        #endregion

        #endregion

        #region Métodos

        #region PopulateViewData
        protected void PopulateViewData(PrestadorServicoModel model)
        {
            // PrestadorServicoUnidade
            var prestadorServicoUnidades = _pessoaRepository.Find(p => p.Unidade != null).ToList();
            ViewData["PrestadorServicoUnidade"] = prestadorServicoUnidades.ToSelectList("Nome", model.PrestadorServicoUnidade);

            // Tela de novo PrestadorServico
            var novoPrestadorServicoModel = model as NovoPrestadorServicoModel;
            if (novoPrestadorServicoModel != null)
            {
                var ufId = 0L;
                var cidadeId = 0L;

                var cidade = _cidadeRepository.Get(novoPrestadorServicoModel.EnderecoCidade);
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
            var prestadorServico = model as PrestadorServicoModel;

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