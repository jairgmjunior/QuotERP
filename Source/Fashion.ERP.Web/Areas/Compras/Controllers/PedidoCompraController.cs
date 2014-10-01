﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Reporting.Compras;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Compras.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Microsoft.Ajax.Utilities;
using NHibernate.Linq;
using Ninject.Extensions.Logging;
using Telerik.Reporting;

namespace Fashion.ERP.Web.Areas.Compras.Controllers
{
    public partial class PedidoCompraController : BaseController
    {
        #region Variaveis

        private readonly IRepository<PedidoCompra> _pedidoCompraRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<UnidadeMedida> _unidadeMedidaRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<Prazo> _prazoRepository;
        private readonly IRepository<MeioPagamento> _meioPagamentoRepository;
        private readonly IRepository<PedidoCompraItem> _pedidoCompraItemRepository;
        private readonly IRepository<MotivoCancelamentoPedidoCompra> _motivoCancelamentoPedidoCompraRepository;
        
        
        private readonly ILogger _logger;
        private readonly string[] _tipoRelatorio = {"Detalhado", "Listagem", "Sintético"};

        private static readonly Dictionary<string, string> ColunasPesquisaPedidoCompra = new Dictionary<string, string>
            {
                {"Comprador", "Comprador.Nome"},
                {"Data compra", "DataCompra"},
                {"Data entrega", "DataEntrega"},
                {"Fornecedor", "Fornecedor.Nome"},
                {"Número", "Numero"},
                {"Valor compra", "ValorCompra"},
            };

        #endregion

        #region Construtores

        public PedidoCompraController(ILogger logger, IRepository<PedidoCompra> pedidoCompraRepository,
            IRepository<Material> materialRepository, IRepository<UnidadeMedida> unidadeMedidaRepository,
            IRepository<Pessoa> pessoaRepository, IRepository<Prazo> prazoRepository,
            IRepository<MeioPagamento> meioPagamentoRepository, IRepository<Usuario> usuarioRepository)
        {
            _pedidoCompraRepository = pedidoCompraRepository;
            _materialRepository = materialRepository;
            _unidadeMedidaRepository = unidadeMedidaRepository;
            _pessoaRepository = pessoaRepository;
            _prazoRepository = prazoRepository;
            _meioPagamentoRepository = meioPagamentoRepository;
            _logger = logger;
        }

        #endregion

        #region Index

        [PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index()
        {
            var pedidoCompras = _pedidoCompraRepository.Find();

            var model = new PesquisaPedidoCompraModel {ModoConsulta = "Listar"};

            model.Grid = pedidoCompras.Select(p => new GridPedidoCompraModel
            {
                Id = p.Id.GetValueOrDefault(),
                Comprador = p.Comprador.Nome,
                DataCompra = p.DataCompra,
                Fornecedor = p.Fornecedor.Nome,
                Numero = p.Numero,
                ValorCompra = p.ValorCompra,
                Autorizado = p.Autorizado,
                SituacaoCompra = p.SituacaoCompra
            }).ToList();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index(PesquisaPedidoCompraModel model)
        {
            var pedidoCompras = _pedidoCompraRepository.Find();

            try
            {
                #region Filtros

                var filtros = new StringBuilder();

                if (model.UnidadeEstocadora.HasValue)
                {
                    pedidoCompras = pedidoCompras.Where(p => p.UnidadeEstocadora.Id == model.UnidadeEstocadora);
                    filtros.AppendFormat("Unidade estocadora: {0}, ",
                                         _pessoaRepository.Get(model.UnidadeEstocadora.Value).Nome);
                }

                if (model.Numero.HasValue)
                {
                    pedidoCompras = pedidoCompras.Where(p => p.Numero == model.Numero);
                    filtros.AppendFormat("Número: {0}, ", model.Numero.Value);
                }

                if (model.Fornecedor.HasValue)
                {
                    pedidoCompras = pedidoCompras.Where(p => p.Fornecedor.Id == model.Fornecedor);
                    filtros.AppendFormat("Fornecedor: {0}, ", _pessoaRepository.Get(model.Fornecedor.Value).Nome);
                }

                if (model.SituacaoCompra.HasValue)
                {
                    pedidoCompras = pedidoCompras.Where(p => p.SituacaoCompra == model.SituacaoCompra);
                    filtros.AppendFormat("Situação: {0}, ", model.SituacaoCompra.Value.EnumToString());
                }

                if (model.DataCompraInicio.HasValue && model.DataCompraFim.HasValue)
                {
                    pedidoCompras = pedidoCompras.Where(p => p.DataCompra.Date >= model.DataCompraInicio.Value
                                                             && p.DataCompra.Date <= model.DataCompraFim.Value);
                    filtros.AppendFormat("Data compra de '{0}' até '{1}', ",
                                         model.DataCompraInicio.Value.ToString("dd/MM/yyyy"),
                                         model.DataCompraFim.Value.ToString("dd/MM/yyyy"));
                }

                if (model.ValorCompraInicio.HasValue && model.ValorCompraFim.HasValue)
                {
                    pedidoCompras = pedidoCompras.Where(p => p.ValorCompra >= model.ValorCompraInicio.Value
                                                             && p.ValorCompra <= model.ValorCompraFim.Value);
                    filtros.AppendFormat("Valor compra de '{0}' até '{1}', ",
                                         model.ValorCompraInicio.Value.ToString("C2"),
                                         model.ValorCompraFim.Value.ToString("C2"));
                }

                if (model.PrevisaoFaturamentoInicio.HasValue && model.PrevisaoFaturamentoFim.HasValue)
                {
                    pedidoCompras =
                        pedidoCompras.Where(p => p.PrevisaoFaturamento.Date >= model.PrevisaoFaturamentoInicio.Value
                                                 && p.PrevisaoFaturamento.Date <= model.PrevisaoFaturamentoFim.Value);
                    filtros.AppendFormat("Previsão faturamento de '{0}' até '{1}', ",
                                         model.PrevisaoFaturamentoInicio.Value.ToString("dd/MM/yyyy"),
                                         model.PrevisaoFaturamentoFim.Value.ToString("dd/MM/yyyy"));
                }

                if (model.PrevisaoEntregaInicio.HasValue && model.PrevisaoEntregaFim.HasValue)
                {
                    pedidoCompras = pedidoCompras.Where(p => p.PrevisaoEntrega.Date >= model.PrevisaoEntregaInicio.Value
                                                             && p.PrevisaoEntrega.Date <= model.PrevisaoEntregaFim.Value);
                    filtros.AppendFormat("Data aprovação de '{0}' até '{1}', ",
                                         model.PrevisaoEntregaInicio.Value.ToString("dd/MM/yyyy"),
                                         model.PrevisaoEntregaFim.Value.ToString("dd/MM/yyyy"));
                }

                if (model.Material.HasValue)
                {
                    pedidoCompras = pedidoCompras.Where(p => p.PedidoCompraItens.Any(i => i.Material.Id == model.Material));
                    filtros.AppendFormat("Material: {0}, ", _materialRepository.Get(model.Material.Value).Descricao);
                }

                if (model.Comprador.HasValue)
                {
                    pedidoCompras = pedidoCompras.Where(p => p.Comprador.Id == model.Comprador);
                    filtros.AppendFormat("Comprador: {0}, ", _pessoaRepository.Get(model.Comprador.Value).Nome);
                }

                #endregion

                // Verifica se é uma listagem
                if (model.ModoConsulta == "Listar")
                {
                    if (model.OrdenarPor != null)
                        pedidoCompras = model.OrdenarEm == "asc"
                                            ? pedidoCompras.OrderBy(model.OrdenarPor)
                                            : pedidoCompras.OrderByDescending(model.OrdenarPor);

                    model.Grid = pedidoCompras.Select(p => new GridPedidoCompraModel
                        {
                            Id = p.Id.GetValueOrDefault(),
                            Comprador = p.Comprador.Nome,
                            DataCompra = p.DataCompra,
                            Fornecedor = p.Fornecedor.Nome,
                            Numero = p.Numero,
                            ValorCompra = p.ValorCompra,
                            Autorizado = p.Autorizado,
                            SituacaoCompra = p.SituacaoCompra
                        }).ToList();

                    return View(model);
                }

                // Se não é uma listagem, gerar o relatório
                var result = pedidoCompras
                    .Fetch(p => p.Fornecedor).Fetch(p => p.Comprador)
                    .ToList();

                if (!result.Any())
                    return Json(new {Error = "Nenhum item encontrado."});

                #region Montar Relatório

                var report = new ListaPedidoCompraReport {DataSource = result};

                if (filtros.Length > 2)
                    report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

                var grupo = report.Groups.First(p => p.Name.Equals("Grupo"));

                if (model.AgruparPor != null)
                {
                    grupo.Groupings.Add("=Fields." + model.AgruparPor);

                    var key = ColunasPesquisaPedidoCompra.First(p => p.Value == model.AgruparPor).Key;
                    var titulo = string.Format("= \"{0}: \" + Fields.{1}", key, model.AgruparPor);
                    grupo.GroupHeader.GetTextBox("Titulo").Value = titulo;
                }
                else
                {
                    report.Groups.Remove(grupo);
                }

                if (model.OrdenarPor != null)
                    report.Sortings.Add("=Fields." + model.OrdenarPor,
                                        model.OrdenarEm == "asc" ? SortDirection.Asc : SortDirection.Desc);

                #endregion

                var filename = report.ToByteStream().SaveFile(".pdf");

                return Json(new {Url = filename});
            }
            catch (Exception exception)
            {
                var message = exception.GetMessage();
                _logger.Info(message);

                if (HttpContext.Request.IsAjaxRequest())
                    return Json(new {Error = message});

                ModelState.AddModelError(string.Empty, message);
                return View(model);
            }
        }

        #endregion

        #region Novo

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            var model = new PedidoCompraModel {Numero = ProximoNumero()};

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(PedidoCompraModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<PedidoCompra>(model);

                    // Itens do pedido de compra
                    for (int i = 0; i < model.Materiais.Count; i++)
                    {
                        var idx = i;

                        var unidadeMedida = _unidadeMedidaRepository.Get(model.UnidadeMedidas[idx]);

                        var item = new PedidoCompraItem
                        {
                            Material = _materialRepository.Load(model.Materiais[idx]),
                            UnidadeMedida = unidadeMedida,
                            Quantidade = model.Quantidades[idx],
                            ValorUnitario = model.ValorUnitarios[i],
                            SituacaoCompra = SituacaoCompra.NaoAtendido,
                            PrevisaoEntrega = domain.PrevisaoEntrega
                        };

                        domain.AddPedidoCompraItem(item);
                    }

                    _pedidoCompraRepository.Save(domain);

                    this.AddSuccessMessage("Pedido de compra cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty,
                                             "Não é possível salvar o pedido de compra. Confira se os dados foram informados corretamente: " +
                                             exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }

        #endregion
        
        #region Editar

		[ImportModelStateFromTempData, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(long id)
        {
            var domain = _pedidoCompraRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<PedidoCompraModel>(domain);

                foreach (var item in domain.PedidoCompraItens)
                {
                    model.PedidoCompraItens.Add(item.Id);
                    model.Materiais.Add(item.Material.Id.GetValueOrDefault());
                    model.UnidadeMedidas.Add(item.UnidadeMedida.Id.GetValueOrDefault());
                    model.Quantidades.Add(item.Quantidade);
                    model.ValorUnitarios.Add(item.ValorUnitario);
                    model.ValorTotais.Add(item.ValorUnitario * item.Quantidade);
                    model.SituacaoCompras.Add(item.SituacaoCompra);
                }

                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o pedido de compra.");
            return RedirectToAction("Index");
        }


        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(PedidoCompraModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _pedidoCompraRepository.Get(model.Id));

                    // Pesquisar os itens que estão no DB e não estão no model
                    var idsExcluidos = domain.PedidoCompraItens.Select(p => p.Id).Except(model.PedidoCompraItens);
                    var itensExcluidos = domain.PedidoCompraItens.Where(p => idsExcluidos.Contains(p.Id)).ToArray();
                    domain.RemovePedidoCompraItem(itensExcluidos);

                    // Adicionar itens do pedido de compra
                    for (int i = 0; i < model.Materiais.Count; i++)
                    {
                        var idx = i;

                        if (model.PedidoCompraItens[idx].HasValue)
                            continue;

                        var unidadeMedida = _unidadeMedidaRepository.Get(model.UnidadeMedidas[idx]);

                        var item = new PedidoCompraItem
                        {
                            Material = _materialRepository.Load(model.Materiais[idx]),
                            UnidadeMedida = unidadeMedida,
                            Quantidade = model.Quantidades[idx],
                            ValorUnitario = model.ValorUnitarios[i],
                            SituacaoCompra = SituacaoCompra.NaoAtendido,
                            PrevisaoEntrega = domain.PrevisaoEntrega
                        };

                        domain.AddPedidoCompraItem(item);
                    }
                    //verifica se pedido de compra foi autorizado --jamyl
                    if (domain.Autorizado.Equals(true))
                    {
                        _pedidoCompraRepository.Evict(domain);

                        this.AddErrorMessage("Pedido de Compra já autorizado. Exclusão/Alteração não permitida.");
                        return RedirectToAction("Index");
                    }

                    if (domain.Autorizado.Equals(false))
                    {
                        _pedidoCompraRepository.Update(domain);

                        this.AddSuccessMessage("Pedido de compra atualizado com sucesso.");
                        return RedirectToAction("Index");
                    }
                   
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o pedido de compra. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }
        #endregion

        #region Excluir

        [HttpPost, ValidateAntiForgeryToken, ExportModelStateToTempData]
        public virtual ActionResult Excluir(long? id)
        {
			if (ModelState.IsValid)
            {
				try
				{
                    
					var domain = _pedidoCompraRepository.Get(id);
                    //verifica se pedido de compra foi autorizado para exclusão --jamyl
				    if (domain.Autorizado.Equals(true))
				    {
                        _pedidoCompraRepository.Evict(domain);

                        this.AddErrorMessage("Pedido de Compra já autorizado. Exclusão/Alteração não permitida.");
                        return RedirectToAction("Index");
				    }

				    if (domain.Autorizado.Equals(false))
				    {

				        _pedidoCompraRepository.Delete(domain);

				        this.AddSuccessMessage("Pedido de compra excluído com sucesso");
				        return RedirectToAction("Index");
				    }
				}
				catch (Exception exception)
				{
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao excluir o pedido de compra: " + exception.Message);
					_logger.Info(exception.GetMessage());
				}
			}

			return RedirectToAction("Editar", new { id });

        }
        #endregion

        #region PesquisarReferenciaExterna
        [HttpGet, AjaxOnly]
        public virtual ActionResult PesquisarReferenciaExterna(string referenciaExterna)
        {
            if (string.IsNullOrWhiteSpace(referenciaExterna) == false)
            {
                var material = _materialRepository.Find(p => p.ReferenciaExternas.Any(r => r.Referencia == referenciaExterna) && p.Ativo).FirstOrDefault();

                if (material != null)
                    return Json(new { material.Id, material.Referencia, material.Descricao }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { erro = "Nenhum material encontrado." }, JsonRequestBehavior.AllowGet);
        }
        #endregion		

        //#region PesquisarId
        //[HttpGet, AjaxOnly]
        //public virtual ActionResult PesquisarId(long numero)
        //{
        //    var pedidoCompra = _pedidoCompraRepository.Find(x=> x.Numero == numero).FirstOrDefault();

        //    if (pedidoCompra != null)
        //        return Json(new { cliente.Id, cliente.Funcionario.Codigo, cliente.Nome }, JsonRequestBehavior.AllowGet);

        //    return Json(new { erro = "Nenhum funcionário encontrado." }, JsonRequestBehavior.AllowGet);
        //}
        //#endregion
        
        #region Métodos
		
        #region PopulateViewData
        protected void PopulateViewData(PedidoCompraModel model)
        {
            // UnidadeEstocadora
            var unidades = _pessoaRepository.Find(p => p.Unidade != null && p.Unidade.Ativo).ToList();
            ViewBag.UnidadeEstocadora = unidades.ToSelectList("NomeFantasia", model.UnidadeEstocadora);

            // PrazoDescricao
            var prazos = _prazoRepository.Find(p => p.Ativo).ToList();
            ViewBag.PrazoDescricao = prazos.ToSelectList("Descricao", model.Prazo);

            // MeioPagamento
            var meioPagamentos = _meioPagamentoRepository.Find().ToList();
            ViewBag.MeioPagamento = meioPagamentos.ToSelectList("Descricao", model.Prazo);
            
            // Materiais
            var materiais = _materialRepository.Find();
            ViewBag.MaterialReferenciasDicionario = materiais.Select(c => new { Id = c.Id.GetValueOrDefault(), c.Referencia })
                                                               .ToDictionary(k => k.Id, v => v.Referencia);
            ViewBag.MaterialDescricoesDicionario = materiais.Select(c => new { Id = c.Id.GetValueOrDefault(), c.Descricao })
                                                               .ToDictionary(k => k.Id, v => v.Descricao);

            // Unidade de medida
            var unidadeMedidas = _unidadeMedidaRepository.Find().OrderBy(p => p.Sigla).ToList();
            var unidadeMediasAtivo = unidadeMedidas.Where(p => p.Ativo).ToList();
            ViewData["UnidadeMedida"] = unidadeMediasAtivo.ToSelectList("Sigla");

            var unidadeMedidasSigla = unidadeMedidas.Select(c => new { Id = c.Id.GetValueOrDefault(), c.Sigla }).ToDictionary(k => k.Id, v => v.Sigla);
            ViewBag.UnidadeMedidasDicionario = unidadeMedidasSigla;

            // Situacao
            ViewBag.SituacoesDicionario = ((SituacaoCompra[])typeof(SituacaoCompra).GetEnumValues()).ToDictionary(k => k, e => e.EnumToString());
        }
        #endregion

        #region PopulateViewDataItemCancelado
        protected void PopulateViewDataItemCancelado(PedidoCompraItemCanceladoModel model)
        {


            // UnidadeEstocadora
            var unidades = _pessoaRepository.Find(p => p.Unidade != null && p.Unidade.Ativo).ToList();
            ViewBag.UnidadeEstocadora = unidades.ToSelectList("NomeFantasia", model.UnidadeEstocadora);

            // PrazoDescricao
            var prazos = _prazoRepository.Find(p => p.Ativo).ToList();
            ViewBag.Prazo = prazos.ToSelectList("Descricao", model.Prazo);

            // MeioPagamento
            var meioPagamentos = _meioPagamentoRepository.Find().ToList();
            ViewBag.MeioPagamento = meioPagamentos.ToSelectList("Descricao", model.Prazo);

            //Motivo Cancelamento
            var motivoCancelamento = _motivoCancelamentoPedidoCompraRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewBag.MotivoCancelamento = motivoCancelamento.ToSelectList("Descricao", model.MotivoCancelamento);
            
            // Materiais
            var materiais = _materialRepository.Find();
            ViewBag.CatalogoReferenciasDicionario = materiais.Select(c => new { Id = c.Id.GetValueOrDefault(), c.Referencia })
                                                               .ToDictionary(k => k.Id, v => v.Referencia);
            ViewBag.CatalogoDescricaoDicionario = materiais.Select(c => new { Id = c.Id.GetValueOrDefault(), c.Descricao })
                                                              .ToDictionary(k => k.Id, v => v.Descricao);
            // Unidade de medida
            var unidadeMedidas = _unidadeMedidaRepository.Find().OrderBy(p => p.Sigla).ToList();
            var unidadeMediasAtivo = unidadeMedidas.Where(p => p.Ativo).ToList();
            ViewData["UnidadeMedida"] = unidadeMediasAtivo.ToSelectList("Sigla");
           
            var unidadeMedidasSigla = unidadeMedidas.Select(c => new { Id = c.Id.GetValueOrDefault(), c.Sigla }).ToDictionary(k => k.Id, v => v.Sigla);
            ViewBag.UnidadeMedidasDicionario = unidadeMedidasSigla;

            var quantidadeEntrega = _pedidoCompraItemRepository.Find();
            ViewBag.QuantidadeEntregaDicionario = quantidadeEntrega.Select(c => new { Id = c.Id.GetValueOrDefault(), c.QuantidadeEntrega })
                                                              .ToDictionary(k => k.Id, v => v.QuantidadeEntrega);
            // Situacao
            ViewBag.SituacoesDicionario = ((SituacaoCompra[])typeof(SituacaoCompra).GetEnumValues()).ToDictionary(k => k, e => e.EnumToString());
        }
        #endregion

        #region PopulateViewDataGridPedidoCompraItemCancelado
       
        #endregion

        #region PopulateViewDataPesquisa
        protected void PopulateViewDataPesquisa(PesquisaPedidoCompraModel model)
        {
            // UnidadeEstocadora
            var unidades = _pessoaRepository.Find(p => p.Unidade != null && p.Unidade.Ativo).ToList();
            ViewBag.UnidadeEstocadora = unidades.ToSelectList("NomeFantasia", model.UnidadeEstocadora);

            ViewBag.TipoRelatorio = new SelectList(_tipoRelatorio);
            ViewBag.OrdenarPor = new SelectList(ColunasPesquisaPedidoCompra, "value", "key");
            ViewBag.AgruparPor = new SelectList(ColunasPesquisaPedidoCompra, "value", "key");
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var pedidoCompra = (PedidoCompraModel)model;

            // Validar o número
            if (_pedidoCompraRepository.Find().Any(p => p.Numero == pedidoCompra.Numero && p.Id != pedidoCompra.Id))
                ModelState.AddModelError("Numero", "Já existe um pedido de compra cadastrado com este número.");

            // Validar se tem itens
            if (pedidoCompra.Materiais == null || pedidoCompra.Materiais.Count == 0)
                ModelState.AddModelError("", "Cadastre pelo menos 1 (um) item ao pedido de compra.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
        }
        #endregion

        #region ProximoNumero
        private long ProximoNumero()
        {
            try
            {
                return _pedidoCompraRepository.Find().Max(p => p.Numero) + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }
        #endregion

        #endregion
    }
}


 
