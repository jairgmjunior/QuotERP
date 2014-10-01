using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class BordadoMap : FashionClassMap<Bordado>
    {
        public BordadoMap()
            : base("bordado", 0)
        {
            Map(x => x.Descricao).Length(100);
            Map(x => x.Pontos).Length(100);
            Map(x => x.Aplicacao).Length(100);
            Map(x => x.Observacao).Length(100);
        }
    }
}