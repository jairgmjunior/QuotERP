using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class FichaTecnicaModelagemMedidaMap : EmpresaClassMap<FichaTecnicaModelagemMedida>
    {
        public FichaTecnicaModelagemMedidaMap()
            : base("fichatecnicamodelagemmedida", 0)
        {
            Map(x => x.DescricaoMedida);
            
            HasMany(x => x.Itens)
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan();
        }
    }
}