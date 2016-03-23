using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.Producao;
using Fashion.ERP.Domain.Producao.ObjetosRelatorio;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Reporting.Producao;
using Fashion.ERP.Web.Areas.Producao.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Repository;
using Fashion.Framework.Common.Extensions;
using Ninject.Extensions.Logging;
using Telerik.Reporting;

namespace Fashion.ERP.Web.Areas.Producao.Controllers
{
    public partial class RelatorioConsumoMaterialProgramadoController : BaseController
    {
        #region Variaveis
        private readonly ILogger _logger;
        private readonly IRepository<ProgramacaoProducao> _programacaoProducaoRepository;
        private readonly IRepository<Colecao> _colecaoRepository;
        private readonly IRepository<RemessaProducao> _remessaProducaoRepository;
        private readonly IRepository<Categoria> _categoriaRepository;
        private readonly IRepository<Subcategoria> _subcategoriaRepository;
        private readonly IRepository<EstoqueMaterial> _estoqueMaterialRepository;
        private readonly IRepository<ReservaEstoqueMaterial> _reservaEstoqueMaterialRepository;
        private readonly IRepository<Material> _materialRepository;
        
        #region ColunasConsumoMaterialProgramado
        private static readonly Dictionary<string, string> ColunasConsumoMaterialProgramado = new Dictionary<string, string>
        {
            {"Referência", "Referencia"},
            {"Descrição", "Descricao"}
        };
        #endregion

        #endregion
        
        #region Construtores
        public RelatorioConsumoMaterialProgramadoController(ILogger logger, IRepository<Colecao> colecaoRepository, 
            IRepository<Categoria> categoriaRepository, IRepository<Subcategoria> subcategoriaRepository,
            IRepository<ReservaEstoqueMaterial> reservaEstoqueMaterialRepository, IRepository<EstoqueMaterial> estoqueMaterialRepository, 
            IRepository<ProgramacaoProducao> programacaoProducaorRepository, IRepository<Material> materialRepository,
            IRepository<RemessaProducao> remessaProducaoRepository )
        {
            _logger = logger;
            _colecaoRepository = colecaoRepository;
            _remessaProducaoRepository = remessaProducaoRepository;
            _categoriaRepository = categoriaRepository;
            _subcategoriaRepository = subcategoriaRepository;
            _reservaEstoqueMaterialRepository = reservaEstoqueMaterialRepository;
            _estoqueMaterialRepository = estoqueMaterialRepository;
            _programacaoProducaoRepository = programacaoProducaorRepository;
            _materialRepository = materialRepository;
        }
        #endregion

        #region ConsumoMaterialProducao
        [PopulateViewData("PopulateConsumoMaterialProgramado")]
        public virtual ActionResult ConsumoMaterialProgramado()
        {
            var model = new ConsumoMaterialProgramadoModel();

            model.DataFinal = DateTime.Now;
            model.DataInicial = DateTime.Now.AddMonths(-3);

            return View(model);
        }

        [HttpPost, AjaxOnly, PopulateViewData("PopulateConsumoMaterialProgramado")]
        public virtual JsonResult ConsumoMaterialProgramado(ConsumoMaterialProgramadoModel model)
        {
            var query = _programacaoProducaoRepository.Find()
                .SelectMany(x => x.ProgramacaoProducaoMateriais, (x, s) => new { ProgramacaoProducao = x, MaterialProgramacaoProducao = s });

            var filtros = new StringBuilder();

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

            if (model.RemessaProducao.HasValue)
            {
                query = query.Where(p => p.ProgramacaoProducao.RemessaProducao.Id == model.RemessaProducao);
                filtros.AppendFormat("Remessa de produção: {0}, ", _remessaProducaoRepository.Get(model.RemessaProducao.Value).Descricao);
            }

            if (model.Lote.HasValue)
            {
                query = query.Where(p => model.Lote == p.ProgramacaoProducao.Lote);
                filtros.AppendFormat("Lote: {0}, ", model.Lote);
            }

            if (model.Ano.HasValue)
            {
                query = query.Where(p => model.Ano == p.ProgramacaoProducao.Ano);
                filtros.AppendFormat("Ano: {0}, ", model.Ano);
            }

            if (model.SituacaoProgramacaoProducao.HasValue)
            {
                query = query.Where(p => model.SituacaoProgramacaoProducao == p.ProgramacaoProducao.SituacaoProgramacaoProducao);
                filtros.AppendFormat("Situação: {0}, ", model.SituacaoProgramacaoProducao.GetValueOrDefault().EnumToString());
            }

            if (model.Material.HasValue)
            {
                query = query.Where(p => model.Material == p.MaterialProgramacaoProducao.Material.Id);

                var materialDomain = _materialRepository.Find(m => model.Material == m.Id).FirstOrDefault();
                filtros.AppendFormat("Referência do Material: {0}, ", materialDomain.Referencia);
            }

            var result = query.Select(q => new
            {
                q.ProgramacaoProducao.Id,
                q.ProgramacaoProducao.DataProgramada,
                q.ProgramacaoProducao.Lote,
                q.ProgramacaoProducao.Ano,
                ReferenciaMaterial = q.MaterialProgramacaoProducao.Material.Referencia,
                IdMaterial = q.MaterialProgramacaoProducao.Material.Id,
                DescricaoMaterial = q.MaterialProgramacaoProducao.Material.Descricao,
                QuantidadeAprovada = q.ProgramacaoProducao.Quantidade,
                QuantidadeMaterial = q.MaterialProgramacaoProducao.Quantidade,
                QuantidadeTotalMaterial = q.MaterialProgramacaoProducao.Quantidade,
                NomeFoto = q.MaterialProgramacaoProducao.Material.Foto.Nome.GetFileUrl(),
                UnidadeMedida = q.MaterialProgramacaoProducao.Material.UnidadeMedida.Sigla
            });

            //Possível problema de estouro de memória, o groupby subsequente é executado em memória
            //e não no banco de dados.
            var result2 = result.ToList();

            var resultadoFinal = result2.GroupBy(x => new { x.IdMaterial, x.ReferenciaMaterial, x.DescricaoMaterial, x.NomeFoto, x.UnidadeMedida }, (chave, grupo) =>
                new ConsumoMaterialProducaoRelatorio
                {
                    Referencia = chave.ReferenciaMaterial,
                    Descricao = chave.DescricaoMaterial,
                    NomeFoto = chave.NomeFoto,
                    UnidadeMedida = chave.UnidadeMedida,
                    TotalQuantidadeMaterial = grupo.Sum(q => q.QuantidadeMaterial),
                    TotalQuantidadeAprovada = grupo.Sum(q => q.QuantidadeAprovada),
                    TotalQuantidadeTotalMaterial = grupo.Sum(q => q.QuantidadeTotalMaterial),
                    QuantidadeDisponivel = ObtenhaQuantidadeDisponivel(chave.IdMaterial.Value),
                    FichasTecnicas = grupo.Select(w => new FichaTecnicaConsumoMaterialRelatorio()
                    {
                        Lote = w.Lote,
                        Ano = w.Ano,
                        DataProgramada = w.DataProgramada,
                        QuantidadeAprovada = w.QuantidadeAprovada,
                        QuantidadeMaterial = w.QuantidadeMaterial,
                        QuantidadeTotalMaterial = w.QuantidadeTotalMaterial
                    }).OrderBy(u => u.Lote)
                }).ToList();

            if (!query.Any())
                return Json(new { Error = "Nenhum item foi encontrado." });

            var report = new ConsumoMaterialProgramadoReport { DataSource = resultadoFinal };

            if (filtros.Length > 2)
                report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

            if (model.OrdenarPor != null)
                report.Sortings.Add("=Fields." + model.OrdenarPor, model.OrdenarEm == "asc" ? SortDirection.Asc : SortDirection.Desc);

            var filename = report.ToByteStream().SaveFile(".pdf");

            return Json(new { Url = filename });
        }

        public double ObtenhaQuantidadeDisponivel(long idMaterial)
        {
            var reservasEstoque = _reservaEstoqueMaterialRepository.Find(r => r.Material.Id == idMaterial);
            var quantidadeReserva = reservasEstoque.ToList().Sum(r => r.Quantidade);

            var estoquesMaterial = _estoqueMaterialRepository.Find(r => r.Material.Id == idMaterial);
            var quantidadeEstoque = estoquesMaterial.ToList().Sum(e => e.Quantidade);

            return quantidadeEstoque - quantidadeReserva;
        }

        #endregion

        #region PopulateConsumoMaterialProgramado
        protected void PopulateConsumoMaterialProgramado(ConsumoMaterialProgramadoModel model)
        {
            var remessaProducao = _remessaProducaoRepository.Find().OrderBy(p => p.Descricao).ToList();
            ViewData["RemessaProducao"] = remessaProducao.ToSelectList("Descricao", model.RemessaProducao);
            
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
            
            ViewBag.OrdenarPor = new SelectList(ColunasConsumoMaterialProgramado, "value", "key");
        }
        #endregion
    }
}