using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class ModeloAprovacaoMatrizCorteItem : DomainEmpresaBase<ModeloAprovacaoMatrizCorteItem>
    {
        public virtual long Quantidade{ get; set; }
        public virtual long QuantidadeVezes { get; set; }
        public virtual Tamanho Tamanho{ get; set; }
    }
}