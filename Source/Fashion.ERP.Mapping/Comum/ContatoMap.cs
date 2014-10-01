using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class ContatoMap : FashionClassMap<Contato>
    {
        public ContatoMap()
            : base("contato", 10)
        {
            Map(x => x.TipoContato).Not.Nullable();
            Map(x => x.Nome).Not.Nullable().Length(100);
            Map(x => x.Telefone).Length(14);
            Map(x => x.Operadora).Length(20);
            Map(x => x.Email).Length(100);

            References(x => x.Pessoa);
        }
    }
}