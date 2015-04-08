using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Reporting.EngenhariaProduto;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Web.Areas.EngenhariaProduto.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using FluentNHibernate.Conventions;
using NHibernate.Linq;
using Ninject.Extensions.Logging;
using Telerik.Reporting;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers
{
    public partial class RelatorioMateriaisModelosAprovadosController : BaseController
    {        
        private readonly ILogger _logger;
        private readonly IRepository<Modelo> _modeloRepository;
        private readonly IRepository<Colecao> _colecaoRepository;
        private readonly IRepository<Categoria> _categoriaRepository;
        private readonly IRepository<Subcategoria> _subcategoriaRepository;
        private readonly IRepository<DepartamentoProducao> _departamentoProducaoRepository;

        #region MateriaisModelosAprovados
        private static readonly Dictionary<string, string> ColunasMateriaisModelosAprovados = new Dictionary<string, string>
        {
            {"Coleção", "Colecao.Descricao"},
            {"Coleção Aprovada", "ModeloAprovado.Colecao.Descricao"},
            {"Data de Programação da Produção", "ModeloAprovado.DataProgramacaoProducao"}
        };
        #endregion
        
        #region Construtores
        public RelatorioMateriaisModelosAprovadosController(ILogger logger, IRepository<Modelo> modeloRepository,
            IRepository<Colecao> colecaoRepository, IRepository<Categoria> categoriaRepository, 
            IRepository<Subcategoria> subcategoriaRepository, IRepository<DepartamentoProducao> departamentoProducaoRepository)
        {
            _logger = logger;
            _modeloRepository = modeloRepository;
            _colecaoRepository = colecaoRepository;
            _categoriaRepository = categoriaRepository;
            _subcategoriaRepository = subcategoriaRepository;
            _departamentoProducaoRepository = departamentoProducaoRepository;
        }

        #endregion

        #region Relatorio
        [PopulateViewData("PopulateMateriaisModelosAprovados")]
        public virtual ActionResult MateriaisModelosAprovados()
        {
            return View(new MateriaisModelosAprovadosModel());
        }

        [HttpPost, AjaxOnly, PopulateViewData("PopulateMateriaisModelosAprovados")]
        public virtual JsonResult MateriaisModelosAprovados(MateriaisModelosAprovadosModel model)
        {
            var query =
                _modeloRepository.Find()
                    .Where(x => x.Aprovado == true && x.ModeloAprovado != null);

            var filtros = new StringBuilder();

            try
            {
                if (!model.Colecoes.IsNullOrEmpty())
                {
                    query = query.Where(p => model.Colecoes.Contains(p.Colecao.Id ?? 0));

                    var colecoesDomain = _colecaoRepository.Find(m => model.Colecoes.Contains(m.Id ?? 0));
                    filtros.AppendFormat("Coleção(s): {0}, ", colecoesDomain.Select(c => c.Descricao).ToList().Join(","));
                }

                if (!model.ColecoesAprovadas.IsNullOrEmpty())
                {
                    query = query.Where(p => model.ColecoesAprovadas.Contains(p.ModeloAprovado.Colecao.Id ?? 0));

                    var colecoesAprovadasDomain = _colecaoRepository.Find(m => model.ColecoesAprovadas.Contains(m.Id ?? 0));
                    filtros.AppendFormat("Coleção Aprovada(s): {0}, ", colecoesAprovadasDomain.Select(c => c.Descricao).ToList().Join(","));
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
                        query = query.Where(p => p.SequenciaProducoes.Any(seq =>
                            seq.MaterialComposicaoModelos.Any(materialComposicao => model.Subcategorias.Contains(materialComposicao.Material.Subcategoria.Id ?? 0L)
                            || categorias.Contains(materialComposicao.Material.Subcategoria.Categoria.Id ?? 0L))));

                        filtros.AppendFormat("Subcategoria: {0}, ", subcategoriasDomain.Select(s => s.Nome).ToList().Join(","));
                    }
                    else
                    {
                        // Se não existe subcategoria, selecionar todas as categorias selecionadas na tela
                        query = query.Where(p => p.SequenciaProducoes.Any(seq =>
                            seq.MaterialComposicaoModelos.Any(materialComposicao => categorias.Contains(materialComposicao.Material.Subcategoria.Categoria.Id ?? 0L))));
                    }
                }

                if (model.DataProgramacaoProducao.HasValue)
                {
                    query = query.Where(p => p.ModeloAprovado.DataProgramacaoProducao.Date == model.DataProgramacaoProducao.Value.Date);

                    filtros.AppendFormat("Data de Programação da Produção: {0:dd/MM/yyyy}, ", model.DataProgramacaoProducao.Value.Date);
                }

                if (!string.IsNullOrWhiteSpace(model.Tag))
                {
                    query = query.Where(p => p.ModeloAprovado.Tag.Contains(model.Tag));

                    filtros.AppendFormat("Tag: {0}, ", model.Tag);
                }

                if (!model.DepartamentosProducao.IsNullOrEmpty())
                {
                    query = query.Where(p => p.SequenciaProducoes.Any(seq => model.DepartamentosProducao.Contains(seq.DepartamentoProducao.Id)));

                    var departamentosDomain = _departamentoProducaoRepository.Find(m => model.DepartamentosProducao.Contains(m.Id ?? 0));
                    filtros.AppendFormat("Departamento de produção(s): {0}, ", departamentosDomain.Select(c => c.Nome).ToList().Join(","));
                }

                // Se não é uma listagem, gerar o relatório
                query
                   .FetchMany(x => x.SequenciaProducoes).ThenFetchMany(x => x.MaterialComposicaoModelos)
                   .ThenFetch(x => x.Material).ThenFetch(x => x.Subcategoria);

                var result = query.ToList();

                if (!query.Any())
                    return Json(new { Error = "Nenhum item encontrado." });

                #region Montar Relatório
                Report report = new MateriaisModelosAprovadosReport { DataSource = result };

                if (filtros.Length > 2)
                    report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

                var detailSection = report.Items.First(item => item.Name == "detail") as DetailSection;
                var tableMateriais = detailSection.Items.First(item => item.Name == "tableMateriais") as Table;

                if (model.Categorias.IsEmpty())
                {
                    tableMateriais.Filters[0] = new Telerik.Reporting.Filter("1", FilterOperator.Equal, "1");
                }

                if (model.Subcategorias.IsEmpty())
                {
                    tableMateriais.Filters[1] = new Telerik.Reporting.Filter("1", FilterOperator.Equal, "1");
                }

                report.ReportParameters["Categorias"].Value = model.Categorias.Select(categoria => categoria.Value).ToList();
                report.ReportParameters["Subcategorias"].Value = model.Subcategorias.Select(subcategoria => subcategoria.Value).ToList();
                report.ReportParameters["DepartamentosProducao"].Value = model.DepartamentosProducao.Select(departamentoProducao => departamentoProducao.Value).ToList();

                var grupo = report.Groups.First(p => p.Name.Equals("Grupo"));

                if (model.AgruparPor != null)
                {
                    grupo.Groupings.Add("= FashionErp.AjusteValores(Fields." + model.AgruparPor + ")");

                    var key = ColunasMateriaisModelosAprovados.First(p => p.Value == model.AgruparPor).Key;
                    var titulo = string.Format("= \"{0}: \" + FashionErp.AjusteValores(Fields.{1})", key, model.AgruparPor);
                    grupo.GroupHeader.GetTextBox("Titulo").Value = titulo;
                }
                else
                {
                    report.Groups.Remove(grupo);
                }
                
                if (model.AgruparPor != null)
                    report.Sortings.Add("=Fields." + model.AgruparPor,  SortDirection.Asc);

                report.Sortings.Add("=Fields.ModeloAprovado.Tag", SortDirection.Asc);
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

        #region PopulateMateriaisModelosAprovados
        protected void PopulateMateriaisModelosAprovados(MateriaisModelosAprovadosModel model)
        {
            var colecoes = _colecaoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Colecoes"] = colecoes.ToSelectList("Descricao");

            var departamentosProducao = _departamentoProducaoRepository.Find(p => p.Ativo).OrderBy(p => p.Nome).ToList();
            ViewData["DepartamentosProducao"] = departamentosProducao.ToSelectList("Nome");

            var colecaoAprovada = _colecaoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["ColecoesAprovadas"] = colecaoAprovada.ToSelectList("Descricao");

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

            ViewBag.AgruparPor = new SelectList(ColunasMateriaisModelosAprovados, "value", "key");
        }
        #endregion
    }
}