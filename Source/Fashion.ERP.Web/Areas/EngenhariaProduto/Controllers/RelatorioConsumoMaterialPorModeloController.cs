using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Domain.EngenhariaProduto.ObjetosRelatorio;
using Fashion.ERP.Reporting.EngenhariaProduto;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Web.Areas.EngenhariaProduto.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;
using Telerik.Reporting;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers
{
    public partial class RelatorioConsumoMaterialPorModeloController : BaseController
    {
        #region Variaveis
        private readonly ILogger _logger;
        private readonly IRepository<Modelo> _modeloRepository;
        private readonly IRepository<Colecao> _colecaoRepository;
        private readonly IRepository<Classificacao> _classificacaoRepository;
        private readonly IRepository<Categoria> _categoriaRepository;
        private readonly IRepository<Subcategoria> _subcategoriaRepository;
        private readonly IRepository<Familia> _familiaRepository;
        private readonly IRepository<EstoqueMaterial> _estoqueMaterialRepository;
        private readonly IRepository<ReservaEstoqueMaterial> _reservaEstoqueMaterialRepository;
        
        #region ColunasConsumoMaterialPorModelo
        private static readonly Dictionary<string, string> ColunasConsumoMaterialPorModelo = new Dictionary<string, string>
        {
            {"Referência", "Referencia"},
            {"Descrição", "Descricao"}
        };
        #endregion

        #endregion
        
        #region Construtores
        public RelatorioConsumoMaterialPorModeloController(ILogger logger, IRepository<Modelo> modeloRepository,
            IRepository<Colecao> colecaoRepository, IRepository<Classificacao> classificacaoRepository,
            IRepository<Categoria> categoriaRepository, IRepository<Subcategoria> subcategoriaRepository,
            IRepository<Familia> familiaRepository, IRepository<ReservaEstoqueMaterial> reservaEstoqueMaterialRepository,
            IRepository<EstoqueMaterial> estoqueMaterialRepository)
        {
            _logger = logger;
            _modeloRepository = modeloRepository;
            _colecaoRepository = colecaoRepository;;
            _classificacaoRepository = classificacaoRepository;
            _categoriaRepository = categoriaRepository;
            _subcategoriaRepository = subcategoriaRepository;
            _familiaRepository = familiaRepository;
            _reservaEstoqueMaterialRepository = reservaEstoqueMaterialRepository;
            _estoqueMaterialRepository = estoqueMaterialRepository;
        }
        #endregion

        #region ConsumoMaterialPorModelo
        [PopulateViewData("PopulateConsumoMaterialPorModelo")]
        public virtual ActionResult ConsumoMaterialPorModelo()
        {
            var model = new ConsumoMaterialPorModeloModel();

            model.DataFinal = DateTime.Now;
            model.DataInicial = DateTime.Now.AddMonths(-3);

            return View(model);
        }

        [HttpPost, AjaxOnly, PopulateViewData("PopulateConsumoMaterialPorModelo")]
        public virtual JsonResult ConsumoMaterialPorModelo(ConsumoMaterialPorModeloModel model)
        {
            //var query = _modeloRepository.Find().Where(x => x.Aprovado == true && x.ModeloAprovado != null)
            //    .SelectMany(x => x.SequenciaProducoes, (x, s) => new { x, s })
            //    .SelectMany(t => t.s.MaterialComposicaoModelos, (t, m) => new { Modelo = @t.x, MaterialComposicao = m });

            //var filtros = new StringBuilder();

            //if (model.DataInicial.HasValue)
            //{
            //    query = query.Where(p => p.Modelo.ModeloAprovado.Data >= model.DataInicial.Value);
            //    filtros.AppendFormat("Aprovados a partir de: {0:dd/MM/yyyy}, ", model.DataInicial.Value);
            //}

            //if (model.DataFinal.HasValue)
            //{
            //    query = query.Where(p => p.Modelo.ModeloAprovado.Data <= model.DataFinal.Value);
            //    filtros.AppendFormat("Aprovados até: {0:dd/MM/yyyy}, ", model.DataFinal.Value);
            //}

            //if (model.Colecao.HasValue)
            //{
            //    query = query.Where(p => p.Modelo.Colecao.Id == model.Colecao);
            //    filtros.AppendFormat("Coleção: {0}, ", _colecaoRepository.Get(model.Colecao.Value).Descricao);
            //}

            //if (model.Categoria.HasValue)
            //{
            //    query = query.Where(q => q.MaterialComposicao.Material.Subcategoria.Categoria.Id == model.Categoria);
            //    filtros.AppendFormat("Categoria: {0}, ", _categoriaRepository.Get(model.Categoria.Value).Nome);
            //}

            //if (model.Subcategoria.HasValue)
            //{
            //    query = query.Where(q => q.MaterialComposicao.Material.Subcategoria.Id == model.Subcategoria);
            //    filtros.AppendFormat("Subcategoria: {0}, ", _subcategoriaRepository.Get(model.Subcategoria.Value).Nome);
            //}

            //if (model.Familia.HasValue)
            //{
            //    query = query.Where(q => q.MaterialComposicao.Material.Familia.Id == model.Familia);
            //    filtros.AppendFormat("Família: {0}, ", _classificacaoRepository.Get(model.Familia.Value).Descricao);
            //}

            //if (model.ColecaoAprovada.HasValue)
            //{
            //    query = query.Where(p => p.Modelo.ModeloAprovado.Colecao.Id == model.ColecaoAprovada);
            //    filtros.AppendFormat("Coleção Aprovada: {0}, ", _colecaoRepository.Get(model.ColecaoAprovada.Value).Descricao);
            //}

            //var result = query.Select(q => new
            //{
            //    q.Modelo.Id,
            //    q.Modelo.ModeloAprovado.Tag,
            //    q.Modelo.Descricao,
            //    ReferenciaMaterial = q.MaterialComposicao.Material.Referencia,
            //    IdMaterial = q.MaterialComposicao.Material.Id,
            //    DescricaoMaterial = q.MaterialComposicao.Material.Descricao,
            //    QuantidadeAprovada = q.Modelo.ModeloAprovado.Quantidade,
            //    QuantidadeMaterial = q.MaterialComposicao.Quantidade * q.MaterialComposicao.Material.UnidadeMedida.FatorMultiplicativo,
            //    QuantidadeTotalMaterial = (q.MaterialComposicao.Quantidade * q.Modelo.ModeloAprovado.Quantidade) * q.MaterialComposicao.Material.UnidadeMedida.FatorMultiplicativo,
            //    NomeFoto = q.MaterialComposicao.Material.Foto.Nome.GetFileUrl(),
            //    UnidadeMedida = q.MaterialComposicao.Material.UnidadeMedida.Sigla
            //});

            ////Possível problema de estouro de memória, o groupby subsequente é executado em memória
            ////e não no banco de dados.
            //var result2 = result.ToList();

            //var resultadoFinal = result2.GroupBy(x => new { x.IdMaterial, x.ReferenciaMaterial, x.DescricaoMaterial, x.NomeFoto, x.UnidadeMedida }, (chave, grupo) =>
            //    new ConsumoMaterialPorModeloRelatorio
            //    {
            //        Referencia = chave.ReferenciaMaterial,
            //        Descricao = chave.DescricaoMaterial,
            //        NomeFoto = chave.NomeFoto,
            //        UnidadeMedida = chave.UnidadeMedida,
            //        TotalQuantidadeMaterial = grupo.Sum(q => q.QuantidadeMaterial),
            //        TotalQuantidadeAprovada = grupo.Sum(q => q.QuantidadeAprovada),
            //        TotalQuantidadeTotalMaterial = grupo.Sum(q => q.QuantidadeTotalMaterial),
            //        QuantidadeDisponivel = ObtenhaQuantidadeDisponivel(chave.IdMaterial.Value),
            //        Modelos = grupo.Select(w => new ModeloConsumoMaterialRelatorio
            //        {
            //            Descricao = w.Descricao,
            //            Tag = w.Tag,
            //            QuantidadeAprovada = w.QuantidadeAprovada,
            //            QuantidadeMaterial = w.QuantidadeMaterial,
            //            QuantidadeTotalMaterial = w.QuantidadeTotalMaterial
            //        })
            //    }).ToList();

            //if (!query.Any())
            //    return Json(new { Error = "Nenhum item foi encontrado." });

            //var report = new ConsumoMaterialPorModeloReport { DataSource = resultadoFinal };

            //if (filtros.Length > 2)
            //    report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

            //if (model.OrdenarPor != null)
            //    report.Sortings.Add("=Fields." + model.OrdenarPor, model.OrdenarEm == "asc" ? SortDirection.Asc : SortDirection.Desc);

            //var filename = report.ToByteStream().SaveFile(".pdf");

            //return Json(new { Url = filename });
            return Json(new {  });
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

        #region PopulateConsumoMaterialPorModelo
        protected void PopulateConsumoMaterialPorModelo(ConsumoMaterialPorModeloModel model)
        {
            var colecoes = _colecaoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Colecao"] = colecoes.ToSelectList("Descricao", model.Colecao);

            var colecaoAprovada = _colecaoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["ColecaoAprovada"] = colecaoAprovada.ToSelectList("Descricao", model.Colecao);

            var categorias = _categoriaRepository.Find(p => p.Ativo).OrderBy(o => o.Nome).ToList();
            ViewData["Categoria"] = categorias.ToSelectList("Nome", model.Categoria);

            var subcategorias = _subcategoriaRepository.Find(p => p.Categoria.Id == model.Categoria && p.Ativo).OrderBy(o => o.Nome).ToList();
            ViewData["Subcategoria"] = subcategorias.ToSelectList("Nome", model.Subcategoria);

            var familias = _familiaRepository.Find(p => p.Ativo).OrderBy(o => o.Nome).ToList();
            ViewData["Familia"] = familias.ToSelectList("Nome", model.Familia);

            ViewBag.OrdenarPor = new SelectList(ColunasConsumoMaterialPorModelo, "value", "key");
        }
        #endregion
    }
}