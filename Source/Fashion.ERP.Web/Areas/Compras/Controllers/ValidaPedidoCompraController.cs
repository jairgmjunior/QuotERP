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
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Compras.Models;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using NHibernate.Linq;
using Ninject.Extensions.Logging;
using Telerik.Reporting;

namespace Fashion.ERP.Web.Areas.Compras.Controllers
{
    public partial class ValidaPedidoCompraController : BaseController
    {
		#region Variaveis
        private readonly IRepository<PedidoCompra> _pedidoCompraRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<ProcedimentoModuloCompras> _procedimentoModuloCompras;
        private readonly IRepository<Usuario> _usuarioRepository;
        private readonly ILogger _logger;
        private readonly string[] _tipoRelatorio = { "Detalhado", "Listagem", "Sintético" };
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
        public ValidaPedidoCompraController(ILogger logger, IRepository<PedidoCompra> pedidoCompraRepository,
            IRepository<Pessoa> pessoaRepository, IRepository<Material> materialRepository,
            IRepository<ProcedimentoModuloCompras> procedimentoModuloCompras, IRepository<Usuario> usuarioRepository)
        {
            _pedidoCompraRepository = pedidoCompraRepository;
            _pessoaRepository = pessoaRepository;
            _materialRepository = materialRepository;
            _procedimentoModuloCompras = procedimentoModuloCompras;
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }
        #endregion
        
        #region Validar

        [ImportModelStateFromTempData, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Validar(long id)
        {
            var domain = _pedidoCompraRepository.Get(id);
            
            var model = new ValidaPedidoCompraModel
            {
                Id = domain.Id,
                Autorizado = domain.Autorizado,
                UnidadeEstocadora = domain.UnidadeEstocadora.NomeFantasia,
                Numero = domain.Numero,
                Fornecedor = domain.Fornecedor.Nome,
                Comprador = domain.Comprador.Nome,
                Prazo = domain.Prazo == null ? null : domain.Prazo.Descricao,
                Observacao = domain.Observacao,
                SituacaoCompra = domain.SituacaoCompra.EnumToString(),
                DataCompra = domain.DataCompra,
                PrevisaoEntrega = domain.PrevisaoEntrega,
                MeioPagamento = domain.MeioPagamento == null ? null : domain.MeioPagamento.Descricao,
                ValorCompra = domain.ValorCompra
            };

            model.GridItensPedidoCompra = domain.PedidoCompraItens.Select(p => new ValidaPedidoCompraItemModel
            {
                Referencia = p.Material.Referencia,
                Descricao = p.Material.Descricao,
                UnidadeMedida = p.UnidadeMedida.Descricao,
                Quantidade = p.Quantidade,
                ValorUnitario = p.ValorUnitario,
                ValorTotal = p.Quantidade * p.ValorUnitario
            }).ToList();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Validar(ValidaPedidoCompraModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _pedidoCompraRepository.Get(model.Id);

                    var usuario = _usuarioRepository.Find(u => u.Funcionario.Id == model.Funcionario).FirstOrDefault();
                    if (usuario != null)
                    {
                        if (!usuario.Autenticar(model.Assinatura))
                        {
                            ModelState.AddModelError("Assinatura", "A assinatura não confere com a senha do usuário.");
                            return View(model);
                        }
                    }

                    if (domain.Autorizado)
                    {
                        if (domain.SituacaoCompra == SituacaoCompra.AtendidoParcial)
                        {
                            this.AddErrorMessage("Não é possível invalidar um pedido de compra com situação 'Atendido Parcial'.");
                            return View(model);
                        }

                        domain.Autorizado = false;
                        domain.DataAutorizacao = null;
                        domain.ObservacaoAutorizacao = null;
                        this.AddSuccessMessage("Invalidação do pedido de compra realizada com sucesso.");
                    }
                    else
                    {
                        domain.Autorizado = true;
                        domain.DataAutorizacao = DateTime.Now;
                        domain.ObservacaoAutorizacao = model.ObservacaoValidacao;
                        this.AddSuccessMessage("Validação do pedido de compra realizada com sucesso.");
                    }

                    _pedidoCompraRepository.Update(domain);
                    return RedirectToAction("Index", "PedidoCompra");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao validar/invalidar o pedido de compra. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }
        #endregion
        
        #region Métodos

        #region PopulateViewData
        protected void PopulateViewData(ValidaPedidoCompraModel model)
        {
            var funcionarios = _procedimentoModuloCompras.Find(p => p.Codigo == 1).SelectMany(s => s.Funcionarios).ToList();
            ViewBag.Funcionario = funcionarios.ToSelectList("Nome", model.Funcionario);
        }
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

        #endregion
    }
}