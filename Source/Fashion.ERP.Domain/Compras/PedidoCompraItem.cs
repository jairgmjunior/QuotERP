using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Fashion.ERP.Domain.Almoxarifado;
using NHibernate.Util;

namespace Fashion.ERP.Domain.Compras
{
    public class PedidoCompraItem : DomainBase<PedidoCompraItem>
    {
        public virtual double Quantidade { get; set; }
        public virtual double ValorUnitario { get; set; }
        public virtual double ValorDesconto { get; set; }
        public virtual DateTime? PrevisaoEntrega { get; set; }
        public virtual double QuantidadeEntrega { get; set; }
        public virtual DateTime? DataEntrega { get; set; }
        public virtual SituacaoCompra SituacaoCompra { get; set; }

        public virtual PedidoCompra PedidoCompra { get; set; }
        public virtual Material Material { get; set; }
        public virtual UnidadeMedida UnidadeMedida { get; set; }

        public virtual PedidoCompraItemCancelado PedidoCompraItemCancelado { get; set; }

        public virtual string ReferenciaExterna
        {
            get
            {
                var referencia =
                    Material.ReferenciaExternas.Where(r => r.Fornecedor.Id == PedidoCompra.Fornecedor.Id)
                        .FirstOrDefault();
                return referencia.Referencia;
            }
        }
        public virtual string ReferenciaExternaMaterial
        {
            get
            {
                var firstOrDefault = Material.ReferenciaExternas.FirstOrDefault(x => x.Fornecedor.Id == PedidoCompra.Fornecedor.Id);
                if (firstOrDefault != null)
                {
                    return firstOrDefault.Referencia;
                }
                return null;
            }
        }

        public virtual double ObtenhaDiferenca()
        {
            return Quantidade - QuantidadeEntrega;
        }

        public virtual double ValorTotal
        {
            get
            {
                return (ValorUnitario * Quantidade);    
            }
        }

        public virtual void AtualizeSituacao()
        {
            var quantidadeCancelada = PedidoCompraItemCancelado != null ? PedidoCompraItemCancelado.QuantidadeCancelada : 0;
            var quantidadeTotal = QuantidadeEntrega + quantidadeCancelada;
            
            if (quantidadeTotal == 0)
                SituacaoCompra = SituacaoCompra.NaoAtendido;
            else if (Quantidade.Equals(quantidadeCancelada))
                SituacaoCompra = SituacaoCompra.Cancelado;
            else if (Quantidade <= quantidadeTotal)
                SituacaoCompra = SituacaoCompra.AtendidoTotal;
            else if (Quantidade > quantidadeTotal)
                SituacaoCompra = SituacaoCompra.AtendidoParcial;
        }
    }
}
