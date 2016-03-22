using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Domain.Producao;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Reporting.Producao;
using Fashion.ERP.Web.Areas.Producao.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using NHibernate.Linq;
using Ninject.Extensions.Logging;
using Telerik.Reporting;

namespace Fashion.ERP.Web.Areas.Producao.Controllers
{
    public partial class RelatorioMateriaisProgramacaoProducaoController : BaseController
    {        
        private readonly ILogger _logger;
        private readonly IRepository<RemessaProducao> _remessaProducaoRepository;
        private readonly IRepository<Categoria> _categoriaRepository;
        private readonly IRepository<Subcategoria> _subcategoriaRepository;
        private readonly IRepository<DepartamentoProducao> _departamentoProducaoRepository;
        private readonly IRepository<ProgramacaoProducao> _programacaoProducaoRepository;
        private readonly IRepository<Material> _materialRepository;

        #region MateriaisProgramacaoProducao
        private static readonly Dictionary<string, string> ColunasMateriaisProgramacaoProducao = new Dictionary<string, string>
        {
            {"Lote", "Lote"},
            {"Descrição", "Descricao"},
            {"Referência", "Referencia"}
        };
        #endregion
        
        #region Construtores
        public RelatorioMateriaisProgramacaoProducaoController(ILogger logger, IRepository<Modelo> modeloRepository,
            IRepository<Categoria> categoriaRepository, 
            IRepository<Subcategoria> subcategoriaRepository, IRepository<DepartamentoProducao> departamentoProducaoRepository,
            IRepository<ProgramacaoProducao> programacaoProducaoRepository, IRepository<Material> materialRepository,
            IRepository<RemessaProducao> remesssaProducaoRepository )
        {
            _logger = logger;
            _categoriaRepository = categoriaRepository;
            _subcategoriaRepository = subcategoriaRepository;
            _departamentoProducaoRepository = departamentoProducaoRepository;
            _programacaoProducaoRepository = programacaoProducaoRepository;
            _materialRepository = materialRepository;
            _remessaProducaoRepository = remesssaProducaoRepository;
        }

        #endregion

        #region Relatorio
        [PopulateViewData("PopulateMateriaisProgramacaoProducao")]
        public virtual ActionResult MateriaisProgramacaoProducao()
        {
            return View(new MateriaisProgramacaoProducaoModel() {SemFoto = false});
        }

        [HttpPost, AjaxOnly, PopulateViewData("PopulateMateriaisProgramacaoProducao")]
        public virtual JsonResult MateriaisProgramacaoProducao(MateriaisProgramacaoProducaoModel model)
        {
            var query = _programacaoProducaoRepository.Find()
                .SelectMany(x => x.ProgramacaoProducaoMateriais, (x, s) => 
                    new { ProgramacaoProducao = x, MaterialProgramacaoProducao = s })
                .SelectMany(x => x.ProgramacaoProducao.ProgramacaoProducaoItems, (x, z) =>
                    new { x.ProgramacaoProducao, x.MaterialProgramacaoProducao, Item = z });

            var filtros = new StringBuilder();

            try
            {

                if (!string.IsNullOrWhiteSpace(model.Tag))
                {
                    query = query.Where(p => p.ProgramacaoProducao.ProgramacaoProducaoItems.Any(item => item.FichaTecnica.Tag == model.Tag));
                    filtros.AppendFormat("Tag: {0}, ", model.Tag);
                }

                if (model.Ano.HasValue)
                {
                    query = query.Where(p => p.ProgramacaoProducao.ProgramacaoProducaoItems.Any(item => item.FichaTecnica.Ano == model.Ano));
                    filtros.AppendFormat("Ano: {0}, ", model.Ano);
                }

                if (model.RemessaProducao.HasValue)
                {
                    query = query.Where(p => p.ProgramacaoProducao.RemessaProducao.Id == model.RemessaProducao);
                    filtros.AppendFormat("Remessa: {0}, ", _remessaProducaoRepository.Get(model.RemessaProducao.Value).Descricao);
                }

                if (model.DataInicial.HasValue)
                {
                    query = query.Where(p => p.ProgramacaoProducao.DataProgramada >= model.DataInicial.Value);
                    filtros.AppendFormat("Data programada a partir de: {0:dd/MM/yyyy}, ", model.DataInicial.Value);
                }

                if (model.DataFinal.HasValue)
                {
                    query = query.Where(p => p.ProgramacaoProducao.DataProgramada <= model.DataFinal.Value);
                    filtros.AppendFormat("Data programada até: {0:dd/MM/yyyy}, ", model.DataFinal.Value);
                }

                if (!model.Categorias.IsNullOrEmpty())
                {
                    var categorias = new List<long?>(model.Categorias);

                    //var categoriasClosure = categorias; // Copiar para variável local
                    var categoriasDomain = _categoriaRepository.Find(c => model.Categorias.Contains(c.Id ?? 0));
                    filtros.AppendFormat("Categoria(s): {0}, ", categoriasDomain.Select(c => c.Nome).ToList().Join(","));

                    // Inserir a subcategoria antes da categoria com 'OR'
                    if (model.Subcategorias.IsNullOrEmpty() == false)
                    {
                        //var subcategorias = model.Subcategorias.ConvertAll(long.Parse);
                        var subcategoriasDomain = _subcategoriaRepository.Find(s => model.Subcategorias.Contains(s.Id ?? 0L)).ToList();

                        // Remover o filtro de categoria que já possui subcategoria para não filtrar 2 vezes
                        categorias = categorias.Except(subcategoriasDomain.Select(s => s.Categoria.Id)).ToList();

                        // Selecionar as subcategorias ou as outras categorias
                        query = query.Where(p => model.Subcategorias.Contains(p.MaterialProgramacaoProducao.Material.Subcategoria.Id ?? 0L)
                            || categorias.Contains(p.MaterialProgramacaoProducao.Material.Subcategoria.Categoria.Id ?? 0L));

                        filtros.AppendFormat("Subcategoria: {0}, ", subcategoriasDomain.Select(s => s.Nome).ToList().Join(","));
                    }
                    else
                    {
                        // Se não existe subcategoria, selecionar todas as categorias selecionadas na tela
                        query = query.Where(p => categorias.Contains(p.MaterialProgramacaoProducao.Material.Subcategoria.Categoria.Id ?? 0L));
                    }
                }
                
                if (model.Material.HasValue)
                {
                    query = query.Where(p => model.Material == p.MaterialProgramacaoProducao.Material.Id);

                    var materialDomain = _materialRepository.Find(m => model.Material == m.Id).FirstOrDefault();
                    filtros.AppendFormat("Referência do Material: {0}, ", materialDomain.Referencia);
                }

                if (!model.Departamentos.IsNullOrEmpty())
                {
                    query = query.Where(p => model.Departamentos.Contains(p.MaterialProgramacaoProducao.DepartamentoProducao.Id ?? 0));

                    var departamentos = _departamentoProducaoRepository.Find(m => model.Departamentos.Contains(m.Id ?? 0));
                    filtros.AppendFormat("Departamentos(s): {0}, ", departamentos.Select(c => c.Nome).ToList().Join(","));
                }

                if (!model.GeneroCategorias.IsNullOrEmpty())
                {
                    query = query.Where(p => model.GeneroCategorias.Contains(p.MaterialProgramacaoProducao.Material.Subcategoria.Categoria.GeneroCategoria));
                    filtros.AppendFormat("Gênero da Categoria(s): {0}, ", model.GeneroCategorias.Select(c => c.EnumToString()).ToList().Join(","));
                }
                
                var query_ = query.ToList();

                var result = query_.Select(q => new
                {
                    Id = q.ProgramacaoProducao.Id,
                    FichasTecnicas = q.ProgramacaoProducao.ProgramacaoProducaoItems.Select(x => 
                        new { x.FichaTecnica.Tag, x.FichaTecnica.Descricao, x.FichaTecnica.Referencia, x.FichaTecnica.Estilista }),
                    q.ProgramacaoProducao.DataProgramada,
                    q.ProgramacaoProducao.Lote,
                    q.ProgramacaoProducao.Ano,
                    QuantidadeProgramada = q.ProgramacaoProducao.Quantidade,
                    QuantidadeMaterial = q.MaterialProgramacaoProducao.Quantidade,
                    ReferenciaMaterial = q.MaterialProgramacaoProducao.Material.Referencia,
                    DescricaoMaterial = q.MaterialProgramacaoProducao.Material.Descricao,
                    UnidadeMedida = q.MaterialProgramacaoProducao.Material.UnidadeMedida.Sigla,
                    NomeFoto = q.MaterialProgramacaoProducao.Material.Foto != null? q.MaterialProgramacaoProducao.Material.Foto.Nome.GetFileUrl() : "",
                    Departamento = q.MaterialProgramacaoProducao.DepartamentoProducao.Nome
                }).ToList();

                var resultadoAgrupado = result.GroupBy(x => new { x.Id, x.QuantidadeProgramada, x.DataProgramada, x.Lote, x.Ano }, (chave, grupo) => 
                    new
                    {
                        chave.Id,
                        chave.QuantidadeProgramada,
                        grupo.First().FichasTecnicas,
                        chave.DataProgramada,
                        chave.Lote,
                        chave.Ano,
                        Materiais = grupo.Select(y => new
                        {
                            Referencia = y.ReferenciaMaterial,
                            Descricao = y.DescricaoMaterial,
                            y.UnidadeMedida,
                            y.NomeFoto,
                            QuantidadeConsumo = y.QuantidadeMaterial / chave.QuantidadeProgramada,
                            y.QuantidadeMaterial,
                            y.Departamento
                        }).Distinct().OrderBy(y => model.OrdenarPor == "Referencia" ? y.Referencia : y.Descricao)
                    });

                if (!query.Any())
                    return Json(new { Error = "Nenhum item encontrado." });

                #region Montar Relatório

                Report report;

                if (model.SemFoto)
                {
                    report = new MateriaisProgramacaoProducaoSemFotoReport() {DataSource = resultadoAgrupado};
                }
                else
                {
                    report = new MateriaisProgramacaoProducaoReport() { DataSource = resultadoAgrupado };
                }

                if (filtros.Length > 2)
                    report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

                if (model.OrdenarPor != null)
                    report.Sortings.Add("=Fields." + "Lote", SortDirection.Asc);
                
                #endregion

                var filename = report.ToByteStream().SaveFile(".pdf");

                return Json(new { Url = filename });
            }
            catch (Exception exception)
            {
                var message = exception.GetMessage();
                _logger.Info(message);

                return Json(new { Error = message });
            }
        }
        #endregion

        #region PopulateMateriaisProgramacaoProducao
        protected void PopulateMateriaisProgramacaoProducao(MateriaisProgramacaoProducaoModel model)
        {
            var departamentosProducao = _departamentoProducaoRepository.Find(p => p.Ativo).OrderBy(p => p.Nome).ToList();
            ViewData["Departamentos"] = departamentosProducao.ToSelectList("Nome");

            var remessasProducao = _remessaProducaoRepository.Find().OrderBy(p => p.Descricao).ToList();
            ViewData["RemessaProducao"] = remessasProducao.ToSelectList("Descricao");

            var categorias = _categoriaRepository.Find(p => p.Ativo).OrderBy(o => o.Nome).ToList();
            ViewData["Categorias"] = categorias.ToSelectList("Nome");

            if (model.Categorias.IsNullOrEmpty())
            {
                ViewBag.Subcategorias = new SelectList(Enumerable.Empty<Subcategoria>(), "Id", "Nome");
            }
            else
            {
                var subcategorias = _subcategoriaRepository.Find(p => model.Subcategorias.Contains(p.Categoria.Id ?? 0) && p.Ativo).ToList();
                ViewData["Subcategorias"] = subcategorias.ToSelectList("Nome");
            }

            ViewBag.GeneroCategorias = GeneroCategoria.Aviamento.ToSelectList();

            ViewBag.OrdenarPor = new SelectList(ColunasMateriaisProgramacaoProducao, "value", "key");
        }
        #endregion
    }
}