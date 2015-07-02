using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.EngenhariaProduto.Models;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using NHibernate.Linq;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers
{
    public partial class ModeloAvaliacaoController : BaseController
    {
		#region Variaveis
        
        private readonly IRepository<Modelo> _modeloRepository;
        private readonly IRepository<Colecao> _colecaoRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<ProdutoBase> _produtoBaseRepository;
        private readonly IRepository<Comprimento> _comprimentoRepository;
        private readonly IRepository<Barra> _barraRepository;
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
            {"Descricao", "Descricao"},
            {"Referencia", "Referencia"},
            {"Tag", "ModeloAvaliacao.Tag"},
            {"Colecao", "Colecao.Descricao"},
            {"ColecaoAprovada", "ModeloAvaliacao.Colecao.Descricao"},
            {"Estilista", "Estilista.Nome"},
            {"Situacao", "Situacao"}
        };
        #endregion

        #endregion

        #region Construtores
        public ModeloAvaliacaoController(ILogger logger, IRepository<Modelo> modeloRepository,
            IRepository<Colecao> colecaoRepository, IRepository<Pessoa> pessoaRepository,
            IRepository<ProdutoBase> produtoBaseRepository, IRepository<Comprimento> comprimentoRepository,
            IRepository<Barra> barraRepository, IRepository<ClassificacaoDificuldade> classificacaoDificuldadeRepository )
        {
            _modeloRepository = modeloRepository;
            _colecaoRepository = colecaoRepository;
            _pessoaRepository = pessoaRepository;
            _produtoBaseRepository = produtoBaseRepository;
            _comprimentoRepository = comprimentoRepository;
            _barraRepository = barraRepository;
            _classificacaoDificuldadeRepository = classificacaoDificuldadeRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        [PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index()
        {
            var model = new PesquisaModeloAvaliacaoModel {ModoConsulta = "listagem"};
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index(PesquisaModeloAvaliacaoModel model)
        {
            return View(model);
        }
        
        public virtual ActionResult ObtenhaListaGridModeloAvaliacaoModel([DataSourceRequest] DataSourceRequest request, PesquisaModeloAvaliacaoModel model)
        {
            try
            {
                var modelos = _modeloRepository.Find();

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

                if (!string.IsNullOrWhiteSpace(model.Tag))
                {
                    modelos = modelos.Where(p => p.ModeloAvaliacao != null && p.ModeloAvaliacao.Tag.Contains(model.Tag));
                    filtros.AppendFormat("Tag: {0}, ", model.Tag);
                }

                if (model.Situacao.HasValue)
                {
                    modelos = modelos.Where(p => p.Situacao == model.Situacao);
                    filtros.AppendFormat("Situação: {0}, ", model.Situacao.Value.EnumToString());
                }

                if (model.ColecaoAprovada.HasValue)
                {
                    modelos = modelos.Where(p => p.ModeloAvaliacao != null && p.ModeloAvaliacao.Colecao.Id == model.ColecaoAprovada);
                    filtros.AppendFormat("Coleção Aprovada: {0}, ", _colecaoRepository.Get(model.ColecaoAprovada.Value).Descricao);
                }

                if (model.Estilista.HasValue)
                {
                    modelos = modelos.Where(p => p.Estilista.Id == model.Estilista);
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

                modelos = modelos.OrderByDescending(o => o.DataAlteracao); 
                
                var total = modelos.Count();
                
                if (request.Page > 0)
                {
                    modelos = modelos.Skip((request.Page - 1) * request.PageSize);
                }
                
                var resultado = modelos.Take(request.PageSize).ToList();
                
                var list = resultado.Select(p => new GridModeloAvaliacaoModel
                {
                    Id = p.Id.GetValueOrDefault(),
                    Descricao = p.Descricao,
                    Referencia = p.Referencia,
                    Foto = !p.Fotos.IsNullOrEmpty() ? ObtenhaModeloFoto(p).Foto.Nome.GetFileUrl() : string.Empty,
                    Estilista = p.Estilista.Nome,
                    Colecao = p.Colecao.Descricao,
                    ColecaoAprovada = p.Situacao == SituacaoModelo.Aprovado ? p.ModeloAvaliacao.Colecao.Descricao : string.Empty,
                    Situacao = p.Situacao,
                    Tag = p.Situacao == SituacaoModelo.Aprovado? p.ModeloAvaliacao.ObtenhaTagCompleta() : string.Empty
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

        public ModeloFoto ObtenhaModeloFoto(Modelo modelo)
        {
            return !modelo.Fotos.IsNullOrEmpty() ? modelo.Fotos.First(x => x.Padrao) : modelo.Fotos.First();
        }

        #endregion

        #region Avaliar

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Avaliar(long id)
        {
            var domain = _modeloRepository.Get(id);

            if (domain != null)
            {
                var model = new ModeloAvaliacaoModel
                {
                    Estilista = domain.Estilista.Nome,
                    Colecao = domain.Colecao.Descricao,
                    Forro = domain.Forro,
                    Tecido = domain.Tecido,
                    IdModelo = domain.Id,
                    Descricao = domain.Descricao,
                    Referencia = domain.Referencia,
                    SequenciaTag = 1,
                    AprovadoReprovado = true
                };

                if (domain.ModeloAvaliacao != null)
                {
                    if (domain.ModeloAvaliacao.Aprovado)
                    {

                        model.IdAvaliacao = domain.ModeloAvaliacao.Id;
                        model.Tag = domain.ModeloAvaliacao.Tag;
                        model.Ano = domain.ModeloAvaliacao.Ano;
                        model.SequenciaTag = domain.ModeloAvaliacao.SequenciaTag;
                        model.AprovadoReprovado = domain.ModeloAvaliacao.Aprovado;
                        model.ClassificacaoDificuldade = domain.ModeloAvaliacao.ClassificacaoDificuldade.Id;
                        model.Catalogo = domain.ModeloAvaliacao.Catalogo;
                        model.ColecaoAprovada = domain.ModeloAvaliacao.Colecao.Id;
                        model.Complemento = domain.ModeloAvaliacao.Complemento;
                        model.GridItens = new List<ModeloAprovacaoModel>();

                        domain.ModeloAvaliacao.ModelosAprovados.ForEach(x =>
                        {
                            var modeloAprovacaoModel = new ModeloAprovacaoModel
                            {
                                Barra = x.Barra != null ? x.Barra.Id : null,
                                Comprimento = x.Comprimento != null ? x.Comprimento.Id : null,
                                ProdutoBase = x.ProdutoBase != null ? x.ProdutoBase.Id : null,
                                Descricao = x.Descricao,
                                Referencia = x.Referencia,
                                Id = x.Id,
                                Quantidade = x.Quantidade,
                                Observacao = x.Observacao,
                                MedidaBarra = x.MedidaBarra,
                                MedidaComprimento = x.MedidaComprimento,
                            };

                            model.GridItens.Add(modeloAprovacaoModel);
                        });
                    }
                    else
                    {
                        model.AprovadoReprovado = false;
                        model.Motivo = domain.ModeloAvaliacao.ModeloReprovacao.Motivo;
                    }
                }

                return View(model);
            }

            this.AddErrorMessage("Não foi possível encontrar o modelo.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Avaliar(ModeloAvaliacaoModel model)
        {
            foreach (var modelValue in ModelState.Values)
            {
                modelValue.Errors.Clear();
            }

            var modeloMesmoTagAno = _modeloRepository.Get(x => x.ModeloAvaliacao.Tag == model.Tag 
                && x.ModeloAvaliacao.Ano == model.Ano && x.ModeloAvaliacao.Id != model.IdAvaliacao);

            if (modeloMesmoTagAno != null)
            {
                ModelState.AddModelError(string.Empty, "Já existe um modelo avaliado com a mesma tag e ano.");
                return View(model);
            }

            try
            {
                var modelo = _modeloRepository.Get(model.IdModelo);

                if (modelo.ModeloAvaliacao == null)
                    modelo.ModeloAvaliacao = CrieModeloAvaliacao(model);
                else
                    AtualizeModeloAvaliacao(model, modelo.ModeloAvaliacao);

                modelo.Situacao = model.AprovadoReprovado ? SituacaoModelo.Aprovado : SituacaoModelo.Reprovado;
                
                _modeloRepository.SaveOrUpdate(modelo);
                
                Framework.UnitOfWork.Session.Current.Flush();
                this.AddSuccessMessage("O modelo foi avaliado com sucesso.");
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao avaliar o modelo. Confira se os dados foram informados corretamente: " + exception.Message);
                _logger.Info(exception.GetMessage());
            }
            return View(model);
        }
        
        private ModeloAvaliacao CrieModeloAvaliacao(ModeloAvaliacaoModel model)
        {
            ModeloAvaliacao modeloAvaliacao;

            if (model.AprovadoReprovado)
            {
                modeloAvaliacao = new ModeloAvaliacao
                {
                    Ano = model.Ano.Value,
                    Aprovado = model.AprovadoReprovado,
                    Catalogo = model.Catalogo.Value,
                    SequenciaTag = model.SequenciaTag,
                    ClassificacaoDificuldade =
                        model.ClassificacaoDificuldade.HasValue
                            ? _classificacaoDificuldadeRepository.Load(model.ClassificacaoDificuldade)
                            : null,
                    Colecao = model.ColecaoAprovada.HasValue ? _colecaoRepository.Load(model.ColecaoAprovada) : null,
                    Complemento = model.Complemento,
                    Data = DateTime.Now,
                    Tag = model.Tag,
                    ModelosAprovados = new List<ModeloAprovacao>()
                };

                model.GridItens.ForEach(x => modeloAvaliacao.ModelosAprovados.Add(CrieModeloAprovacao(x)));
                modeloAvaliacao.QuantidadeTotaAprovacao = modeloAvaliacao.ModelosAprovados.Sum(x => x.Quantidade);
            }
            else
            {
                modeloAvaliacao = new ModeloAvaliacao
                {
                    Aprovado = model.AprovadoReprovado,
                    Data = DateTime.Now,
                    ModeloReprovacao = new ModeloReprovacao()
                    {
                        Motivo = model.Motivo
                    }
                };
            }
            return modeloAvaliacao;
        }

        private void AtualizeModeloAvaliacaoAprovado(ModeloAvaliacaoModel model, ModeloAvaliacao modeloAvaliacao)
        {
            modeloAvaliacao.Catalogo = model.Catalogo.Value;
            modeloAvaliacao.ClassificacaoDificuldade = model.ClassificacaoDificuldade.HasValue
                ? _classificacaoDificuldadeRepository.Load(model.ClassificacaoDificuldade)
                : null;
            modeloAvaliacao.Colecao = model.ColecaoAprovada.HasValue
                ? _colecaoRepository.Load(model.ColecaoAprovada)
                : null;
            modeloAvaliacao.Complemento = model.Complemento;
            modeloAvaliacao.SequenciaTag = model.SequenciaTag;

            model.GridItens.ForEach(modelItem =>
            {
                var modeloAprovacao = modeloAvaliacao.ModelosAprovados.SingleOrDefault(x => x.Id == modelItem.Id);

                if (modeloAprovacao == null)
                {
                    modeloAvaliacao.ModelosAprovados.Add(CrieModeloAprovacao(modelItem));
                }
                else
                {
                    AtualizeModeloAprovacao(modelItem, modeloAprovacao);
                }
            });

            var modelosAprovados = new List<ModeloAprovacao>(modeloAvaliacao.ModelosAprovados);

            modelosAprovados.ForEach(modeloAprovacao =>
            {
                var modelItem = model.GridItens.SingleOrDefault(x => x.Id == modeloAprovacao.Id);
                if (modelItem == null && modeloAprovacao.Id != null)
                {
                    modeloAvaliacao.ModelosAprovados.Remove(modeloAprovacao);
                }
            });


            modeloAvaliacao.QuantidadeTotaAprovacao = modeloAvaliacao.ModelosAprovados.Sum(x => x.Quantidade);
        }

        private void AtualizeModeloAvaliacaoReprovado(ModeloAvaliacaoModel model, ModeloAvaliacao modeloAvaliacao)
        {
            modeloAvaliacao.Catalogo = null;
            modeloAvaliacao.ClassificacaoDificuldade = null;
            modeloAvaliacao.Colecao = null;
            modeloAvaliacao.Complemento = null;
            modeloAvaliacao.Tag = null;
            modeloAvaliacao.ModelosAprovados = null;

            if (modeloAvaliacao.ModeloReprovacao == null)
            {
                modeloAvaliacao.ModeloReprovacao = new ModeloReprovacao
                {
                    Motivo = model.Motivo
                };
            }
            else
            {
                modeloAvaliacao.ModeloReprovacao.Motivo = model.Motivo;
            }
        }

        private void AtualizeModeloAvaliacao(ModeloAvaliacaoModel model, ModeloAvaliacao modeloAvaliacao)
        {
            //modeloAvaliacao.Ano = model.Ano.Value;
            modeloAvaliacao.Aprovado = model.AprovadoReprovado;

            if (modeloAvaliacao.Aprovado)
            {
                AtualizeModeloAvaliacaoAprovado(model, modeloAvaliacao);
            }
            else
            {
                AtualizeModeloAvaliacaoReprovado(model, modeloAvaliacao);
            }
        }

        private void AtualizeModeloAprovacao(ModeloAprovacaoModel modelItem, ModeloAprovacao modeloAprovacao)
        {
            modeloAprovacao.Barra = modelItem.Barra.HasValue ? _barraRepository.Load(modelItem.Barra) : null;
            modeloAprovacao.Comprimento = modelItem.Comprimento.HasValue ? _comprimentoRepository.Load(modelItem.Comprimento) : null;
            modeloAprovacao.ProdutoBase = modelItem.ProdutoBase.HasValue ? _produtoBaseRepository.Load(modelItem.ProdutoBase) : null;
            modeloAprovacao.Descricao = modelItem.Descricao;
            modeloAprovacao.Referencia = modelItem.Referencia;
            modeloAprovacao.Observacao = modelItem.Observacao;
            modeloAprovacao.Quantidade = modelItem.Quantidade.Value;
            modeloAprovacao.MedidaBarra = modelItem.MedidaBarra;
            modeloAprovacao.MedidaComprimento = modelItem.MedidaComprimento;
        }

        private ModeloAprovacao CrieModeloAprovacao(ModeloAprovacaoModel modelItem)
        {
            return new ModeloAprovacao
            {
                Barra = modelItem.Barra.HasValue ? _barraRepository.Load(modelItem.Barra) : null,
                Comprimento = modelItem.Comprimento.HasValue ? _comprimentoRepository.Load(modelItem.Comprimento) : null,
                ProdutoBase = modelItem.ProdutoBase.HasValue ? _produtoBaseRepository.Load(modelItem.ProdutoBase) : null,
                Descricao = modelItem.Descricao,
                Referencia = modelItem.Referencia,
                Observacao = modelItem.Observacao,
                Quantidade = modelItem.Quantidade.Value,
                MedidaBarra = modelItem.MedidaBarra,
                MedidaComprimento = modelItem.MedidaComprimento,
            };
        }

        #endregion
        
        #endregion

        #region Métodos
        
        #region PopulateViewData
        protected void PopulateViewDataPesquisa(PesquisaModeloAvaliacaoModel model)
        {
            var colecoes = _colecaoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["ColecaoAprovada"] = colecoes.ToSelectList("Descricao", model.ColecaoAprovada);
            
            var estilistas = _pessoaRepository.Find(p => p.Funcionario != null
                && p.Funcionario.FuncaoFuncionario == FuncaoFuncionario.Estilista)
                .OrderBy(p => p.Nome).ToList();
            ViewData["Estilista"] = estilistas.ToSelectList("Nome", model.Estilista);

            ViewBag.OrdenarPor = new SelectList(ColunasPesquisaModeloAvaliacao, "value", "key");
        }
        #endregion        

        #region PopulateViewData
        protected void PopulateViewData(ModeloAvaliacaoModel model)
        {
            var classificacaoDificuldades = _classificacaoDificuldadeRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["ClassificacaoDificuldade"] = classificacaoDificuldades.ToSelectList("Descricao", model.ClassificacaoDificuldade);

            var colecoes = _colecaoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["ColecaoAprovada"] = colecoes.ToSelectList("Descricao", model.ColecaoAprovada);

            var comprimentos = _comprimentoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Comprimento"] = comprimentos.ToSelectList("Descricao");

            var produtosBase = _produtoBaseRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["ProdutoBase"] = produtosBase.ToSelectList("Descricao");

            var barras = _barraRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Barra"] = barras.ToSelectList("Descricao", model.ColecaoAprovada);
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            //var natureza = model as NaturezaModel;

            // Verificar duplicado
            //if (_naturezaRepository.Find(p => p.Descricao == natureza.Descricao && p.Id != natureza.Id).Any())
            //    ModelState.AddModelError("Descricao", "Já existe uma natureza do produto cadastrado com esta descrição.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            //var domain = _naturezaRepository.Get(id);

            //// Verificar relacionamento
            //if (_modeloRepository.Find().Any(p => p.Natureza == domain))
            //    ModelState.AddModelError("", "Não é possível excluir esta natureza do produto, pois existe(m) modelo(s) associadas a ela.");
        }
        #endregion

        #endregion
        [HttpGet, AjaxOnly]
        public virtual JsonResult ObtenhaDescricaoModeloAprovacao(long idModelo, string descricaoProdutoBase, string descricaoBarra, string descricaoComprimento)
        {
            var modelo = _modeloRepository.Get(idModelo);

            if (descricaoProdutoBase.IsNullOrEmpty() || descricaoProdutoBase == "-- Selecione --")
            {
                descricaoProdutoBase = modelo.ProdutoBase == null ? "" : modelo.ProdutoBase.Descricao;
            }

            if (descricaoBarra.IsNullOrEmpty() || descricaoBarra == "-- Selecione --")
            {
                descricaoBarra = modelo.Barra == null ? "" : modelo.Barra.Descricao;
            }

            if (descricaoComprimento.IsNullOrEmpty() || descricaoComprimento == "-- Selecione --")
            {
                descricaoComprimento = modelo.Comprimento == null ? "" : modelo.Comprimento.Descricao;
            }

            var descricao = modelo.Natureza.Descricao + " " + modelo.Classificacao.Descricao + " " + modelo.Artigo.Descricao + " " +
                descricaoProdutoBase + " " + descricaoComprimento + " " + descricaoBarra;

            return Json(descricao, JsonRequestBehavior.AllowGet);
        }
    }
}