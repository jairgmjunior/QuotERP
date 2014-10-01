using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class DependenteMap : FashionClassMap<Dependente>
    {
        public DependenteMap()
            : base("dependente", 10)
        {
            Map(x => x.Nome).Not.Nullable().Length(100);
            Map(x => x.Cpf).Not.Nullable().Length(14);
            Map(x => x.Rg).Not.Nullable().Length(20);
            Map(x => x.OrgaoExpedidor).Not.Nullable().Length(100);

            //References(x => x.Cliente);
            References(x => x.GrauDependencia);
        }
    }
}