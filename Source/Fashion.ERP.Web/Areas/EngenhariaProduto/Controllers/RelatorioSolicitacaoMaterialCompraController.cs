using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Reporting.EngenhariaProduto;
using Fashion.ERP.Reporting.EngenhariaProduto.Models;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Web.Areas.EngenhariaProduto.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;
using Telerik.Reporting;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers
{
    public partial class RelatorioSolicitacaoMaterialCompraController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IRepository<Modelo> _modeloRepository;
        private readonly IRepository<MarcaMaterial> _marcaMaterialRepository;
        private readonly IRepository<Colecao> _colecaoRepository;
        private readonly IRepository<Categoria> _categoriaRepository;
        private readonly IRepository<Subcategoria> _subcategoriaRepository;
        private readonly IRepository<EstoqueMaterial> _estoqueMaterialRepository;
        private readonly IRepository<ReservaEstoqueMaterial> _reservaEstoqueMaterialRepository;
        private readonly IRepository<PedidoCompraItem> _pedidoCompraItemRepository;
        private readonly IRepository<Material> _materialRepository; 

        #region Valores agrupamento e ordenação
        private static readonly Dictionary<string, string> Colunas = new Dictionary<string, string>
        {
            {"Referência", "MaterialComposicao.Material.Referencia"},
            {"Descrição", "MaterialComposicao.Material.Descricao"} 
        };
        #endregion
                
        #region Construtores
        public RelatorioSolicitacaoMaterialCompraController(ILogger logger, IRepository<Modelo> modeloRepository,
            IRepository<MarcaMaterial> marcaMaterialRepository, IRepository<Colecao> colecaoRepository, 
            IRepository<Categoria> categoriaRepository, IRepository<Subcategoria> subcategoriaRepository,
            IRepository<EstoqueMaterial> estoqueMaterialRepository, IRepository<ReservaEstoqueMaterial> reservaMaterialRepository,
            IRepository<PedidoCompraItem> pedidoCompraItemRepository, IRepository<Material> materialRepository )
        {
            _logger = logger;
            _modeloRepository = modeloRepository;
            _colecaoRepository = colecaoRepository;
            _categoriaRepository = categoriaRepository;
            _subcategoriaRepository = subcategoriaRepository;
            _marcaMaterialRepository = marcaMaterialRepository;
            _estoqueMaterialRepository = estoqueMaterialRepository;
            _reservaEstoqueMaterialRepository = reservaMaterialRepository;
            _pedidoCompraItemRepository = pedidoCompraItemRepository;
            _materialRepository = materialRepository;
        }

        #endregion

        #region SolicitacaoMaterialCompra
        [PopulateViewData("PopulateSolicitacaoMaterialCompra")]
        public virtual ActionResult SolicitacaoMaterialCompra()
        {
            var model = new SolicitacaoMaterialCompraModel
            {
                DataFinal = DateTime.Now,
                DataInicial = DateTime.Now.AddMonths(-3)
            };

            return View(model);
        }

        [HttpPost, AjaxOnly, PopulateViewData("PopulateSolicitacaoMaterialCompra")]
        public virtual JsonResult SolicitacaoMaterialCompra(SolicitacaoMaterialCompraModel model)
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

            //if (model.ColecaoAprovada.HasValue)
            //{
            //    query = query.Where(p => p.Modelo.ModeloAprovado.Colecao.Id == model.ColecaoAprovada);
            //    filtros.AppendFormat("Coleção Aprovada: {0}, ", _colecaoRepository.Get(model.ColecaoAprovada.Value).Descricao);
            //}

            //if (!model.Marcas.IsNullOrEmpty())
            //{
            //    query = query.Where(p => model.Marcas.Contains(p.MaterialComposicao.Material.MarcaMaterial.Id ?? 0));

            //    var colecoesAprovadasDomain = _colecaoRepository.Find(m => model.Marcas.Contains(m.Id ?? 0));
            //    filtros.AppendFormat("Marcas(s): {0}, ", colecoesAprovadasDomain.Select(c => c.Descricao).ToList().Join(","));
            //}

            //if (model.Material.HasValue)
            //{
            //    query = query.Where(p => model.Material == p.MaterialComposicao.Material.Id);

            //    var materialDomain = _materialRepository.Find(m => model.Material == m.Id).FirstOrDefault();
            //    filtros.AppendFormat("Referência do Material: {0}, ", materialDomain.Referencia);
            //}

            //if (!model.Categorias.IsNullOrEmpty())
            //{
            //    var categorias = new List<long?>(model.Categorias);

            //    //var categoriasClosure = categorias; // Copiar para variável local
            //    var categoriasDomain = _categoriaRepository.Find(c => model.Categorias.Contains(c.Id ?? 0));
            //    filtros.AppendFormat("Categoria(s): {0}, ", categoriasDomain.Select(c => c.Nome).ToList().Join(","));

            //    // Inserir a subcategoria antes da categoria com 'OR'
            //    if (model.Subcategorias.IsNullOrEmpty() == false)
            //    {
            //        //var subcategorias = model.Subcategorias.ConvertAll(long.Parse);
            //        var subcategoriasDomain = _subcategoriaRepository.Find(s => model.Subcategorias.Contains(s.Id ?? 0L)).ToList();

            //        // Remover o filtro de categoria que já possui subcategoria para não filtrar 2 vezes
            //        categorias = categorias.Except(subcategoriasDomain.Select(s => s.Categoria.Id)).ToList();

            //        // Selecionar as subcategorias ou as outras categorias
            //        query = query.Where(p => model.Subcategorias.Contains(p.MaterialComposicao.Material.Subcategoria.Id ?? 0L)
            //            || categorias.Contains(p.MaterialComposicao.Material.Subcategoria.Categoria.Id ?? 0L));

            //        filtros.AppendFormat("Subcategoria: {0}, ", subcategoriasDomain.Select(s => s.Nome).ToList().Join(","));
            //    }
            //    else
            //    {
            //        // Se não existe subcategoria, selecionar todas as categorias selecionadas na tela
            //        query = query.Where(p => categorias.Contains(p.MaterialComposicao.Material.Subcategoria.Categoria.Id ?? 0L));
            //    }
            //}

            //if (model.OrdenarPor != null)
            //    query = model.OrdenarEm == "asc"
            //        ? query.OrderBy("MaterialComposicao.Material.MarcaMaterial.Nome").ThenBy(model.OrdenarPor)
            //        : query.OrderBy("MaterialComposicao.Material.MarcaMaterial.Nome").ThenByDescending(model.OrdenarPor);
            //else
            //    query = query.OrderBy("MaterialComposicao.Material.MarcaMaterial.Nome").ThenBy("MaterialComposicao.Material.Descricao");
            
            //var result = query.Select(q => new
            //{
            //    q.Modelo.Id,
            //    q.Modelo.ModeloAprovado.Tag,
            //    q.Modelo.Descricao,
            //    ReferenciaMaterial = q.MaterialComposicao.Material.Referencia,
            //    IdMaterial = q.MaterialComposicao.Material.Id,
            //    DescricaoMaterial = q.MaterialComposicao.Material.Descricao,
            //    DataUltimoCusto = ObtenhaDataUltimoCusto(q.MaterialComposicao.Material),
            //    UltimoCusto = ObtenhaUltimoCusto(q.MaterialComposicao.Material),
            //    DataProgramacao = q.Modelo.ModeloAprovado.DataProgramacaoProducao.Date,
            //    UnidadeMedida = q.MaterialComposicao.Material.UnidadeMedida.Sigla,
            //    Marca = q.MaterialComposicao.Material.MarcaMaterial.Nome,
            //    QuantidadeAprovada = (q.MaterialComposicao.Quantidade * q.Modelo.ModeloAprovado.Quantidade) * q.MaterialComposicao.Material.UnidadeMedida.FatorMultiplicativo
            //});

            ////Possível problema de estouro de memória, o groupby subsequente é executado em memória
            ////e não no banco de dados.
            //var result2 = result.ToList();

            //var resultadoFinal =
            //    result2.GroupBy(x => new {x.Marca}, (chave1, grupo1) =>
            //        new MarcaSolicitacaoMaterialCompraModel
            //        {
            //            Marca = chave1.Marca,
            //            Materiais = grupo1.GroupBy(y => new { y.IdMaterial, y.ReferenciaMaterial, y.DescricaoMaterial, y.UnidadeMedida, y.UltimoCusto, y.DataUltimoCusto}, (chave2, grupo2) =>
            //                new MaterialSolicitacaoMaterialCompraModel
            //                {
            //                    Referencia = chave2.ReferenciaMaterial,
            //                    Descricao = chave2.DescricaoMaterial,
            //                    UnidadeMedida = chave2.UnidadeMedida,
            //                    DataUltimoCusto = chave2.DataUltimoCusto,
            //                    UltimoCusto = chave2.UltimoCusto,
            //                    QuantidadeDisponivel = ObtenhaQuantidadeDisponivel(chave2.IdMaterial.Value),
            //                    QuantidadeReservada =  ObtenhaQuantidadeReservada(chave2.IdMaterial.Value),
            //                    QuantidadeEstoque = ObtenhaQuantidadeEstoque(chave2.IdMaterial.Value),
            //                    QuantidadeCompras = ObtenhaQuantidadeCompras(chave2.IdMaterial.Value),
            //                    Programacoes = grupo2.GroupBy(y => new { y.DataProgramacao }, (chave3, grupo3) =>
            //                        new ProgramacaoSolicitacaoMaterialCompraModel
            //                        {
            //                                Data = chave3.DataProgramacao,
            //                                QuantidadeAprovada = grupo3.Sum(z => z.QuantidadeAprovada)
            //                        }).OrderBy(o => o.Data)
            //                }),
            //        }).ToList();
            

            //if (!query.Any())
            //    return Json(new { Error = "Nenhum item foi encontrado." });

            //var report = new SolicitacaoMaterialCompraReport { DataSource = resultadoFinal };

            //if (filtros.Length > 2)
            //    report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);
            
            //var filename = report.ToByteStream().SaveFile(".pdf");

            return Json(new { });
        }

        public double ObtenhaQuantidadeDisponivel(long idMaterial)
        {
            var quantidadeReservada = ObtenhaQuantidadeReservada(idMaterial);
            
            var quantidadeEstoque = ObtenhaQuantidadeEstoque(idMaterial);

            return quantidadeEstoque - quantidadeReservada;
        }

        public double ObtenhaQuantidadeReservada(long idMaterial)
        {
            var reservasEstoque = _reservaEstoqueMaterialRepository.Find(r => r.Material.Id == idMaterial);
            return reservasEstoque.ToList().Sum(r => r.Quantidade);
        }

        public double ObtenhaQuantidadeEstoque(long idMaterial)
        {
            var estoquesMaterial = _estoqueMaterialRepository.Find(r => r.Material.Id == idMaterial);
            return estoquesMaterial.ToList().Sum(e => e.Quantidade);
        }

        public double ObtenhaUltimoCusto(Material material)
        {
            var custoMaterial = material.CustoMaterials.FirstOrDefault(c => c.Ativo);

            return custoMaterial == null ? 0 : custoMaterial.Custo;
        }

        public DateTime? ObtenhaDataUltimoCusto(Material material)
        {
            var custoMaterial = material.CustoMaterials.FirstOrDefault(c => c.Ativo);

            return custoMaterial == null ? (DateTime?)null : custoMaterial.Data;
        }

        private double ObtenhaQuantidadeCompras(long idMaterial)
        {
            var pedidosCompra = _pedidoCompraItemRepository.Find(x => x.Material.Id == idMaterial &&
                (x.SituacaoCompra == SituacaoCompra.NaoAtendido || x.SituacaoCompra == SituacaoCompra.AtendidoParcial));

            if (pedidosCompra.IsNullOrEmpty())
                return 0;
            
            var quantidade = pedidosCompra.Sum(x => x.Quantidade);
            var quantidadeEntregue = pedidosCompra.Sum(x => x.QuantidadeEntrega);
            
            return quantidade - quantidadeEntregue;
        }

        #endregion

        #region PopulateSolicitacaoMaterialCompra
        protected void PopulateSolicitacaoMaterialCompra(SolicitacaoMaterialCompraModel model)
        {
            var marcas = _marcaMaterialRepository.Find(p => p.Ativo).OrderBy(p => p.Nome).ToList();
            ViewData["Marcas"] = marcas.ToSelectList("Nome");
            
            var colecoesAprovadas = _colecaoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["ColecaoAprovada"] = colecoesAprovadas.ToSelectList("Descricao");

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

            ViewBag.AgruparPor = new SelectList(Colunas, "value", "key");
            ViewBag.OrdenarPor = new SelectList(Colunas, "value", "key");
        }
        #endregion
    }
}