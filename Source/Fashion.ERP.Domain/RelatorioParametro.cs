namespace Fashion.ERP.Domain
{
    public class RelatorioParametro : DomainBase<RelatorioParametro>
    {
        public virtual Relatorio Relatorio { get; set; }
        public virtual string Nome { get; set; }
        public virtual TipoRelatorioParametro TipoRelatorioParametro { get; set; }
    }
}