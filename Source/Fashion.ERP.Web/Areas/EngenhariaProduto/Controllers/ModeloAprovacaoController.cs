using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain;
using Fashion.ERP.Domain.Almoxarifado;
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
        private readonly IRepository<ClassificacaoDificuldade> _classificacaoDificuldadeRepository;
        private readonly IRepository<Material> _materialRepository;
         
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
            IRepository<ModeloAprovacao> modeloAprovacaoRepository, 
            IRepository<ClassificacaoDificuldade> classificacaoDificuldadeRepository,
            IRepository<Material> materialRepository
        )
        {
            _modeloRepository = modeloRepository;
            _colecaoRepository = colecaoRepository;
            _pessoaRepository = pessoaRepository;
            _modeloAprovacaoRepository = modeloAprovacaoRepository;
            _materialRepository = materialRepository;
            
            _classificacaoDificuldadeRepository = classificacaoDificuldadeRepository;
            
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

        public virtual ActionResult CriarFichaTecnica(IEnumerable<long> ids)
        {
            var model = new CriacaoFichaTecnicaModel
            {
                Ids = new List<long>(ids)
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult CriarFichaTecnica(CriacaoFichaTecnicaModel model)
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
                if (modeloAprovacao.FichaTecnica != null)
                    continue;
                try
                {
                    var modelo =
                        _modeloRepository.Find()
                            .FirstOrDefault(x => x.ModeloAvaliacao.ModelosAprovados.Any(y => y.Id == modeloAprovacao.Id));

                    var fichaTecnica = new FichaTecnicaJeans
                    {
                        Tag = modelo.ModeloAvaliacao.Tag,
                        Ano = modelo.ModeloAvaliacao.Ano,
                        Referencia = modeloAprovacao.Referencia,
                        Artigo = modelo.Artigo,
                        Descricao = modeloAprovacao.Descricao,
                        DataCadastro = DateTime.Now,
                        Catalogo = modelo.ModeloAvaliacao.Catalogo,
                        Classificacao = modelo.Classificacao,
                        ClassificacaoDificuldade = modelo.ModeloAvaliacao.ClassificacaoDificuldade,
                        Colecao = modelo.ModeloAvaliacao.Colecao,
                        Marca = modelo.Marca,
                        Natureza = modelo.Natureza,
                        Observacao = modeloAprovacao.Observacao,
                        Complemento = modelo.ModeloAvaliacao.Complemento,
                        Detalhamento = modelo.Detalhamento,
                        Segmento = modelo.Segmento,
                        Estilista = modelo.Estilista,

                        Comprimento = modeloAprovacao.Comprimento,
                        MedidaComprimento =
                            modeloAprovacao.MedidaComprimento.HasValue ? modeloAprovacao.MedidaComprimento.Value : 0,
                        Barra = modeloAprovacao.Barra,
                        MedidaBarra = modeloAprovacao.MedidaBarra.HasValue ? modeloAprovacao.MedidaBarra.Value : 0,
                        ProdutoBase = modeloAprovacao.ProdutoBase,
                        MedidaCos = modelo.Cos.HasValue ? modelo.Cos.Value : 0,
                        Lavada = modelo.Lavada,
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

                        fichaTecnica.FichaTecnicaMatriz.FichaTecnicaVariacaoMatrizs.Add(fichaTecnicaVariacaoMatriz);
                    });

                    modelo.MateriaisConsumo.ForEach(
                        materialConsumo => fichaTecnica.MateriaisConsumo.Add(new FichaTecnicaMaterialConsumo()
                        {
                            Quantidade = materialConsumo.Quantidade*materialConsumo.UnidadeMedida.FatorMultiplicativo,
                            DepartamentoProducao = materialConsumo.DepartamentoProducao,
                            Material = materialConsumo.Material,
                            Custo = materialConsumo.Material.ObtenhaUltimoCusto()
                        }));

                    model.GridItens.ForEach(modelMaterialComposicaoCusto => fichaTecnica.MateriaisComposicaoCusto.Add(new FichaTecnicaMaterialComposicaoCusto()
                    {
                        Material = _materialRepository.Find().First(x => x.Referencia == modelMaterialComposicaoCusto.Referencia),
                        Custo = modelMaterialComposicaoCusto.Custo.GetValueOrDefault()
                    }));

                    if (modelo.Modelista != null && modelo.DataModelagem.HasValue)
                    {
                        fichaTecnica.FichaTecnicaModelagem = new FichaTecnicaModelagem()
                        {
                            DataModelagem = modelo.DataModelagem.Value,
                            Modelista = modelo.Modelista,
                            Observacao = modelo.Modelagem,
                        };
                    }

                    modelo.Fotos.ForEach(modeloFoto => fichaTecnica.FichaTecnicaFotos.Add(new FichaTecnicaFoto()
                    {
                        Arquivo = CopieArquivo(modeloFoto.Foto),
                        Descricao = modeloFoto.Foto.Titulo,
                        Impressao = modeloFoto.Impressao,
                        Padrao = modeloFoto.Padrao
                    }));
                
                    modeloAprovacao.FichaTecnica = fichaTecnica;
                    _modeloAprovacaoRepository.SaveOrUpdate(modeloAprovacao);
                    Framework.UnitOfWork.Session.Current.Flush();
                }
                catch (FileNotFoundException exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Ocorreu um erro ao tentar criar a ficha técnica: Uma foto do modelo não foi encontrado no disco.");
                    _logger.Info(exception.GetMessage());
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Ocorreu um erro ao tentar criar a ficha técnica " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            this.AddSuccessMessage("Fichas técnicas criadas com sucesso.");
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
        protected void PopulateViewCriarFichasTecnicas()
        {
            var colecoes = _colecaoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["ColecaoAprovada"] = colecoes.ToSelectList("Descricao");

            var estilistas = _pessoaRepository.Find(p => p.Funcionario != null
                && p.Funcionario.FuncaoFuncionario == FuncaoFuncionario.Estilista)
                .OrderBy(p => p.Nome).ToList();
            ViewData["Estilista"] = estilistas.ToSelectList("Nome");

            var classificacaoDificuldades = _classificacaoDificuldadeRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["ClassificacaoDificuldade"] = classificacaoDificuldades.ToSelectList("Descricao");

            ViewBag.OrdenarPor = new SelectList(ColunasPesquisaModeloAvaliacao, "value", "key");
        }
        #endregion

        #endregion

        public Arquivo CopieArquivo(Arquivo arquivoAtual)
        {
            var filePathAtual = arquivoAtual.Nome.GetFilePath();
            
            var filename = Helpers.Upload.GenerateTempFilename(arquivoAtual.Extensao).GetTempFilePath();
            var filePathNovo = filename.GetFilePath();

            System.IO.File.Copy(filePathAtual, filePathNovo, true);

            var fileInfo = new FileInfo(filePathNovo);
            var arquivo = new Arquivo
            {
                Nome = filename,
                Titulo = arquivoAtual.Titulo,
                Data = fileInfo.CreationTime,
                Extensao = fileInfo.Extension,
                Tamanho = fileInfo.Length
            };

            return arquivo;
        }
    }
}