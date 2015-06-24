using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.Producao;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Reporting.Producao;
using Fashion.ERP.Reporting.Producao.Models;
using Fashion.ERP.Web.Areas.Producao.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Producao.Controllers
{
    public partial class RelatorioEstimativaConsumoProgramadoController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IRepository<ProgramacaoProducao> _programacaoProducaoRepository;
        private readonly IRepository<MarcaMaterial> _marcaMaterialRepository;
        private readonly IRepository<Colecao> _colecaoRepository;
        private readonly IRepository<Categoria> _categoriaRepository;
        private readonly IRepository<Subcategoria> _subcategoriaRepository;
        private readonly IRepository<EstoqueMaterial> _estoqueMaterialRepository;
        private readonly IRepository<ReservaEstoqueMaterial> _reservaEstoqueMaterialRepository;
        private readonly IRepository<PedidoCompraItem> _pedidoCompraItemRepository;
        private readonly IRepository<Material> _materialRepository;
        
        #region Valores agrupamento e ordenação
        private static readonly Dictionary<string, string> ColunasAgrupamento = new Dictionary<string, string>
        {
            {"Coleção", "Colecao"},
            {"Categoria", "Categoria"},
            {"Subcategoria", "Subcategoria"},
            {"Marca", "Marca"} 
        };

        private static readonly Dictionary<string, string> ColunasOrdenacao = new Dictionary<string, string>
        {
            {"Descrição", "DescricaoMaterial"},
            {"Referência", "ReferenciaMaterial"}
        };
        #endregion
                
        #region Construtores
        public RelatorioEstimativaConsumoProgramadoController(ILogger logger, 
            IRepository<MarcaMaterial> marcaMaterialRepository, IRepository<Colecao> colecaoRepository, 
            IRepository<Categoria> categoriaRepository, IRepository<Subcategoria> subcategoriaRepository,
            IRepository<EstoqueMaterial> estoqueMaterialRepository, IRepository<ReservaEstoqueMaterial> reservaMaterialRepository,
            IRepository<PedidoCompraItem> pedidoCompraItemRepository, IRepository<ProgramacaoProducao> programacaoProducaoRepository,
            IRepository<Material> materialRepository)
        {
            _logger = logger;
            _colecaoRepository = colecaoRepository;
            _categoriaRepository = categoriaRepository;
            _subcategoriaRepository = subcategoriaRepository;
            _marcaMaterialRepository = marcaMaterialRepository;
            _estoqueMaterialRepository = estoqueMaterialRepository;
            _reservaEstoqueMaterialRepository = reservaMaterialRepository;
            _pedidoCompraItemRepository = pedidoCompraItemRepository;
            _programacaoProducaoRepository = programacaoProducaoRepository;
            _materialRepository = materialRepository;
        }

        #endregion

        #region EstimativaConsumoProgramado
        [PopulateViewData("PopulateEstimativaConsumoProgramado")]
        public virtual ActionResult EstimativaConsumoProgramado()
        {
            var model = new EstimativaConsumoProgramadoModel
            {
                DataFinal = DateTime.Now,
                DataInicial = DateTime.Now.AddMonths(-3),
                AgruparPor = "Colecao"
            };

            return View(model);
        }

        [HttpPost, AjaxOnly, PopulateViewData("PopulateEstimativaConsumoProgramado")]
        public virtual JsonResult EstimativaConsumoProgramado(EstimativaConsumoProgramadoModel model)
        {
            var queryMateriaisConsumo = _programacaoProducaoRepository.Find().SelectMany(x => x.FichaTecnica.MateriaisConsumo,
                        (x, s) => new { ProgramacaoProducao = x, MaterialFichaTecnica = s });
            
            var queryMateriaisConsumoVariacao = _programacaoProducaoRepository.Find().SelectMany(x => x.FichaTecnica.MateriaisConsumoVariacao,
                        (x, s) => new { ProgramacaoProducao = x, MaterialFichaTecnica = s });
            
            var filtros = new StringBuilder();

            if (model.DataInicial.HasValue)
            {
                queryMateriaisConsumo = queryMateriaisConsumo.Where(p => p.ProgramacaoProducao.DataProgramada >= model.DataInicial.Value);
                queryMateriaisConsumoVariacao = queryMateriaisConsumoVariacao.Where(p => p.ProgramacaoProducao.DataProgramada >= model.DataInicial.Value);
                filtros.AppendFormat("Data programada a partir de: {0:dd/MM/yyyy}, ", model.DataInicial.Value);
            }

            if (model.DataFinal.HasValue)
            {
                queryMateriaisConsumo = queryMateriaisConsumo.Where(p => p.ProgramacaoProducao.DataProgramada <= model.DataFinal.Value);
                queryMateriaisConsumoVariacao = queryMateriaisConsumoVariacao.Where(p => p.ProgramacaoProducao.DataProgramada <= model.DataFinal.Value);
                filtros.AppendFormat("Data programada até: {0:dd/MM/yyyy}, ", model.DataFinal.Value);
            }

            if (model.ColecaoAprovada.HasValue)
            {
                queryMateriaisConsumo = queryMateriaisConsumo.Where(p => p.ProgramacaoProducao.Colecao.Id == model.ColecaoAprovada);
                queryMateriaisConsumoVariacao = queryMateriaisConsumoVariacao.Where(p => p.ProgramacaoProducao.Colecao.Id == model.ColecaoAprovada);
                filtros.AppendFormat("Coleção Aprovada: {0}, ", _colecaoRepository.Get(model.ColecaoAprovada.Value).Descricao);
            }

            if (!model.Marcas.IsNullOrEmpty())
            {
                queryMateriaisConsumo = queryMateriaisConsumo.Where(p => model.Marcas.Contains(p.MaterialFichaTecnica.Material.MarcaMaterial.Id ?? 0));
                queryMateriaisConsumoVariacao = queryMateriaisConsumoVariacao.Where(p => model.Marcas.Contains(p.MaterialFichaTecnica.Material.MarcaMaterial.Id ?? 0));

                var marcas = _marcaMaterialRepository.Find(m => model.Marcas.Contains(m.Id ?? 0));
                filtros.AppendFormat("Marcas(s): {0}, ", marcas.Select(c => c.Nome).ToList().Join(","));
            }

            if (model.Material.HasValue)
            {
                queryMateriaisConsumo = queryMateriaisConsumo.Where(p => model.Material == p.MaterialFichaTecnica.Material.Id);
                queryMateriaisConsumoVariacao = queryMateriaisConsumoVariacao.Where(p => model.Material == p.MaterialFichaTecnica.Material.Id);

                var materialDomain = _materialRepository.Find(m => model.Material == m.Id).FirstOrDefault();
                filtros.AppendFormat("Referência do Material: {0}, ", materialDomain.Referencia);
            }

            if (!string.IsNullOrWhiteSpace(model.Tag))
            {
                queryMateriaisConsumo = queryMateriaisConsumo.Where(p => model.Tag == p.ProgramacaoProducao.FichaTecnica.Tag);
                queryMateriaisConsumoVariacao = queryMateriaisConsumoVariacao.Where(p => model.Tag == p.ProgramacaoProducao.FichaTecnica.Tag);
                filtros.AppendFormat("Tag: {0}, ", model.Tag);
            }

            if (model.Ano.HasValue)
            {
                queryMateriaisConsumo = queryMateriaisConsumo.Where(p => model.Ano == p.ProgramacaoProducao.FichaTecnica.Ano);
                queryMateriaisConsumoVariacao = queryMateriaisConsumoVariacao.Where(p => model.Ano == p.ProgramacaoProducao.FichaTecnica.Ano);
                filtros.AppendFormat("Ano: {0}, ", model.Ano);
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
                    queryMateriaisConsumo = queryMateriaisConsumo.Where(p => model.Subcategorias.Contains(p.MaterialFichaTecnica.Material.Subcategoria.Id ?? 0L)
                        || categorias.Contains(p.MaterialFichaTecnica.Material.Subcategoria.Categoria.Id ?? 0L));
                    queryMateriaisConsumoVariacao = queryMateriaisConsumoVariacao.Where(p => model.Subcategorias.Contains(p.MaterialFichaTecnica.Material.Subcategoria.Id ?? 0L)
                        || categorias.Contains(p.MaterialFichaTecnica.Material.Subcategoria.Categoria.Id ?? 0L));

                    filtros.AppendFormat("Subcategoria: {0}, ", subcategoriasDomain.Select(s => s.Nome).ToList().Join(","));
                }
                else
                {
                    // Se não existe subcategoria, selecionar todas as categorias selecionadas na tela
                    queryMateriaisConsumo = queryMateriaisConsumo.Where(p => categorias.Contains(p.MaterialFichaTecnica.Material.Subcategoria.Categoria.Id ?? 0L));
                    queryMateriaisConsumoVariacao = queryMateriaisConsumoVariacao.Where(p => categorias.Contains(p.MaterialFichaTecnica.Material.Subcategoria.Categoria.Id ?? 0L));
                }
            }
         
            var resultMateriaisConsumo = queryMateriaisConsumo.Select(q => new
            {
                ReferenciaMaterial = q.MaterialFichaTecnica.Material.Referencia,
                IdMaterial = q.MaterialFichaTecnica.Material.Id,
                DescricaoMaterial = q.MaterialFichaTecnica.Material.Descricao,
                DataUltimoCusto = ObtenhaDataUltimoCusto(q.MaterialFichaTecnica.Material),
                UltimoCusto = ObtenhaUltimoCusto(q.MaterialFichaTecnica.Material),
                DataProgramacao = q.ProgramacaoProducao.DataProgramada.Date,
                UnidadeMedida = q.MaterialFichaTecnica.Material.UnidadeMedida.Sigla,
                Marca = q.MaterialFichaTecnica.Material.MarcaMaterial.Nome,
                Colecao = q.ProgramacaoProducao.Colecao.Descricao,
                Categoria = q.MaterialFichaTecnica.Material.Subcategoria.Categoria.Nome,
                Subcategoria = q.MaterialFichaTecnica.Material.Subcategoria.Nome,
                QuantidadeAprovada = (q.MaterialFichaTecnica.Quantidade * q.ProgramacaoProducao.Quantidade) * q.MaterialFichaTecnica.Material.UnidadeMedida.FatorMultiplicativo
            }).ToList();

            var resultMateriaisConsumoVariacao = queryMateriaisConsumoVariacao.Select(q => new
            {
                ReferenciaMaterial = q.MaterialFichaTecnica.Material.Referencia,
                IdMaterial = q.MaterialFichaTecnica.Material.Id,
                DescricaoMaterial = q.MaterialFichaTecnica.Material.Descricao,
                DataUltimoCusto = ObtenhaDataUltimoCusto(q.MaterialFichaTecnica.Material),
                UltimoCusto = ObtenhaUltimoCusto(q.MaterialFichaTecnica.Material),
                DataProgramacao = q.ProgramacaoProducao.DataProgramada.Date,
                UnidadeMedida = q.MaterialFichaTecnica.Material.UnidadeMedida.Sigla,
                Marca = q.MaterialFichaTecnica.Material.MarcaMaterial.Nome,
                Colecao = q.ProgramacaoProducao.Colecao.Descricao,
                Categoria = q.MaterialFichaTecnica.Material.Subcategoria.Categoria.Nome,
                Subcategoria = q.MaterialFichaTecnica.Material.Subcategoria.Nome,
                QuantidadeAprovada = (q.MaterialFichaTecnica.Quantidade * q.ProgramacaoProducao.Quantidade) * q.MaterialFichaTecnica.Material.UnidadeMedida.FatorMultiplicativo
            }).ToList();

            var resultadoUnido = resultMateriaisConsumo.Union(resultMateriaisConsumoVariacao);


            if (model.AgruparPor == null)
            {
                model.AgruparPor = "Colecao";
            }

            if (model.OrdenarPor != null)
            {
                resultadoUnido = model.OrdenarEm == "asc"
                    ? resultadoUnido.AsQueryable().OrderBy(model.AgruparPor).ThenBy(model.OrdenarPor)
                    : resultadoUnido.AsQueryable().OrderBy(model.AgruparPor).ThenByDescending(model.OrdenarPor);
            }
            else
                resultadoUnido =
                    resultadoUnido.AsQueryable().OrderBy(model.AgruparPor)
                        .ThenBy("DescricaoMaterial");

            
            var resultadoAgrupado = resultadoUnido.GroupBy(x => new { x.Colecao }, (chave1, grupo1) => new { Valor = chave1.Colecao, NomeAgrupamento = "Coleção", grupo1 });

            if (model.AgruparPor == "Categoria")
            {
                resultadoAgrupado = resultadoUnido.GroupBy(x => new { x.Categoria }, (chave1, grupo1) => new { Valor = chave1.Categoria, NomeAgrupamento = "Categoria", grupo1 });
            }
            else if (model.AgruparPor == "Marca")
            {
                resultadoAgrupado = resultadoUnido.GroupBy(x => new { x.Marca }, (chave1, grupo1) => new { Valor = chave1.Marca, NomeAgrupamento = "Marca", grupo1 });
            }
            else if (model.AgruparPor == "Subcategoria")
            {
                resultadoAgrupado = resultadoUnido.GroupBy(x => new {x.Subcategoria}, (chave1, grupo1) => new {Valor = chave1.Subcategoria, NomeAgrupamento = "Subcategoria", grupo1});
            }
            
            var resultadoFinal = resultadoAgrupado.Select(x => new AgrupamentoEstimativaConsumoProgramadoModel
            {
                Valor = x.Valor,
                NomeAgrupamento = x.NomeAgrupamento,
                Materiais = x.grupo1.GroupBy(y => new { y.IdMaterial, y.ReferenciaMaterial, y.DescricaoMaterial, y.UnidadeMedida, y.UltimoCusto, y.DataUltimoCusto }, (chave2, grupo2) =>
                    new MaterialEstimativaConsumoProgramadoModel()
                    {
                        Referencia = chave2.ReferenciaMaterial,
                        Descricao = chave2.DescricaoMaterial,
                        UnidadeMedida = chave2.UnidadeMedida,
                        DataUltimoCusto = chave2.DataUltimoCusto,
                        UltimoCusto = chave2.UltimoCusto,
                        QuantidadeDisponivel = ObtenhaQuantidadeDisponivel(chave2.IdMaterial.Value),
                        QuantidadeReservada = ObtenhaQuantidadeReservada(chave2.IdMaterial.Value),
                        QuantidadeEstoque = ObtenhaQuantidadeEstoque(chave2.IdMaterial.Value),
                        QuantidadeCompras = ObtenhaQuantidadeCompras(chave2.IdMaterial.Value),
                        Programacoes = grupo2.GroupBy(y => new { y.DataProgramacao }, (chave3, grupo3) =>
                            new ProgramacaoEstimativaConsumoProgramadoModel()
                            {
                                Data = chave3.DataProgramacao,
                                QuantidadeAprovada = grupo3.Sum(z => z.QuantidadeAprovada)
                            }).OrderBy(o => o.Data)
                    }),
            });
            
            if (!resultadoFinal.Any())
                return Json(new { Error = "Nenhum item foi encontrado." });

            var report = new EstimativaConsumoProgramadoReport { DataSource = resultadoFinal };
            
            if (filtros.Length > 2)
                report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

            var filename = report.ToByteStream().SaveFile(".pdf");

            return Json(new { Url = filename });
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

        #region PopulateEstimativaConsumoProgramado
        protected void PopulateEstimativaConsumoProgramado(EstimativaConsumoProgramadoModel model)
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

            ViewBag.AgruparPor = new SelectList(ColunasAgrupamento, "value", "key");
            ViewBag.OrdenarPor = new SelectList(ColunasOrdenacao, "value", "key");
        }
        #endregion
    }
}