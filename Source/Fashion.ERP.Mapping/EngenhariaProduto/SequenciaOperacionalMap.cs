using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.EngenhariaProduto
{
    public class SequenciaOperacionalMap : FashionClassMap<SequenciaOperacional>
    {
        public SequenciaOperacionalMap()
            : base("sequenciaoperacionalnatureza", 10)
        {
            Map(x => x.Sequencia);

            References(x => x.DepartamentoProducao).Not.Nullable();
            References(x => x.SetorProducao);
            References(x => x.OperacaoProducao);
           
            
        }
    }
}
