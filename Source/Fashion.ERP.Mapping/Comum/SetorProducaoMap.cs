using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class SetorProducaoMap : FashionClassMap<SetorProducao>
    {
        public SetorProducaoMap()
            : base("setorproducao", 0)
        {
            Map(x => x.Nome).Not.Nullable().Length(100);
            Map(x => x.Ativo).Not.Nullable();
            References(x => x.DepartamentoProducao).Not.Nullable();
        }
    }
}