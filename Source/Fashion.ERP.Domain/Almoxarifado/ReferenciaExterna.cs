using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Almoxarifado
{
    public class ReferenciaExterna : DomainBase<ReferenciaExterna>
    {
        public virtual Material Material { get; set; }

        public virtual string Referencia { get; set; }
        public virtual string Descricao { get; set; }
        public virtual string CodigoBarra { get; set; }
        public virtual double Preco { get; set; }

        public virtual Pessoa Fornecedor { get; set; }
    }
}