using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Domain.Producao;
using Fashion.ERP.Web.Areas.Producao.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using NHibernate.Linq;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Producao.Controllers
{
    public partial class FichaTecnicaController : BaseController
    {
        #region Variaveis
        private readonly IRepository<FichaTecnica> _fichaTecnicaRepository;
        private readonly IRepository<FichaTecnicaJeans> _fichaTecnicaJeansRepository;
        private readonly IRepository<Natureza> _naturezaRepository;
        private readonly IRepository<Colecao> _colecaoRepository;
        private readonly IRepository<Marca> _marcaRepository;
        private readonly IRepository<Classificacao> _classficacaoRepository;
        private readonly IRepository<Artigo> _artigoRepository;
        private readonly IRepository<Segmento> _segmentoRepository;
        private readonly IRepository<ClassificacaoDificuldade> _classificacaoDificuldadeRepository;
        private readonly IRepository<Variacao> _variacaoRepository;
        private readonly IRepository<Cor> _corRepository;
        private readonly IRepository<Grade> _gradeRepository;
        private readonly IRepository<ProdutoBase> _produtoBaseRepository;
        private readonly IRepository<Comprimento> _comprimentoRepository;
        private readonly IRepository<Barra> _barraRepository;

        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public FichaTecnicaController(ILogger logger,
            IRepository<FichaTecnica> fichaTecnicaRepository,
            IRepository<FichaTecnicaJeans> fichaTecnicaJeansRepository,
            IRepository<Natureza> naturezaRepository,
            IRepository<Colecao> colecaoRepository,
            IRepository<Marca> marcaRepository,
            IRepository<Classificacao> classificacaoRepository,
            IRepository<Artigo> artigoRepository,
            IRepository<Segmento> segmentoRepository,
            IRepository<ClassificacaoDificuldade> classificacaoDificuldadeRepository,
            IRepository<Variacao> variacaoRepository,
            IRepository<Cor> corRepository,
            IRepository<Grade> gradeRepository,
            IRepository<ProdutoBase> produtoBaseRepository,
            IRepository<Comprimento> comprimentoRepository,
            IRepository<Barra> barraRepository)
        {
            _fichaTecnicaRepository = fichaTecnicaRepository;
            _fichaTecnicaJeansRepository = fichaTecnicaJeansRepository;
            _naturezaRepository = naturezaRepository;
            _colecaoRepository = colecaoRepository;
            _marcaRepository = marcaRepository;
            _classificacaoDificuldadeRepository = classificacaoDificuldadeRepository;
            _classficacaoRepository = classificacaoRepository;
            _artigoRepository = artigoRepository;
            _segmentoRepository = segmentoRepository;
            _variacaoRepository = variacaoRepository;
            _corRepository = corRepository;
            _gradeRepository = gradeRepository;
            _produtoBaseRepository = produtoBaseRepository;
            _comprimentoRepository = comprimentoRepository;
            _barraRepository = barraRepository;
            _logger = logger;
        }
        #endregion

        #region Index
        public virtual ActionResult Index()
        {
            var fichaTecnicas = _fichaTecnicaRepository.Find();

            var pesquisaFichaTecnicaModel = new PesquisaFichaTecnicaModel();

            var list = fichaTecnicas.Select(p => new GridFichaTecnicaModel
            {
                Id = p.Id.GetValueOrDefault(),
                Descricao = p.Descricao,
                Tag = p.Tag,
                Ano = p.Ano,
                Colecao = p.Colecao.Descricao,
                Marca = p.Marca.Nome,
                Natureza = p.Natureza.Descricao
                
            }).OrderBy(o => o.Tag).ToList();
            
            pesquisaFichaTecnicaModel.Grid = list;

            return View(pesquisaFichaTecnicaModel);
        }
        #endregion
     
        public virtual ActionResult Novo()
        {
            return View(new FichaTecnicaModel());
        }

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Basicos(long? fichaTecnicaId)
        {
            if (!fichaTecnicaId.HasValue)
            {
                return PartialView("Basicos", new FichaTecnicaBasicosModel());
            }

            var domain = _fichaTecnicaJeansRepository.Get(fichaTecnicaId);

            if (domain != null)
            {
                var model  = Mapper.Flat<FichaTecnicaBasicosModel>(domain);

                model.QuantidadeAprovada = domain.QuantidadeProducaoAprovada;
                model.PrazoMaximo = domain.TempoMaximoProducao;
                model.Grade = domain.FichaTecnicaMatriz.Grade.Id;
                model.GridFichaTecnicaVariacao = new List<GridFichaTecnicaVariacaoModel>();

                domain.FichaTecnicaMatriz.FichaTecnicaVariacaoMatrizs.ForEach(x => x.Cores.ForEach(y => model.GridFichaTecnicaVariacao.Add(new GridFichaTecnicaVariacaoModel()
                {
                    Cor = y.Id.ToString(),
                    Variacao = x.Variacao.Id.ToString()
                })));

                return PartialView("Basicos", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a ficha técnica.");
            
            return PartialView("Basicos", new FichaTecnicaBasicosModel());
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Basicos(FichaTecnicaBasicosModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!model.Id.HasValue)
                    {
                        NovoBasicos(model);
                        this.AddSuccessMessage("Ficha técnica cadastrada com sucesso.");
                    }
                    else
                    {
                        EditarBasicos(model);
                        this.AddSuccessMessage("Ficha técnica atualizada com sucesso.");
                    } 

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a ficha técnica. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }

        protected virtual void EditarBasicos(FichaTecnicaBasicosModel model)
        {
            var domain = _fichaTecnicaJeansRepository.Get(model.Id);

            domain = Mapper.Unflat(model, domain);
            domain.QuantidadeProducaoAprovada = model.QuantidadeAprovada.HasValue ? model.QuantidadeAprovada.Value : 0;
            domain.TempoMaximoProducao = model.PrazoMaximo;
            //EditarFichaTecnicaMatriz(domain.FichaTecnicaMatriz, model);

            _fichaTecnicaJeansRepository.SaveOrUpdate(domain);
        }

        protected virtual void EditarFichaTecnicaMatriz(FichaTecnicaMatriz fichaTecnicaMatriz, FichaTecnicaBasicosModel model)
        {

            throw new NotImplementedException();
        }

        protected virtual void NovoBasicos(FichaTecnicaBasicosModel model)
        {
            var domain = Mapper.Unflat<FichaTecnicaJeans>(model);
            domain.DataCadastro = DateTime.Now;
            domain.QuantidadeProducaoAprovada = model.QuantidadeAprovada.HasValue ? model.QuantidadeAprovada.Value : 0;
            domain.TempoMaximoProducao = model.PrazoMaximo;
            domain.FichaTecnicaMatriz = ObtenhaFichaTecnicaMatriz(model);

            _fichaTecnicaJeansRepository.Save(domain);
        }

        public virtual FichaTecnicaMatriz ObtenhaFichaTecnicaMatriz(FichaTecnicaBasicosModel model)
        {
            var fichaTecnicaMatriz = new FichaTecnicaMatriz();

            fichaTecnicaMatriz.Grade = _gradeRepository.Get(x => x.Id == model.Grade);
            fichaTecnicaMatriz.FichaTecnicaVariacaoMatrizs = ObtenhaFichaTecnicaVariacaoMatriz(model);

            return fichaTecnicaMatriz;
        }

        public virtual IList<FichaTecnicaVariacaoMatriz> ObtenhaFichaTecnicaVariacaoMatriz(FichaTecnicaBasicosModel model)
        {
            var variacaoIds = model.GridFichaTecnicaVariacao.GroupBy(x => new {x.Variacao}).Select(x => x.Key.Variacao);
            var fichaTecnicaVariacaoMatrizs = new List<FichaTecnicaVariacaoMatriz>();

            foreach (string variacaoId in variacaoIds)
            {
                var fichaTecnicaVariacaoMatriz = new FichaTecnicaVariacaoMatriz();
                var variacao = _variacaoRepository.Get(x => x.Id == long.Parse(variacaoId));

                fichaTecnicaVariacaoMatriz.Variacao = variacao;

                var gridCores = model.GridFichaTecnicaVariacao.Where(x => x.Variacao == variacaoId).ToList();

                gridCores.ForEach(x =>
                {
                    var cor = _corRepository.Get(y => y.Id == long.Parse(x.Cor));
                    fichaTecnicaVariacaoMatriz.AddCor(cor);
                });

                fichaTecnicaVariacaoMatrizs.Add(fichaTecnicaVariacaoMatriz);
            }

            return fichaTecnicaVariacaoMatrizs;
        }

        #region Editar
        
        public virtual ActionResult Editar(long id)
        {            
            var model = new FichaTecnicaModel {Id = id};
            return View(model);
        }
        
        #region Excluir

        [HttpPost, ValidateAntiForgeryToken, ExportModelStateToTempData]
        public virtual ActionResult Excluir(long? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _fichaTecnicaJeansRepository.Get(id);
                    _fichaTecnicaJeansRepository.Delete(domain);

                    this.AddSuccessMessage("Ficha técnica excluída com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir a ficha técnica: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { id });
        }
        #endregion

        #endregion

        #region PopulateViewData
        protected void PopulateViewData(FichaTecnicaBasicosModel model)
        {
            var naturezas = _naturezaRepository.Find(p => p.Ativo).ToList();
            ViewBag.Natureza = naturezas.ToSelectList("Descricao", model.Natureza);

            var colecaos = _colecaoRepository.Find(p => p.Ativo).ToList();
            ViewBag.Colecao = colecaos.ToSelectList("Descricao", model.Colecao);

            var marcas = _marcaRepository.Find(p => p.Ativo).ToList();
            ViewBag.Marca = marcas.ToSelectList("Nome", model.Marca);

            var classificacaos = _classficacaoRepository.Find(p => p.Ativo).ToList();
            ViewBag.Classificacao = classificacaos.ToSelectList("Descricao", model.Classificacao);

            var artigos = _artigoRepository.Find(p => p.Ativo).ToList();
            ViewBag.Artigo = artigos.ToSelectList("Descricao", model.Artigo);

            var segmento = _segmentoRepository.Find(p => p.Ativo).ToList();
            ViewBag.Segmento = segmento.ToSelectList("Descricao", model.Segmento);

            var classificacaoDificuldades = _classificacaoDificuldadeRepository.Find(p => p.Ativo).ToList();
            ViewBag.ClassificacaoDificuldade = classificacaoDificuldades.ToSelectList("Descricao", model.ClassificacaoDificuldade);

            var variacaos = _variacaoRepository.Find(p => p.Ativo).ToList();
            ViewBag.Variacaos = variacaos.Select(s => new { s.Id, s.Nome });
            ViewBag.VariacaosDicionario = variacaos.ToDictionary(k => k.Id, e => e.Nome);
            ViewBag.VariacaosDicionarioJson = variacaos.ToDictionary(k => k.Id.ToString(), e => e.Nome).FromDictionaryToJson();

            var cors = _corRepository.Find(p => p.Ativo).ToList();
            ViewBag.Cors = cors.Select(s => new { s.Id, s.Nome });
            ViewBag.CorsDicionario = cors.ToDictionary(k => k.Id, e => e.Nome);
            ViewBag.CorsDicionarioJson = cors.ToDictionary(k => k.Id.ToString(), e => e.Nome).FromDictionaryToJson();

            var grades = _gradeRepository.Find(p => p.Ativo).ToList();
            ViewBag.Grade = grades.ToSelectList("Descricao", model.Grade);

            var produtoBases = _produtoBaseRepository.Find(p => p.Ativo).ToList();
            ViewBag.ProdutoBase = produtoBases.ToSelectList("Descricao", model.ProdutoBase);

            var comprimentos = _comprimentoRepository.Find(p => p.Ativo).ToList();
            ViewBag.Comprimento = comprimentos.ToSelectList("Descricao", model.Comprimento);

            var barras = _barraRepository.Find(p => p.Ativo).ToList();
            ViewBag.Barra = barras.ToSelectList("Descricao", model.Barra);
        }
        #endregion

        #region Actions Grid
        //Não são utilizadas pois as alterações são realizadas no submit e não durante a edição
        public virtual ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(request);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult EditingInline_Create([DataSourceRequest] DataSourceRequest request, GridFichaTecnicaVariacaoModel fichaTecnicaVariacaoModel)
        {
            return Json(new[] { fichaTecnicaVariacaoModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, GridFichaTecnicaVariacaoModel fichaTecnicaVariacaoModel)
        {
            //simula a persistência do item
            var random = new Random();
            int randomNumber = random.Next(0, 10000);
            fichaTecnicaVariacaoModel.Id = randomNumber * -1;

            return Json(new[] { fichaTecnicaVariacaoModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult EditingInline_Destroy([DataSourceRequest] DataSourceRequest request, GridFichaTecnicaVariacaoModel fichaTecnicaVariacaoModel)
        {
            return Json(new[] { fichaTecnicaVariacaoModel }.ToDataSourceResult(request, ModelState));
        }
        #endregion
    }
}