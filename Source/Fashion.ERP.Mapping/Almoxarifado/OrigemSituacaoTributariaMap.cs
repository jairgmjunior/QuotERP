using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class OrigemSituacaoTributariaMap : FashionClassMap<OrigemSituacaoTributaria>
    {
        public OrigemSituacaoTributariaMap()
        {
            Table("origemsituacaotributaria");
            Id(x => x.Id).Column("id").GeneratedBy.Identity();
            Map(x => x.Codigo).Length(5);
            Map(x => x.Descricao).Length(200);
        }
    }
}