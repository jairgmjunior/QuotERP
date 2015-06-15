using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Producao
{
    public class ProgramacaoProducaoMatrizCorteItem : DomainEmpresaBase<ProgramacaoProducaoMatrizCorteItem>
    {
        public virtual long Quantidade{ get; set; }
        public virtual long QuantidadeVezes { get; set; }
        public virtual Tamanho Tamanho{ get; set; }
    }
}