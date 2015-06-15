using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Domain.Producao;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.EngenhariaProduto.Models;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using NHibernate.Linq;
using NHibernate.Util;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers
{
    public partial class ModeloAprovacaoController : BaseController
    {
		#region Variaveis
        
        private readonly IRepository<Modelo> _modeloRepository;
        private readonly IRepository<Colecao> _colecaoRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<ModeloAprovacao> _modeloAprovacaoRepository;
        private readonly IRepository<ModeloAprovacaoMatrizCorte> _modeloAprovacaoMatrizCorteRepository;
        private readonly IRepository<Tamanho> _tamanhoRepository;
        private readonly IRepository<ClassificacaoDificuldade> _classificacaoDificuldadeRepository;
         
        private readonly ILogger _logger;

        #region ColunasPesquisaAprovarModelo
        private static readonly Dictionary<string, string> ColunasPesquisaModeloAvaliacao = new Dictionary<string, string>
        {
            {"Descrição", "Descricao"},
            {"Referência", "Referencia"},
            {"Tag", "ModeloAvaliacao.Tag"},
        };

        private static readonly Dictionary<string, string> ColunasOrdenacaoGrid = new Dictionary<string, string>
        {
            {"Descricao", "ModeloAprovacao.Descricao"},
            {"Referencia", "ModeloAprovacao.Referencia"},
            {"TagAno", "Modelo.ModeloAvaliacao.Tag"},
            {"ColecaoAprovada", "Modelo.ModeloAvaliacao.Colecao.Descricao"},
            {"Estilista", "Modelo.Estilista.Nome"},
            {"Quantidade", "Modelo.ModeloAvaliacao.QuantidadeTotaAprovacao"},
            {"Dificuldade", "Modelo.ModeloAvaliacao.ClassificacaoDificuldade.Descricao"}
        };
        #endregion

        #endregion

        #region Construtores
        public ModeloAprovacaoController(ILogger logger, IRepository<Modelo> modeloRepository,
            IRepository<Colecao> colecaoRepository, IRepository<Pessoa> pessoaRepository,
            IRepository<ModeloAprovacao> modeloAprovacaoRepository, IRepository<Tamanho> tamanhoRepository,
            IRepository<ClassificacaoDificuldade> classificacaoDificuldadeRepository,
            IRepository<ModeloAprovacaoMatrizCorte> modeloAprovacaoMatrizCorteRepository 
        )
        {
            _modeloRepository = modeloRepository;
            _colecaoRepository = colecaoRepository;
            _pessoaRepository = pessoaRepository;
            _modeloAprovacaoRepository = modeloAprovacaoRepository;
            _tamanhoRepository = tamanhoRepository;
            _classificacaoDificuldadeRepository = classificacaoDificuldadeRepository;
            _modeloAprovacaoMatrizCorteRepository = modeloAprovacaoMatrizCorteRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        [PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index()
        {
            var model = new PesquisaModeloAprovacaoModel {ModoConsulta = "listagem"};
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index(PesquisaModeloAprovacaoModel model)
        {
            return View(model);
        }

        public virtual ActionResult ObtenhaListaGridModeloAprovacaoModel([DataSourceRequest] DataSourceRequest request, PesquisaModeloAprovacaoModel model)
        {
            try
            {
                var modelos = _modeloRepository.Find(x => x.Situacao == SituacaoModelo.Aprovado)
                    .SelectMany(x => x.ModeloAvaliacao.ModelosAprovados, (x, s) => new {Modelo = x, ModeloAprovacao = s});
                
                #region Filtros
                var filtros = new StringBuilder();


                if (!string.IsNullOrWhiteSpace(model.Tag))
                {
                    modelos = modelos.Where(p => p.Modelo.ModeloAvaliacao != null && p.Modelo.ModeloAvaliacao.Tag.Contains(model.Tag));
                    filtros.AppendFormat("Tag: {0}, ", model.Tag);
                }

                if (model.Situacao.HasValue)
                {
                    modelos = modelos.Where(p => p.Modelo.Situacao == model.Situacao);
                    filtros.AppendFormat("Situação: {0}, ", model.Situacao.Value.EnumToString());
                }

                if (model.ColecaoAprovada.HasValue)
                {
                    modelos = modelos.Where(p => p.Modelo.ModeloAvaliacao != null && p.Modelo.ModeloAvaliacao.Colecao.Id == model.ColecaoAprovada);
                    filtros.AppendFormat("Coleção Aprovada: {0}, ", _colecaoRepository.Get(model.ColecaoAprovada.Value).Descricao);
                }

                if (model.Estilista.HasValue)
                {
                    modelos = modelos.Where(p => p.Modelo.Estilista.Id == model.Estilista);
                    filtros.AppendFormat("Estilista: {0}, ", _pessoaRepository.Get(model.Estilista.Value).Nome);
                }

                #endregion
                
                if (!request.Sorts.IsNullOrEmpty())
                {
                    foreach (SortDescriptor sortDescriptor in request.Sorts)
                    {
                        modelos = sortDescriptor.SortDirection.ToString() == "Descending"
                            ? modelos.OrderByDescending(ColunasOrdenacaoGrid[sortDescriptor.Member])
                            : modelos.OrderBy(ColunasOrdenacaoGrid[sortDescriptor.Member]);
                    }
                }

                modelos = modelos.OrderByDescending(o => o.Modelo.DataAlteracao); 
                
                var total = modelos.Count();
                
                if (request.Page > 0)
                {
                    modelos = modelos.Skip((request.Page - 1) * request.PageSize);
                }
                
                var resultado = modelos.Take(request.PageSize).ToList();
                
                var list = resultado.Select(p => new GridModeloAprovacaoModel
                {
                    Id = p.ModeloAprovacao.Id.GetValueOrDefault(),
                    Descricao = p.ModeloAprovacao.Descricao,
                    Referencia = p.ModeloAprovacao.Referencia,
                    ColecaoAprovada = p.Modelo.ModeloAvaliacao.Colecao.Descricao,
                    Dificuldade = p.Modelo.ModeloAvaliacao.ClassificacaoDificuldade.Descricao,
                    Quantidade = p.ModeloAprovacao.Quantidade,
                    TagAno = p.Modelo.ModeloAvaliacao.ObtenhaTagCompleta()
                }).ToList();

                var valorPage = request.Page;
                request.Page = 1;
                var data = list.ToDataSourceResult(request);
                request.Page = valorPage;

                var result = new DataSourceResult()
                {
                    AggregateResults = data.AggregateResults,
                    Data = data.Data, 
                    Total = total
                };

                return Json(result);
            }
            catch (Exception ex)
            {
                return this.Json(new DataSourceResult
                {
                    Errors = ex.GetMessage()
                });
            }
        }

        #endregion

        #region Criar Fichas Tecnicas

        //[PopulateViewData("PopulateViewDataCriarFichasTecnicas")]
        public virtual ActionResult CriarFichasTecnicas(IEnumerable<long> ids)
        {
            var model = new CriacaoFichaTecnicaModel()
            {
                Ids = new List<long>(ids)
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult CriarFichasTecnicas(CriacaoFichaTecnicaModel model)
        {
            var modelosAprovacao = new List<ModeloAprovacao>();
            foreach (var idModeloAprovacao in model.Ids)
            {
                var modeloAprovacao = _modeloAprovacaoRepository.Find().FirstOrDefault(x => x.Id == idModeloAprovacao);
                if (modeloAprovacao != null && modeloAprovacao.FichaTecnica == null)
                {
                    modelosAprovacao.Add(modeloAprovacao);
                }
            }

            if (!modelosAprovacao.Any())
            {
                ModelState.AddModelError(string.Empty, "Todos os modelos aprovação já possuem fichas técnicas.");
                return View(model);
            }

            foreach (var modeloAprovacao in modelosAprovacao)
            {
                var modelo = _modeloRepository.Find().FirstOrDefault(x => x.ModeloAvaliacao.ModelosAprovados.Any(y => y.Id == modeloAprovacao.Id));
               
                var fichaTecnica = new FichaTecnica()
                {
                    Tag = modelo.ModeloAvaliacao.Tag,
                    Ano = modelo.ModeloAvaliacao.Ano,
                    Artigo = modelo.Artigo,
                    Descricao = modeloAprovacao.Descricao,
                    DataCadastro = DateTime.Now,
                    Catalogo = modelo.ModeloAvaliacao.Catalogo,
                    Classificacao = modelo.Classificacao,
                    ClassificacaoDificuldade = modelo.ModeloAvaliacao.ClassificacaoDificuldade,
                    Colecao = modelo.ModeloAvaliacao.Colecao,
                    Marca = modelo.Marca,
                    Natureza = modelo.Natureza,
                    Observacao = modelo.Observacao,
                    Complemento = modelo.ModeloAvaliacao.Complemento,
                    Detalhamento = modelo.Detalhamento,
                    Segmento = modelo.Segmento,
                    FichaTecnicaMatriz = new FichaTecnicaMatriz()
                    {
                        Grade = modelo.Grade
                    }
                };

                modelo.VariacaoModelos.ForEach(variacaoModelo =>
                {
                    var fichaTecnicaVariacaoMatriz = new FichaTecnicaVariacaoMatriz()
                    {
                        Variacao = variacaoModelo.Variacao
                    };

                    variacaoModelo.Cores.ForEach(cor => fichaTecnicaVariacaoMatriz.AddCor(cor));
                });

                //modelo.ObtenhaMaterialComposicaoModelos().ForEach(materialComposicaoModelo =>
                //{
                //    if (materialComposicaoModelo.VariacaoModelo != null)
                //    {
                //        fichaTecnica.FichaTecnicaMatriz.MaterialConsumoItems.Add(new FichaTecnicaMaterialConsumoVariacao()
                //        {
                //            CompoeCusto = true, 
                //            Quantidade = materialComposicaoModelo.Quantidade,
                //            Tamanho = materialComposicaoModelo.Tamanho
                //        });
                //    }
                //});
                
            }
            
            this.AddErrorMessage("xxxxxxxxx.");
            return RedirectToAction("Index");
        }

        #endregion

        #region Esboçar Corte

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult EsbocarCorte(long id)
        {
            var modeloDomain = _modeloRepository.Find(x => x.ModeloAvaliacao.ModelosAprovados.Any(y => y.Id == id)).First();

            var modeloAprovacaoDomain = modeloDomain.ModeloAvaliacao.ModelosAprovados.First(x => x.Id == id);

            if (modeloAprovacaoDomain != null)
            {
                var model = new ModeloAprovacaoMatrizCorteModel
                {
                    IdModelo = modeloDomain.Id,
                    IdModeloAprovacao = modeloAprovacaoDomain.Id,
                    Colecao = modeloDomain.Colecao.Descricao,
                    ColecaoAprovada = modeloDomain.ModeloAvaliacao.Colecao.Descricao,
                    Descricao = modeloAprovacaoDomain.Descricao,
                    Complemento = modeloDomain.ModeloAvaliacao.Complemento,
                    DescricaoModelo = modeloDomain.Descricao,
                    Dificuldade = modeloDomain.ModeloAvaliacao.ClassificacaoDificuldade.Descricao,
                    EstilistaModelo = modeloDomain.Estilista.Nome,
                    Forro = modeloDomain.Forro,
                    Tag = modeloDomain.ModeloAvaliacao.ObtenhaTagCompleta(),
                    Tecido = modeloDomain.Tecido,
                    ReferenciaModelo = modeloDomain.Referencia,
                    Catalogo = modeloDomain.ModeloAvaliacao.ObtenhaCatalogo(),
                    QtdeTotalAprovada = modeloDomain.ModeloAvaliacao.QuantidadeTotaAprovacao,
                    Quantidade = modeloAprovacaoDomain.Quantidade,
                    Referencia = modeloAprovacaoDomain.Referencia,
                    GridItens = new List<ModeloAprovacaoMatrizCorteItemModel>()
                };

                if (modeloAprovacaoDomain.ModeloAprovacaoMatrizCorte == null)
                {
                    modeloDomain.Grade.Tamanhos.Keys.ForEach(tamanho =>
                    {
                        var modelItem = new ModeloAprovacaoMatrizCorteItemModel
                        {
                            DescricaoTamanho = tamanho.Descricao,
                            Tamanho = tamanho.Id
                        };
                        model.GridItens.Add(modelItem);
                    });
                }
                else
                {
                    model.TipoEnfestoTecido = modeloAprovacaoDomain.ModeloAprovacaoMatrizCorte.TipoEnfestoTecido;
                    long totalNumeroVezes = 0;
                    modeloDomain.Grade.Tamanhos.Keys.ForEach(tamanho =>
                    {
                        var modeloAprovacaoMatrizCorteItem = modeloAprovacaoDomain.ModeloAprovacaoMatrizCorte.
                            ModeloAprovacaoMatrizCorteItens.FirstOrDefault(x => x.Tamanho.Id == tamanho.Id);

                        var modelItem = new ModeloAprovacaoMatrizCorteItemModel
                        {
                            DescricaoTamanho = tamanho.Descricao,
                            Tamanho = tamanho.Id,
                            Quantidade = modeloAprovacaoMatrizCorteItem != null ? modeloAprovacaoMatrizCorteItem.Quantidade : (long?)null,
                            QuantidadeVezes = modeloAprovacaoMatrizCorteItem != null ? modeloAprovacaoMatrizCorteItem.QuantidadeVezes : (long?)null,
                        };
                        
                        totalNumeroVezes += modeloAprovacaoMatrizCorteItem != null
                            ? modeloAprovacaoMatrizCorteItem.QuantidadeVezes
                            : 0;

                        model.GridItens.Add(modelItem);
                    });
                    model.TotalNumeroVezes = totalNumeroVezes;
                }

                return View(model);
            }

            this.AddErrorMessage("Não foi possível encontrar o modelo.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult EsbocarCorte(ModeloAprovacaoMatrizCorteModel model)
        {
            foreach (var modelValue in ModelState.Values)
            {
                modelValue.Errors.Clear();
            }

            try
            {
                var modeloAprovacao = _modeloAprovacaoRepository.Get(model.IdModeloAprovacao);

                if (modeloAprovacao.ModeloAprovacaoMatrizCorte != null)
                {
                    _modeloAprovacaoMatrizCorteRepository.Delete(modeloAprovacao.ModeloAprovacaoMatrizCorte);
                }

                var modeloAprovacaoMatrizCorte = new ModeloAprovacaoMatrizCorte
                {
                    TipoEnfestoTecido = model.TipoEnfestoTecido
                };

                model.GridItens.ForEach(modelItem =>
                {
                    if (!modelItem.Quantidade.HasValue)
                        return;

                    var modeloAprovacaoMatrizCorteItem = new ModeloAprovacaoMatrizCorteItem
                    {
                        Quantidade = modelItem.Quantidade.GetValueOrDefault(),
                        QuantidadeVezes = modelItem.QuantidadeVezes.GetValueOrDefault(),
                        Tamanho = _tamanhoRepository.Load(modelItem.Tamanho)
                    };
                    modeloAprovacaoMatrizCorte.ModeloAprovacaoMatrizCorteItens.Add(modeloAprovacaoMatrizCorteItem);
                });
                    
                modeloAprovacao.ModeloAprovacaoMatrizCorte = modeloAprovacaoMatrizCorte;
                modeloAprovacao.Quantidade = model.Quantidade;

                _modeloAprovacaoRepository.SaveOrUpdate(modeloAprovacao);

                Framework.UnitOfWork.Session.Current.Flush();
                this.AddSuccessMessage("O esboço de corte foi criado com sucesso.");
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty,
                    "Ocorreu um erro esboçar o corte. Confira se os dados foram informados corretamente: " +
                    exception.Message);
                _logger.Info(exception.GetMessage());
            }
            
            return View(model);
        }
        #endregion

        #endregion

        #region Métodos

        #region PopulateViewData
        protected void PopulateViewDataPesquisa(PesquisaModeloAprovacaoModel model)
        {
            var colecoes = _colecaoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["ColecaoAprovada"] = colecoes.ToSelectList("Descricao", model.ColecaoAprovada);
            
            var estilistas = _pessoaRepository.Find(p => p.Funcionario != null
                && p.Funcionario.FuncaoFuncionario == FuncaoFuncionario.Estilista)
                .OrderBy(p => p.Nome).ToList();
            ViewData["Estilista"] = estilistas.ToSelectList("Nome", model.Estilista);

            var classificacaoDificuldades = _classificacaoDificuldadeRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["ClassificacaoDificuldade"] = classificacaoDificuldades.ToSelectList("Descricao", model.ClassificacaoDificuldade);

            ViewBag.OrdenarPor = new SelectList(ColunasPesquisaModeloAvaliacao, "value", "key");
        }
        #endregion        

        #region PopulateViewData
        protected void PopulateViewData(ModeloAprovacaoMatrizCorteModel model)
        {

        }
        #endregion

        #endregion
    }
}