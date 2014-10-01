using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Compras
{
    public class OrdemEntradaCompra : DomainBase<OrdemEntradaCompra>
    {
        private readonly IList<PedidoCompra> _pedidoCompras;

        public OrdemEntradaCompra()
        {
            _pedidoCompras = new List<PedidoCompra>();
        }

        public virtual long Numero { get; set; }
        public virtual SituacaoOrdemEntradaCompra SituacaoOrdemEntradaCompra { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual DateTime DataAlteracao { get; set; }
        public virtual string Observacao { get; set; }

        public virtual Pessoa UnidadeEstocadora { get; set; }
        public virtual Pessoa Comprador { get; set; }
        public virtual Pessoa Fornecedor { get; set; }
        public virtual ConferenciaEntradaMaterial ConferenciaEntradaMaterial { get; set; }
        

        #region PedidoCompras
        public virtual IReadOnlyCollection<PedidoCompra> PedidoCompras
        {
            get { return new ReadOnlyCollection<PedidoCompra>(_pedidoCompras); }
        }

        public virtual void AddPedidoCompra(params PedidoCompra[] pedidoCompras)
        {
            foreach (var pedidoCompra in pedidoCompras)
            {
                _pedidoCompras.Add(pedidoCompra);
            }
        }

        public virtual void RemovePedidoCompra(params PedidoCompra[] pedidoCompras)
        {
            foreach (var pedidoCompra in pedidoCompras)
            {
                if (_pedidoCompras.Contains(pedidoCompra))
                    _pedidoCompras.Remove(pedidoCompra);
            }
        }

        public virtual void ClearPedidoCompra()
        {
            _pedidoCompras.Clear();
        }

        #endregion
    }
}