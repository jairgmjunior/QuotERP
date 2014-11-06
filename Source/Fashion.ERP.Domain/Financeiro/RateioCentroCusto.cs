using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Financeiro
{
    public class RateioCentroCusto : DomainEmpresaBase<RateioCentroCusto>
    {
        public virtual double Valor { get; set; }

        public virtual TituloPagar TituloPagar { get; set; }
        public virtual CentroCusto CentroCusto { get; set; }
    }
}