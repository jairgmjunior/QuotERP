using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class ProducaoItemMap : EmpresaClassMap<ProducaoItem>
    {
        public ProducaoItemMap()
            : base("producaoitem", 0)
        {
            Map(x => x.QuantidadeProducao);
            Map(x => x.QuantidadeProgramada);

            References(x => x.FichaTecnica);
            References(x => x.ProducaoMatrizCorte).Cascade.All();

            HasMany(x => x.ProducaoItemMateriais)
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan();
        }
    }
}