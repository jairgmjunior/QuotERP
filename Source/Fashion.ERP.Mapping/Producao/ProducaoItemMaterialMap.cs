using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class ProducaoItemMaterialMap : EmpresaClassMap<ProducaoItemMaterial>
    {
        public ProducaoItemMaterialMap()
            : base("producaoitemmaterial", 0)
        {
            Map(x => x.QuantidadeCancelada);
            Map(x => x.QuantidadeNecessidade);
            Map(x => x.QuantidadeProgramada);
            Map(x => x.QuantidadeUsada);
            
            References(x => x.Material);
            References(x => x.Responsavel);
            References(x => x.DepartamentoProducao);
            
            HasMany(x => x.ProducaoItemMateriais)
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan();
        }
    }
}