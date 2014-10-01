using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class GeneroFiscalMap : FashionClassMap<GeneroFiscal>
    {
        public GeneroFiscalMap()
            : base("generofiscal", 0)
        {
            Map(x => x.Descricao).Length(500).Not.Nullable();
            Map(x => x.Codigo).Length(2).Not.Nullable();
        }
    }
}