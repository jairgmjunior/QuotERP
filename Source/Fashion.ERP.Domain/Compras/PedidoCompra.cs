using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Fashion.ERP.Domain.Comum;
using System;
using System.Collections.Generic;
using Fashion.Framework.Common.Base;

namespace Fashion.ERP.Domain.Compras
{
    public class PedidoCompra : DomainBase<PedidoCompra>, IPesquisavelPorData
    {
        private readonly IList<PedidoCompraItem> _pedidoCompraItens;

        public PedidoCompra()
        {
            _pedidoCompraItens = new List<PedidoCompraItem>();
        }

        public virtual long Numero { get; set; }
        public virtual DateTime DataCompra { get; set; }
        public virtual DateTime PrevisaoFaturamento { get; set; }
        public virtual DateTime PrevisaoEntrega { get; set; }
        public virtual DateTime DataAlteracao { get; set; }
        public virtual TipoCobrancaFrete TipoCobrancaFrete { get; set; }
        public virtual double ValorFrete { get; set; }
        public virtual double ValorEmbalagem { get; set; }
        public virtual double ValorEncargos { get; set; }
        public virtual string Observacao { get; set; }
        public virtual bool Autorizado { get; set; }
        public virtual DateTime? DataAutorizacao { get; set; }
        public virtual string ObservacaoAutorizacao { get; set; }
        public virtual SituacaoCompra SituacaoCompra { get; set; }
        public virtual string Contato { get; set; }
        public virtual Pessoa Comprador { get; set; }
        public virtual Pessoa FuncionarioAutorizador { get; set; }
        public virtual Pessoa Fornecedor { get; set; }
        public virtual Pessoa UnidadeEstocadora { get; set; }
        public virtual Prazo Prazo { get; set; }
        public virtual MeioPagamento MeioPagamento { get; set; }
        public virtual Pessoa Transportadora { get; set; }
        public virtual double ValorMercadoria { get; set; }
        public virtual double ValorDesconto { get; set; }
        public virtual double ValorCompra { get; set; }
        
        #region pedidoCompraItem

        public virtual IReadOnlyCollection<PedidoCompraItem> PedidoCompraItens
        {
            get { return new ReadOnlyCollection<PedidoCompraItem>(_pedidoCompraItens); }
        }

        public virtual void AddPedidoCompraItem(params PedidoCompraItem[] pedidoCompraItens)
        {
            foreach (var pedidoCompraItem in pedidoCompraItens)
            {
                pedidoCompraItem.PedidoCompra = this;
                _pedidoCompraItens.Add(pedidoCompraItem);
            }
        }

        public virtual void RemovePedidoCompraItem(params PedidoCompraItem[] pedidoCompraItens)
        {
            foreach (var pedidoCompraItem in pedidoCompraItens)
            {
                if (_pedidoCompraItens.Contains(pedidoCompraItem))
                    _pedidoCompraItens.Remove(pedidoCompraItem);
            }
        }

        public virtual void AtualizeSituacao()
        {
            if (_pedidoCompraItens.All(x => x.SituacaoCompra == SituacaoCompra.Cancelado))
            {
                SituacaoCompra = SituacaoCompra.Cancelado;
            }
            else if (_pedidoCompraItens.All(x => x.SituacaoCompra == SituacaoCompra.NaoAtendido || x.SituacaoCompra == SituacaoCompra.Cancelado))
            {
                SituacaoCompra = SituacaoCompra.NaoAtendido;
            }
            else if (_pedidoCompraItens.All(x => x.SituacaoCompra == SituacaoCompra.Cancelado || x.SituacaoCompra == SituacaoCompra.AtendidoTotal))
            {
                SituacaoCompra = SituacaoCompra.AtendidoTotal;
            } 
            else 
            {
                SituacaoCompra = SituacaoCompra.AtendidoParcial;
            } 
        }

        public virtual PedidoCompraItem ObtenhaPedidoCompraItem(string referenciaMaterial)
        {
            try
            {
                return PedidoCompraItens.SingleOrDefault(
                    s => s.Material.Referencia == referenciaMaterial);
            }
            catch (Exception ex)
            {
                throw new Exception("O mesmo material foi cadastrado mais de uma vez no pedido de compra. Referência: " +  referenciaMaterial, ex);
            }
        }

        #endregion
    }
}
