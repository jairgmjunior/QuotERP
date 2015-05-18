using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.EngenhariaProduto
{
    public class ModeloAprovacaoMatrizCorteMap: EmpresaClassMap<ModeloAprovacaoMatrizCorte>
    {
        public ModeloAprovacaoMatrizCorteMap()
            : base("modeloaprovacaomatrizcorte", 10)
        {
            Map(x => x.TipoEnfestoTecido).Not.Nullable();

            HasMany(x => x.ModeloAprovacaoMatrizCorteItens)
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan();
        }
    }
}
