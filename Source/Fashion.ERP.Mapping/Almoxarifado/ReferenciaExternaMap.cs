using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class ReferenciaExternaMap : FashionClassMap<ReferenciaExterna>
    {
        public ReferenciaExternaMap()
            : base("referenciaexterna", 100)
        {
            Map(x => x.Referencia).Length(20).Not.Nullable();
            Map(x => x.Descricao).Length(100);
            Map(x => x.CodigoBarra).Length(128);
            Map(x => x.Preco).Not.Nullable();

            References(x => x.Material).Not.Nullable();
            References(x => x.Fornecedor).Not.Nullable();
        }
    }
}