using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Reporting.Compras;
using Fashion.ERP.Reporting.Compras.Models;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Web.Areas.Compras.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;
using NUnit.Framework;
using Telerik.Reporting;

namespace Fashion.ERP.Web.Areas.Compras.Controllers
{
    public partial class RelatorioNecessidadeCompraMaterialController : BaseController
    {        
        private readonly ILogger _logger;
        private readonly IRepository<PedidoCompra> _pedidoCompraRepository;
        private readonly IRepository<ReservaMaterial> _reservaMaterialRepository;
        private readonly IRepository<Categoria> _categoriaRepository;
        private readonly IRepository<Subcategoria> _subcategoriaRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<EstoqueMaterial> _estoqueMaterialRepository;
        private readonly IRepository<ReservaEstoqueMaterial> _reservaEstoqueMaterialRepository;
        private readonly IRepository<Colecao> _colecaoRepository;
        
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
            {"Coleção", "Colecao"},
            {"Fornecedor", "Fornecedor"},
            {"Categoria", "Categoria"},
            {"Subcategoria", "Subcategoria"}
        };
        #endregion
        
        #region Construtores
        public RelatorioNecessidadeCompraMaterialController(ILogger logger, IRepository<PedidoCompra> pedidoCompraRepository,
            IRepository<Categoria> categoriaRepository, IRepository<Subcategoria> subcategoriaRepository,
            IRepository<Material> materialRepository, IRepository<Pessoa> pessoaRepository,
            IRepository<ReservaMaterial> reservaMaterialRepository, IRepository<EstoqueMaterial> estoqueMaterialRepository,
            IRepository<ReservaEstoqueMaterial> reservaEstoqueMaterialRepository, IRepository<Colecao> colecaoRepository)
        {
            _logger = logger;
            _pedidoCompraRepository = pedidoCompraRepository;
            _reservaMaterialRepository = reservaMaterialRepository;
            _categoriaRepository = categoriaRepository;
            _subcategoriaRepository = subcategoriaRepository;
            _materialRepository = materialRepository;
            _pessoaRepository = pessoaRepository;
            _estoqueMaterialRepository = estoqueMaterialRepository;
            _reservaEstoqueMaterialRepository = reservaEstoqueMaterialRepository;
            _colecaoRepository = colecaoRepository;
        }

        #endregion

        #region Relatorio
        [PopulateViewData("PopulateNecessidadeCompraMaterial")]
        public virtual ActionResult NecessidadeCompraMaterial()
        {
            return View(new NecessidadeCompraMaterialModel());
        }

        [HttpPost, AjaxOnly, PopulateViewData("PopulateNecessidadeCompraMaterial")]
        public virtual JsonResult NecessidadeCompraMaterial(NecessidadeCompraMaterialModel model)
        {
            var query = _reservaMaterialRepository.Find(x => x.SituacaoReservaMaterial != SituacaoReservaMaterial.AtendidaTotal)
                .SelectMany(x => x.ReservaMaterialItems, (x, s) => new { ReservaMaterial = x, ReservaMaterialItem = s })
                .Where(x => x.ReservaMaterialItem.SituacaoReservaMaterial != SituacaoReservaMaterial.AtendidaTotal && x.ReservaMaterialItem.SituacaoReservaMaterial != SituacaoReservaMaterial.Cancelada);

            var filtros = new StringBuilder();

            try
            {
                query = query.Where(p => model.Unidade == p.ReservaMaterial.Unidade.Id);

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
                        query = query.Where(p => model.Subcategorias.Contains(p.ReservaMaterialItem.Material.Subcategoria.Id ?? 0L)
                            || categorias.Contains(p.ReservaMaterialItem.Material.Subcategoria.Categoria.Id ?? 0L));

                        filtros.AppendFormat("Subcategoria: {0}, ", subcategoriasDomain.Select(s => s.Nome).ToList().Join(","));
                    }
                    else
                    {
                        // Se não existe subcategoria, selecionar todas as categorias selecionadas na tela
                        query = query.Where(p => categorias.Contains(p.ReservaMaterialItem.Material.Subcategoria.Categoria.Id ?? 0L));
                    }
                }

                if (!model.Colecoes.IsNullOrEmpty())
                {
                    query = query.Where(p => model.Colecoes.Contains(p.ReservaMaterial.Colecao.Id));
                }

                if (model.Material.HasValue)
                {
                    query = query.Where(p => model.Material == p.ReservaMaterialItem.Material.Id);

                    var materialDomain = _materialRepository.Find(m => model.Material == m.Id).FirstOrDefault();
                    filtros.AppendFormat("Referência do Material: {0}, ", materialDomain.Referencia);
                }
                
                if (model.Fornecedor.HasValue)
                {
                    query = query.Where(p => p.ReservaMaterialItem.Material.ReferenciaExternas.Any(r => r.Fornecedor.Id == model.Fornecedor));
                    filtros.AppendFormat("Fornecedor: {0}, ", _pessoaRepository.Get(model.Fornecedor.Value).Nome);
                }

                var resultadoConsulta = query.ToList().Select(q => new
                {
                    Fornecedores = q.ReservaMaterialItem.Material.ReferenciaExternas.Select(x => x.Fornecedor.Nome),
                    q.ReservaMaterialItem.Material.Referencia,
                    q.ReservaMaterialItem.Material.Descricao,
                    IdMaterial = q.ReservaMaterialItem.Material.Id,
                    Categoria = q.ReservaMaterialItem.Material.Subcategoria.Categoria.Nome,
                    Subcategoria = q.ReservaMaterialItem.Material.Subcategoria.Nome,
                    Colecao = q.ReservaMaterial.Colecao != null ? q.ReservaMaterial.Colecao.Descricao : "",
                    UnidadeMedida = q.ReservaMaterialItem.Material.UnidadeMedida.Sigla,
                    QuantidadeReservaCancelada = q.ReservaMaterialItem.ReservaMaterialItemCancelado != null ? q.ReservaMaterialItem.ReservaMaterialItemCancelado.QuantidadeCancelada : 0,
                    q.ReservaMaterialItem.QuantidadeReserva,
                    QuantidadeReservaAtendida = q.ReservaMaterialItem.QuantidadeAtendida,
                    NomeFoto = (q.ReservaMaterialItem.Material.Foto != null ? q.ReservaMaterialItem.Material.Foto.Nome.GetFileUrl() : "")
                });

                IEnumerable<MaterialNecessidadeCompraMaterialModel> resultadoAgrupado = resultadoConsulta.GroupBy(x => new { x.IdMaterial, x.Descricao, x.Referencia, x.Categoria, x.Subcategoria, x.UnidadeMedida, x.NomeFoto, x.Fornecedores, x.Colecao }, (chave, grupo1) =>
                    new MaterialNecessidadeCompraMaterialModel
                    {
                        IdMaterial = chave.IdMaterial,
                        Descricao = chave.Descricao,
                        Referencia = chave.Referencia,
                        Categoria = chave.Categoria,
                        Fornecedores =chave.Fornecedores,
                        Colecao = chave.Colecao,
                        Subcategoria = chave.Subcategoria,
                        UnidadeMedida = chave.UnidadeMedida,
                        NomeFoto = chave.NomeFoto,
                        QuantidadeReserva = grupo1.Sum(g => g.QuantidadeReserva),
                        QuantidadeReservaCancelada = grupo1.Sum(g => g.QuantidadeReservaCancelada),
                        QuantidadeReservaAtendida = grupo1.Sum(g => g.QuantidadeReservaAtendida)
                    });
                
                if (model.AgruparPor == "Fornecedor")
                {
                    var resultadoSemiAgrupadoFornecedor = resultadoAgrupado.SelectMany(x => x.Fornecedores, (x, s) =>
                        new
                        {
                            x.IdMaterial,
                            x.Descricao,
                            x.Referencia,
                            x.Categoria,
                            x.Subcategoria,
                            x.Colecao,
                            x.UnidadeMedida,
                            x.QuantidadeReservaCancelada,
                            x.QuantidadeReserva,
                            x.QuantidadeReservaAtendida,
                            x.NomeFoto,
                            Fornecedor = s
                        });

                    resultadoAgrupado = resultadoSemiAgrupadoFornecedor.GroupBy(x => new { x.Fornecedor, x.IdMaterial, x.Descricao, x.Referencia, x.Categoria, x.Subcategoria, x.UnidadeMedida, x.NomeFoto }, (chave, grupo1) =>
                        new MaterialNecessidadeCompraMaterialModel
                        {
                            IdMaterial = chave.IdMaterial,
                            Descricao = chave.Descricao,
                            Referencia = chave.Referencia,
                            Categoria = chave.Categoria,
                            Fornecedor = chave.Fornecedor,
                            Colecao = "",
                            Subcategoria = chave.Subcategoria,
                            UnidadeMedida = chave.UnidadeMedida,
                            NomeFoto = chave.NomeFoto,
                            QuantidadeReserva = grupo1.Sum(g => g.QuantidadeReserva),
                            QuantidadeReservaCancelada = grupo1.Sum(g => g.QuantidadeReservaCancelada),
                            QuantidadeReservaAtendida = grupo1.Sum(g => g.QuantidadeReservaAtendida)
                        });
                } else if (model.AgruparPor == "Colecao" )
                {
                    resultadoAgrupado = resultadoAgrupado.GroupBy(x => new { x.Colecao, x.IdMaterial, x.Descricao, x.Referencia, x.Categoria, x.Subcategoria, x.UnidadeMedida, x.NomeFoto }, (chave, grupo1) =>
                        new MaterialNecessidadeCompraMaterialModel
                        {
                            IdMaterial = chave.IdMaterial,
                            Descricao = chave.Descricao,
                            Referencia = chave.Referencia,
                            Categoria = chave.Categoria,
                            Fornecedor = "",
                            Colecao = chave.Colecao,
                            Subcategoria = chave.Subcategoria,
                            UnidadeMedida = chave.UnidadeMedida,
                            NomeFoto = chave.NomeFoto,
                            QuantidadeReserva = grupo1.Sum(g => g.QuantidadeReserva),
                            QuantidadeReservaCancelada = grupo1.Sum(g => g.QuantidadeReservaCancelada),
                            QuantidadeReservaAtendida = grupo1.Sum(g => g.QuantidadeReservaAtendida)
                        });

                } else //categoria, subcategoria ou nenhum agrupamento
                {
                    resultadoAgrupado = resultadoAgrupado.GroupBy(x => new { x.IdMaterial, x.Descricao, x.Referencia, x.Categoria, x.Subcategoria, x.UnidadeMedida, x.NomeFoto }, (chave, grupo1) =>
                                    new MaterialNecessidadeCompraMaterialModel
                                    {
                                        IdMaterial = chave.IdMaterial,
                                        Descricao = chave.Descricao,
                                        Referencia = chave.Referencia,
                                        Categoria = chave.Categoria,
                                        Colecao = "",
                                        Subcategoria = chave.Subcategoria,
                                        UnidadeMedida = chave.UnidadeMedida,
                                        NomeFoto = chave.NomeFoto,
                                        QuantidadeReserva = grupo1.Sum(g => g.QuantidadeReserva),
                                        QuantidadeReservaCancelada = grupo1.Sum(g => g.QuantidadeReservaCancelada),
                                        QuantidadeReservaAtendida = grupo1.Sum(g => g.QuantidadeReservaAtendida)
                                    });
                }

                IEnumerable<MaterialNecessidadeCompraMaterialModel> resultadoFinal = resultadoAgrupado.Select(q => 
                    new MaterialNecessidadeCompraMaterialModel
                {
                    IdMaterial = q.IdMaterial,
                    Descricao = q.Descricao,
                    Referencia = q.Referencia,
                    Categoria = q.Categoria,
                    Colecao = q.Colecao,
                    Subcategoria = q.Subcategoria,
                    UnidadeMedida = q.UnidadeMedida,
                    NomeFoto = q.NomeFoto,
                    Fornecedor = q.Fornecedor,
                    QuantidadeReservada = q.QuantidadeReserva - (q.QuantidadeReservaCancelada + q.QuantidadeReservaAtendida), 
                    QuantidadeCompras = ObtenhaQuantidadeCompras(q.IdMaterial, model.Unidade),
                    QuantidadeEstoque = ObtenhaQuantidadeEstoque(q.IdMaterial, model.Unidade)
                }).ToList();

                resultadoFinal = resultadoFinal.Where(x => (x.QuantidadeEstoque + x.QuantidadeCompras) < 0).ToList();

                if (!resultadoFinal.Any())
                    return Json(new { Error = "Nenhum item encontrado." });

                Report report = new NecessidadeCompraMaterialReport { DataSource = resultadoFinal };
                
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

        #region PopulateNecessidadeCompraMaterial
        protected void PopulateNecessidadeCompraMaterial(NecessidadeCompraMaterialModel model)
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

            var colecoes = _colecaoRepository.Find(x => x.Ativo).ToList();
            ViewBag.Colecoes = colecoes.ToSelectList("Descricao");
            
            var unidades = _pessoaRepository.Find(p => p.Unidade != null && p.Unidade.Ativo).ToList();
            ViewBag.Unidade = unidades.ToSelectList("NomeFantasia");
            
            ViewBag.AgruparPor = new SelectList(ColunasAgrupamento, "value", "key");
            ViewBag.OrdenarPor = new SelectList(ColunasOrdenacao, "value", "key");
        }
        #endregion

        //private double QuantidadeReservaCancelada(ReservaMaterialItem reservaMaterialItem)
        //{
        //    return reservaMaterialItem.ReservaMaterialItemCancelado != null
        //        ? reservaMaterialItem.ReservaMaterialItemCancelado.QuantidadeCancelada
        //        : 0;
        //}

        private double ObtenhaQuantidadeEstoque(long? materialId, long? unidadeRequisitada)
        {
            double quantidadesEstoqueMaterial = 0;

            var estoques =
                _estoqueMaterialRepository.Find(y => y.Material.Id == materialId && y.DepositoMaterial.Unidade.Id == unidadeRequisitada);

            if (estoques.Any())
            {
                quantidadesEstoqueMaterial = estoques.Sum(x => x.Quantidade);
            }

            //if (quantidadesEstoqueMaterial == 0)
            //    return 0;

            var reservaEstoque = _reservaEstoqueMaterialRepository.Get(y => y.Material.Id == materialId && y.Unidade.Id == unidadeRequisitada);

            if (reservaEstoque == null)
                return quantidadesEstoqueMaterial;

            return Math.Round(quantidadesEstoqueMaterial - reservaEstoque.Quantidade, 4);
        }

        private double ObtenhaQuantidadeCompras(long? idMaterial, long? idUnidade)
        {
            var queryPedidoCompra = _pedidoCompraRepository.Find(x => (x.SituacaoCompra == SituacaoCompra.NaoAtendido || x.SituacaoCompra == SituacaoCompra.AtendidoParcial) && x.UnidadeEstocadora.Id == idUnidade)
                .SelectMany(x => x.PedidoCompraItens, (x, s) => new { PedidoCompra = x, PedidoCompraItem = s }).Where(x => x.PedidoCompraItem.SituacaoCompra != SituacaoCompra.AtendidoTotal);

            var pedidosCompra = queryPedidoCompra.Where(x => x.PedidoCompraItem.Material.Id == idMaterial).ToList();

            if (pedidosCompra.IsNullOrEmpty())
                return 0;

            //var quantidade = pedidosCompra.Sum(x => x.PedidoCompraItem.ObtenhaDiferenca());
            var quantidade = pedidosCompra.Sum(x => x.PedidoCompraItem.Quantidade);
            var quantidadeEntregue = pedidosCompra.Sum(x => x.PedidoCompraItem.QuantidadeEntrega);
            var quantidadeCancelada = pedidosCompra.Sum(x => x.PedidoCompraItem.PedidoCompraItemCancelado != null ? x.PedidoCompraItem.PedidoCompraItemCancelado.QuantidadeCancelada : 0);

            //return quantidade ;
            return quantidade - (quantidadeEntregue + quantidadeCancelada);
        }

        //private double ObtenhaQuantidadeDisponivelEstoque(long? idMaterial, long? idUnidade)
        //{
        //    return ObtenhaQuantidadeEstoque(idMaterial, idUnidade) + ObtenhaQuantidadeCompras(idMaterial, idUnidade);
        //}

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