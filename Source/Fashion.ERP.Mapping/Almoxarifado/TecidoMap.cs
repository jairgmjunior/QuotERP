using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class TecidoMap : FashionClassMap<Tecido>
    {
        public TecidoMap()
            : base("tecido", 0)
        {
            Map(x => x.Composicao).Length(200);
            Map(x => x.Armacao).Length(100);
            Map(x => x.Gramatura).Length(100);
            Map(x => x.Largura).Length(100);
            Map(x => x.Rendimento).Length(100);
        }
    }
}