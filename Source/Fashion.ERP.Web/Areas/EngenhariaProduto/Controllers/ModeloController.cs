using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Reporting.EngenhariaProduto;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.EngenhariaProduto.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using NHibernate.Linq;
using Ninject.Extensions.Logging;
using Telerik.Reporting;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers
{
    public partial class ModeloController : BaseController
    {
        #region Variaveis
        private readonly IRepository<Modelo> _modeloRepository;
        private readonly IRepository<Grade> _gradeRepository;
        private readonly IRepository<Colecao> _colecaoRepository;
        private readonly IRepository<Classificacao> _classificacaoRepository;
        //private readonly IRepository<FichaTecnica> _fichaTecnicaRepository;
        private readonly IRepository<Segmento> _segmentoRepository;
        private readonly IRepository<Natureza> _naturezaRepository;
        private readonly IRepository<Barra> _barraRepository;
        private readonly IRepository<Comprimento> _comprimentoRepository;
        private readonly IRepository<ProdutoBase> _produtoBaseRepository;
        private readonly IRepository<Artigo> _artigoRepository;
        private readonly IRepository<Marca> _marcaRepository;
        private readonly IRepository<Arquivo> _arquivoRepository;
        private readonly IRepository<Cor> _corRepository;
        private readonly IRepository<DepartamentoProducao> _departamentoProducaoRepository;
        private readonly IRepository<SequenciaProducao> _sequenciaProducaoRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<Tamanho> _tamanhoRepository;
        private readonly IRepository<UnidadeMedida> _unidadeMedidaRepository;
        private readonly IRepository<Variacao> _variacaoRepository;
        private readonly IRepository<SetorProducao> _setorProducaoRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<ProgramacaoBordado> _programacaoBordadoRepository;
        private readonly ILogger _logger;
        private readonly string[] _tipoRelatorio = { "Detalhado", "Listagem", "Sintético" };
        #region ColunasPesquisaModelo
        private static readonly Dictionary<string, string> ColunasPesquisaModelo = new Dictionary<string, string>
        {
            {"Artigo", "Artigo.Descricao"},
            {"Barra", "Barra.Descricao"},
            {"Classificação", "Classificacao.Descricao"},
            {"Coleção", "Colecao.Descricao"},
            {"Complemento", "Complemento"},
            {"Comprimento", "Comprimento.Descricao"},
            {"Descrição", "Descricao"},
            {"Detalhamento", "Detalhamento"},
            {"Estilista", "Estilista.Nome"},
            {"Grade", "Grade.Descricao"},
            {"Lavada", "Lavada"},
            {"Linha casa", "LinhaCasa"},
            {"Marca", "Marca.Nome"},
            {"Modelista", "Modelista.Nome"},
            {"Natureza", "Natureza.Descricao"},
            {"Produto base", "ProdutoBase.Descricao"},
            {"Referência", "Referencia"},
            {"Segmento", "Segmento.Descricao"},
        };
        #endregion
        public const string ChaveFoto = "2ACB63F2-2329-4aaf-8AD4-23DA40759E38";
        public const string ChaveLinhaBordado = "AA85B860-CF00-4f58-AC8F-4C6A433D22E2";
        public const string ChaveLinhaPesponto = "276C37CF-0122-4171-81B7-BC7704128BCD";
        public const string ChaveLinhaTravete = "43736768-57E8-42d9-98E2-F3D4CC322C93";
        #endregion

        #region Construtores
        public ModeloController(ILogger logger, IRepository<Modelo> modeloRepository, IRepository<Grade> gradeRepository,
            IRepository<Colecao> colecaoRepository, IRepository<Classificacao> classificacaoRepository, IRepository<Segmento> segmentoRepository,
            IRepository<Natureza> naturezaRepository, IRepository<Barra> barraRepository, IRepository<Comprimento> comprimentoRepository,
            IRepository<ProdutoBase> produtoBaseRepository, IRepository<Artigo> artigoRepository, IRepository<Marca> marcaRepository,
            IRepository<Arquivo> arquivoRepository, IRepository<Cor> corRepository, IRepository<DepartamentoProducao> departamentoProducaoRepository,
            IRepository<Pessoa> pessoaRepository, IRepository<Tamanho> tamanhoRepository, IRepository<UnidadeMedida> unidadeMedidaRepository,
            IRepository<Variacao> variacaoRepository, IRepository<SetorProducao> setorProducaoRepository,
            IRepository<Material> materialRepository, 
            IRepository<ProgramacaoBordado> programacaoBordadoRepository, //IRepository<FichaTecnica> fichaTecnicaRepository,
            IRepository<SequenciaProducao> sequenciaRepository)
        {
            _modeloRepository = modeloRepository;
            _gradeRepository = gradeRepository;
            _colecaoRepository = colecaoRepository;
            _classificacaoRepository = classificacaoRepository;
            _segmentoRepository = segmentoRepository;
            _naturezaRepository = naturezaRepository;
            _barraRepository = barraRepository;
            _comprimentoRepository = comprimentoRepository;
            _produtoBaseRepository = produtoBaseRepository;
            _artigoRepository = artigoRepository;
            _marcaRepository = marcaRepository;
            _arquivoRepository = arquivoRepository;
            _corRepository = corRepository;
            _departamentoProducaoRepository = departamentoProducaoRepository;
            _pessoaRepository = pessoaRepository;
            _tamanhoRepository = tamanhoRepository;
            _unidadeMedidaRepository = unidadeMedidaRepository;
            _variacaoRepository = variacaoRepository;
            _setorProducaoRepository = setorProducaoRepository;
            _materialRepository = materialRepository;
            _programacaoBordadoRepository = programacaoBordadoRepository;
            //_fichaTecnicaRepository = fichaTecnicaRepository;
            _sequenciaProducaoRepository = sequenciaRepository;
            _logger = logger;
        }
        #endregion
        
        #region View

        #region Index
        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Index()
        {
            var modelos = _modeloRepository.Find().OrderByDescending(m => m.DataAlteracao).Take(20);

            var model = new PesquisaModeloModel { ModoConsulta = "Listar" };

            model.Grid = modelos.Select(p => new GridModeloModel
            {
                Id = p.Id.GetValueOrDefault(),
                Referencia = p.Referencia,
                Descricao = p.Descricao,
                Colecao = p.Colecao.Descricao,
                Estilista = p.Estilista.Nome,
                Grade = p.Grade.Descricao,
                Marca = p.Marca.Nome,
                Segmento = p.Segmento != null ? p.Segmento.Descricao : null,
               
            }).ToList();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Index(PesquisaModeloModel model)
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

                if (model.Modelista.HasValue)
                {
                    modelos = modelos.Where(p => p.Modelista.Id == model.Modelista);
                    filtros.AppendFormat("Modelista: {0}, ", _pessoaRepository.Get(model.Modelista.Value).Nome);
                }

                if (model.Grade.HasValue)
                {
                    modelos = modelos.Where(p => p.Grade.Id == model.Grade);
                    filtros.AppendFormat("Grade: {0}, ", _gradeRepository.Get(model.Grade.Value).Descricao);
                }

                if (model.Marca.HasValue)
                {
                    modelos = modelos.Where(p => p.Marca.Id == model.Marca);
                    filtros.AppendFormat("Marca: {0}, ", _marcaRepository.Get(model.Marca.Value).Nome);
                }


                if (model.Segmento.HasValue)
                {
                    modelos = modelos.Where(p => p.Segmento.Id == model.Segmento);
                    filtros.AppendFormat("Segmento: {0}, ", _segmentoRepository.Get(model.Segmento.Value).Descricao);
                }


                if (!string.IsNullOrWhiteSpace(model.ReferenciaMaterial))
                {

                    modelos = modelos.Where(p => 
                    p.MateriaisConsumo.Any(material =>
                    material.Material.Referencia == model.ReferenciaMaterial));

                    filtros.AppendFormat("ReferenciaMaterial: {0}, ", model.ReferenciaMaterial);
                }
               

                if (model.Natureza.HasValue)
                {
                    modelos = modelos.Where(p => p.Natureza.Id == model.Natureza);
                    filtros.AppendFormat("Natureza: {0}, ", _naturezaRepository.Get(model.Natureza.Value).Descricao);
                }

                if (model.Classificacao.HasValue)
                {
                    modelos = modelos.Where(p => p.Classificacao.Id == model.Classificacao);
                    filtros.AppendFormat("Classificação: {0}, ", _classificacaoRepository.Get(model.Classificacao.Value).Descricao);
                }

                //todo agora
                //if (!string.IsNullOrWhiteSpace(model.Tag))
                //{
                //    modelos = modelos.Where(p => p.ModeloAprovado!= null && p.ModeloAprovado.Tag.Contains(model.Tag));
                //    filtros.AppendFormat("Tag: {0}, ", model.Tag);
                //}

                if (model.Artigo.HasValue)
                {
                    modelos = modelos.Where(p => p.Artigo.Id == model.Artigo);
                    filtros.AppendFormat("Artigo: {0}, ", _artigoRepository.Get(model.Artigo.Value).Descricao);
                }

                if (model.ProdutoBase.HasValue)
                {
                    modelos = modelos.Where(p => p.ProdutoBase.Id == model.ProdutoBase);
                    filtros.AppendFormat("ProdutoBase: {0}, ", _produtoBaseRepository.Get(model.ProdutoBase.Value).Descricao);
                }

                if (model.Comprimento.HasValue)
                {
                    modelos = modelos.Where(p => p.Comprimento.Id == model.Comprimento);
                    filtros.AppendFormat("Comprimento: {0}, ", _comprimentoRepository.Get(model.Comprimento.Value).Descricao);
                }

                if (model.Barra.HasValue)
                {
                    modelos = modelos.Where(p => p.Barra.Id == model.Barra);
                    filtros.AppendFormat("Barra: {0}, ", _barraRepository.Get(model.Barra.Value).Descricao);
                }
                #endregion

                // Verifica se é uma listagem
                if (model.ModoConsulta == "Listar")
                {
                    if (model.OrdenarPor != null)
                        modelos = model.OrdenarEm == "asc"
                            ? modelos.OrderBy(model.OrdenarPor)
                            : modelos.OrderByDescending(model.OrdenarPor);
                    else
                        modelos = modelos.OrderByDescending(m => m.DataAlteracao);

                    model.Grid = modelos.Select(p => new GridModeloModel
                    {
                        Id = p.Id.GetValueOrDefault(),
                        Referencia = p.Referencia,
                        Descricao = p.Descricao,
                        Colecao = p.Colecao.Descricao,
                        Estilista = p.Estilista.Nome,
                        Grade = p.Grade.Descricao,
                        Marca = p.Marca.Nome,
                        Segmento = p.Segmento != null ? p.Segmento.Descricao : null,
                        
                    }).ToList();

                    return View(model);
                }

                // Se não é uma listagem, gerar o relatório
                var result = modelos
                    .Fetch(p => p.Colecao).Fetch(p => p.Estilista).Fetch(p => p.Modelista).Fetch(p => p.Grade).Fetch(p => p.Marca).Fetch(p => p.Segmento)
                    .ToList();

                if (!result.Any())
                    return Json(new { Error = "Nenhum item encontrado." });

                #region Montar Relatório
                //Report report = null;

                var report = new ListaModeloReport { DataSource = result };

                if (filtros.Length > 2)
                    report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

                var grupo = report.Groups.First(p => p.Name.Equals("Grupo"));

                if (model.AgruparPor != null)
                {
                    grupo.Groupings.Add("=Fields." + model.AgruparPor);

                    var key = ColunasPesquisaModelo.First(p => p.Value == model.AgruparPor).Key;
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

        #endregion

        #region Novo

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            TempData[ChaveLinhaBordado] = new List<string>();
            TempData[ChaveLinhaPesponto] = new List<string>();
            TempData[ChaveLinhaTravete] = new List<string>();

            return View(new ModeloModel());
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(ModeloModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<Modelo>(model);
                    var dataAgora = DateTime.Now;
                    domain.DataCriacao = dataAgora;
                    domain.DataAlteracao = dataAgora;

                    // Fotos
                    foreach (var foto in Fotos)
                        domain.AddFoto(new ModeloFoto
                        {
                            Foto = _arquivoRepository.Save(ArquivoController.SalvarArquivo(foto.FotoNome, foto.FotoTitulo)),
                            Impressao = foto.Impressao,
                            Padrao = foto.Padrao
                        });

                    // LinhaBordados
                    if (LinhaBordados.Any())
                        domain.AddLinhaBordado(LinhaBordados.ToArray());

                    // LinhaPespontos
                    if (LinhaPespontos.Any())
                        domain.AddLinhaPesponto(LinhaPespontos.ToArray());

                    // LinhaTravetes
                    if (LinhaTravetes.Any())
                        domain.AddLinhaTravete(LinhaTravetes.ToArray());

                    domain.GereChaveExterna();
                    _modeloRepository.Save(domain);

                    this.AddSuccessMessage("Modelo cadastrado com sucesso.");
                    return RedirectToAction("Detalhar", new { modeloId = domain.Id });
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o modelo. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }
        #endregion

        #region Editar

        [ImportModelStateFromTempData, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(long modeloId)
        {
            var domain = _modeloRepository.Get(modeloId);

            if (domain != null)
            {
                var model = Mapper.Flat<ModeloModel>(domain);

                Fotos = ViewBag.Fotos = domain.Fotos.Select(p => new GridModeloFotoModel
                {
                    Id = p.Id ?? 0,
                    FotoNome = p.Foto.Nome,
                    FotoTitulo = p.Foto.Titulo,
                    Impressao = p.Impressao,
                    Padrao = p.Padrao
                }).ToList();

                TempData[ChaveLinhaBordado] = ViewBag.LinhaBordados = domain.LinhasBordado.ToList();

                TempData[ChaveLinhaPesponto] = ViewBag.LinhaPespontos = domain.LinhasPesponto.ToList();

                TempData[ChaveLinhaTravete] = ViewBag.LinhaTravetes = domain.LinhasTravete.ToList();

                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o modelo.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(ModeloModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _modeloRepository.Get(model.Id));

                    // Fotos
                    var fotos = domain.Fotos.Where(p => Fotos.Any(q => p.Id == q.Id) == false).ToList();
                    foreach (var foto in fotos)
                        domain.RemoveFoto(foto);
                    foreach (var foto in Fotos.Where(foto => foto.Id == 0))
                        domain.AddFoto(new ModeloFoto
                        {
                            Foto = _arquivoRepository.Save(ArquivoController.SalvarArquivo(foto.FotoNome, foto.FotoTitulo)),
                            Impressao = foto.Impressao,
                            Padrao = foto.Padrao
                        });

                    // LinhaBordados
                    domain.ClearLinhaBordado();
                    if (LinhaBordados.Any())
                        domain.AddLinhaBordado(LinhaBordados.ToArray());

                    // LinhaPespontos
                    domain.ClearLinhaPesponto();
                    if (LinhaPespontos.Any())
                        domain.AddLinhaPesponto(LinhaPespontos.ToArray());

                    // LinhaTravetes
                    domain.ClearLinhaTravete();
                    if (LinhaTravetes.Any())
                        domain.AddLinhaTravete(LinhaTravetes.ToArray());

                    domain.DataAlteracao = DateTime.Now;
                    _modeloRepository.Update(domain);

                    this.AddSuccessMessage("Modelo atualizado com sucesso.");
                    return RedirectToAction("Detalhar", new { modeloId = model.Id });
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o modelo. Confira se os dados foram informados corretamente: " + exception.Message);
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
                    var domain = _modeloRepository.Get(id);

                    //// Excluir MaterialComposicao
                    //foreach (var sequencia in domain.SequenciaProducoes)
                    //    foreach (var composicao in sequencia.MaterialComposicaoModelos)
                    //        _materialComposicaoModeloRepository.Delete(composicao);

                    _modeloRepository.Delete(domain);

                    this.AddSuccessMessage("Modelo excluído com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir o modelo: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { id });
        }
        #endregion

        #region Detalhar
        public virtual ActionResult Detalhar(long modeloId)
        {
            try
            {
                var domain = _modeloRepository.Get(modeloId);
                var model = new DetalharModeloModel
                {
                    Id = domain.Id ?? 0,
                    Referencia = domain.Referencia,
                    Colecao = domain.Colecao.Descricao,
                    Estilista = domain.Estilista.Nome,
                    Descricao = domain.Descricao,
                    DataCriacao = domain.DataCriacao.ToString("dd/MM/yyyy"),
                    Detalhamento = domain.Detalhamento,
                    Grade = domain.Grade.Descricao,
                    TamanhoPadrao = domain.TamanhoPadrao,
                    Marca = domain.Marca.Nome,
                    Segmento = domain.Segmento != null ? domain.Segmento.Descricao : null,
                    Lavada = domain.Lavada,
                    LinhaCasa = domain.LinhaCasa,
                    Situacao = domain.Aprovado.HasValue == false
                                    ? "Aguardando aprovação"
                                    : domain.Aprovado.Value ? "Aprovado" : "Reprovado",
                    DataAprovacao = domain.Aprovado ?? false
                                    ? DateTime.Now.ToString("dd/MM/yyyy") : string.Empty,
                    Modelista = domain.Modelista != null ? domain.Modelista.Nome : null,
                    Cos = domain.Cos.HasValue ? domain.Cos.Value.ToString("N2") : string.Empty,
                    Passante = domain.Passante.HasValue ? domain.Passante.Value.ToString("N2") : string.Empty,
                    Entrepernas = domain.Entrepernas.HasValue ? domain.Entrepernas.Value.ToString("N2") : string.Empty,
                    Boca = domain.Boca.HasValue ? domain.Boca.Value.ToString("N2") : string.Empty,
                    Modelagem = domain.Modelagem,
                    EtiquetaMarca = domain.EtiquetaMarca,
                    EtiquetaComposicao = domain.EtiquetaComposicao,
                    Tag = domain.ModeloAvaliacao != null && domain.ModeloAvaliacao.Aprovado ? domain.ModeloAvaliacao.Tag : null,
                    Observacao = domain.Observacao,
                    Forro = domain.Forro,
                    TecidoComplementar = domain.TecidoComplementar,
                    Dificuldade = domain.Dificuldade,
                    QuantidadeSubmodelos = 0,
                    Fotos = domain.Fotos.Select(Mapper.Flat<ModeloFotoModel>).ToList(),
                    LinhaBordados = domain.LinhasBordado.ToArray(),
                    LinhaPespontos = domain.LinhasPesponto.ToArray(),
                    LinhaTravetes = domain.LinhasTravete.ToArray(),
                    Variacoes =
                        domain.VariacaoModelos.ToDictionary(k => k.Variacao.Nome, e => e.Cores.Select(c => c.Nome).ToList()),
                    SequenciaProducao = domain.SequenciaProducoes
                        .Select(p => new GridSequenciaProducaoModel
                        {
                            Nome = p.DepartamentoProducao.Nome,
                            Entrada = p.DataEntrada,
                            Saida = p.DataSaida
                        }).ToList(),
                };

                return View(model);
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a variação. Confira se os dados foram informados corretamente: " + exception.Message);
                _logger.Info(exception.GetMessage());
            }
            
            return View();
        }
        #endregion

        #endregion 

        #region Variacao
        [PopulateViewData("PopulaVariacaoViewData")]
        public virtual ActionResult Variacao(long modeloId)
        {
            var modelo = _modeloRepository.Get(modeloId);

            var variacoes = new List<long?>();
            var cores = new List<long>();

            foreach (var variacaoModelo in modelo.VariacaoModelos)
            {
                foreach (var cor in variacaoModelo.Cores)
                {
                    variacoes.Add(variacaoModelo.Variacao.Id);
                    cores.Add(cor.Id ?? 0);
                }
            }

            var model = new VariacaoModeloModel
            {
                ModeloId = modelo.Id ?? 0,
                ModeloReferencia = modelo.Referencia,
                ModeloDescricao = modelo.Descricao,
                ModeloEstilistaNome = modelo.Estilista.Nome,
                ModeloDataCriacao = modelo.DataCriacao,
                Variacoes = variacoes,
                Cores = cores
            };
            
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [PopulateViewData("PopulaVariacaoViewData")]
        public virtual ActionResult Variacao(VariacaoModeloModel model)
        {
            if (model.Variacoes.IsNullOrEmpty())
                ModelState.AddModelError("", "Adicione pelo menos uma variação.");

            if (ModelState.IsValid)
            {
                try
                {
                    var modelo = _modeloRepository.Get(model.ModeloId);
                    modelo.ClearVariacaoModelo();
                    Framework.UnitOfWork.Session.Current.Flush();

                    VariacaoModelo variacaoModelo = null;
                    double variacaoId = 0;
                    for (int i = 0; i < model.Variacoes.Count; i++)
                    {
                        if (variacaoId != model.Variacoes[i])
                        {
                            variacaoId = model.Variacoes[i].Value;

                            if (variacaoModelo != null)
                                modelo.AddVariacaoModelo(variacaoModelo);

                            variacaoModelo = new VariacaoModelo
                            {
                                Variacao = _variacaoRepository.Get(model.Variacoes[i])
                            };
                        }

                        variacaoModelo.AddCor(_corRepository.Load(model.Cores[i]));
                    }

                    modelo.AddVariacaoModelo(variacaoModelo);
                    
                    _modeloRepository.Update(modelo);
                    return RedirectToAction("Detalhar", new { id = model.ModeloId });
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a variação. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }
            
            return View(model);
        }
        #endregion
        
        #region Modelagem
        [PopulateViewData("PopulaModelagemViewData")]
        public virtual ActionResult Modelagem(long modeloId)
        {
            var domain = _modeloRepository.Get(modeloId);

            var model = new ModelagemModeloModel
            {
                ModeloId = domain.Id ?? 0,
                ModeloReferencia = domain.Referencia,
                ModeloDescricao = domain.Descricao,
                ModeloEstilistaNome = domain.Estilista.Nome,
                ModeloDataCriacao = domain.DataCriacao,
                Modelista = domain.Modelista != null ? domain.Modelista.Id : null,
                DataModelagem = domain.DataModelagem,
                Tecido = domain.Tecido,
                Cos = domain.Cos,
                Passante = domain.Passante,
                Entrepernas = domain.Entrepernas,
                Boca = domain.Boca,
                Modelagem = domain.Modelagem,
                Tamanho = domain.Tamanho != null ? domain.Tamanho.Id : null,
                Localizacao = domain.Localizacao,
                EtiquetaMarca = domain.EtiquetaMarca,
                EtiquetaComposicao = domain.EtiquetaComposicao,
                Tag = domain.ModeloAvaliacao != null && domain.ModeloAvaliacao.Aprovado ? domain.ModeloAvaliacao.Tag : null,
                Observacao = domain.Observacao
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulaModelagemViewData")]
        public virtual ActionResult Modelagem(ModelagemModeloModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _modeloRepository.Get(model.ModeloId);
                    domain.Modelista = _pessoaRepository.Load(model.Modelista);
                    domain.DataModelagem = model.DataModelagem;
                    domain.Tecido = model.Tecido;
                    domain.Cos = model.Cos;
                    domain.Passante = model.Passante;
                    domain.Entrepernas = model.Entrepernas;
                    domain.Boca = model.Boca;
                    domain.Modelagem = model.Modelagem;

                    if (model.Tamanho.HasValue)
                        domain.Tamanho = _tamanhoRepository.Get(model.Tamanho);

                    domain.Localizacao = model.Localizacao;
                    domain.EtiquetaMarca = model.EtiquetaMarca;
                    domain.EtiquetaComposicao = model.EtiquetaComposicao;
                    domain.Observacao = model.Observacao;

                    domain.DataAlteracao = DateTime.Now;
                    _modeloRepository.Update(domain);

                    this.AddSuccessMessage("Modelagem atualizada com sucesso.");
                    return RedirectToAction("Detalhar", new { id = model.ModeloId });
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a modelagem. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }
        #endregion

        #region ProgramacaoBordado

        public virtual ActionResult ProgramacaoBordado(long modeloId)
        {
            var modelo = _modeloRepository.Get(modeloId);

            var grid = modelo.ProgramacaoBordados.Select(p => new GridProgramacaoBordadoModel
                    {
                        Id = p.Id.GetValueOrDefault(),
                        Descricao = p.Descricao,
                        NomeArquivo = p.NomeArquivo,
                        Data = p.Data,
                        QuantidadePontos = p.QuantidadePontos,
                        QuantidadeCores = p.QuantidadeCores,
                        Aplicacao = p.Aplicacao,
                        Arquivo = p.Arquivo != null ? p.Arquivo.Id : null
                    });

            var model = new ProgramacaoBordadoModeloModel
            {
                ModeloId = modelo.Id ?? 0,
                ModeloReferencia = modelo.Referencia,
                ModeloDescricao = modelo.Descricao,
                ModeloEstilistaNome = modelo.Estilista.Nome,
                ModeloDataCriacao = modelo.DataCriacao,
                Grid = grid.ToList()
            };

            return View(model);
        }

        #endregion

        #region NovoProgramacaoBordado
        [PopulateViewData("PopulaProgramacaoBordado")]
        public virtual ActionResult NovoProgramacaoBordado(long modeloId)
        {
            var model = new ProgramacaoBordadoModel { Modelo = modeloId };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulaProgramacaoBordado")]
        public virtual ActionResult NovoProgramacaoBordado(long modeloId, ProgramacaoBordadoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<ProgramacaoBordado>(model);

                    domain.Arquivo = null;
                    if (string.IsNullOrEmpty(model.NomeArquivoUpload) == false)
                    {
                        if (model.NomeArquivo == null)
                        {
                            ModelState.AddModelError("NomeArquivo", "Informe o nome do arquivo.");
                            return View(model);
                        }

                        var arquivo = ArquivoController.SalvarArquivo(model.NomeArquivoUpload, model.NomeArquivo);
                        domain.Arquivo = _arquivoRepository.Save(arquivo);
                    }

                    var modelo = _modeloRepository.Load(modeloId);
                    modelo.AddProgramacaoBordado(domain);
                    modelo.DataAlteracao = DateTime.Now;
                    _modeloRepository.Update(modelo);

                    this.AddSuccessMessage("Programação do bordado cadastrado com sucesso.");
                    return RedirectToAction("ProgramacaoBordado", new { id = model.Modelo });
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a programação do modelo. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }

        #endregion

        #region EditarProgramacaoBordado
        [ImportModelStateFromTempData, PopulateViewData("PopulaProgramacaoBordado")]
        public virtual ActionResult EditarProgramacaoBordado(long modeloId, long id)
        {
            var domain = _programacaoBordadoRepository.Get(id);
            if (domain != null)
            {
                var model = Mapper.Flat<ProgramacaoBordadoModel>(domain);
                model.Modelo = modeloId;
                
                if (domain.Arquivo != null)
                    model.NomeArquivoUpload = domain.Arquivo.Nome;

                return View(model);
            }

            this.AddErrorMessage("Não foi possível encontrar a programação do bordado.");
            return RedirectToAction("ProgramacaoBordado", new { id = modeloId });
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulaProgramacaoBordado")]
        public virtual ActionResult EditarProgramacaoBordado(long modeloId, ProgramacaoBordadoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var modelo = _modeloRepository.Load(modeloId);
                    var programacaoBordado = modelo.ProgramacaoBordados.Single(pb => pb.Id == model.Id);
                    
                    var domain = Mapper.Unflat(model, programacaoBordado);

                    if (model.NomeArquivoUpload != null && model.NomeArquivo == null)
                    {
                        ModelState.AddModelError("NomeArquivo", "Informe o nome do arquivo.");
                        return View(model);
                    }

                    // a operação unflat instancia Arquivo por causa da propriedade ArquivoId
                    if (model.ArquivoId == null)
                    {
                        domain.Arquivo = null;
                    }

                    if (model.NomeArquivo != null && model.NomeArquivoUpload != null)
                    {
                        var nomeArquivo = domain.Arquivo != null ? domain.Arquivo.Nome : "";

                        if (nomeArquivo != model.NomeArquivoUpload)
                            domain.Arquivo = _arquivoRepository.Save(ArquivoController.SalvarArquivo(model.NomeArquivoUpload, model.NomeArquivo));
                    }

                    modelo.DataAlteracao = DateTime.Now;
                    _modeloRepository.Update(modelo);

                    this.AddSuccessMessage("Programação do bordado atualizada com sucesso.");
                    return RedirectToAction("ProgramacaoBordado", new { id = model.Modelo });
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a programação do modelo. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }

        #endregion

        #region ExcluirProgramacaoBordado
        [HttpPost, ValidateAntiForgeryToken, ExportModelStateToTempData]
        public virtual ActionResult ExcluirProgramacaoBordado(long? modeloId, long? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var modelo = _modeloRepository.Load(modeloId);
                    var programacaoBordado = modelo.ProgramacaoBordados.Single(pb => pb.Id == id);
                    modelo.RemoveProgramacaoBordado(programacaoBordado);
                    modelo.DataAlteracao = DateTime.Now;
                    _modeloRepository.Update(modelo);

                    this.AddSuccessMessage("Programação do bordado excluído com sucesso");
                    return RedirectToAction("ProgramacaoBordado", new { modeloId = modelo.Id });
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir a programação do bordado: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("EditarProgramacaoBordado", new { id });
        }

        #endregion
        
        #region Fotos

        #region Fotos
        protected List<GridModeloFotoModel> Fotos
        {
            get
            {
                var fotos = TempData.Peek(ChaveFoto) as List<GridModeloFotoModel>;

                if (fotos == null)
                    TempData[ChaveFoto] = fotos = new List<GridModeloFotoModel>();

                return fotos;
            }
            set { TempData[ChaveFoto] = value; }
        }
        #endregion

        #region LerFotos
        [AjaxOnly, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual ActionResult LerFotos([DataSourceRequest]DataSourceRequest request)
        {
            return Json(Fotos.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region AdicionarFoto
        [HttpPost]
        public virtual ActionResult AdicionarFoto(ModeloFotoModel model)
        {
            // Verifica se já existe uma foto padrão
            if (Fotos.Any(f => f.Padrao && model.Padrao))
                return Json(new { Error = "Já existe uma foto padrão." });

            // Verifica o título
            if (string.IsNullOrWhiteSpace(model.FotoTitulo))
                return Json(new { Error = "Informe um título para a foto." });
            if (model.FotoTitulo.Length > 100)
                return Json(new { Error = "O título não deve ser maior que 100 caracteres." });

            var foto = new GridModeloFotoModel
            {
                Id = model.Id ?? 0,
                FotoNome = model.FotoNome,
                FotoTitulo = model.FotoTitulo,
                Impressao = model.Impressao,
                Padrao = model.Padrao
            };

            Fotos.Add(foto);

            return Json(new { url = model.FotoNome.GetTempFileUrl(), titulo = model.FotoTitulo });
        }
        #endregion

        #region RemoverFoto
        [HttpPost]
        public virtual ActionResult RemoverFoto(string fotoNome)
        {
            var foto = Fotos.FirstOrDefault(p => p.FotoNome == fotoNome);

            if (foto != null)
            {
                Fotos.Remove(foto);
                return Json(new { fotoNome });
            }

            return Json(new { Error = "Foto não encontrada!" });
        }
        #endregion

        #endregion

        #region LinhaBordados

        #region LinhaBordados
        protected List<string> LinhaBordados
        {
            get
            {
                var linhaBordados = TempData.Peek(ChaveLinhaBordado) as List<string>;

                if (linhaBordados == null)
                    TempData[ChaveLinhaBordado] = linhaBordados = new List<string>();

                return linhaBordados;
            }
        }
        #endregion

        #region LerLinhaBordados
        [AjaxOnly, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual ActionResult LerLinhaBordados([DataSourceRequest]DataSourceRequest request)
        {
            var linhaBordados = TempData.Peek(ChaveLinhaBordado) as List<string>;

            if (linhaBordados != null)
                return Json(linhaBordados.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region AdicionarLinhaBordado
        [HttpPost, ValidateInput(false)]
        public virtual ActionResult AdicionarLinhaBordado(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return Json(new { error = "Ocorreu um erro ao adicionar uma linha bordado. Verifique o uso de caracteres especiais como: <, >, &, \", etc." });

            // Verifica se já está cadastrado
            if (LinhaBordados.All(f => f != nome))
            {
                LinhaBordados.Add(nome);
                TempData[ChaveLinhaBordado] = LinhaBordados;
            }
            else
            {
                return Json(new { Error = "Linha bordado já cadastrada." });
            }

            return Json(new { });
        }
        #endregion

        #region RemoverLinhaBordado
        [HttpPost]
        public virtual ActionResult RemoverLinhaBordado(string nome)
        {
            var linhaBordados = TempData.Peek(ChaveLinhaBordado) as List<string>;

            if (linhaBordados != null)
            {
                var linhaBordado = linhaBordados.SingleOrDefault(p => p == nome);

                if (linhaBordado != null)
                    linhaBordados.Remove(linhaBordado);
            }

            return Content(string.Empty);
        }
        #endregion

        #endregion

        #region LinhaPespontos

        #region LinhaPespontos
        protected List<string> LinhaPespontos
        {
            get
            {
                var linhaPespontos = TempData.Peek(ChaveLinhaPesponto) as List<string>;

                if (linhaPespontos == null)
                    TempData[ChaveLinhaPesponto] = linhaPespontos = new List<string>();

                return linhaPespontos;
            }
        }
        #endregion

        #region LerLinhaPespontos
        [AjaxOnly, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual ActionResult LerLinhaPespontos([DataSourceRequest]DataSourceRequest request)
        {
            return Json(LinhaPespontos.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region AdicionarLinhaPesponto
        [HttpPost]
        public virtual ActionResult AdicionarLinhaPesponto(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return Json(new { Error = "Ocorreu um erro ao adicionar uma linha pesponto. Verifique o uso de caracteres especiais como: <, >, &, \", etc." });

            if (LinhaPespontos.All(f => f != nome))
            {
                LinhaPespontos.Add(nome);
                TempData[ChaveLinhaPesponto] = LinhaPespontos;
            }
            else
            {
                return Json(new { Error = "Linha pesponto já cadastrada." });
            }

            return Json(new { });
        }
        #endregion

        #region RemoverLinhaPesponto
        [HttpPost]
        public virtual ActionResult RemoverLinhaPesponto(string nome)
        {
            var linhaPesponto = LinhaPespontos.SingleOrDefault(p => p == nome);

            if (linhaPesponto != null)
                LinhaPespontos.Remove(linhaPesponto);

            return Content(string.Empty);
        }
        #endregion

        #endregion

        #region LinhaTravetes

        #region LinhaTravetes
        protected List<string> LinhaTravetes
        {
            get
            {
                var linhaTravetes = TempData.Peek(ChaveLinhaTravete) as List<string>;

                if (linhaTravetes == null)
                    TempData[ChaveLinhaTravete] = linhaTravetes = new List<string>();

                return linhaTravetes;
            }
        }
        #endregion

        #region LerLinhaTravetes
        [AjaxOnly, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual ActionResult LerLinhaTravetes([DataSourceRequest]DataSourceRequest request)
        {
            return Json(LinhaTravetes.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region AdicionarLinhaTravete
        [HttpPost]
        public virtual ActionResult AdicionarLinhaTravete(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return Json(new { Error = "Ocorreu um erro ao adicionar uma linha travete. Verifique o uso de caracteres especiais como: <, >, &, \", etc." });

            if (LinhaTravetes.All(f => f != nome))
            {
                LinhaTravetes.Add(nome);
                TempData[ChaveLinhaTravete] = LinhaTravetes;
            }
            else
            {
                return Json(new { Error = "Linha travete já cadastrada." });
            }

            return Json(new { });
        }
        #endregion

        #region RemoverLinhaTravete
        [HttpPost]
        public virtual ActionResult RemoverLinhaTravete(string nome)
        {
            var linhaTravete = LinhaTravetes.SingleOrDefault(p => p == nome);

            if (linhaTravete != null)
                LinhaTravetes.Remove(linhaTravete);

            return Content(string.Empty);
        }
        #endregion

        #endregion

        #region ImprimirDetalhe
        public virtual ActionResult ImprimirDetalhe(long modeloId)
        {
            var modelo = _modeloRepository.Get(modeloId);

            var report = new DetalheModeloReport { DataSource = modelo };
            var filename = report.ToByteStream().SaveFile(".pdf");

            return File(filename);
        }
        #endregion

        #region Copiar
        [ImportModelStateFromTempData]
        public virtual ActionResult Copiar(long modeloId)
        {
            var domain = _modeloRepository.Get(modeloId);

            if (domain != null)
            {
                var model = new CopidarModeloModel();
                model.ReferenciaExistente = domain.Referencia;

                return View("Copiar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o modelo.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Copiar(CopidarModeloModel model)
        {
            // Valida referência
            if (_modeloRepository.Find(p => p.Referencia == model.ReferenciaNova).Any())
                ModelState.AddModelError("Nome", "Já existe um modelo cadastrado com esta referência.");

            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _modeloRepository.Find(m => m.Referencia == model.ReferenciaExistente).FirstOrDefault();

                    if (domain != null)
                    {
                        var novo = new Modelo();

                        novo.Referencia = model.ReferenciaNova;
                        novo.Artigo = domain.Artigo;
                        novo.Barra = domain.Barra;
                        novo.Boca = domain.Boca;
                        novo.Classificacao = domain.Classificacao;
                        novo.Colecao = domain.Colecao;
                        novo.Complemento = domain.Complemento;
                        novo.Comprimento = domain.Comprimento;
                        novo.Cos = domain.Cos;
                        novo.DataCriacao = domain.DataCriacao;
                        novo.DataAlteracao = DateTime.Now;
                        novo.Descricao = domain.Descricao;
                        novo.Detalhamento = domain.Detalhamento;
                        novo.Dificuldade = domain.Dificuldade;
                        novo.Entrepernas = domain.Entrepernas;
                        novo.Estilista = domain.Estilista;
                        novo.EtiquetaComposicao = domain.EtiquetaComposicao;
                        novo.EtiquetaMarca = domain.EtiquetaMarca;
                        novo.Forro = domain.Forro;
                        novo.Grade = domain.Grade;
                        novo.Lavada = domain.Lavada;
                        novo.LinhaCasa = domain.LinhaCasa;
                        novo.AddLinhaBordado(domain.LinhasBordado.ToArray());
                        novo.AddLinhaPesponto(domain.LinhasPesponto.ToArray());
                        novo.AddLinhaTravete(domain.LinhasTravete.ToArray());
                        novo.Localizacao = domain.Localizacao;
                        novo.Marca = domain.Marca;
                        novo.DataModelagem = domain.DataModelagem;
                        novo.Modelagem = domain.Modelagem;
                        novo.Modelista = domain.Modelista;
                        novo.Natureza = domain.Natureza;
                        novo.Observacao = domain.Observacao + Environment.NewLine + "Cópia do modelo: " + model.ReferenciaExistente;
                        novo.Passante = domain.Passante;
                        novo.ProdutoBase = domain.ProdutoBase;
                        novo.Segmento = domain.Segmento;
                        novo.Tamanho = domain.Tamanho;
                        novo.TamanhoPadrao = domain.TamanhoPadrao;
                        novo.Tecido = domain.Tecido;
                        novo.TecidoComplementar = domain.TecidoComplementar;
                        novo.ZiperBraguilha = domain.ZiperBraguilha;
                        novo.ZiperDetalhe = domain.ZiperDetalhe;
                        novo.ChaveExterna = domain.ChaveExterna;
                        
                        foreach (var foto in domain.Fotos)
                        {
                            var novaFoto = new ModeloFoto
                            {
                                Impressao = foto.Impressao,
                                Padrao = foto.Padrao
                            };

                            if (foto.Foto != null)
                            {
                                novaFoto.Foto = _arquivoRepository.Save(new Arquivo
                                {
                                    Data = foto.Foto.Data,
                                    Extensao = foto.Foto.Extensao,
                                    Nome = foto.Foto.Nome,
                                    Tamanho = foto.Foto.Tamanho,
                                    Titulo = foto.Foto.Titulo
                                });
                            }
                            
                            novo.AddFoto(novaFoto);
                        }

                        foreach (var programacao in domain.ProgramacaoBordados)
                        {
                            var novaProgramacao = new ProgramacaoBordado
                            {
                                Aplicacao = programacao.Aplicacao,
                                Data = programacao.Data,
                                Descricao = programacao.Descricao,
                                NomeArquivo = programacao.NomeArquivo,
                                Observacao = programacao.Observacao,
                                ProgramadorBordado = programacao.ProgramadorBordado,
                                QuantidadeCores = programacao.QuantidadeCores,
                                QuantidadePontos = programacao.QuantidadePontos
                            };

                            if (programacao.Arquivo != null)
                            {
                                novaProgramacao.Arquivo = new Arquivo
                                {
                                    Data = programacao.Arquivo.Data,
                                    Extensao = programacao.Arquivo.Extensao,
                                    Nome = programacao.Arquivo.Nome,
                                    Tamanho = programacao.Arquivo.Tamanho,
                                    Titulo = programacao.Arquivo.Titulo
                                };
                            }

                            novo.AddProgramacaoBordado(novaProgramacao);
                        }

                        // Composições para serem salvas depois
                        foreach (var sequencia in domain.SequenciaProducoes)
                        {
                            var novaSequencia = new SequenciaProducao
                            {
                                DataEntrada = sequencia.DataEntrada,
                                DataSaida = sequencia.DataSaida,
                                DepartamentoProducao = sequencia.DepartamentoProducao,
                                SetorProducao = sequencia.SetorProducao
                            };

                            

                            novo.AddSequenciaProducao(novaSequencia);
                        }


                        foreach (var materialConsumo in domain.MateriaisConsumo)
                        {
                            var novaComposicao = new ModeloMaterialConsumo()
                            {
                                Material = materialConsumo.Material,
                                Quantidade = materialConsumo.Quantidade,
                                UnidadeMedida = materialConsumo.UnidadeMedida,
                                DepartamentoProducao = materialConsumo.DepartamentoProducao
                            };

                            novo.MateriaisConsumo.Add(novaComposicao);
                        }

                        foreach (var variacaoModelo in domain.VariacaoModelos)
                        {
                            var novaVariacaoModelo = new VariacaoModelo
                            {
                                Variacao = variacaoModelo.Variacao
                            };
                            novaVariacaoModelo.AddCor(variacaoModelo.Cores.ToArray());

                            novo.AddVariacaoModelo(novaVariacaoModelo);
                        }

                        _modeloRepository.Save(novo);
                        
                        this.AddSuccessMessage("Modelo copiado com sucesso.");
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao copiar o modelo. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }

        #endregion
        
        #region Métodos

        #region PopulateViewData
        protected void PopulateViewData(IModeloDropdownModel model)
        {
            var grades = _gradeRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Grade"] = grades.ToSelectList("Descricao", model.Grade);

            var colecoes = _colecaoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Colecao"] = colecoes.ToSelectList("Descricao", model.Colecao);
                       
            var classificacoes = _classificacaoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Classificacao"] = classificacoes.ToSelectList("Descricao", model.Classificacao);
            
            var segmentos = _segmentoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Segmento"] = segmentos.ToSelectList("Descricao", model.Segmento);

            var naturezas = _naturezaRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Natureza"] = naturezas.ToSelectList("Descricao", model.Natureza);

            var barras = _barraRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Barra"] = barras.ToSelectList("Descricao", model.Barra);

            var comprimentos = _comprimentoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Comprimento"] = comprimentos.ToSelectList("Descricao", model.Comprimento);

            var produtosBase = _produtoBaseRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["ProdutoBase"] = produtosBase.ToSelectList("Descricao", model.ProdutoBase);

            var artigos = _artigoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Artigo"] = artigos.ToSelectList("Descricao", model.Artigo);

            var marcas = _marcaRepository.Find(p => p.Ativo).OrderBy(p => p.Nome).ToList();
            ViewData["Marca"] = marcas.ToSelectList("Nome", model.Marca);

            // Se tela de pesquisa
            var pesquisaModel = model as PesquisaModeloModel;
            if (pesquisaModel != null)
            {
                var estilistas = _pessoaRepository.Find(p => p.Funcionario != null
                    && p.Funcionario.FuncaoFuncionario == FuncaoFuncionario.Estilista)
                    .OrderBy(p => p.Nome).ToList();
                ViewData["Estilista"] = estilistas.ToSelectList("Nome", pesquisaModel.Estilista);

                var modelistas = _pessoaRepository.Find(p => p.Funcionario != null
                    && p.Funcionario.FuncaoFuncionario == FuncaoFuncionario.Modelista)
                    .OrderBy(p => p.Nome).ToList();
                ViewData["Modelista"] = modelistas.ToSelectList("Nome", pesquisaModel.Modelista);

                ViewBag.TipoRelatorio = new SelectList(_tipoRelatorio);
                ViewBag.OrdenarPor = new SelectList(ColunasPesquisaModelo, "value", "key");
                ViewBag.AgruparPor = new SelectList(ColunasPesquisaModelo, "value", "key");
            }
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var modelo = model as ModeloModel;

            // Valida referência
            if (_modeloRepository.Find(p => p.Referencia == modelo.Referencia && p.Id != modelo.Id).Any())
                ModelState.AddModelError("Nome", "Já existe um modelo cadastrado com esta referência.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
        }
        #endregion

        #region PopulaVariacaoViewData
        protected void PopulaVariacaoViewData(VariacaoModeloModel model)
        {
            var cores = _corRepository.Find().OrderBy(p => p.Nome).ToList();
            ViewBag.Cor = new SelectList(cores.Where(p => p.Ativo), "Id", "Nome");
            ViewBag.CoresDicionario = cores.ToDictionary(t => t.Id, t => t.Nome);

            var variacoes = _variacaoRepository.Find().OrderBy(p => p.Nome).ToList();
            ViewBag.Variacao = new SelectList(variacoes.Where(p => p.Ativo), "Id", "Nome");
            ViewBag.VariacoesDicionario = variacoes.ToDictionary(t => t.Id, t => t.Nome);
        }
        #endregion
      
        #region PopulaModelagemViewData
        protected void PopulaModelagemViewData(ModelagemModeloModel model)
        {
            // Preenche os tamanhos da grade escolhida no modelo
            var tamanhos = _modeloRepository.Get(model.ModeloId).Grade.Tamanhos.Keys.ToList();
            ViewBag.Tamanho = tamanhos.ToSelectList("Sigla", model.Tamanho);
        }
        #endregion

        #region PopulaProgramacaoBordado
        protected void PopulaProgramacaoBordado(ProgramacaoBordadoModel model)
        {
            var modelo = _modeloRepository.Get(model.Modelo);

            ViewBag.ModeloReferencia = modelo.Referencia;
            ViewBag.ModeloDescricao = modelo.Descricao;
            ViewBag.ModeloEstilistaNome = modelo.Estilista.Nome;
            ViewBag.ModeloDataCriacao = modelo.DataCriacao.ToString("dd/MM/yyyy");
        }

        //protected void PopulaProgramacaoBordado(ProgramacaoBordadoModeloModel model)
        //{
        //    var modelo = _modeloRepository.Get(model.ModeloId);

        //    ViewBag.ModeloReferencia = modelo.Referencia;
        //    ViewBag.ModeloDescricao = modelo.Descricao;
        //    ViewBag.ModeloEstilistaNome = modelo.Estilista.Nome;
        //    ViewBag.ModeloDataCriacao = modelo.DataCriacao.ToString("dd/MM/yyyy");
        //}
        #endregion

        #region VerificarReferencia
        [AjaxOnly]
        public virtual JsonResult VerificarReferencia(string referencia)
        {
            bool existeModelo = false;
            long modeloId = 0;
            var modelo = _modeloRepository.Get(p => p.Referencia == referencia);

            if (modelo != null)
            {
                existeModelo = true;
                modeloId = modelo.Id.GetValueOrDefault();
            }

            var result = new { existeModelo, modeloId };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion
    }
}