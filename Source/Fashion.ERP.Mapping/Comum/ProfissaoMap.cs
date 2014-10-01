using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class ProfissaoMap : FashionClassMap<Profissao>
    {
        public ProfissaoMap()
            : base("profissao", 0)
        {
            Map(x => x.Nome).Not.Nullable().Length(100);
        }
    }
}