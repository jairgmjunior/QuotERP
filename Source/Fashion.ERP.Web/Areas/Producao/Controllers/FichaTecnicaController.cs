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
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Reporting.Producao;
using Fashion.ERP.Web.Areas.Producao.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Fashion.Framework.UnitOfWork.DinamicFilter;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using NHibernate.Linq;
using Ninject.Extensions.Logging;
using Telerik.Reporting;

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
        private readonly IRepository<Arquivo> _arquivoRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;

        private readonly ILogger _logger;
        #endregion

        #region ColunasAgrupamentoOrdenacao
        private static readonly Dictionary<string, string> ColunasAgrupamentoOrdenacao = new Dictionary<string, string>
        {
            {"Catálogo", "Catalogo"},
            {"Coleção", "Colecao.Descricao"},
            {"Dificuldade", "ClassificacaoDificuldade.Descricao"},
            {"Estilista", "Estilista.Nome"}
        };
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
            IRepository<Material> materialRepository,
            IRepository<Arquivo> arquivoRepository,
            IRepository<Pessoa> pessoaRepository)
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
            _arquivoRepository = arquivoRepository;
            _pessoaRepository = pessoaRepository;
        }
        #endregion

        #region Index
        [PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index()
        {
            return View( new PesquisaFichaTecnicaModel() );
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index(PesquisaFichaTecnicaModel model)
        {
            try
            {
                var filtros = new StringBuilder();

                var fichasTecnicas = ObtenhaFichasTecnicasFiltradas(model, filtros);
            
                // Se não é uma listagem, gerar o relatório
                var result = fichasTecnicas.ToList();

                if (!result.Any())
                    return Json(new { Error = "Nenhum item encontrado." });

                #region Montar Relatório

                var report = new ListaFichaTecnicaReport { DataSource = result };

                if (filtros.Length > 2)
                    report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

                var grupo = report.Groups.First(p => p.Name.Equals("Grupo"));

                if (model.AgruparPor != null)
                {
                    grupo.Groupings.Add("=Fields." + model.AgruparPor);

                    var key = ColunasAgrupamentoOrdenacao.First(p => p.Value == model.AgruparPor).Key;
                    var titulo = string.Format("= \"{0}: \" + Fields.{1}", key, model.AgruparPor);
                    grupo.GroupHeader.GetTextBox("Titulo").Value = titulo;
                }
                else
                {
                    report.Groups.Remove(grupo);
                }

                if (model.OrdenarPor != null)
                    report.Sortings.Add("=Fields." + model.OrdenarPor, model.OrdenarEm == "asc" ? SortDirection.Asc : SortDirection.Desc);
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
                return View(model);
            }
        }

        public IQueryable<FichaTecnica> ObtenhaFichasTecnicasFiltradas(PesquisaFichaTecnicaModel model, StringBuilder filtros)
        {
            var fichaTecnicas = _fichaTecnicaRepository.Find();
            
            if (!string.IsNullOrWhiteSpace(model.Referencia))
            {
                fichaTecnicas = fichaTecnicas.Where(p => p.Referencia == model.Referencia);
                filtros.AppendFormat("Referência: {0}, ", model.Referencia);
            }

            if (!string.IsNullOrWhiteSpace(model.Descricao))
            {
                fichaTecnicas = fichaTecnicas.Where(p => p.Descricao.Contains(model.Descricao));
                filtros.AppendFormat("Descrição: {0}, ", model.Descricao);
            }

            if (!string.IsNullOrWhiteSpace(model.Tag))
            {
                fichaTecnicas = fichaTecnicas.Where(p => p.Tag.Contains(model.Tag));
                filtros.AppendFormat("Tag: {0}, ", model.Tag);
            }

            if (model.Ano.HasValue)
            {
                fichaTecnicas = fichaTecnicas.Where(p => p.Ano == model.Ano);
                filtros.AppendFormat("Ano: {0}, ", model.Ano);
            }

            if (model.ClassificacaoDificuldade.HasValue)
            {
                fichaTecnicas = fichaTecnicas.Where(p => p.ClassificacaoDificuldade.Id == model.ClassificacaoDificuldade);
                filtros.AppendFormat("Dificuldade: {0}, ", _classificacaoDificuldadeRepository.Get(model.ClassificacaoDificuldade.Value).Descricao);
            }

            if (model.Estilista.HasValue)
            {
                fichaTecnicas = fichaTecnicas.Where(p => p.Estilista.Id == model.Estilista);
                filtros.AppendFormat("Estilista: {0}, ", _pessoaRepository.Get(model.Estilista.Value).Nome);
            }

            if (model.Colecao.HasValue)
            {
                fichaTecnicas = fichaTecnicas.Where(p => p.Colecao.Id == model.Colecao);
                filtros.AppendFormat("Coleção: {0}, ", _colecaoRepository.Get(model.Colecao));
            }

            if (model.Catalogo.HasValue)
            {
                fichaTecnicas = fichaTecnicas.Where(p => p.Catalogo == model.Catalogo);
                filtros.AppendFormat("Catálogo: {0}, ", model.Catalogo.GetValueOrDefault() ? "sim" : "não");
            }

            if (model.DataCadastro.HasValue && !model.DataCadastroAte.HasValue)
            {
                fichaTecnicas = fichaTecnicas.Where(p => p.DataCadastro.Date >= model.DataCadastro.Value);

                filtros.AppendFormat("Data de cadastro de '{0}', ", model.DataCadastro.Value.ToString("dd/MM/yyyy"));
            }

            if (!model.DataCadastro.HasValue && model.DataCadastroAte.HasValue)
            {
                fichaTecnicas = fichaTecnicas.Where(p => p.DataCadastro.Date <= model.DataCadastroAte.Value);

                filtros.AppendFormat("Data de cadastro até '{0}', ", model.DataCadastroAte.Value.ToString("dd/MM/yyyy"));
            }

            if (model.DataCadastro.HasValue && model.DataCadastroAte.HasValue)
            {
                fichaTecnicas = fichaTecnicas.Where(p => p.DataCadastro.Date >= model.DataCadastro.Value
                                                         && p.DataCadastro.Date <= model.DataCadastroAte.Value);
                filtros.AppendFormat("Data de cadastro de '{0}' até '{1}', ",
                                     model.DataCadastro.Value.ToString("dd/MM/yyyy"),
                                     model.DataCadastroAte.Value.ToString("dd/MM/yyyy"));
            }

            return fichaTecnicas;
        }

        public virtual ActionResult ObtenhaListaGridModel([DataSourceRequest] DataSourceRequest request, PesquisaFichaTecnicaModel model)
        {
            try
            {
                var filtros = new StringBuilder();
                
                var fichasTecnicas = ObtenhaFichasTecnicasFiltradas(model, filtros);

                fichasTecnicas = fichasTecnicas.OrderByDescending(o => o.DataAlteracao);

                var total = fichasTecnicas.Count();

                if (request.Page > 0)
                {
                    fichasTecnicas = fichasTecnicas.Skip((request.Page - 1) * request.PageSize);
                }

                var resultado = fichasTecnicas.Take(request.PageSize).ToList();

                var list = resultado.Select(p => new GridFichaTecnicaModel
                {
                    Id = p.Id.GetValueOrDefault(),
                    Referencia = p.Referencia,
                    Descricao = p.Descricao,
                    Tag = p.Tag,
                    Ano = p.Ano,
                    Colecao = p.Colecao.Descricao,
                    Marca = p.Marca.Nome,
                    Natureza = p.Natureza.Descricao,
                    Foto = ObtenhaUrlFoto(p)
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

        public string ObtenhaUrlFoto(FichaTecnica fichaTecnica)
        {
            if (fichaTecnica.FichaTecnicaFotos.Any(x => x.Padrao))
            {
                return fichaTecnica.FichaTecnicaFotos.Where(x => x.Padrao).ElementAt(0).Arquivo.Nome.GetFileUrl();
            }
            
            return fichaTecnica.FichaTecnicaFotos.Any() ? fichaTecnica.FichaTecnicaFotos.ElementAt(0).Arquivo.Nome.GetFileUrl() : null;
        }

        #endregion

        #region Novo

        public virtual ActionResult Novo()
        {
            return View("Basicos", new FichaTecnicaBasicosModel());
        }
        #endregion

        #region Básicos

        [PopulateViewData("PopulateViewDataBasicos")]
        public virtual ActionResult Basicos(long id)
        {
            var domain = _fichaTecnicaJeansRepository.Get(id);

            if (domain != null)
            {
                var model  = Mapper.Flat<FichaTecnicaBasicosModel>(domain);
                
                model.Grade = domain.FichaTecnicaMatriz.Grade.Id;
                model.GridFichaTecnicaVariacao = new List<GridFichaTecnicaVariacaoModel>();
                model.Funcionario = domain.Estilista.Id.GetValueOrDefault();

                domain.FichaTecnicaMatriz.FichaTecnicaVariacaoMatrizs.ForEach(x => x.Cores.ForEach(y => model.GridFichaTecnicaVariacao.Add(new GridFichaTecnicaVariacaoModel()
                {
                    Cor = y.Id.ToString(),
                    Variacao = x.Variacao.Id.ToString()
                })));

                model.Catalogo = domain.Catalogo.HasValue ? domain.Catalogo : false;

                return View("Basicos", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a ficha técnica.");
            
            return View("Basicos", new FichaTecnicaBasicosModel());
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
                    this.AddErrorMessage("Não é possível salvar a ficha técnica. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { model.Id });
        }

        protected virtual void EditarBasicos(FichaTecnicaBasicosModel model)
        {
            var domain = _fichaTecnicaJeansRepository.Get(model.Id);

            domain = Mapper.Unflat(model, domain);
            domain.FichaTecnicaMatriz = ObtenhaFichaTecnicaMatriz(model);
            domain.Estilista = _pessoaRepository.Get(model.Funcionario);

            _fichaTecnicaJeansRepository.SaveOrUpdate(domain);
        }

        protected virtual void NovoBasicos(FichaTecnicaBasicosModel model)
        {
            var domain = Mapper.Unflat<FichaTecnicaJeans>(model);
            domain.DataCadastro = DateTime.Now;
            domain.FichaTecnicaMatriz = ObtenhaFichaTecnicaMatriz(model);
            domain.Estilista = _pessoaRepository.Get(model.Funcionario);
            _fichaTecnicaJeansRepository.Save(domain);
            model.Id = domain.Id;
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
        public virtual ActionResult Processos(long id)
        {
            var domain = _fichaTecnicaJeansRepository.Get(id);

            if (domain != null)
            {
                var model = new FichaTecnicaProcessosModel { Id = domain.Id };
                var modelList = new List<GridFichaTecnicaProcessosModel>();
                model.GridFichaTecnicaProcessos = modelList;

                domain.FichaTecnicaSequenciaOperacionals.ForEach(y => modelList.Add(new GridFichaTecnicaProcessosModel
                {
                    Id = y.Id,
                    Custo = y.Custo,
                    Tempo = y.Tempo,
                    PesoProdutividade = y.PesoProdutividade,
                    SetorProducao = y.SetorProducao.Id.ToString(),
                    DepartamentoProducao = y.DepartamentoProducao.Id.ToString(),
                    OperacaoProducao = y.OperacaoProducao.Id.ToString()
                }));

                return View(model);
            }

            this.AddErrorMessage("Não foi possível encontrar a ficha técnica.");

            return View(new FichaTecnicaProcessosModel());
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

                    return RedirectToAction("Editar", new { model.Id });
                }
                catch (Exception exception)
                {
                    this.AddErrorMessage("Não é possível salvar/atualizar os dados de processo da ficha técnica. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { model.Id });
        }

        protected virtual void EditarProcessos(FichaTecnicaJeans domain, FichaTecnicaProcessosModel model)
        {
            model.GridFichaTecnicaProcessos.ForEach(x =>
            {
                if (x.Id == 0 || !x.Id.HasValue)
                {
                    domain.FichaTecnicaSequenciaOperacionals.Add(new FichaTecnicaSequenciaOperacional()
                    {
                        Custo = x.Custo,
                        Tempo = x.Tempo,
                        PesoProdutividade = x.PesoProdutividade,
                        DepartamentoProducao = _departamentoProducaoRepository.Load(long.Parse(x.DepartamentoProducao)),
                        SetorProducao = _setorProducaoRepository.Load(long.Parse(x.SetorProducao)),
                        OperacaoProducao = _operacaoProducaoRepository.Load(long.Parse(x.OperacaoProducao)),
                    });
                }
                else
                {
                    var fichaTecnicaSequenciaOperacional = domain.FichaTecnicaSequenciaOperacionals.FirstOrDefault(y => y.Id == x.Id);

                    fichaTecnicaSequenciaOperacional.Custo = x.Custo;
                    fichaTecnicaSequenciaOperacional.Tempo = x.Tempo;
                    fichaTecnicaSequenciaOperacional.PesoProdutividade = x.PesoProdutividade;
                    fichaTecnicaSequenciaOperacional.DepartamentoProducao = _departamentoProducaoRepository.Load(long.Parse(x.DepartamentoProducao));
                    fichaTecnicaSequenciaOperacional.SetorProducao = _setorProducaoRepository.Load(long.Parse(x.SetorProducao));
                    fichaTecnicaSequenciaOperacional.OperacaoProducao = _operacaoProducaoRepository.Load(long.Parse(x.OperacaoProducao));
                }
            });


            var listaExcluir = new List<FichaTecnicaSequenciaOperacional>();

            domain.FichaTecnicaSequenciaOperacionals.ForEach(tecnicaSequenciaOperacional =>
            {
                if (model.GridFichaTecnicaProcessos.All(x => x.Id != tecnicaSequenciaOperacional.Id && tecnicaSequenciaOperacional.Id != null))
                {
                    listaExcluir.Add(tecnicaSequenciaOperacional);
                }
            });

            foreach (var tecnicaSequenciaOperacional in listaExcluir)
            {
                domain.FichaTecnicaSequenciaOperacionals.Remove(tecnicaSequenciaOperacional);
            }

            _fichaTecnicaJeansRepository.Update(domain);
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
            model.Id = domain.Id;
        }

        #endregion

        #region Materiais

        [PopulateViewData("PopulateViewDataMaterial")]
        public virtual ActionResult Material(long id)
        {
            var domain = _fichaTecnicaJeansRepository.Get(id);

            if (domain != null)
            {
                var model = new FichaTecnicaMaterialModel
                {
                    Id = domain.Id,
                    GridMaterialComposicaoCustoMatriz = new List<GridMaterialComposicaoCustoMatrizModel>(),
                    GridMaterialConsumoItem = new List<GridMaterialConsumoItemModel>(),
                    GridMaterialConsumoMatriz = new List<GridMaterialConsumoMatrizModel>()
                };

                domain.MateriaisComposicaoCusto.ForEach(x => 
                    model.GridMaterialComposicaoCustoMatriz.Add(new GridMaterialComposicaoCustoMatrizModel
                {
                    Id = x.Id,
                    Custo = x.Custo,
                    Descricao = x.Material.Descricao,
                    Referencia = x.Material.Referencia,
                    UnidadeMedida = x.Material.UnidadeMedida.Sigla,
                    Foto = (x.Material.Foto != null ? x.Material.Foto.Nome.GetFileUrl() : string.Empty),
                }));
                model.GridMaterialComposicaoCustoMatriz = model.GridMaterialComposicaoCustoMatriz.OrderBy(x => x.Descricao).ToList();

                domain.MateriaisConsumo.ForEach(x =>
                    model.GridMaterialConsumoMatriz.Add(new GridMaterialConsumoMatrizModel
                    {
                        Id = x.Id,
                        Custo = x.Custo,
                        Descricao = x.Material.Descricao,
                        Referencia = x.Material.Referencia,
                        UnidadeMedida = x.Material.UnidadeMedida.Sigla,
                        DepartamentoProducao = x.DepartamentoProducao.Id.ToString(),
                        DepartamentoProducaoNome = x.DepartamentoProducao.Nome,
                        Quantidade = x.Quantidade,
                        CustoTotal = x.Quantidade * x.Custo,
                        Foto = (x.Material.Foto != null ? x.Material.Foto.Nome.GetFileUrl() : string.Empty),
                    }));
                model.GridMaterialConsumoMatriz = model.GridMaterialConsumoMatriz.OrderBy(x => x.DepartamentoProducaoNome).ThenBy(x => x.Descricao).ToList();

                domain.MateriaisConsumoVariacao.ForEach(x =>
                    model.GridMaterialConsumoItem.Add(new GridMaterialConsumoItemModel
                    {
                        Id = x.Id,
                        Custo = x.Custo,
                        Descricao = x.Material.Descricao,
                        Referencia = x.Material.Referencia,
                        UnidadeMedida = x.Material.UnidadeMedida.Sigla,
                        DepartamentoProducao = x.DepartamentoProducao.Id.ToString(),
                        DepartamentoProducaoNome = x.DepartamentoProducao.Nome,
                        Quantidade = x.Quantidade,
                        CompoeCusto = x.CompoeCusto,
                        Tamanho = x.Tamanho.Id.ToString(),
                        Variacao = x.FichaTecnicaVariacaoMatriz.Variacao.Id.ToString(),
                        CustoTotal = x.Quantidade * x.Custo,
                        Foto = (x.Material.Foto != null ? x.Material.Foto.Nome.GetFileUrl() : string.Empty),
                    }));
                model.GridMaterialConsumoItem = model.GridMaterialConsumoItem.OrderBy(x => x.DepartamentoProducaoNome).ThenBy(x => x.Descricao).ToList();

                return View(model);
            }

            this.AddErrorMessage("Não foi possível encontrar a ficha técnica.");

            return View(new FichaTecnicaMaterialModel());
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
                    this.AddErrorMessage("Não é possível salvar/atualizar os dados de processo da ficha técnica. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { model.Id });
        }

        private void TrateMaterialConsumoMatriz(FichaTecnicaJeans domain, IEnumerable<GridMaterialConsumoMatrizModel> gridMaterialConsumoMatriz)
        {
            if (gridMaterialConsumoMatriz != null)
            {
                gridMaterialConsumoMatriz.ForEach(x =>
                {
                    if (!x.Id.HasValue || x.Id == 0)
                    {
                        NovoMaterialConsumoMatriz(domain, x);
                    }
                    else
                    {
                        var materialConsumo = domain.MateriaisConsumo.First(y => y.Id == x.Id);

                        materialConsumo.Custo = x.Custo.Value;
                        materialConsumo.DepartamentoProducao = _departamentoProducaoRepository.Load(long.Parse(x.DepartamentoProducao));
                        materialConsumo.Quantidade = x.Quantidade.Value;
                    }
                });
            }

            var listaExcluir = new List<FichaTecnicaMaterialConsumo>();

            domain.MateriaisConsumo.ForEach(fichaTecnicaMaterialConsumo =>
            {
                if (gridMaterialConsumoMatriz == null || gridMaterialConsumoMatriz.All(x => x.Id != fichaTecnicaMaterialConsumo.Id && fichaTecnicaMaterialConsumo.Id != null))
                {
                    listaExcluir.Add(fichaTecnicaMaterialConsumo);
                }
            });

            foreach (var fichaTecnicaMaterialConsumo in listaExcluir)
            {
                domain.MateriaisConsumo.Remove(fichaTecnicaMaterialConsumo);
            }
        }

        private void NovoMaterialConsumoMatriz(FichaTecnicaJeans domain, GridMaterialConsumoMatrizModel gridMaterialConsumoMatrizModel)
        {
            domain.MateriaisConsumo.Add(new FichaTecnicaMaterialConsumo
            {
                Custo = gridMaterialConsumoMatrizModel.Custo.Value,
                DepartamentoProducao = _departamentoProducaoRepository.Load(long.Parse(gridMaterialConsumoMatrizModel.DepartamentoProducao)),
                Quantidade = gridMaterialConsumoMatrizModel.Quantidade.Value,
                Material = _materialRepository.Get(y => y.Referencia == gridMaterialConsumoMatrizModel.Referencia)
            });
        }

        private void TrateMaterialConsumoItem(FichaTecnicaJeans domain, IEnumerable<GridMaterialConsumoItemModel> gridMaterialConsumoItem)
        {
            if (gridMaterialConsumoItem != null)
            {
                gridMaterialConsumoItem.ForEach(x =>
                {
                    if (!x.Id.HasValue || x.Id == 0)
                    {
                        NovoMaterialConsumoItem(domain, x);
                    }
                    else
                    {
                        var materialConsumoVariacao = domain.MateriaisConsumoVariacao.First(y => y.Id == x.Id);

                        materialConsumoVariacao.Custo = x.Custo.Value;
                        materialConsumoVariacao.CompoeCusto = x.CompoeCusto.Value;
                        materialConsumoVariacao.DepartamentoProducao = _departamentoProducaoRepository.Load(long.Parse(x.DepartamentoProducao));
                        materialConsumoVariacao.Quantidade = x.Quantidade.Value;
                        materialConsumoVariacao.Tamanho = _tamanhoRepository.Load(long.Parse(x.Tamanho));
                        materialConsumoVariacao.FichaTecnicaVariacaoMatriz =
                            ObtenhaFichaTecnicaVariacaoMatriz(domain.FichaTecnicaMatriz.FichaTecnicaVariacaoMatrizs,
                                x.Variacao);
                    }
                });
            }

            var listaExcluir = new List<FichaTecnicaMaterialConsumoVariacao>();

            domain.MateriaisConsumoVariacao.ForEach(fichaTecnicaMaterialConsumoVariacao =>
            {
                if (gridMaterialConsumoItem == null || gridMaterialConsumoItem.All(x => x.Id != fichaTecnicaMaterialConsumoVariacao.Id && fichaTecnicaMaterialConsumoVariacao.Id != null))
                {
                    listaExcluir.Add(fichaTecnicaMaterialConsumoVariacao);
                }
            });

            foreach (var fichaTecnicaMaterialConsumoVariacao in listaExcluir)
            {
                domain.MateriaisConsumoVariacao.Remove(fichaTecnicaMaterialConsumoVariacao);
            }
        }

        private void NovoMaterialConsumoItem(FichaTecnicaJeans domain, GridMaterialConsumoItemModel gridMaterialConsumoItemModel)
        {
            domain.MateriaisConsumoVariacao.Add(new FichaTecnicaMaterialConsumoVariacao
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
            if (gridMaterialComposicaoCustoMatriz != null)
            {
                gridMaterialComposicaoCustoMatriz.ForEach(x =>
                {
                    if (!x.Id.HasValue || x.Id == 0)
                    {
                        NovoMaterialComposicaoCustoMatriz(domain, x);
                    }
                    else
                    {
                        var materialComposicaoCusto = domain.MateriaisComposicaoCusto.First(y => y.Id == x.Id);

                        materialComposicaoCusto.Custo = x.Custo.Value;
                    }
                });
            }
            
            var listaExcluir = new List<FichaTecnicaMaterialComposicaoCusto>();

            domain.MateriaisComposicaoCusto.ForEach(fichaTecnicaMaterialComposicaoCusto =>
            {
                if (gridMaterialComposicaoCustoMatriz == null || gridMaterialComposicaoCustoMatriz.All(x => x.Id != fichaTecnicaMaterialComposicaoCusto.Id && fichaTecnicaMaterialComposicaoCusto.Id != null))
                {
                    listaExcluir.Add(fichaTecnicaMaterialComposicaoCusto);
                }
            });

            foreach (var fichaTecnicaMaterialComposicaoCusto in listaExcluir)
            {
                domain.MateriaisComposicaoCusto.Remove(fichaTecnicaMaterialComposicaoCusto);
            }
        }

        private void NovoMaterialComposicaoCustoMatriz(FichaTecnicaJeans domain, GridMaterialComposicaoCustoMatrizModel gridMaterialComposicaoCustoMatrizModel)
        {
            domain.MateriaisComposicaoCusto.Add(new FichaTecnicaMaterialComposicaoCusto
            {
                Custo = gridMaterialComposicaoCustoMatrizModel.Custo.Value,
                Material = _materialRepository.Get(y => y.Referencia == gridMaterialComposicaoCustoMatrizModel.Referencia)
            });
        }
        
        #endregion

        #region Modelagem

        [PopulateViewData("PopulateViewDataModelagem")]
        public virtual ActionResult Modelagem(long id)
        {
            var domain = _fichaTecnicaJeansRepository.Get(id);

            if (domain != null)
            {
                if (domain.FichaTecnicaModelagem == null)
                {
                    var model = new FichaTecnicaModelagemModel
                    {
                        Id = domain.Id,
                        GridMedidas = new List<GridFichaTecnicaModelagemMedidaModel>(),
                    };
                    return View(model);
                }
                else
                {
                    var model = new FichaTecnicaModelagemModel
                    {
                        Id = domain.Id,
                        DataModelagem = domain.FichaTecnicaModelagem.DataModelagem,
                        Observacao = domain.FichaTecnicaModelagem.Observacao,
                        Descricao = domain.FichaTecnicaModelagem.Descricao,
                        Modelista = domain.FichaTecnicaModelagem.Modelista.Id,
                        NomeArquivoUpload =
                            domain.FichaTecnicaModelagem.Arquivo != null
                                ? domain.FichaTecnicaModelagem.Arquivo.Nome
                                : null,
                        Arquivo = domain.FichaTecnicaModelagem.Arquivo != null
                                ? domain.FichaTecnicaModelagem.Arquivo.Id
                                : null,
                        GridMedidas = new List<GridFichaTecnicaModelagemMedidaModel>()
                    };

                    domain.FichaTecnicaModelagem.Medidas.ForEach(
                        x => x.Itens.ForEach(y => model.GridMedidas.Add(new GridFichaTecnicaModelagemMedidaModel()
                        {
                            Id = 0,
                            DescricaoMedida = x.DescricaoMedida,
                            Medida = y.Medida,
                            Tamanho = y.Tamanho.Id.ToString()
                        })));

                    return View(model);
                }
            }

            this.AddErrorMessage("Não foi possível encontrar a ficha técnica.");

            return View(new FichaTecnicaModelagemModel());
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewDataModelagem")]
        public virtual ActionResult Modelagem(FichaTecnicaModelagemModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _fichaTecnicaJeansRepository.Get(model.Id);

                    if (domain.FichaTecnicaModelagem == null)
                    {
                        Arquivo arquivo = null;
                        if (!string.IsNullOrEmpty(model.NomeArquivoUpload))
                        {
                            arquivo = _arquivoRepository.Save(ArquivoController.SalvarArquivo(model.NomeArquivoUpload));
                        }

                        var fichaTecnicaModelagem = new FichaTecnicaModelagem();
                        fichaTecnicaModelagem.Modelista = _pessoaRepository.Load(model.Modelista);
                        fichaTecnicaModelagem.DataModelagem = model.DataModelagem.Value;
                        fichaTecnicaModelagem.Observacao = model.Observacao;
                        fichaTecnicaModelagem.Descricao = model.Descricao;
                        fichaTecnicaModelagem.Arquivo = arquivo;
                        domain.FichaTecnicaModelagem = fichaTecnicaModelagem;

                        AtualizeFichaTecnicaModelageMedida(model, domain);
                    }
                    else
                    {
                        domain.FichaTecnicaModelagem.Modelista = _pessoaRepository.Load(model.Modelista);
                        domain.FichaTecnicaModelagem.DataModelagem = model.DataModelagem.Value;
                        domain.FichaTecnicaModelagem.Observacao = model.Observacao;
                        domain.FichaTecnicaModelagem.Descricao = model.Descricao;
                        
                        if (!string.IsNullOrEmpty(model.NomeArquivoUpload))
                        {
                            var nomeArquivo = domain.FichaTecnicaModelagem.Arquivo != null ? domain.FichaTecnicaModelagem.Arquivo.Nome : "";
                            if (nomeArquivo != model.NomeArquivoUpload)
                            {
                                domain.FichaTecnicaModelagem.Arquivo =
                                    _arquivoRepository.Save(ArquivoController.SalvarArquivo(model.NomeArquivoUpload));
                            }
                        }

                        domain.FichaTecnicaModelagem.Medidas.Clear();
                        AtualizeFichaTecnicaModelageMedida(model, domain);
                    }

                    _fichaTecnicaJeansRepository.SaveOrUpdate(domain);

                    this.AddSuccessMessage("Dados da modelagem da ficha técnica salvos/atualizados com sucesso.");

                    return RedirectToAction("Editar", new { domain.Id });
                }
                catch (Exception exception)
                {
                    this.AddErrorMessage("Não é possível salvar/atualizar os dados da modelagem da ficha técnica. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { model.Id });
        }

        private void AtualizeFichaTecnicaModelageMedida(FichaTecnicaModelagemModel model, FichaTecnicaJeans domain)
        {
            if (model.GridMedidas == null)
                return;

            var grupoMedidas = model.GridMedidas.GroupBy(x => new { x.DescricaoMedida },
                (chave, grupo) => new { chave, grupo });

            grupoMedidas.ForEach(x =>
            {
                var fichaTecnicaModelgemMedida = new FichaTecnicaModelagemMedida()
                {
                    DescricaoMedida = x.chave.DescricaoMedida
                };

                x.grupo.ForEach(y =>
                {
                    var fichaTecnicaModelagemItem = new FichaTecnicaModelagemMedidaItem()
                    {
                        Medida = y.Medida,
                        Tamanho = _tamanhoRepository.Load(long.Parse(y.Tamanho))
                    };
                    fichaTecnicaModelgemMedida.Itens.Add(fichaTecnicaModelagemItem);
                });

                domain.FichaTecnicaModelagem.Medidas.Add(fichaTecnicaModelgemMedida);
            });
        }

        #endregion
        
        #region Fotos
        
        public virtual ActionResult Fotos(long id)
        {
            var domain = _fichaTecnicaJeansRepository.Get(id);

            var model = new FichaTecnicaFotosModel()
            {
                Id = id,
                GridFotos = new List<GridFichaTecnicaFotosModel>()
            };

            if (domain != null)
            {
                domain.FichaTecnicaFotos.ForEach(domainFoto =>
                {
                    var modelFoto = new GridFichaTecnicaFotosModel()
                    {
                        Id = domainFoto.Id,
                        FotoNome = domainFoto.Arquivo.Nome,
                        FotoTitulo = domainFoto.Descricao,
                        Padrao = domainFoto.Padrao,
                        Impressao = domainFoto.Impressao,
                    };
                    model.GridFotos.Add(modelFoto);
                });
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Fotos(FichaTecnicaFotosModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _fichaTecnicaJeansRepository.Get(model.Id);

                    if (model.GridFotos == null)
                    {
                        model.GridFotos = new List<GridFichaTecnicaFotosModel>();
                    }

                    model.GridFotos.ForEach(modelFoto =>
                    {
                        if (!modelFoto.Id.HasValue || modelFoto.Id.Value == 0)
                        {
                            var arquivo = _arquivoRepository.Save(ArquivoController.SalvarArquivo(modelFoto.FotoNome));
                            
                            var fichaTecnicaFoto = new FichaTecnicaFoto
                            {
                                Descricao = modelFoto.FotoTitulo,
                                Arquivo = arquivo,
                                Padrao = modelFoto.Padrao,
                                Impressao = modelFoto.Impressao
                            };
                            domain.FichaTecnicaFotos.Add(fichaTecnicaFoto);    
                        }
                    });
                    
                    var fichaTecnicaFotosExcluir = new List<FichaTecnicaFoto>();

                    domain.FichaTecnicaFotos.ForEach(domainFoto =>
                    {
                        if (model.GridFotos.All(x => x.Id != domainFoto.Id && domainFoto.Id != null))
                        {
                            fichaTecnicaFotosExcluir.Add(domainFoto);
                        }
                    });

                    fichaTecnicaFotosExcluir.ForEach(domainFoto => domain.FichaTecnicaFotos.Remove(domainFoto));

                    _fichaTecnicaJeansRepository.SaveOrUpdate(domain);

                    this.AddSuccessMessage("Dados das fotos da ficha técnica salvos/atualizados com sucesso.");

                    return RedirectToAction("Editar", new { domain.Id });
                }
                catch (Exception exception)
                {
                    this.AddErrorMessage("Não é possível salvar/atualizar os dados das fotos da ficha técnica. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { model.Id });
        }
        
        #endregion

        #region Editar

        [PopulateViewData("PopulateViewDataBasicos")]
        public virtual ActionResult Editar(long id)
        {
            //if (!fichaTecnicaId.HasValue)
            //{
            //    return View(new FichaTecnicaBasicosModel());
            //}

            var domain = _fichaTecnicaJeansRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<FichaTecnicaBasicosModel>(domain);

                model.Grade = domain.FichaTecnicaMatriz.Grade.Id;
                model.GridFichaTecnicaVariacao = new List<GridFichaTecnicaVariacaoModel>();
                model.Funcionario = domain.Estilista.Id.GetValueOrDefault();

                domain.FichaTecnicaMatriz.FichaTecnicaVariacaoMatrizs.ForEach(x => x.Cores.ForEach(y => model.GridFichaTecnicaVariacao.Add(new GridFichaTecnicaVariacaoModel()
                {
                    Cor = y.Id.ToString(),
                    Variacao = x.Variacao.Id.ToString()
                })));

                model.Catalogo = domain.Catalogo.HasValue ? domain.Catalogo : false;

                return View("Basicos", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a ficha técnica.");

            return View("Basicos", new FichaTecnicaBasicosModel());
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

        #region Copiar
        public virtual ActionResult Copiar(long id)
        {
            var domain = _fichaTecnicaJeansRepository.Get(id);

            if (domain != null)
            {
                var model = new CopiarFichaTecnicaModel
                {
                    ReferenciaOrigem = domain.Referencia,
                    DescricaoOrigem = domain.Descricao,
                    Observacao = "CÓPIA DA FICHA TÉCNICA DE REF." + domain.Referencia
                };

                return View("Copiar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a ficha técnica.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Copiar(CopiarFichaTecnicaModel model)
        {
            // Valida referência
            if (_fichaTecnicaJeansRepository.Find(p => p.Referencia == model.ReferenciaNova).Any())
                ModelState.AddModelError("Nome", "Já existe uma ficha técnica cadastrada com esta referência.");

            if (ModelState.IsValid)
            {
                try
                {
                    var domain =
                        _fichaTecnicaJeansRepository.Find(m => m.Referencia == model.ReferenciaOrigem).FirstOrDefault();

                    if (domain != null)
                    {
                        var fichaTecnica = new FichaTecnicaJeans
                        {
                            Referencia = model.ReferenciaNova,
                            Observacao = model.Observacao,
                            Tag = domain.Tag,
                            Ano = domain.Ano,
                            Artigo = domain.Artigo,
                            Descricao = domain.Descricao,
                            DataCadastro = DateTime.Now,
                            Catalogo = domain.Catalogo,
                            Classificacao = domain.Classificacao,
                            ClassificacaoDificuldade = domain.ClassificacaoDificuldade,
                            Colecao = domain.Colecao,
                            Marca = domain.Marca,
                            Natureza = domain.Natureza,
                            Complemento = domain.Complemento,
                            Detalhamento = domain.Detalhamento,
                            Segmento = domain.Segmento,
                            Estilista = domain.Estilista,
                            Comprimento = domain.Comprimento,
                            MedidaComprimento = domain.MedidaComprimento,
                            Barra = domain.Barra,
                            MedidaBarra = domain.MedidaBarra,
                            ProdutoBase = domain.ProdutoBase,
                            MedidaCos = domain.MedidaCos,
                            Lavada = domain.Lavada,
                            Bordado = domain.Bordado,
                            MedidaPassante = domain.MedidaPassante,
                            Pedraria = domain.Pedraria,
                            Pesponto = domain.Pesponto,
                            Silk = domain.Silk,
                            
                            FichaTecnicaMatriz = new FichaTecnicaMatriz()
                            {
                                Grade = domain.FichaTecnicaMatriz.Grade
                            }
                        };

                        domain.FichaTecnicaMatriz.FichaTecnicaVariacaoMatrizs.ForEach(domainVariacaoMatriz =>
                        {
                            var fichaTecnicaVariacaoMatriz = new FichaTecnicaVariacaoMatriz()
                            {
                                Variacao = domainVariacaoMatriz.Variacao
                            };

                            domainVariacaoMatriz.Cores.ForEach(cor => fichaTecnicaVariacaoMatriz.AddCor(cor));

                            fichaTecnica.FichaTecnicaMatriz.FichaTecnicaVariacaoMatrizs.Add(fichaTecnicaVariacaoMatriz);
                        });

                        domain.MateriaisConsumo.ForEach(
                            materialConsumo => fichaTecnica.MateriaisConsumo.Add(new FichaTecnicaMaterialConsumo()
                            {
                                Quantidade = materialConsumo.Quantidade,
                                DepartamentoProducao = materialConsumo.DepartamentoProducao,
                                Material = materialConsumo.Material,
                                Custo = materialConsumo.Custo
                            }));

                        domain.MateriaisConsumoVariacao.ForEach(
                            materialConsumoVariacao =>
                                fichaTecnica.MateriaisConsumoVariacao.Add(new FichaTecnicaMaterialConsumoVariacao()
                                {
                                    Quantidade = materialConsumoVariacao.Quantidade,
                                    DepartamentoProducao = materialConsumoVariacao.DepartamentoProducao,
                                    Material = materialConsumoVariacao.Material,
                                    Custo = materialConsumoVariacao.Custo,
                                    CompoeCusto = materialConsumoVariacao.CompoeCusto,
                                    Tamanho = materialConsumoVariacao.Tamanho,
                                    FichaTecnicaVariacaoMatriz =
                                        domain.FichaTecnicaMatriz.FichaTecnicaVariacaoMatrizs.First(
                                            x =>
                                                x.Variacao.Id ==
                                                materialConsumoVariacao.FichaTecnicaVariacaoMatriz.Variacao.Id)
                                }));

                        domain.MateriaisComposicaoCusto.ForEach(
                            materialComposicaoCusto =>
                                fichaTecnica.MateriaisComposicaoCusto.Add(new FichaTecnicaMaterialComposicaoCusto()
                                {
                                    Material = materialComposicaoCusto.Material,
                                    Custo = materialComposicaoCusto.Custo
                                }));

                        if (domain.FichaTecnicaModelagem != null)
                        {
                            fichaTecnica.FichaTecnicaModelagem = new FichaTecnicaModelagem()
                            {
                                DataModelagem = domain.FichaTecnicaModelagem.DataModelagem,
                                Modelista = domain.FichaTecnicaModelagem.Modelista,
                                Descricao = domain.FichaTecnicaModelagem.Descricao,
                                Observacao = domain.FichaTecnicaModelagem.Observacao
                            };

                            if (domain.FichaTecnicaModelagem.Arquivo != null)
                            {
                                var filePathAtual = domain.FichaTecnicaModelagem.Arquivo.Nome.GetFilePath();
                                if (System.IO.File.Exists(filePathAtual))
                                {
                                    fichaTecnica.FichaTecnicaModelagem.Arquivo =
                                        CopieArquivo(domain.FichaTecnicaModelagem.Arquivo);
                                }    
                            }

                            domain.FichaTecnicaModelagem.Medidas.ForEach(domainMedida =>
                            {
                                var medida = new FichaTecnicaModelagemMedida()
                                {
                                    DescricaoMedida = domainMedida.DescricaoMedida
                                };

                                domainMedida.Itens.ForEach(domainMedidaItem => medida.Itens.Add(new FichaTecnicaModelagemMedidaItem()
                                {
                                    Medida = domainMedidaItem.Medida,
                                    Tamanho = domainMedidaItem.Tamanho
                                }));

                                fichaTecnica.FichaTecnicaModelagem.Medidas.Add(medida);
                            });
                        }

                        domain.FichaTecnicaFotos.ForEach(fichaTecnicaFoto =>
                        {
                            var filePathAtual = fichaTecnicaFoto.Arquivo.Nome.GetFilePath();
                            if (System.IO.File.Exists(filePathAtual))
                            {
                                fichaTecnica.FichaTecnicaFotos.Add(new FichaTecnicaFoto()
                                {
                                    Arquivo = CopieArquivo(fichaTecnicaFoto.Arquivo),
                                    Descricao = fichaTecnicaFoto.Descricao,
                                    Impressao = fichaTecnicaFoto.Impressao,
                                    Padrao = fichaTecnicaFoto.Padrao
                                });
                            }
                        });

                        domain.FichaTecnicaSequenciaOperacionals.ForEach(domainSequenciaOperacional => 
                            fichaTecnica.FichaTecnicaSequenciaOperacionals.Add(new FichaTecnicaSequenciaOperacional()
                        {
                            Custo = domainSequenciaOperacional.Custo,
                            DepartamentoProducao = domainSequenciaOperacional.DepartamentoProducao,
                            OperacaoProducao = domainSequenciaOperacional.OperacaoProducao,
                            PesoProdutividade = domainSequenciaOperacional.PesoProdutividade,
                            SetorProducao = domainSequenciaOperacional.SetorProducao,
                            Tempo = domainSequenciaOperacional.Tempo
                        }));

                        _fichaTecnicaJeansRepository.SaveOrUpdate(fichaTecnica);
                        Framework.UnitOfWork.Session.Current.Flush();

                        this.AddSuccessMessage("Ficha técnica copiada com sucesso");
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Ocorreu um erro ao tentar criar a ficha técnica " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            
            }
            return View(model);
        }

        #endregion

        #region PopulateViewData

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
            ViewBag.VariacaosDicionarioJson_Material = variacaos.ToDictionary(k => k.Id.ToString(), e => e.Nome).FromDictionaryToJson();

            var dicionarioTamanhos = fichaTecnica.FichaTecnicaMatriz.Grade.Tamanhos;
            var tamanhoLista = dicionarioTamanhos.Keys;

            ViewBag.TamanhoGrades = tamanhoLista.Select(s => new { Id = s.Id.ToString(), s.Descricao }).OrderBy(x => x.Descricao);
            ViewBag.TamanhosDicionarioJson_Material = tamanhoLista.ToDictionary(k => k.Id.ToString(), e => e.Descricao).FromDictionaryToJson();
        }

        protected void PopulateViewDataModelagem(FichaTecnicaModelagemModel model)
        {
            if (!model.Id.HasValue)
            {
                return;
            }

            var fichaTecnica = _fichaTecnicaJeansRepository.Get(model.Id);

            var dicionarioTamanhos = fichaTecnica.FichaTecnicaMatriz.Grade.Tamanhos;
            var tamanhoLista = dicionarioTamanhos.Keys;

            ViewBag.TamanhoGrades = tamanhoLista.Select(s => new { Id = s.Id.ToString(), s.Descricao }).OrderBy(x => x.Descricao);
            ViewBag.TamanhosDicionarioJson_Modelagem = tamanhoLista.ToDictionary(k => k.Id.ToString(), e => e.Descricao).FromDictionaryToJson();
        }

        protected void PopulateViewDataPesquisa(PesquisaFichaTecnicaModel model)
        {
            var estilistas = _pessoaRepository.Find(p => p.Funcionario != null
                && p.Funcionario.FuncaoFuncionario == FuncaoFuncionario.Estilista)
                .OrderBy(p => p.Nome).ToList();
            ViewData["Estilista"] = estilistas.ToSelectList("Nome", model.Estilista);
            
            var classificacaoDificuldades = _classificacaoDificuldadeRepository.Find(p => p.Ativo).ToList();
            ViewBag.ClassificacaoDificuldade = classificacaoDificuldades.ToSelectList("Descricao", model.ClassificacaoDificuldade);

            var colecoes = _colecaoRepository.Find(p => p.Ativo).ToList();
            ViewBag.Colecao = colecoes.ToSelectList("Descricao", model.Colecao);

            ViewBag.OrdenarPor = new SelectList(ColunasAgrupamentoOrdenacao, "value", "key");
            ViewBag.AgruparPor = new SelectList(ColunasAgrupamentoOrdenacao, "value", "key");
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

        #region Componente Pesquisar

        #region PesquisarVarios
        [ChildActionOnly]//OutputCache(Duration = 3600)
        public virtual ActionResult PesquisarVarios()
        {
            PreencheColuna();
            return PartialView();
        }
        #endregion

        [ChildActionOnly]
        public virtual ActionResult Pesquisar()
        {
            PreencheColuna();
            return PartialView();
        }
        
        private void PreencheColuna()
        {
            var coluna = new[]
                              {
                                  new { value = "Descricao", text = "Descrição"},
                                  new { value = "Referencia", text = "Referência"},
                                  new { value = "Tag", text = "Tag"}
                              };
            ViewData["ColunaPesquisa"] = new SelectList(coluna, "value", "text");
        }
        
        [HttpPost, AjaxOnly]
        public virtual ActionResult PesquisarFiltro(PesquisarModel model)
        {
            var filters = new List<FilterExpression>
            {
                // Ativo
                //new FilterExpression("Ativo", ComparisonOperator.IsEqual, true, LogicOperator.And),
                // Filtro da tela
                model.Filtrar<FichaTecnica>()
            };

            var fichasTecnicas = _fichaTecnicaRepository.Find(filters.ToArray()).ToList().OrderBy(o => o.Descricao);

            var list = fichasTecnicas.Select(p => new GridFichaTecnicaModel()
            {
                Id = p.Id.GetValueOrDefault(),
                Referencia = p.Referencia,
                Tag = p.Tag,
                Ano = p.Ano,
                Descricao = p.Descricao,
                Classificacao = p.ClassificacaoDificuldade.Descricao,
                Catalogo = p.Catalogo.HasValue && p.Catalogo.Value ? "Sim" : "Não",
                Foto = ObtenhaUrlFoto(p),
                Estilista = p.Estilista.Nome
            }).ToList();
            return Json(list);
        }
        
        [HttpGet, AjaxOnly]
        public virtual ActionResult PesquisarReferencia(string referencia)
        {
            if (!string.IsNullOrWhiteSpace(referencia))
            {
                var filters = new List<FilterExpression>
                {
                    //new FilterExpression("Ativo", ComparisonOperator.IsEqual, true, LogicOperator.And),
                    new FilterExpression("Referencia", ComparisonOperator.IsEqual, referencia, LogicOperator.And),
                };

                var fichaTecnica = _fichaTecnicaRepository.Find(filters.ToArray()).FirstOrDefault();

                if (fichaTecnica != null)
                    return Json(new
                    {
                        fichaTecnica.Id,
                        fichaTecnica.Referencia,
                        fichaTecnica.Ano,
                        fichaTecnica.Tag,
                        fichaTecnica.Descricao,
                        Classificacao = fichaTecnica.ClassificacaoDificuldade.Descricao,
                        Catalogo = fichaTecnica.Catalogo.HasValue && fichaTecnica.Catalogo.Value ? "Sim" : "Não"
                    }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { erro = "Nenhuma ficha técnica encontrada." }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet, AjaxOnly]
        public virtual ActionResult PesquisarId(long id)
        {
            var fichaTecnica = _fichaTecnicaRepository.Get(id);

            if (fichaTecnica != null)
                return Json(new
                {
                    fichaTecnica.Id,
                    fichaTecnica.Referencia,
                    fichaTecnica.Ano,
                    fichaTecnica.Tag,
                    fichaTecnica.Descricao,
                    Classificacao = fichaTecnica.ClassificacaoDificuldade.Descricao,
                    Catalogo = fichaTecnica.Catalogo.HasValue && fichaTecnica.Catalogo.Value ? "Sim" : "Não"
                }, JsonRequestBehavior.AllowGet);

            return Json(new { erro = "Nenhuma ficha técnica encontrada." }, JsonRequestBehavior.AllowGet);
        }

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
                Nome = Path.GetFileName(filename),
                Titulo = arquivoAtual.Titulo,
                Data = fileInfo.CreationTime,
                Extensao = fileInfo.Extension,
                Tamanho = fileInfo.Length
            };

            return arquivo;
        }

        [HttpGet, AjaxOnly]
        public virtual JsonResult ObtenhaGridMaterialConsumoMatriz(long? fichaTecnicaId)
        {
            try
            {
                var domain = _fichaTecnicaJeansRepository.Get(fichaTecnicaId);

                var gridMaterialConsumoMatriz = new List<GridMaterialConsumoMatrizModel>();

                domain.MateriaisConsumo.ForEach(x =>
                {
                    var ultimoCusto = x.Material.ObtenhaUltimoCusto();
                    var custoUnitario = ultimoCusto == 0 ? x.Custo : ultimoCusto;

                    gridMaterialConsumoMatriz.Add(new GridMaterialConsumoMatrizModel
                    {
                        Id = x.Id,
                        Custo = custoUnitario,
                        Descricao = x.Material.Descricao,
                        Referencia = x.Material.Referencia,
                        UnidadeMedida = x.Material.UnidadeMedida.Sigla,
                        DepartamentoProducao = x.DepartamentoProducao.Id.ToString(),
                        DepartamentoProducaoNome = x.DepartamentoProducao.Nome,
                        Quantidade = x.Quantidade,
                        CustoTotal = x.Quantidade * custoUnitario,
                        Foto = (x.Material.Foto != null ? x.Material.Foto.Nome.GetFileUrl() : string.Empty),
                    });
                });
                gridMaterialConsumoMatriz = gridMaterialConsumoMatriz.OrderBy(x => x.DepartamentoProducaoNome).ThenBy(x => x.Descricao).ToList();

                return new JsonResult { Data = gridMaterialConsumoMatriz, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception exception)
            {
                _logger.Error(exception.GetMessage());
                return Json(new { Error = "Ocorreu um erro: " + exception.GetMessage() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet, AjaxOnly]
        public virtual JsonResult ObtenhaGridMaterialConsumoVariacao(long? fichaTecnicaId)
        {
            try
            {
                var domain = _fichaTecnicaJeansRepository.Get(fichaTecnicaId);

                var gridMaterialConsumoVariacao = new List<GridMaterialConsumoItemModel>();

                domain.MateriaisConsumoVariacao.ForEach(x =>
                {
                    var ultimoCusto = x.Material.ObtenhaUltimoCusto();
                    var custoUnitario = ultimoCusto == 0 ? x.Custo : ultimoCusto;

                    gridMaterialConsumoVariacao.Add(new GridMaterialConsumoItemModel
                    {
                        Id = x.Id,
                        Custo = custoUnitario,
                        Descricao = x.Material.Descricao,
                        Referencia = x.Material.Referencia,
                        UnidadeMedida = x.Material.UnidadeMedida.Sigla,
                        DepartamentoProducao = x.DepartamentoProducao.Id.ToString(),
                        DepartamentoProducaoNome = x.DepartamentoProducao.Nome,
                        Quantidade = x.Quantidade,
                        CompoeCusto = x.CompoeCusto,
                        Tamanho = x.Tamanho.Id.ToString(),
                        Variacao = x.FichaTecnicaVariacaoMatriz.Variacao.Id.ToString(),
                        CustoTotal = x.Quantidade * custoUnitario,
                        Foto = (x.Material.Foto != null ? x.Material.Foto.Nome.GetFileUrl() : string.Empty)
                    });
                });
                gridMaterialConsumoVariacao = gridMaterialConsumoVariacao.OrderBy(x => x.DepartamentoProducaoNome).ThenBy(x => x.Descricao).ToList();

                return new JsonResult { Data = gridMaterialConsumoVariacao, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception exception)
            {
                _logger.Error(exception.GetMessage());
                return Json(new { Error = "Ocorreu um erro: " + exception.GetMessage() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}