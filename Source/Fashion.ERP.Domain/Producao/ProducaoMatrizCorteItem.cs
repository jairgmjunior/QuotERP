using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Producao
{
    public class ProducaoMatrizCorteItem : DomainEmpresaBase<ProducaoMatrizCorteItem>
    {
        public virtual long QuantidadeProgramada { get; set; }
        public virtual long QuantidadeCorte { get; set; }
        public virtual long QuantidadeAdicional { get; set; }

        public virtual long QuantidadeProducao { get; set; }//QuantidadeCorte + QuantidadeAdicional

        public virtual long QuantidadeVezes { get; set; }
        public virtual Tamanho Tamanho{ get; set; }
    }
}