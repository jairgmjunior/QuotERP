using System.Collections.Generic;
using Fashion.ERP.Domain.Almoxarifado;

namespace Fashion.ERP.Domain.Compras
{
    public class RecebimentoCompraItem : DomainBase<RecebimentoCompraItem>
    {
        private IList<ConferenciaEntradaMaterialItem> _conferenciaEntradaMaterialItens = new List<ConferenciaEntradaMaterialItem>();
        private IList<DetalhamentoRecebimentoCompraItem>  _detalhamentoRecebimentoCompraItens = new List<DetalhamentoRecebimentoCompraItem>();

        public virtual double Quantidade { get; set; }
        public virtual double ValorUnitario { get; set; }
        public virtual double ValorTotal { get; set; }
        public virtual Material Material { get; set; }

        public virtual IList<ConferenciaEntradaMaterialItem> ConferenciaEntradaMaterialItens
        {
            get { return _conferenciaEntradaMaterialItens; }
            set { _conferenciaEntradaMaterialItens = value; }
        }

        public virtual IList<DetalhamentoRecebimentoCompraItem> DetalhamentoRecebimentoCompraItens
        {
            get { return _detalhamentoRecebimentoCompraItens; }
            set { _detalhamentoRecebimentoCompraItens = value; }
        }
    }
}