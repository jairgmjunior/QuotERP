using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Web.Areas.EngenhariaProduto.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers
{
    public partial class AprovarModeloController : BaseController
    {
		#region Variaveis
        private readonly IRepository<Modelo> _modeloRepository;
        private readonly IRepository<Colecao> _colecaoRepository;
        private readonly IRepository<Comprimento> _comprimentoRepository;
        private readonly IRepository<ProdutoBase> _produtoBaseRepository;
        private readonly IRepository<ClassificacaoDificuldade> _classificacaoDificuldadeRepository;
        private readonly IRepository<Barra> _barraRepository;
        private readonly IRepository<FichaTecnica> _fichaTecnica;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<Marca> _marcaRepository;

        #region ColunasPesquisaAprovarModelo
        private static readonly Dictionary<string, string> ColunasPesquisaAprovarModelo = new Dictionary<string, string>
        {
            {"Coleção", "Colecao.Descricao"},
            {"Descrição", "Descricao"},
            {"Estilista", "Estilista.Nome"},
            {"Marca", "Marca.Nome"},
            {"Referência", "Referencia"},
        };
        #endregion
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public AprovarModeloController(ILogger logger, IRepository<Modelo> modeloRepository, IRepository<Pessoa> pessoaRepository,
            IRepository<Colecao> colecaoRepository, IRepository<ProdutoBase> produtoBaseRepository, 
            IRepository<Comprimento> comprimentoRepository, IRepository<Marca> marcaRepository,
            IRepository<ClassificacaoDificuldade> classificacaoDificuldadeRepository, IRepository<Barra> barraRepository,
            IRepository<FichaTecnica> fichaTecnica)
        {
            _modeloRepository = modeloRepository;
            _colecaoRepository = colecaoRepository;
            _comprimentoRepository = comprimentoRepository;
            _marcaRepository = marcaRepository;
            _produtoBaseRepository = produtoBaseRepository;
            _classificacaoDificuldadeRepository = classificacaoDificuldadeRepository;
            _barraRepository = barraRepository;
            _fichaTecnica = fichaTecnica;
            _pessoaRepository = pessoaRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Index()
        {
            var aprovarModelos = _modeloRepository.Find(p => p.Aprovado == true).OrderByDescending(o => o.DataAprovacao).Take(20);

            var model = new PesquisaAprovarModeloModel();

            model.Grid = aprovarModelos.Select(p => new GridAprovarModeloModel
            {
                Id = p.Id.GetValueOrDefault(),
                Referencia = p.Referencia,
                Descricao = p.Descricao,
                Estilista = p.Estilista.Nome,
                Marca = p.Marca.Nome,
                Colecao = p.Colecao.Descricao,
                Aprovado = p.Aprovado
            }).ToList();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Index(PesquisaAprovarModeloModel model)
        {
            var modelos = _modeloRepository.Find();

            try
            {
                #region Filtros
                var filtros = new StringBuilder();

                if (!string.IsNullOrWhiteSpace(model.Referencia))
                {
                    modelos = modelos.Where(p => p.Referencia == model.Referencia);
                    filtros.AppendFormat("Referência: {0}, ", model.Referencia);
                }

                if (!string.IsNullOrWhiteSpace(model.Descricao))
                {
                    modelos = modelos.Where(p => p.Descricao.Contains(model.Descricao));
                    filtros.AppendFormat("Descrição: {0}, ", model.Descricao);
                }
                
                if (model.Aprovado.HasValue)
                {
                    modelos = modelos.Where(p => p.Aprovado == model.Aprovado);
                    filtros.AppendFormat("Aprovado: {0}, ", model.Aprovado.Value.ToSimNao());
                }

                if (model.Colecao.HasValue)
                {
                    modelos = modelos.Where(p => p.Colecao.Id == model.Colecao);
                    filtros.AppendFormat("Coleção: {0}, ", _colecaoRepository.Get(model.Colecao.Value).Descricao);
                }

                if (model.Estilista.HasValue)
                {
                    modelos = modelos.Where(p => p.Estilista.Id == model.Estilista);
                    filtros.AppendFormat("Estilista: {0}, ", _pessoaRepository.Get(model.Estilista.Value).Nome);
                }

                if (model.Marca.HasValue)
                {
                    modelos = modelos.Where(p => p.Marca.Id == model.Marca);
                    filtros.AppendFormat("Marca: {0}, ", _marcaRepository.Get(model.Marca.Value).Nome);
                }

                #endregion

                if (model.OrdenarPor != null)
                    modelos = model.OrdenarEm == "asc"
                        ? modelos.OrderBy(model.OrdenarPor)
                        : modelos.OrderByDescending(model.OrdenarPor);
                else
                    modelos = modelos.OrderBy(o => o.DataAprovacao).ThenBy(t => t.DataCriacao);

                model.Grid = modelos.Select(p => new GridAprovarModeloModel
                {
                    Id = p.Id.GetValueOrDefault(),
                    Referencia = p.Referencia,
                    Descricao = p.Descricao,
                    Colecao = p.Colecao.Descricao,
                    Estilista = p.Estilista.Nome,
                    Marca = p.Marca.Nome,
                    Aprovado = p.Aprovado
                }).ToList();

                return View(model);
            }
            catch (Exception exception)
            {
                var message = exception.GetMessage();
                _logger.Info(message);

                ModelState.AddModelError(string.Empty, message);
                return View(model);
            }
        }

        #endregion

        #region Aprovar

        [PopulateViewData("PopulateAprovarViewData")]
        public virtual ActionResult Aprovar(long? id)
        {
            if (id.HasValue == false)
                return RedirectToAction("Index");

            var model = new AprovarModeloModel();
            model.Id = id;

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateAprovarViewData")]
        public virtual ActionResult Aprovar(AprovarModeloModel model)
        {
            // Validações
            if (model.QuantidadeProducoes != null && model.PossuiSubmodelos && model.QuantidadeProducao != model.QuantidadeProducoes.Sum())
                ModelState.AddModelError("QuantidadeProducao", "A soma da quantidade dos submodelos deve ser igual a quantidade de produção.");

            if (ModelState.IsValid)
            {
                try
                {
                    // TAG
                    var anoAprovacao = DateTime.Now.Year;
                    var numeroAprovacao = 1;
                    if (model.Tag.IndexOf('-') > 0)
                    {
                        var temp = model.Tag.Substring(model.Tag.IndexOf('-') + 1);
                        int num;
                        if (int.TryParse(temp, out num))
                            numeroAprovacao = num;
                    }

                    //var numeros = _fichaTecnica
                    //    .Find(f => f.Modelo.AnoAprovacao == anoAprovacao)
                    //    .Select(s => s.Modelo.NumeroAprovacao);
                    //var ultimoNumero = numeros.Any() ? numeros.Max() : 0;
                    //var numeroAprovacao = ultimoNumero + 1;

                    //var tag = string.Format("{0}-{1}", anoAprovacao, numeroAprovacao);
                    // Fim TAG

                    var modelo = _modeloRepository.Get(model.Id);
                    modelo.DataAprovacao = model.DataAprovacao;
                    modelo.Tag = model.Tag;
                    modelo.AnoAprovacao = anoAprovacao;
                    modelo.NumeroAprovacao = numeroAprovacao;
                    modelo.ObservacaoAprovacao = model.ObservacaoAprovacao;
                    modelo.Aprovado = true;
                    
                    var fichaTecnica = new FichaTecnica();
                    fichaTecnica.Referencia = modelo.Tag;
                    fichaTecnica.Descricao = modelo.Descricao;
                    fichaTecnica.Marca = modelo.Marca;
                    fichaTecnica.Colecao = _colecaoRepository.Get(model.Colecao);
                    fichaTecnica.Barra = modelo.Barra;
                    fichaTecnica.Segmento = modelo.Segmento;
                    fichaTecnica.ProdutoBase = modelo.ProdutoBase;
                    fichaTecnica.Comprimento = modelo.Comprimento;
                    fichaTecnica.Natureza = modelo.Natureza;
                    fichaTecnica.ClassificacaoDificuldade = model.ClassificacaoDificuldade != null 
                                                          ? _classificacaoDificuldadeRepository.Get(model.ClassificacaoDificuldade)
                                                          : null;
                    fichaTecnica.Grade = modelo.Grade;
                    fichaTecnica.DataCadastro = DateTime.Now;
                    fichaTecnica.Detalhamento = modelo.Detalhamento;
                    fichaTecnica.Modelagem = modelo.Modelagem;

                    fichaTecnica.ProgramacaoProducao = model.ProgramacaoProducao ?? DateTime.Now;
                    fichaTecnica.QuantidadeProducao = model.QuantidadeProducao ?? 0;

                    if (model.Sequencias != null && model.Sequencias.Any())
                    {
                        for (int i = 0; i < model.Sequencias.Count; i++)
                        {
                            var sequencia = model.Sequencias[i];
                            var produtoBase = model.ProdutoBases[i];
                            var comprimento = model.Comprimentos[i];
                            var descricao = model.Descricoes[i];
                            var barra = model.Barras[i];
                            var quantidadeProducao = model.QuantidadeProducoes[i];

                            var subficha = CloneFichaTecnica(fichaTecnica);
                            subficha.Sequencia = sequencia;
                            subficha.Referencia = string.Format("{0}-{1}", modelo.Tag, sequencia);
                            subficha.Descricao = descricao;
                            subficha.QuantidadeProducao = quantidadeProducao;
                            subficha.ProdutoBase = _produtoBaseRepository.Get(produtoBase);
                            subficha.Comprimento = _comprimentoRepository.Get(comprimento);
                            subficha.Barra = _barraRepository.Get(barra);

                            //_fichaTecnica.Save(subficha);
                            modelo.FichaTecnicas.Add(subficha);
                        }
                    }
                    else
                    {
                        //_fichaTecnica.Save(fichaTecnica);
                        modelo.FichaTecnicas.Add(fichaTecnica);
                    }

                    _modeloRepository.Update(modelo);

                    this.AddSuccessMessage("Modelo aprovado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível aprovar o modelo. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }
        #endregion

        #region Desaprovar

        [HttpPost, ValidateAntiForgeryToken, ExportModelStateToTempData]
        public virtual ActionResult Desaprovar(long? id)
        {
			if (ModelState.IsValid)
            {
				try
				{
					var domain = _modeloRepository.Get(id);
				    domain.DataAprovacao = null;
				    domain.Tag = null;
				    domain.ObservacaoAprovacao = null;
				    domain.Aprovado = false;

				    var fichas = _fichaTecnica.Find(p => p.Id == id);

				    foreach (var fichaTecnica in fichas)
				        _fichaTecnica.Delete(fichaTecnica);

					this.AddSuccessMessage("Modelo desaprovado com sucesso");
					return RedirectToAction("Index");
				}
				catch (Exception exception)
				{
					ModelState.AddModelError(string.Empty, "Ocorreu um erro ao desaprovar o modelo: " + exception.Message);
					_logger.Info(exception.GetMessage());
				}
			}

            return RedirectToAction("Aprovar", new { id });
        }
        #endregion

        #region Reprovar

        [HttpPost, ValidateAntiForgeryToken, ExportModelStateToTempData]
        public virtual ActionResult Reprovar(long? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _modeloRepository.Get(id);
                    domain.Aprovado = false;

                    this.AddSuccessMessage("Modelo reprovado com sucesso");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao reprovado o modelo: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Index");
        }
        #endregion
		
        #endregion

		#region Métodos

        #region PopulateViewData
        protected void PopulateViewData(PesquisaAprovarModeloModel model)
        {
            var colecoes = _colecaoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Colecao"] = colecoes.ToSelectList("Descricao", model.Colecao);

            var marcas = _marcaRepository.Find(p => p.Ativo).OrderBy(p => p.Nome).ToList();
            ViewData["Marca"] = marcas.ToSelectList("Nome", model.Marca);

            var estilistas = _pessoaRepository.Find(p => p.Funcionario != null
                && p.Funcionario.FuncaoFuncionario == FuncaoFuncionario.Estilista)
                .OrderBy(p => p.Nome).ToList();
            ViewData["Estilista"] = estilistas.ToSelectList("Nome", model.Estilista);

            ViewBag.OrdenarPor = new SelectList(ColunasPesquisaAprovarModelo, "value", "key");
        }
        #endregion

        #region PopulateAprovarViewData
        protected void PopulateAprovarViewData(AprovarModeloModel model)
        {
            // Preencher os campos que serão mostrados na tela
            var modelo = _modeloRepository.Get(model.Id);
            model.Referencia = modelo.Referencia;
            model.DescricaoModelo = modelo.Descricao;
            model.Descricao = modelo.Descricao;
            model.EstilistaNome = modelo.Estilista.Nome;
            model.NaturezaDescricao = modelo.Natureza.Descricao;
            model.ArtigoDescricao = modelo.Artigo.Descricao;
            model.DataCriacao = modelo.DataCriacao;
            model.ClassificacaoDescricao = modelo.Classificacao.Descricao;
            model.Observacao = modelo.Observacao;
            model.DataAprovacao = DateTime.Now;
            model.Tag = GerarTag();
            model.Colecao = modelo.Colecao.Id;
            model.Barra = modelo.Barra != null ? modelo.Barra.Id : null;
            model.Segmento = modelo.Segmento != null ? modelo.Segmento.Id : null;
            model.ProdutoBase = modelo.ProdutoBase != null ? modelo.ProdutoBase.Id : null;
            model.Comprimento = modelo.Comprimento != null ? modelo.Comprimento.Id : null;;
            model.Natureza = modelo.Natureza.Id;
            model.Grade = modelo.Grade.Id;
            model.ProdutoBase = modelo.ProdutoBase != null ? modelo.ProdutoBase.Id : null;

            var colecoes = _colecaoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Colecao"] = colecoes.ToSelectList("Descricao", model.Colecao);

            var barras = _barraRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Barra"] = barras.ToSelectList("Descricao", model.Barra);

            var comprimentos = _comprimentoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Comprimento"] = comprimentos.ToSelectList("Descricao", model.Comprimento);

            var produtosBase = _produtoBaseRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["ProdutoBase"] = produtosBase.ToSelectList("Descricao", model.ProdutoBase);

            var marcas = _classificacaoDificuldadeRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["ClassificacaoDificuldade"] = marcas.ToSelectList("Descricao", model.ClassificacaoDificuldade);
        }
        #endregion

        #region GerarTag
        private string GerarTag()
        {
            var ano = DateTime.Now.Year;

            var numeros = _modeloRepository
                .Find(f => f.AnoAprovacao == ano)
                .Select(s => s.NumeroAprovacao);
            var ultimoNumero = numeros.Any() ? numeros.Max() : 0;

            return string.Format("{0}-{1}", ano, ultimoNumero + 1);
        }
        #endregion

        #region CloneFichaTecnica
        private static FichaTecnica CloneFichaTecnica(FichaTecnica fichaTecnica)
        {
            return new FichaTecnica
            {
                Referencia = fichaTecnica.Referencia,
                Descricao = fichaTecnica.Descricao,
                Detalhamento = fichaTecnica.Detalhamento,
                Sequencia = fichaTecnica.Sequencia,
                ProgramacaoProducao = fichaTecnica.ProgramacaoProducao,
                DataCadastro = fichaTecnica.DataCadastro,
                Modelagem = fichaTecnica.Modelagem,
                QuantidadeProducao = fichaTecnica.QuantidadeProducao,
                Marca = fichaTecnica.Marca,
                Colecao = fichaTecnica.Colecao,
                Barra = fichaTecnica.Barra,
                Segmento = fichaTecnica.Segmento,
                ProdutoBase = fichaTecnica.ProdutoBase,
                Comprimento = fichaTecnica.Comprimento,
                Natureza = fichaTecnica.Natureza,
                ClassificacaoDificuldade = fichaTecnica.ClassificacaoDificuldade,
                Grade = fichaTecnica.Grade
            };
        }
        #endregion

        #endregion
    }
}