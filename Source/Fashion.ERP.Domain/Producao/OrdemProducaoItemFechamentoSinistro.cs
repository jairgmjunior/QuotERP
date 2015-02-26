namespace Fashion.ERP.Domain.Producao
{
    public class OrdemProducaoItemFechamentoSinistro : DomainBase<OrdemProducaoItemFechamentoSinistro>
    {
        public virtual double Quantidade { get; set; }
        public virtual TipoSinistroFechamentoOrdemProducao TipoSinistroFechamentoOrdemProducao { get; set; }
        public virtual DefeitoProducao DefeitoProducao { get; set; }

        public virtual OrdemProducaoItemFechamento OrdemProducaoItemFechamento { get; set; }
    }
}