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
        private readonly IRepository<Colecao> _colecaoRepository;
        private readonly IRepository<Categoria> _categoriaRepository;
        private readonly IRepository<Subcategoria> _subcategoriaRepository;
        private readonly IRepository<DepartamentoProducao> _departamentoProducaoRepository;
        private readonly IRepository<ProgramacaoProducao> _programacaoProducaoRepository;
        private readonly IRepository<Material> _materialRepository;

        #region MateriaisProgramacaoProducao
        private static readonly Dictionary<string, string> ColunasMateriaisProgramacaoProducao = new Dictionary<string, string>
        {
            {"Tag", "Tag"},
            {"Descrição", "Descricao"}
        };
        #endregion
        
        #region Construtores
        public RelatorioMateriaisProgramacaoProducaoController(ILogger logger, IRepository<Modelo> modeloRepository,
            IRepository<Colecao> colecaoRepository, IRepository<Categoria> categoriaRepository, 
            IRepository<Subcategoria> subcategoriaRepository, IRepository<DepartamentoProducao> departamentoProducaoRepository,
            IRepository<ProgramacaoProducao> programacaoProducaoRepository, IRepository<Material> materialRepository )
        {
            _logger = logger;
            _colecaoRepository = colecaoRepository;
            _categoriaRepository = categoriaRepository;
            _subcategoriaRepository = subcategoriaRepository;
            _departamentoProducaoRepository = departamentoProducaoRepository;
            _programacaoProducaoRepository = programacaoProducaoRepository;
            _materialRepository = materialRepository;
        }

        #endregion

        #region Relatorio
        [PopulateViewData("PopulateMateriaisProgramacaoProducao")]
        public virtual ActionResult MateriaisProgramacaoProducao()
        {
            return View(new MateriaisProgramacaoProducaoModel());
        }

        [HttpPost, AjaxOnly, PopulateViewData("PopulateMateriaisProgramacaoProducao")]
        public virtual JsonResult MateriaisProgramacaoProducao(MateriaisProgramacaoProducaoModel model)
        {
            var query = _programacaoProducaoRepository.Find()
                .SelectMany(x => x.ProgramacaoProducaoMateriais, (x, s) => new { ProgramacaoProducao = x, MaterialProgramacaoProducao = s });

            var filtros = new StringBuilder();

            try
            {
                if (model.ColecaoProgramada.HasValue)
                {
                    query = query.Where(p => p.ProgramacaoProducao.Colecao.Id == model.ColecaoProgramada);
                    filtros.AppendFormat("Coleção Programada: {0}, ", _colecaoRepository.Get(model.ColecaoProgramada.Value).Descricao);
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

                if (!string.IsNullOrWhiteSpace(model.Tag))
                {
                    query = query.Where(p => p.ProgramacaoProducao.FichaTecnica.Tag.Contains(model.Tag));

                    filtros.AppendFormat("Tag: {0}, ", model.Tag);
                }

                if (model.Material.HasValue)
                {
                    query = query.Where(p => model.Material == p.MaterialProgramacaoProducao.Material.Id);

                    var materialDomain = _materialRepository.Find(m => model.Material == m.Id).FirstOrDefault();
                    filtros.AppendFormat("Referência do Material: {0}, ", materialDomain.Referencia);
                }
                
                //var result =   query.Fetch(p => p.ProgramacaoProducao).ThenFetch(p => p.FichaTecnica)
                //    .Fetch(p => p.MaterialProgramacaoProducao).ThenFetch(p => p.Material)
                //    .ToList();
                
                var result = query.Select(q => new
                {
                    Id = q.ProgramacaoProducao.Id,
                    Tag = q.ProgramacaoProducao.FichaTecnica.Tag,
                    Referencia = q.ProgramacaoProducao.FichaTecnica.Referencia,
                    Descricao = q.ProgramacaoProducao.FichaTecnica.Descricao,
                    Estilista = q.ProgramacaoProducao.FichaTecnica.Estilista.Nome,
                    QuantidadeProgramada = q.ProgramacaoProducao.Quantidade,
                    QuantidadeMaterial = q.MaterialProgramacaoProducao.Quantidade,
                    ReferenciaMaterial = q.MaterialProgramacaoProducao.Material.Referencia,
                    DescricaoMaterial = q.MaterialProgramacaoProducao.Material.Descricao,
                    UnidadeMedida = q.MaterialProgramacaoProducao.Material.UnidadeMedida.Sigla,
                    NomeFoto = q.MaterialProgramacaoProducao.Material.Foto.Nome.GetFileUrl()
                }).ToList();

                var resultadoAgrupado = result.GroupBy(x => new { x.Id, x.Tag, x.Referencia, x.QuantidadeProgramada, x.Descricao, x.Estilista }, (chave, grupo) => 
                    new
                    {
                        Id = chave.Id,
                        Tag = chave.Tag, 
                        Referencia = chave.Referencia,
                        Descricao = chave.Descricao,
                        QuantidadeProgramada = chave.QuantidadeProgramada,
                        Estilista = chave.Estilista,
                        Materiais = grupo.Select(y => new
                        {
                            Referencia = y.ReferenciaMaterial,
                            Descricao = y.DescricaoMaterial,
                            UnidadeMedida = y.UnidadeMedida,
                            NomeFoto = y.NomeFoto,
                            QuantidadeConsumo = y.QuantidadeMaterial / chave.QuantidadeProgramada,
                            QuantidadeMaterial = y.QuantidadeMaterial
                        }).OrderBy(y => y.Descricao)
                    });

                //var result = query.ToList();

                if (!query.Any())
                    return Json(new { Error = "Nenhum item encontrado." });

                #region Montar Relatório
                Report report = new MateriaisProgramacaoProducaoReport() { DataSource = resultadoAgrupado };

                if (filtros.Length > 2)
                    report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

                if (model.OrdenarPor != null)
                    report.Sortings.Add("=Fields." + model.OrdenarPor, SortDirection.Asc);
                
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
            ViewData["DepartamentosProducao"] = departamentosProducao.ToSelectList("Nome");

            var colecaoAprovada = _colecaoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["ColecaoProgramada"] = colecaoAprovada.ToSelectList("Descricao");

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

            ViewBag.OrdenarPor = new SelectList(ColunasMateriaisProgramacaoProducao, "value", "key");
        }
        #endregion
    }
}