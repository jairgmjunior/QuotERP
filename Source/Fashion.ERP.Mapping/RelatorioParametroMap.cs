using Fashion.ERP.Domain;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping
{
    public class RelatorioParametroMap : FashionClassMap<RelatorioParametro>
    {
        public RelatorioParametroMap()
            : base("relatorioparametro", 10)
        {
            Map(x => x.Nome).Not.Nullable().Length(100);
            Map(x => x.TipoRelatorioParametro).Not.Nullable();
            
            References(x => x.Relatorio);
        }
    }
}