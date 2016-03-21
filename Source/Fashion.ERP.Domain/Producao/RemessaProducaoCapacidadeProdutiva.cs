using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Producao
{
    public class RemessaProducaoCapacidadeProdutiva : DomainEmpresaBase<RemessaProducaoCapacidadeProdutiva>
    {
        public virtual long Quantidade { get; set; }
        public virtual ClassificacaoDificuldade ClassificacaoDificuldade { get; set; }
    }
}