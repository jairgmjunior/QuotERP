using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Reporting.Almoxarifado;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Almoxarifado.Models;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using NHibernate.Linq;
using Ninject.Extensions.Logging;
using Telerik.Reporting;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Controllers
{
    public partial class ConsultaController : BaseController
    {
		#region Variaveis
        private readonly IRepository<EstoqueMaterial> _estoqueMaterialRepository;
        private readonly IRepository<ReservaEstoqueMaterial> _reservaEstoqueMaterialRepository;
        private readonly IRepository<Familia> _familiaRepository;
        private readonly IRepository<MarcaMaterial> _marcaMaterialRepository;
        private readonly IRepository<Categoria> _categoriaRepository;
        private readonly IRepository<Subcategoria> _subcategoriaRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<DepositoMaterial> _depositoMaterialRepository;
        private readonly IRepository<MovimentacaoEstoqueMaterial> _movimentacaoEstoqueMaterialRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public ConsultaController(ILogger logger, IRepository<EstoqueMaterial> estoqueMaterialRepository, IRepository<ReservaEstoqueMaterial> reservaEstoqueMaterialRepository,
            IRepository<Familia> familiaRepository, IRepository<MarcaMaterial> marcaMaterialRepository,
            IRepository<Categoria> categoriaRepository, IRepository<Subcategoria> subcategoriaRepository,
            IRepository<Pessoa> pessoaRepository, IRepository<DepositoMaterial> depositoMaterialRepository,
            IRepository<MovimentacaoEstoqueMaterial> movimentacaoEstoqueMaterialRepository )
        {
            _estoqueMaterialRepository = estoqueMaterialRepository;
            _reservaEstoqueMaterialRepository = reservaEstoqueMaterialRepository;
            _familiaRepository = familiaRepository;
            _marcaMaterialRepository = marcaMaterialRepository;
            _categoriaRepository = categoriaRepository;
            _subcategoriaRepository = subcategoriaRepository;
            _pessoaRepository = pessoaRepository;
            _depositoMaterialRepository = depositoMaterialRepository;
            _movimentacaoEstoqueMaterialRepository = movimentacaoEstoqueMaterialRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region ConsultaEstoqueMaterial
        [PopulateViewData("PopulateEstoqueMaterialViewData")]
        public virtual ActionResult EstoqueMaterial()
        {
            var model = new ConsultaEstoqueMaterialModel
            {
                ModoConsulta = "Listar",
                //Categorias = new List<string>(),
                //Subcategorias = new List<string>(),
                //Familias = new List<string>(),
                //Marcas = new List<string>()
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateEstoqueMaterialViewData")]
        public virtual ActionResult EstoqueMaterial(ConsultaEstoqueMaterialModel model)
        {
            var estoqueMateriais = _estoqueMaterialRepository.Find();
            var ehImpressao = model.ModoConsulta == "Imprimir";

            try
            {
                #region Filtros
                var filtros = new StringBuilder();

                if (model.Unidade.HasValue)
                {
                    estoqueMateriais = estoqueMateriais.Where(p => p.DepositoMaterial.Unidade.Id == model.Unidade);

                    if (ehImpressao)
                        filtros.AppendFormat("Unidade: {0}, ", _pessoaRepository.Get(model.Unidade).Nome);
                }

                if (model.DepositoMaterial.HasValue)
                {
                    estoqueMateriais = estoqueMateriais.Where(p => p.DepositoMaterial.Id == model.DepositoMaterial);

                    if (ehImpressao)
                        filtros.AppendFormat("Depósito: {0}, ", _depositoMaterialRepository.Get(model.DepositoMaterial).Nome);
                }

                if (!string.IsNullOrWhiteSpace(model.Referencia))
                {
                    estoqueMateriais = estoqueMateriais.Where(p => p.Material.Referencia == model.Referencia);

                    if (ehImpressao)
                        filtros.AppendFormat("Referência: {0}, ", model.Referencia);
                }

                if (!string.IsNullOrWhiteSpace(model.Descricao))
                {
                    estoqueMateriais = estoqueMateriais.Where(p => p.Material.Descricao.Contains(model.Descricao));

                    if (ehImpressao)
                        filtros.AppendFormat("Descrição: {0}, ", model.Descricao);
                }

                if (model.Categorias.IsNullOrEmpty() == false)
                {
                    var categorias = model.Categorias.ConvertAll(long.Parse);

                    if (ehImpressao)
                    {
                        var categoriasClosure = categorias; // Copiar para variável local
                        var categoriasDomain = _categoriaRepository.Find(c => categoriasClosure.Contains(c.Id ?? 0));
                        filtros.AppendFormat("Categoria: {0}, ", categoriasDomain.Select(c => c.Nome).ToList().Join(","));
                    }
                    
                    // Inserir a subcategoria antes da categoria com 'OR'
                    if (model.Subcategorias.IsNullOrEmpty() == false)
                    {
                        var subcategorias = model.Subcategorias.ConvertAll(long.Parse);
                        var subcategoriasDomain = _subcategoriaRepository.Find(s => subcategorias.Contains(s.Id ?? 0L)).ToList();
                        
                        // Remover o filtro de categoria que já possui subcategoria para não filtrar 2 vezes
                        categorias = categorias.Except(subcategoriasDomain.Select(s => s.Categoria.Id.GetValueOrDefault())).ToList();

                        // Selecionar as subcategorias ou as outras categorias
                        estoqueMateriais = estoqueMateriais.Where(p => subcategorias.Contains(p.Material.Subcategoria.Id ?? 0L)
                            || categorias.Contains(p.Material.Subcategoria.Categoria.Id ?? 0L));

                        if (ehImpressao)
                            filtros.AppendFormat("Subcategoria: {0}, ", subcategoriasDomain.Select(s => s.Nome).ToList().Join(","));
                    }
                    else
                    {
                        // Se não existe subcategoria, selecionar todas as categorias
                        estoqueMateriais = estoqueMateriais.Where(p => categorias.Contains(p.Material.Subcategoria.Categoria.Id ?? 0L));
                    }
                }

                if (model.Marcas.IsNullOrEmpty() == false)
                {
                    var marcas = model.Marcas.ConvertAll(long.Parse);
                    estoqueMateriais = estoqueMateriais.Where(p => marcas.Contains(p.Material.MarcaMaterial.Id ?? 0));

                    if (ehImpressao)
                    {
                        var marcasDomain = _marcaMaterialRepository.Find(m => marcas.Contains(m.Id ?? 0));
                        filtros.AppendFormat("Marca do material: {0}, ", marcasDomain.Select(c => c.Nome).ToList().Join(","));
                    }
                }

                if (model.Familias.IsNullOrEmpty() == false)
                {
                    var familias = model.Familias.ConvertAll(long.Parse);
                    estoqueMateriais = estoqueMateriais.Where(p => familias.Contains(p.Material.Familia.Id ?? 0L));

                    if (ehImpressao)
                    {
                        var familiasDomain = _familiaRepository.Find(m => familias.Contains(m.Id ?? 0));
                        filtros.AppendFormat("Família: {0}, ", familiasDomain.Select(c => c.Nome).ToList().Join(","));
                    }
                }
                #endregion
                
                // Verifica se é uma listagem
                if (model.ModoConsulta == "Listar")
                {
                    if (model.OrdenarPor != null)
                        estoqueMateriais = model.OrdenarEm == "asc"
                            ? estoqueMateriais.OrderBy(model.OrdenarPor)
                            : estoqueMateriais.OrderByDescending(model.OrdenarPor);

                    model.Grid = estoqueMateriais.Select(p => new GridConsultaEstoqueMaterialModel
                    {
                        Id = p.Id.GetValueOrDefault(),
                        Referencia = p.Material.Referencia,
                        Descricao = p.Material.Descricao,
                        UnidadeMedida = p.Material.UnidadeMedida.Descricao,
                        DepositoMaterial = p.DepositoMaterial.Nome,
                        Unidade = p.DepositoMaterial.Unidade.Unidade.Codigo,
                        Saldo = p.Quantidade,
                        MaterialId = p.Material.Id,
                        UnidadeId = p.DepositoMaterial.Unidade.Id
                    }).ToList();

                    foreach (GridConsultaEstoqueMaterialModel g in model.Grid)
                    {
                        long? unidadeId = g.UnidadeId;
                        long? materialId = g.MaterialId;
                        var reservaEstoque =
                            _reservaEstoqueMaterialRepository.Find(r=>r.Unidade.Id == unidadeId.Value 
                            && r.Material.Id == materialId ).FirstOrDefault();
                        if (reservaEstoque != null)
                            g.QtdeReservada = reservaEstoque.Quantidade;
                        else
                            g.QtdeReservada = 0;
                    }

                    return View(model);
                }

                // Se não é uma listagem, gerar o relatório
                var result = estoqueMateriais
                    .Fetch(p => p.Material).ThenFetch(p => p.Familia)
                    .Fetch(p => p.Material).ThenFetch(p => p.Subcategoria).ThenFetch(p => p.Categoria)
                    .Fetch(p => p.Material).ThenFetch(p => p.MarcaMaterial)
                    .Fetch(p => p.Material).ThenFetch(p => p.UnidadeMedida)
                    .Fetch(p => p.DepositoMaterial).ThenFetch(p => p.Unidade)
                    .ToList();

                if (!result.Any())
                    return Json(new { Error = "Nenhum item encontrado." });

                #region Montar Relatório
                Report report = new ListaEstoqueMaterialReport { DataSource = result };

                if (filtros.Length > 2)
                    report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

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

        #region ExtratoItem
        [OutputCache(Duration = 0)]
        public virtual ActionResult ExtratoItem(long id)
        {
            var model = new ExtratoItemModel();

            model.Id = id;
            
            var hoje = DateTime.Now.Date;
            var umMesAtras = hoje.AddMonths(-1);

            model.DataInicial = umMesAtras;
            model.DataFinal = hoje;

            return ExtratoItem(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult ExtratoItem(ExtratoItemModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var estoqueMaterial = _estoqueMaterialRepository.Get(model.Id);
                    model.Unidade = string.Format("{0} - {1}", 
                        estoqueMaterial.DepositoMaterial.Unidade.Unidade.Codigo, 
                        estoqueMaterial.DepositoMaterial.Unidade.Nome);
                    model.Deposito = estoqueMaterial.DepositoMaterial.Nome;
                    model.Material = string.Format("{0} - {1}", 
                        estoqueMaterial.Material.Referencia,
                        estoqueMaterial.Material.Descricao);
                    
                    var dataInicial = model.DataInicial;
                    var dataFinal = model.DataFinal.AddDays(1);
                    
                    model.Grid = _movimentacaoEstoqueMaterialRepository.Find(e => e.Data >= dataInicial && e.Data <= dataFinal
                                    && e.EstoqueMaterial.Material.Id == estoqueMaterial.Material.Id 
                                    && e.EstoqueMaterial.DepositoMaterial.Id == estoqueMaterial.DepositoMaterial.Id)
                                    .Select(e => new GridExtratoItemModel
                                    {
                                        Data = e.Data, 
                                        Entrada = e.TipoMovimentacaoEstoqueMaterial == TipoMovimentacaoEstoqueMaterial.Entrada? e.Quantidade : 0,
                                        Saida = e.TipoMovimentacaoEstoqueMaterial == TipoMovimentacaoEstoqueMaterial.Saida? e.Quantidade : 0,
                                    })
                                .ToList();

                    model.SaldoInicial = estoqueMaterial.ObtenhaSaldo(dataInicial);
                    model.SaldoFinal = estoqueMaterial.ObtenhaSaldo(dataFinal);

                    //model.SaldoInicial =
                    //    Framework.UnitOfWork.Session.Current.GetNamedQuery(StoredProcedure.SaldoEstoqueMaterial)
                    //        .SetParameter("IdMaterial", estoqueMaterial.Material.Id.GetValueOrDefault())
                    //        .SetParameter("IdDepositoMaterial", estoqueMaterial.DepositoMaterial.Id.GetValueOrDefault())
                    //        .SetParameter("Data", dataInicial)
                    //        .UniqueResult<double>();

                    //model.SaldoFinal =
                    //    Framework.UnitOfWork.Session.Current.GetNamedQuery(StoredProcedure.SaldoEstoqueMaterial)
                    //        .SetParameter("IdMaterial", estoqueMaterial.Material.Id.GetValueOrDefault())
                    //        .SetParameter("IdDepositoMaterial", estoqueMaterial.DepositoMaterial.Id.GetValueOrDefault())
                    //        .SetParameter("Data", dataFinal)
                    //        .UniqueResult<double>();

                    model.UnidadeMedida = estoqueMaterial.Material.UnidadeMedida.Descricao;
                    var reservaEstoque =
                            _reservaEstoqueMaterialRepository.Find(r=>r.Unidade.Id == estoqueMaterial.DepositoMaterial.Unidade.Id
                            && r.Material.Id == estoqueMaterial.Material.Id ).FirstOrDefault();
                        if (reservaEstoque != null)
                            model.QtdeReservada = reservaEstoque.Quantidade;
                        else
                            model.QtdeReservada = 0;
                    

                }
                catch (Exception exception)
                {
                    var message = exception.GetMessage();
                    _logger.Info(message);
                }
            }
            return View(model);
        }

        #endregion

        #endregion

		#region Métodos

        #region PopulateEstoqueMaterialViewData
        protected void PopulateEstoqueMaterialViewData(ConsultaEstoqueMaterialModel model)
        {
            var familias = _familiaRepository.Find(f => f.Ativo).ToList();
            ViewBag.Familias = familias.ToSelectList("Nome");

            var marcas = _marcaMaterialRepository.Find(m => m.Ativo).ToList();
            ViewBag.Marcas = marcas.ToSelectList("Nome");

            var categorias = _categoriaRepository.Find(c => c.Ativo).ToList();
            ViewBag.Categorias = categorias.ToSelectList("Nome");

            if (model.Categorias.IsNullOrEmpty())
            {
                ViewBag.Subcategorias = new SelectList(Enumerable.Empty<Subcategoria>(), "Id", "Nome");
            }
            else
            {
                var categoriasId = model.Categorias.ConvertAll(long.Parse);
                var subcategorias = _subcategoriaRepository.Find(p => categoriasId.Contains(p.Categoria.Id ?? 0) && p.Ativo).ToList();
                ViewBag.Subcategorias = subcategorias.ToSelectList("Nome");
            }
            
            var unidades = _pessoaRepository.Find(p => p.Unidade != null && p.Unidade.Ativo).ToList();
            ViewBag.Unidade = unidades.ToSelectList("NomeFantasia");

            if (model.Unidade.HasValue)
            {
                var depositos = _depositoMaterialRepository.Find(d => d.Ativo && d.Unidade.Id == model.Unidade).ToList();
                ViewBag.DepositoMaterial = depositos.ToSelectList("Nome");
            }
            else
            {
                ViewBag.DepositoMaterial = new SelectList(Enumerable.Empty<DepositoMaterial>(), "Id", "Nome");
            }
            
            var colunasConsultaEstoqueMaterial = new Dictionary<string, string>
            {
                {"U.E.", "DepositoMaterial.Unidade.Nome"},
                {"Depósito", "DepositoMaterial.Nome"},
                {"Referência", "Material.Referencia"},
                {"Descrição", "Material.Descricao"},
                {"Un. medida", "Material.UnidadeMedida.Sigla"},
                {"Saldo", "Quantidade"},
            };

            ViewBag.OrdenarPor = new SelectList(colunasConsultaEstoqueMaterial, "value", "key");

            // Hack: preencher as listas para não bugar o componente de multiselect
            if (model.Categorias == null)
                model.Categorias = new List<string>();

            if (model.Subcategorias == null)
                model.Subcategorias = new List<string>();

            if (model.Familias == null)
                model.Familias = new List<string>();

            if (model.Marcas == null)
                model.Marcas = new List<string>();
        }
        #endregion

        #endregion
    }
}