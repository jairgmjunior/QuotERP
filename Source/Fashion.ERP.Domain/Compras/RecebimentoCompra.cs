using System;
using System.Collections.Generic;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Compras
{
    public class RecebimentoCompra : DomainBase<RecebimentoCompra>
    {
        private IList<ConferenciaEntradaMaterial> _conferenciaEntradaMateriais = new List<ConferenciaEntradaMaterial>();
        private IList<PedidoCompra> _pedidosCompras = new List<PedidoCompra>();
        private IList<RecebimentoCompraItem> _recebimentoCompraItens = new List<RecebimentoCompraItem>();
        private IList<DetalhamentoRecebimentoCompraItem> _detalhamentoRecebimentoCompraItens = new List<DetalhamentoRecebimentoCompraItem>();

        public virtual long Numero { get; set; }
        public virtual SituacaoRecebimentoCompra SituacaoRecebimentoCompra { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual DateTime DataAlteracao { get; set; }
        public virtual string Observacao { get; set; }
        public virtual double Valor { get; set; }
        public virtual Pessoa Unidade { get; set; }
        public virtual Pessoa Fornecedor { get; set; }

        public virtual IList<ConferenciaEntradaMaterial> ConferenciaEntradaMateriais
        {
            get { return _conferenciaEntradaMateriais; }
            set { _conferenciaEntradaMateriais = value; }
        }

        public virtual IList<PedidoCompra> PedidoCompras
        {
            get { return _pedidosCompras; }
            set { _pedidosCompras = value; }
        }

        public virtual IList<RecebimentoCompraItem> RecebimentoCompraItens
        {
            get { return _recebimentoCompraItens; }
            set { _recebimentoCompraItens = value; }
        }

        public virtual IList<DetalhamentoRecebimentoCompraItem> DetalhamentoRecebimentoCompraItens
        {
            get { return _detalhamentoRecebimentoCompraItens; }
            set { _detalhamentoRecebimentoCompraItens = value; }
        }
    }
}