using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class DepartamentoProducaoMap : FashionClassMap<DepartamentoProducao>
    {
        public DepartamentoProducaoMap()
            : base("departamentoproducao", 0)
        {
            Map(x => x.Nome).Not.Nullable().Length(50);
            Map(x => x.Criacao).Not.Nullable();
            Map(x => x.Producao).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();
        }
    }
}