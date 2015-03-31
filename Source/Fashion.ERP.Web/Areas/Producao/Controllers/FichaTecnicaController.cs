using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
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
        private readonly IRepository<SetorProducao> _setorProducaoRepository;
        private readonly IRepository<DepartamentoProducao> _departamentoProducaoRepository;
        private readonly IRepository<OperacaoProducao> _operacaoProducaoRepository;
        private readonly IRepository<Tamanho> _tamanhoRepository;
        private readonly IRepository<Material> _materialRepository;

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
            IRepository<Barra> barraRepository,
            IRepository<SetorProducao> setorProducaoRepository,
            IRepository<DepartamentoProducao> departamentoProducaoRepository,
            IRepository<OperacaoProducao> operacaoProducaoRepository,
            IRepository<Tamanho> tamanhoRepository,
            IRepository<Material> materialRepository)
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
            _setorProducaoRepository = setorProducaoRepository;
            _departamentoProducaoRepository = departamentoProducaoRepository;
            _operacaoProducaoRepository = operacaoProducaoRepository;
            _tamanhoRepository = tamanhoRepository;
            _materialRepository = materialRepository;
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

        #region Novo

        public virtual ActionResult Novo()
        {
            return View(new FichaTecnicaModel());
        }
        #endregion

        #region Básicos

        [PopulateViewData("PopulateViewDataBasicos")]
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

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewDataBasicos")]
        public virtual ActionResult Basicos(FichaTecnicaBasicosModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!model.Id.HasValue)
                    {
                        NovoBasicos(model);
                        this.AddSuccessMessage("Dados básicos da ficha técnica cadastrados com sucesso.");
                    }
                    else
                    {   EditarBasicos(model);
                        this.AddSuccessMessage("Dados básicos da ficha técnica atualizados com sucesso.");
                    }

                    return RedirectToAction("Editar", new { model.Id });
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
            fichaTecnicaMatriz.FichaTecnicaVariacaoMatrizs.AddRange(ObtenhaFichaTecnicaVariacaoMatriz(model));

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

        #endregion 

        #region Processos

        [PopulateViewData("PopulateViewDataProcessos")]
        public virtual ActionResult Processos(long? fichaTecnicaId)
        {
            if (!fichaTecnicaId.HasValue)
            {
                return PartialView("Processos", new FichaTecnicaProcessosModel());
            }

            var domain = _fichaTecnicaJeansRepository.Get(fichaTecnicaId);

            if (domain != null)
            {
                var model = new FichaTecnicaProcessosModel { Id = domain.Id };
                var modelList = new List<GridFichaTecnicaProcessosModel>();
                model.GridFichaTecnicaProcessos = modelList;

                domain.FichaTecnicaSequenciaOperacionals.ForEach(y => modelList.Add(new GridFichaTecnicaProcessosModel
                {
                    Custo = y.Custo,
                    Tempo = y.Tempo,
                    PesoProdutividade = y.PesoProdutividade,
                    SetorProducao = y.SetorProducao.Id.ToString(),
                    DepartamentoProducao = y.DepartamentoProducao.Id.ToString(),
                    OperacaoProducao = y.OperacaoProducao.Id.ToString()
                }));

                return PartialView("Processos", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a ficha técnica.");

            return PartialView("Processos", new FichaTecnicaProcessosModel());
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewDataProcessos")]
        public virtual ActionResult Processos(FichaTecnicaProcessosModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _fichaTecnicaJeansRepository.Get(model.Id);

                    if (domain.FichaTecnicaSequenciaOperacionals.IsNullOrEmpty())
                    {
                        NovoProcessos(domain, model);
                        this.AddSuccessMessage("Dados de processo da ficha técnica cadastrados com sucesso.");
                    }
                    else
                    {
                        EditarProcessos(domain, model);
                        this.AddSuccessMessage("Dados de processo da ficha técnica atualizados com sucesso.");
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar/atualizar os dados de processo da ficha técnica. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }

        protected virtual void EditarProcessos(FichaTecnicaJeans domain, FichaTecnicaProcessosModel model)
        {
            //_fichaTecnicaJeansRepository.SaveOrUpdate(domain);
        }

        protected virtual void NovoProcessos(FichaTecnicaJeans domain, FichaTecnicaProcessosModel model)
        {
            model.GridFichaTecnicaProcessos.ForEach(x => domain.FichaTecnicaSequenciaOperacionals.Add(new FichaTecnicaSequenciaOperacional()
            {
                Custo = x.Custo,
                Tempo = x.Tempo,
                PesoProdutividade = x.PesoProdutividade,
                DepartamentoProducao = _departamentoProducaoRepository.Load(long.Parse(x.DepartamentoProducao)),
                SetorProducao = _setorProducaoRepository.Load(long.Parse(x.SetorProducao)),
                OperacaoProducao = _operacaoProducaoRepository.Load(long.Parse(x.OperacaoProducao)),
            }));

            _fichaTecnicaJeansRepository.Update(domain);
        }

        #endregion

        #region Materiais

        [PopulateViewData("PopulateViewDataMaterial")]
        public virtual ActionResult Material(long? fichaTecnicaId)
        {
            if (!fichaTecnicaId.HasValue)
            {
                return PartialView("Material", new FichaTecnicaMaterialModel());
            }

            var domain = _fichaTecnicaJeansRepository.Get(fichaTecnicaId);

            if (domain != null)
            {
                var model = new FichaTecnicaMaterialModel
                {
                    Id = domain.Id,
                    GridMaterialComposicaoCustoMatriz = new List<GridMaterialComposicaoCustoMatrizModel>(),
                    GridMaterialConsumoItem = new List<GridMaterialConsumoItemModel>(),
                    GridMaterialConsumoMatriz = new List<GridMaterialConsumoMatrizModel>()
                };

                domain.MaterialComposicaoCustoMatrizs.ForEach(x => 
                    model.GridMaterialComposicaoCustoMatriz.Add(new GridMaterialComposicaoCustoMatrizModel
                {
                    Id = x.Id,
                    Custo = x.Custo,
                    Descricao = x.Material.Descricao,
                    Referencia = x.Material.Referencia,
                    UnidadeMedida = x.Material.UnidadeMedida.Sigla
                }));
                
                domain.FichaTecnicaMatriz.MaterialConsumoMatrizs.ForEach(x =>
                    model.GridMaterialConsumoMatriz.Add(new GridMaterialConsumoMatrizModel
                    {
                        Id = x.Id,
                        Custo = x.Custo,
                        Descricao = x.Material.Descricao,
                        Referencia = x.Material.Referencia,
                        UnidadeMedida = x.Material.UnidadeMedida.Sigla,
                        DepartamentoProducao = x.DepartamentoProducao.Id.ToString(),
                        Quantidade = x.Quantidade,
                        CustoTotal = x.Quantidade * x.Custo
                    }));

                domain.FichaTecnicaMatriz.MaterialConsumoItems.ForEach(x =>
                    model.GridMaterialConsumoItem.Add(new GridMaterialConsumoItemModel
                    {
                        Id = x.Id,
                        Custo = x.Custo,
                        Descricao = x.Material.Descricao,
                        Referencia = x.Material.Referencia,
                        UnidadeMedida = x.Material.UnidadeMedida.Sigla,
                        DepartamentoProducao = x.DepartamentoProducao.Id.ToString(),
                        Quantidade = x.Quantidade,
                        CompoeCusto = x.CompoeCusto,
                        Tamanho = x.Tamanho.Id.ToString(),
                        Variacao = x.FichaTecnicaVariacaoMatriz.Variacao.Id.ToString(),
                        CustoTotal = x.Quantidade * x.Custo
                    }));

                return PartialView("Material", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a ficha técnica.");

            return PartialView("Material", new FichaTecnicaMaterialModel());
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewDataMaterial")]
        public virtual ActionResult Material(FichaTecnicaMaterialModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _fichaTecnicaJeansRepository.Get(model.Id);
                    
                    TrateMaterialComposicaoCustoMatriz(domain, model.GridMaterialComposicaoCustoMatriz);
                    TrateMaterialConsumoItem(domain, model.GridMaterialConsumoItem);
                    TrateMaterialConsumoMatriz(domain, model.GridMaterialConsumoMatriz);
                    
                    _fichaTecnicaJeansRepository.SaveOrUpdate(domain);

                    this.AddSuccessMessage("Dados de material da ficha técnica salvos/atualizados com sucesso.");
                    return RedirectToAction("Editar", new { domain.Id });
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar/atualizar os dados de processo da ficha técnica. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }

        private void TrateMaterialConsumoMatriz(FichaTecnicaJeans domain, IEnumerable<GridMaterialConsumoMatrizModel> gridMaterialConsumoMatriz)
        {
            if (gridMaterialConsumoMatriz == null)
            {
                return;
            }

            gridMaterialConsumoMatriz.ForEach(x =>
            {
                if (!x.Id.HasValue || x.Id == 0)
                {
                    NovoMaterialConsumoMatriz(domain, x);
                }
            });
        }

        private void NovoMaterialConsumoMatriz(FichaTecnicaJeans domain, GridMaterialConsumoMatrizModel gridMaterialConsumoMatrizModel)
        {
            domain.FichaTecnicaMatriz.MaterialConsumoMatrizs.Add(new MaterialConsumoMatriz
            {
                Custo = gridMaterialConsumoMatrizModel.Custo.Value,
                DepartamentoProducao = _departamentoProducaoRepository.Load(long.Parse(gridMaterialConsumoMatrizModel.DepartamentoProducao)),
                Quantidade = gridMaterialConsumoMatrizModel.Quantidade.Value,
                Material = _materialRepository.Get(y => y.Referencia == gridMaterialConsumoMatrizModel.Referencia)
            });
        }

        private void TrateMaterialConsumoItem(FichaTecnicaJeans domain, IEnumerable<GridMaterialConsumoItemModel> gridMaterialConsumoItem)
        {
            if (gridMaterialConsumoItem == null)
            {
                return;
            }

            gridMaterialConsumoItem.ForEach(x =>
            {
                if (!x.Id.HasValue || x.Id == 0)
                {
                    NovoMaterialConsumoItem(domain, x);
                }
            });
        }

        private void NovoMaterialConsumoItem(FichaTecnicaJeans domain, GridMaterialConsumoItemModel gridMaterialConsumoItemModel)
        {
            domain.FichaTecnicaMatriz.MaterialConsumoItems.Add(new MaterialConsumoItem
            {
                Custo = gridMaterialConsumoItemModel.Custo.Value,
                CompoeCusto = gridMaterialConsumoItemModel.CompoeCusto.Value,
                DepartamentoProducao = _departamentoProducaoRepository.Load(long.Parse(gridMaterialConsumoItemModel.DepartamentoProducao)),
                Quantidade = gridMaterialConsumoItemModel.Quantidade.Value,
                Tamanho = _tamanhoRepository.Load(long.Parse(gridMaterialConsumoItemModel.Tamanho)),
                FichaTecnicaVariacaoMatriz =
                    ObtenhaFichaTecnicaVariacaoMatriz(domain.FichaTecnicaMatriz.FichaTecnicaVariacaoMatrizs, gridMaterialConsumoItemModel.Variacao),
                Material = _materialRepository.Get(y => y.Referencia == gridMaterialConsumoItemModel.Referencia)
            });
        }

        private FichaTecnicaVariacaoMatriz ObtenhaFichaTecnicaVariacaoMatriz(IEnumerable<FichaTecnicaVariacaoMatriz> fichaTecnicaVariacaoMatrizs, string variacao)
        {
            return fichaTecnicaVariacaoMatrizs.First(x => x.Variacao.Id.ToString() == variacao);
        }

        private void TrateMaterialComposicaoCustoMatriz(FichaTecnicaJeans domain, IEnumerable<GridMaterialComposicaoCustoMatrizModel> gridMaterialComposicaoCustoMatriz)
        {
            if (gridMaterialComposicaoCustoMatriz == null)
            {
                return;
            }

            gridMaterialComposicaoCustoMatriz.ForEach(x =>
            {
                if (!x.Id.HasValue || x.Id == 0)
                {
                    NovoMaterialComposicaoCustoMatriz(domain, x);
                }
            });
        }

        private void NovoMaterialComposicaoCustoMatriz(FichaTecnicaJeans domain, GridMaterialComposicaoCustoMatrizModel gridMaterialComposicaoCustoMatrizModel)
        {
            domain.MaterialComposicaoCustoMatrizs.Add(new MaterialComposicaoCustoMatriz
            {
                Custo = gridMaterialComposicaoCustoMatrizModel.Custo.Value,
                Material = _materialRepository.Get(y => y.Referencia == gridMaterialComposicaoCustoMatrizModel.Referencia)
            });
        }

        //protected virtual void EditarProcessos(FichaTecnicaJeans domain, FichaTecnicaProcessosModel model)
        //{
        //    //_fichaTecnicaJeansRepository.SaveOrUpdate(domain);
        //}

        //protected virtual void NovoProcessos(FichaTecnicaJeans domain, FichaTecnicaProcessosModel model)
        //{
        //    model.GridFichaTecnicaProcessos.ForEach(x => domain.FichaTecnicaSequenciaOperacionals.Add(new FichaTecnicaSequenciaOperacional()
        //    {
        //        Custo = x.Custo,
        //        Tempo = x.Tempo,
        //        PesoProdutividade = x.PesoProdutividade,
        //        DepartamentoProducao = _departamentoProducaoRepository.Load(long.Parse(x.DepartamentoProducao)),
        //        SetorProducao = _setorProducaoRepository.Load(long.Parse(x.SetorProducao)),
        //        OperacaoProducao = _operacaoProducaoRepository.Load(long.Parse(x.OperacaoProducao)),
        //    }));

        //    _fichaTecnicaJeansRepository.Update(domain);
        //}
        
        #endregion
        
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

        #region PopulateViewDataBasicos

        protected void PopulateViewDataBasicos(FichaTecnicaBasicosModel model)
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
        
        #region PopulateViewDataProcessos

        protected void PopulateViewDataProcessos(FichaTecnicaProcessosModel model)
        {
            var departamentoProducaos = _departamentoProducaoRepository.Find(p => p.Ativo).ToList();
            ViewBag.DepartamentoProducaos = departamentoProducaos.Select(s => new { Id = s.Id.ToString(), s.Nome }).OrderBy(x => x.Nome);
            ViewBag.DepartamentoProducaosDicionarioJson = departamentoProducaos.ToDictionary(k => k.Id.ToString(), e => e.Nome).FromDictionaryToJson();

            var setorProducaos = _setorProducaoRepository.Find(p => p.Ativo).ToList();
            ViewBag.SetorProducaosDicionarioJson = setorProducaos.ToDictionary(k => k.Id.ToString(), e => e.Nome).FromDictionaryToJson();

            var operacaoProducaos = _operacaoProducaoRepository.Find(p => p.Ativo).ToList();
            ViewBag.OperacaoProducaosDicionarioJson = operacaoProducaos.ToDictionary(k => k.Id.ToString(), e => e.Descricao).FromDictionaryToJson();
        }
        #endregion

        #region PopulateViewDataMaterial

        protected void PopulateViewDataMaterial(FichaTecnicaMaterialModel model)
        {
            var departamentoProducaos = _departamentoProducaoRepository.Find(p => p.Ativo).ToList();
            ViewBag.DepartamentoProducaos = departamentoProducaos.Select(s => new { Id = s.Id.ToString(), s.Nome }).OrderBy(x => x.Nome);
            ViewBag.DepartamentoProducaosDicionarioJson_Material = departamentoProducaos.ToDictionary(k => k.Id.ToString(), e => e.Nome).FromDictionaryToJson();

            if (!model.Id.HasValue)
            {
                return;
            }

            var fichaTecnica = _fichaTecnicaJeansRepository.Get(model.Id);

            var variacaos = fichaTecnica.FichaTecnicaMatriz.FichaTecnicaVariacaoMatrizs.Select(x => x.Variacao);
            ViewBag.VariacaoFichaTecnicas = variacaos.Select(s => new { Id = s.Id.ToString(), s.Nome }).OrderBy(x => x.Nome);
            //ViewBag.VariacaosDicionario_Material = variacaos.ToDictionary(k => k.Id, e => e.Nome);
            ViewBag.VariacaosDicionarioJson_Material = variacaos.ToDictionary(k => k.Id.ToString(), e => e.Nome).FromDictionaryToJson();
            
            var dicionarioTamanhos = fichaTecnica.FichaTecnicaMatriz.Grade.Tamanhos;
            var tamanhoLista = dicionarioTamanhos.Keys;

            ViewBag.TamanhoGrades = tamanhoLista.Select(s => new { Id = s.Id.ToString(), s.Descricao }).OrderBy(x => x.Descricao);
            //ViewBag.TamanhosDicionario_Material = tamanhoLista.ToDictionary(k => k.Id, e => e.Descricao);
            ViewBag.TamanhosDicionarioJson_Material = tamanhoLista.ToDictionary(k => k.Id.ToString(), e => e.Descricao).FromDictionaryToJson();
        }
        #endregion
        
        #region Actions Grid Variação
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

        #region Actions Grid Processos
        //Não são utilizadas pois as alterações são realizadas no submit e não durante a edição
        public virtual ActionResult EditingInlineFichaTecnicaProcessos_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(request);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult EditingInlineFichaTecnicaProcessos_Create([DataSourceRequest] DataSourceRequest request, FichaTecnicaProcessosModel fichaTecnicaProcessosModel)
        {
            return Json(new[] { fichaTecnicaProcessosModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult EditingInlineFichaTecnicaProcessos_Update([DataSourceRequest] DataSourceRequest request, FichaTecnicaProcessosModel fichaTecnicaProcessosModel)
        {
            //simula a persistência do item
            var random = new Random();
            int randomNumber = random.Next(0, 10000);
            fichaTecnicaProcessosModel.Id = randomNumber * -1;

            return Json(new[] { fichaTecnicaProcessosModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult EditingInlineFichaTecnicaProcessos_Destroy([DataSourceRequest] DataSourceRequest request, FichaTecnicaProcessosModel fichaTecnicaProcessosModel)
        {
            return Json(new[] { fichaTecnicaProcessosModel }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region Actions GridMaterialConsumoMatriz
        //Não são utilizadas pois as alterações são realizadas no submit e não durante a edição
        public virtual ActionResult EditingInlineMaterialConsumoMatriz_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(request);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult EditingInlineMaterialConsumoMatriz_Create([DataSourceRequest] DataSourceRequest request, GridMaterialConsumoMatrizModel fichaTecnicaMaterialModel)
        {
            return Json(new[] { fichaTecnicaMaterialModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult EditingInlineMaterialConsumoMatriz_Update([DataSourceRequest] DataSourceRequest request, GridMaterialConsumoMatrizModel fichaTecnicaMaterialModel)
        {
            //simula a persistência do item
            var random = new Random();
            int randomNumber = random.Next(0, 10000);
            fichaTecnicaMaterialModel.Id = randomNumber * -1;

            return Json(new[] { fichaTecnicaMaterialModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult EditingInlineMaterialConsumoMatriz_Destroy([DataSourceRequest] DataSourceRequest request, GridMaterialConsumoMatrizModel fichaTecnicaMaterialModel)
        {
            return Json(new[] { fichaTecnicaMaterialModel }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region Actions GridMaterialConsumoItem
        //Não são utilizadas pois as alterações são realizadas no submit e não durante a edição
        public virtual ActionResult EditingInlineMaterialConsumoItem_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(request);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult EditingInlineMaterialConsumoItem_Create([DataSourceRequest] DataSourceRequest request, GridMaterialConsumoItemModel fichaTecnicaMaterialModel)
        {
            return Json(new[] { fichaTecnicaMaterialModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult EditingInlineMaterialConsumoItem_Update([DataSourceRequest] DataSourceRequest request, GridMaterialConsumoItemModel fichaTecnicaMaterialModel)
        {
            //simula a persistência do item
            var random = new Random();
            int randomNumber = random.Next(0, 10000);
            fichaTecnicaMaterialModel.Id = randomNumber * -1;

            return Json(new[] { fichaTecnicaMaterialModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult EditingInlineMaterialConsumoItem_Destroy([DataSourceRequest] DataSourceRequest request, GridMaterialConsumoItemModel fichaTecnicaMaterialModel)
        {
            return Json(new[] { fichaTecnicaMaterialModel }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region Actions GridMaterialComposicaoCustoMatriz
        //Não são utilizadas pois as alterações são realizadas no submit e não durante a edição
        public virtual ActionResult EditingInlineMaterialComposicaoCustoMatriz_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(request);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult EditingInlineMaterialComposicaoCustoMatriz_Create([DataSourceRequest] DataSourceRequest request, GridMaterialComposicaoCustoMatrizModel fichaTecnicaMaterialModel)
        {
            return Json(new[] { fichaTecnicaMaterialModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult EditingInlineMaterialComposicaoCustoMatriz_Update([DataSourceRequest] DataSourceRequest request, GridMaterialComposicaoCustoMatrizModel fichaTecnicaMaterialModel)
        {
            //simula a persistência do item
            var random = new Random();
            int randomNumber = random.Next(0, 10000);
            fichaTecnicaMaterialModel.Id = randomNumber * -1;

            return Json(new[] { fichaTecnicaMaterialModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult EditingInlineMaterialComposicaoCustoMatriz_Destroy([DataSourceRequest] DataSourceRequest request, GridMaterialComposicaoCustoMatrizModel fichaTecnicaMaterialModel)
        {
            return Json(new[] { fichaTecnicaMaterialModel }.ToDataSourceResult(request, ModelState));
        }
        #endregion
    }
}