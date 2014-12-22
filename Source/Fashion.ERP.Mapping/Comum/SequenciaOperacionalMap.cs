using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class SequenciaOperacionalMap : FashionClassMap<SequenciaOperacional>
    {
        public SequenciaOperacionalMap()
            : base("sequenciaoperacional", 10)
        {
            Map(x => x.Sequencia);

            References(x => x.DepartamentoProducao).Not.Nullable();
            References(x => x.SetorProducao);
            References(x => x.OperacaoProducao);
        }
    }
}
