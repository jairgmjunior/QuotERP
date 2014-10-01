using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class FornecedorMap : FashionClassMap<Fornecedor>
    {
        public FornecedorMap()
            : base("fornecedor", 10)
        {
            Map(x => x.Codigo).Not.Nullable();
            Map(x => x.DataCadastro).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();
            References(x => x.TipoFornecedor).Not.Nullable();
        }
    }
}