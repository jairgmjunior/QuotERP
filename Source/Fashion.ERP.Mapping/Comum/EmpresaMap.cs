using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class EmpresaMap : TenantClassMap<Empresa>
    {
        public EmpresaMap()
            : base("empresa", 10)
        {
            Map(x => x.Codigo).Not.Nullable();
            Map(x => x.DataAtualizacao);
            Map(x => x.DataCadastro).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();
        }
    }
}