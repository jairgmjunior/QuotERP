namespace Fashion.ERP.Domain.Financeiro
{
    public class RateioDespesaReceita : DomainEmpresaBase<RateioDespesaReceita>
    {
        public virtual double Valor { get; set; }

        public virtual TituloPagar TituloPagar { get; set; }
        public virtual DespesaReceita DespesaReceita { get; set; }
    }
}