using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class ProducaoMatrizCorteMap : EmpresaClassMap<ProducaoMatrizCorte>
    {
        public ProducaoMatrizCorteMap()
            : base("producaomatrizcorte", 10)
        {
            Map(x => x.TipoEnfestoTecido);
            
            HasMany(x => x.ProducaoMatrizCorteItens)
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan();
        }
    }
}