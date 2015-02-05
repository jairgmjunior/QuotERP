using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.Extensions;
using Fashion.ERP.Reporting.Comum;
using Fashion.ERP.Reporting.Helpers;
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
using Telerik.Reporting;

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

        private readonly string[] _tipoRelatorio = { "Detalhado", "Listagem", "Sintético" };
        private static readonly Dictionary<string, string> ColunasPesquisaFornecedorOrdenacao = new Dictionary<string, string>
            {
                {"Cidade", "EnderecoPadrao.Cidade.Nome"},
                {"Código", "Fornecedor.Codigo"},
                {"Estado", "EnderecoPadrao.Cidade.Uf.Sigla"},
                {"Nome", "Nome"},
                {"Nome Fantasia", "NomeFantasia"},
            };

        private static readonly Dictionary<string, string> ColunasPesquisaFornecedorAgrupamento = new Dictionary<string, string>
            {
                {"Cidade", "EnderecoPadrao.Cidade.Nome"},
                {"Estado", "EnderecoPadrao.Cidade.Uf.Sigla"},
                {"Tipo fornecedor", "Fornecedor.TipoFornecedor.Descricao"}
            };

        #region Views

        #region Index
        [PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index()
        {
            var fornecedores = _pessoaRepository.Find(p => p.Fornecedor != null).OrderByDescending(x => x.DataAlteracao).ToList();

            var list = fornecedores.Select(p => new GridFornecedorModel
            {
                Id = p.Id.GetValueOrDefault(),
                Codigo = p.Fornecedor.Codigo,
                CpfCnpj = p.CpfCnpj.FormateCpfCnpj(),
                Nome = p.Nome,
                NomeFantasia = p.NomeFantasia,
                Cidade = p.EnderecoPadrao == null? null : p.EnderecoPadrao.Cidade.Nome,
                Estado = p.EnderecoPadrao == null ? null : p.EnderecoPadrao.Cidade.UF.Sigla,
                Ativo = p.Fornecedor.Ativo
            }).ToList();

            var retorno = new PesquisaFornecedorModel()
            {
                Grid = list,
                ModoConsulta = "Listar"
            };

            return View(retorno);
        }
        
        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index(PesquisaFornecedorModel model)
        {
            var fornecedores = _pessoaRepository.Find(p => p.Fornecedor != null).SelectMany(x => x.Enderecos, (pessoa, endereco) => new {Pessoa = pessoa, Endereco = endereco});
            
            try
            {
                #region Filtros

                var filtros = new StringBuilder();

                if (!string.IsNullOrEmpty(model.Nome))
                {
                    fornecedores = fornecedores.Where(p => p.Pessoa.Nome.Contains(model.Nome));
                    filtros.AppendFormat("Nome: {0}, ", model.Nome);
                }

                if (!string.IsNullOrEmpty(model.NomeFantasia))
                {
                    fornecedores = fornecedores.Where(p => p.Pessoa.NomeFantasia.Contains(model.NomeFantasia));
                    filtros.AppendFormat("Nome fantasia: {0}, ", model.NomeFantasia);
                }

                if (!string.IsNullOrEmpty(model.CpfCnpj))
                {
                    fornecedores = fornecedores.Where(p => p.Pessoa.CpfCnpj.Contains(model.CpfCnpj.DesformateCpfCnpj()));
                    filtros.AppendFormat("Cpf/Cnpj: {0}, ", model.CpfCnpj);
                }

                if (model.Uf.HasValue)
                {
                    fornecedores = fornecedores.Where(p => p.Endereco.Cidade.UF.Id == model.Uf.Value);
                    filtros.AppendFormat("Uf: {0}, ", _ufRepository.Get(model.Uf.Value).Nome);
                }

                if (model.Cidade.HasValue)
                {
                    fornecedores = fornecedores.Where(p => p.Endereco.Cidade.Id == model.Cidade.Value);
                    filtros.AppendFormat("Cidade: {0}, ", _cidadeRepository.Get(model.Cidade.Value).Nome);
                }
                
                #endregion
                
                //var resultadoDesagrupado = fornecedores.Select(q => new { q.Pessoa, q.Endereco });
                
                // Verifica se é uma listagem
                if (model.ModoConsulta == "Listar")
                {
                    if (model.OrdenarPor != null)
                    {
                        fornecedores = model.OrdenarEm == "asc"
                            ? fornecedores.OrderBy(model.OrdenarPor)
                            : fornecedores.OrderByDescending(model.OrdenarPor);
                    }
                    else
                    {
                        fornecedores = fornecedores.OrderByDescending(x => x.Pessoa.DataAlteracao);
                    }
                    var resultado = fornecedores.ToList();

                    var resultadoAgrupado =
                        resultado.GroupBy(x => new { x.Pessoa }).Select(y => y.Key.Pessoa);
    
                    model.Grid = resultadoAgrupado.Select(p => new GridFornecedorModel
                    {
                        Id = p.Id.GetValueOrDefault(),
                        Codigo = p.Fornecedor.Codigo,
                        CpfCnpj = p.CpfCnpj.FormateCpfCnpj(),
                        Nome = p.Nome,
                        NomeFantasia = p.NomeFantasia,
                        Cidade = p.EnderecoPadrao.Cidade.Nome,
                        Estado = p.EnderecoPadrao.Cidade.UF.Sigla,
                        Ativo = p.Fornecedor.Ativo
                    }).ToList();

                    return View(model);
                }

                var resultado2 = fornecedores.ToList();

                var resultadoAgrupado2 =
                    resultado2.GroupBy(x => new { x.Pessoa }).Select(y => y.Key.Pessoa);

                if (!resultadoAgrupado2.Any())
                    return Json(new { Error = "Nenhum item encontrado." });

                #region Montar Relatório

                var report = new ListaFornecedorReport { DataSource = resultadoAgrupado2 };

                if (filtros.Length > 2)
                    report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

                var grupo = report.Groups.First(p => p.Name.Equals("Grupo"));

                if (model.AgruparPor != null)
                {
                    grupo.Groupings.Add("= Fields." + model.AgruparPor);

                    var key = ColunasPesquisaFornecedorAgrupamento.First(p => p.Value == model.AgruparPor).Key;
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
                return View("Index", model);
            }
        }

        #endregion

        #region Novo

        [PopulateViewData("PopulateViewDataNovoEditar")]
        public virtual ActionResult Novo()
        {
            return View(new NovoFornecedorModel { EnderecoTipoEndereco = TipoEndereco.Residencial });
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewDataNovoEditar")]
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
                    domain.CpfCnpj = model.TipoPessoa == TipoPessoa.Fisica ? model.Cpf.DesformateCpfCnpj()
                                   : model.TipoPessoa == TipoPessoa.Juridica ? model.Cnpj.DesformateCpfCnpj()
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

        [ImportModelStateFromTempData, PopulateViewData("PopulateViewDataNovoEditar")]
        public virtual ActionResult Editar(long id)
        {
            var domain = _pessoaRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<FornecedorModel>(domain);
                
                if (domain.TipoPessoa == TipoPessoa.Fisica)
                    model.Cpf = domain.CpfCnpj.FormateCpfCnpj();
                else if (domain.TipoPessoa == TipoPessoa.Juridica)
                    model.Cnpj = domain.CpfCnpj.FormateCpfCnpj();

                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o fornecedor.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewDataNovoEditar")]
        public virtual ActionResult Editar(FornecedorModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _pessoaRepository.Get(model.Id));
                    domain.CpfCnpj = model.TipoPessoa == TipoPessoa.Fisica ? model.Cpf.DesformateCpfCnpj()
                                   : model.TipoPessoa == TipoPessoa.Juridica ? model.Cnpj.DesformateCpfCnpj()
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
            var pessoa = _pessoaRepository.Get(p => p.CpfCnpj == cpfCnpj.DesformateCpfCnpj());

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
                CpfCnpj = p.CpfCnpj.FormateCpfCnpj(),
                Nome = p.Nome
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
        protected void PopulateViewDataNovoEditar(FornecedorModel model)
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

        protected void PopulateViewDataPesquisa(PesquisaFornecedorModel model)
        {
            var ufId = 0L;
            var cidadeId = 0L;

            if (model.Cidade.HasValue)
            {

                var cidade = _cidadeRepository.Get(model.Cidade);
                if (cidade != null)
                {
                    ufId = cidade.UF.Id.GetValueOrDefault();
                    cidadeId = cidade.Id.GetValueOrDefault();
                }
            } else if (model.Uf.HasValue)
            {
                ufId = model.Uf.Value;
            }

            var ufs = _ufRepository.Find().OrderBy(o => o.Nome).ToList();
            ViewData["Uf"] = ufs.ToSelectList("Nome", ufId);

            var cidades = _cidadeRepository.Find(p => p.UF.Id == ufId).OrderBy(o => o.Nome).ToList();
            ViewData["Cidade"] = cidades.ToSelectList("Nome", cidadeId);


            ViewBag.TipoRelatorio = new SelectList(_tipoRelatorio);
            ViewBag.OrdenarPor = new SelectList(ColunasPesquisaFornecedorOrdenacao, "value", "key");
            ViewBag.AgruparPor = new SelectList(ColunasPesquisaFornecedorAgrupamento, "value", "key");
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