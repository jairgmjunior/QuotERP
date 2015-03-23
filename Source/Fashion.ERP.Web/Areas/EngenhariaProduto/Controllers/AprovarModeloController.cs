using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Web.Areas.EngenhariaProduto.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Mvc.Security;
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
        private readonly IRepository<Usuario> _usuarioRepository;
        private readonly IRepository<ClassificacaoDificuldade> _classificacaoDificuldadeRepository;
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
            IRepository<Colecao> colecaoRepository, IRepository<Marca> marcaRepository, IRepository<Usuario> usuarioRepository, 
            IRepository<ClassificacaoDificuldade> classificacaoDificuldadeRepository)
        {
            _modeloRepository = modeloRepository;
            _colecaoRepository = colecaoRepository;
            _marcaRepository = marcaRepository;
            _classificacaoDificuldadeRepository = classificacaoDificuldadeRepository;
            _pessoaRepository = pessoaRepository;
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Index()
        {
            var aprovarModelos = _modeloRepository.Find(p => p.Aprovado == false).OrderByDescending(o => o.DataAlteracao).Take(20);

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
                    modelos = modelos.OrderBy(o => o.DataAlteracao);

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
            if (ModelState.IsValid)
            {
                try
                {
                    var modelo = _modeloRepository.Get(model.Id);
                    modelo.Aprovado = true;

                    modelo.ModeloAprovado = new ModeloAprovado
                    {
                        Ano = DateTime.Now.Year,
                        Data = model.DataAprovacao.HasValue ? model.DataAprovacao.Value : DateTime.Now,
                        DataProgramacaoProducao = model.ProgramacaoProducao.HasValue ? model.ProgramacaoProducao.Value : DateTime.Now,
                        Tag = model.Tag,
                        Quantidade = model.QuantidadeProducao.HasValue ? model.QuantidadeProducao.Value : 0,
                        Observacao = model.ObservacaoAprovacao,
                        ClassificacaoDificuldade =
                            _classificacaoDificuldadeRepository.Load(model.ClassificacaoDificuldade),
                        Colecao = _colecaoRepository.Load(model.Colecao),
                        Funcionario = _pessoaRepository.Load(ObtenhaFuncionarioLogadoId())
                    };
                    
                    _modeloRepository.Update(modelo);

                    this.AddSuccessMessage("Modelo aprovado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    var mensagem = "Não é possível aprovar o modelo. Confira se os dados foram informados corretamente: " + exception.Message;
                    ModelState.AddModelError(string.Empty, mensagem);
                    _logger.Info(mensagem);
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
				    domain.Aprovado = false;
				    domain.ModeloAprovado = null;

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
            model.Colecao = modelo.Colecao.Id;

            var colecoes = _colecaoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Colecao"] = colecoes.ToSelectList("Descricao", model.Colecao);
            
            var marcas = _classificacaoDificuldadeRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["ClassificacaoDificuldade"] = marcas.ToSelectList("Descricao", model.ClassificacaoDificuldade);
        }
        #endregion

        public virtual long? ObtenhaFuncionarioLogadoId()
        {
            var userId = FashionSecurity.GetLoggedUserId();
            var usuario = _usuarioRepository.Get(userId);
            
            if (usuario.Funcionario == null)
            {
                throw new Exception("O usuário logado não possui funcionário associado a ele.");
            }

            return usuario.Funcionario.Id;
        }

        #endregion
    }
}