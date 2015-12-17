namespace Fashion.ERP.Domain.Producao
{
    public class ProgramacaoProducaoItem : DomainEmpresaBase<ProgramacaoProducaoItem>
    {
        public virtual long Quantidade { get; set; }

        public virtual FichaTecnica FichaTecnica { get; set; }

        public virtual ProgramacaoProducaoMatrizCorte ProgramacaoProducaoMatrizCorte { get; set; }
    }
}