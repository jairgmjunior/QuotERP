using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class ProgramacaoProducaoMatrizCorteMap : EmpresaClassMap<ProgramacaoProducaoMatrizCorte>
    {
        public ProgramacaoProducaoMatrizCorteMap()
            : base("programacaoproducaomatrizcorte", 10)
        {
            Map(x => x.TipoEnfestoTecido).Not.Nullable();

            HasMany(x => x.ProgramacaoProducaoMatrizCorteItens)
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan();
        }
    }
}