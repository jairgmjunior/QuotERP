using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class FichaTecnicaModelagemMap : EmpresaClassMap<FichaTecnicaModelagem>
    {
        public FichaTecnicaModelagemMap()
            : base("fichatecnicamodelagem", 0)
        {
            Map(x => x.Observacao).Nullable();
            Map(x => x.Descricao).Nullable();
            Map(x => x.DataModelagem);
            
            References(x => x.Modelista);
            References(x => x.Arquivo).Nullable().Cascade.All();

            HasMany(x => x.Medidas)
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan();
        }
    }
}