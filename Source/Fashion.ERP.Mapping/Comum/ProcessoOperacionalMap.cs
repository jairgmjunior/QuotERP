using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class ProcessoOperacionalMap : FashionClassMap<ProcessoOperacional>
    {
        public ProcessoOperacionalMap()
            : base("processooperacional", 0)
        {
            Map(x => x.Descricao).Length(60).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();

            HasMany(x => x.SequenciasOperacionais)
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan();
        }
    }
}