using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Reporting.Compras;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Web.Areas.Compras.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;
using Telerik.Reporting;

namespace Fashion.ERP.Web.Areas.Compras.Controllers
{
    public partial class RelatorioMateriaisPedidosCompraController : BaseController
    {        
        private readonly ILogger _logger;
        private readonly IRepository<PedidoCompra> _pedidoCompraRepository;
        private readonly IRepository<Categoria> _categoriaRepository;
        private readonly IRepository<Subcategoria> _subcategoriaRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        
        #region ColunasOrdenacao
        private static readonly Dictionary<string, string> ColunasOrdenacao = new Dictionary<string, string>
        {
            {"Referência", "Referencia"},
            {"Descrição", "Descricao"}
        };
        #endregion

        #region ColunasAgrupamento
        private static readonly Dictionary<string, string> ColunasAgrupamento = new Dictionary<string, string>
        {
            {"Número do Pedido", "Numero"},
            {"Fornecedor", "Fornecedor"},
            {"Categoria", "Categoria"},
            {"Subcategoria", "Subcategoria"},
            {"Data de Entrega", "DataEntrega"}
        };
        #endregion
        
        #region Construtores
        public RelatorioMateriaisPedidosCompraController(ILogger logger, IRepository<PedidoCompra> pedidoCompraRepository,
            IRepository<Categoria> categoriaRepository, IRepository<Subcategoria> subcategoriaRepository,
            IRepository<Material> materialRepository, IRepository<Pessoa> pessoaRepository )
        {
            _logger = logger;
            _pedidoCompraRepository = pedidoCompraRepository;
            _categoriaRepository = categoriaRepository;
            _subcategoriaRepository = subcategoriaRepository;
            _materialRepository = materialRepository;
            _pessoaRepository = pessoaRepository;
        }

        #endregion

        #region Relatorio
        [PopulateViewData("PopulateMateriaisPedidosCompra")]
        public virtual ActionResult MateriaisPedidosCompra()
        {
            return View(new MateriaisPedidosCompraModel());
        }

        [HttpPost, AjaxOnly, PopulateViewData("PopulateMateriaisPedidosCompra")]
        public virtual JsonResult MateriaisPedidosCompra(MateriaisPedidosCompraModel model)
        {
            var query = _pedidoCompraRepository.Find().SelectMany(x => x.PedidoCompraItens, (x, s) => new { PedidoCompra = x, PedidoCompraItem = s });

            var filtros = new StringBuilder();

            try
            {
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
                        query = query.Where(p => model.Subcategorias.Contains(p.PedidoCompraItem.Material.Subcategoria.Id ?? 0L)
                            || categorias.Contains(p.PedidoCompraItem.Material.Subcategoria.Categoria.Id ?? 0L));

                        filtros.AppendFormat("Subcategoria: {0}, ", subcategoriasDomain.Select(s => s.Nome).ToList().Join(","));
                    }
                    else
                    {
                        // Se não existe subcategoria, selecionar todas as categorias selecionadas na tela
                        query = query.Where(p => categorias.Contains(p.PedidoCompraItem.Material.Subcategoria.Categoria.Id ?? 0L));
                    }
                }

                if (model.Material.HasValue)
                {
                    query = query.Where(p => model.Material == p.PedidoCompraItem.Material.Id);

                    var materialDomain = _materialRepository.Find(m => model.Material == m.Id).FirstOrDefault();
                    filtros.AppendFormat("Referência do Material: {0}, ", materialDomain.Referencia);
                }

                if (model.DataEntregaInicial.HasValue)
                {
                    query = query.Where(p => p.PedidoCompraItem.DataEntrega >= model.DataEntregaInicial.Value);
                    filtros.AppendFormat("Data de entrega a partir de: {0:dd/MM/yyyy}, ", model.DataEntregaInicial.Value);
                }

                if (model.DataEntregaFinal.HasValue)
                {
                    query = query.Where(p => p.PedidoCompraItem.DataEntrega <= model.DataEntregaFinal.Value);
                    filtros.AppendFormat("Data de entrega até: {0:dd/MM/yyyy}, ", model.DataEntregaFinal.Value);
                }

                if (model.Numero.HasValue)
                {
                    query = query.Where(p => model.Numero == p.PedidoCompra.Numero);
                    filtros.AppendFormat("Ano: {0}, ", model.Numero);
                }

                if (model.Fornecedor.HasValue)
                {
                    query = query.Where(p => p.PedidoCompra.Fornecedor.Id == model.Fornecedor);
                    filtros.AppendFormat("Fornecedor: {0}, ", _pessoaRepository.Get(model.Fornecedor.Value).Nome);
                }

                if (!model.SituacoesCompra.IsNullOrEmpty())
                {
                    query = query.Where(p => model.SituacoesCompra.Contains(p.PedidoCompraItem.SituacaoCompra));
                    filtros.AppendFormat("Situações: {0}, ", model.SituacoesCompra.Select(s => s.Value.EnumToString()).ToList().Join(","));
                }

                var result = query.Select(q => new
                {
                    q.PedidoCompra.Id,
                    q.PedidoCompra.Numero,
                    Fornecedor = q.PedidoCompra.Fornecedor.Nome,
                    q.PedidoCompraItem.Material.Referencia,
                    q.PedidoCompraItem.Material.Descricao,
                    UnidadeMedida = q.PedidoCompraItem.Material.UnidadeMedida.Sigla,
                    q.PedidoCompraItem.Quantidade,
                    q.PedidoCompraItem.ValorUnitario,
                    ValorTotal = (q.PedidoCompraItem.ValorUnitario * q.PedidoCompraItem.Quantidade) - q.PedidoCompraItem.ValorDesconto,
                    QuantidadeEntregue = q.PedidoCompraItem.QuantidadeEntrega,
                    QuantidadeNaoEntregue = q.PedidoCompraItem.Quantidade - q.PedidoCompraItem.QuantidadeEntrega,
                    Situacao = q.PedidoCompraItem.SituacaoCompra.EnumToString(),
                    q.PedidoCompraItem.DataEntrega,
                    Categoria = q.PedidoCompraItem.Material.Subcategoria.Categoria.Nome,
                    Subcategoria = q.PedidoCompraItem.Material.Subcategoria.Nome
                }).ToList();
                
                if (!result.Any())
                    return Json(new { Error = "Nenhum item encontrado." });
                
                Report report = new MateriaisPedidosCompraReport { DataSource = result };
                
                var grupo = report.Groups.First(p => p.Name.Equals("Grupo"));

                if (model.AgruparPor != null)
                {
                    grupo.Groupings.Add("= FashionErp.AjusteValores(Fields." + model.AgruparPor + ")");

                    var nomeAgrupamento = ColunasAgrupamento.First(p => p.Value == model.AgruparPor).Key;
                    var cabecalhoAgrupamento = ObtenhaCabecalhoAgrupamento(model.AgruparPor);
                    var valorAgrupamento = string.Format("= FashionErp.AjusteValores(Fields.{0})",  model.AgruparPor);
                    
                    grupo.GroupHeader.GetTextBox("CabecalhoAgrupamento").Value = cabecalhoAgrupamento;
                    grupo.GroupHeader.GetTextBox("NomeAgrupamento").Value = nomeAgrupamento;
                    grupo.GroupHeader.GetTextBox("ValorAgrupamento").Value = valorAgrupamento;
                }
                else
                {
                    report.Groups.Remove(grupo);
                }

                if (model.AgruparPor != null)
                    report.Sortings.Add("=Fields." + model.AgruparPor,  SortDirection.Asc);
                
                if (model.OrdenarPor != null)
                    report.Sortings.Add("=Fields." + model.OrdenarPor,
                        model.OrdenarEm == "asc" ? SortDirection.Asc : SortDirection.Desc);
                else
                    report.Sortings.Add("=Fields.Descricao", SortDirection.Asc);
                
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

        #region PopulateMateriaisPedidosCompra
        protected void PopulateMateriaisPedidosCompra(MateriaisPedidosCompraModel model)
        {
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

            ViewBag.AgruparPor = new SelectList(ColunasAgrupamento, "value", "key");
            ViewBag.OrdenarPor = new SelectList(ColunasOrdenacao, "value", "key");
        }
        #endregion

        private string ObtenhaCabecalhoAgrupamento(string agruparPor)
        {
            var nomeAgrupamento = ColunasAgrupamento.First(p => p.Value == agruparPor).Key;
            if (agruparPor == "Numero" || agruparPor == "Fornecedor")
            {
                return "Dados do " + nomeAgrupamento;
            }

            return "Dados da " + nomeAgrupamento;
        }
    }
}