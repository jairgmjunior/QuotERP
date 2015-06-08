using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class FichaTecnicaMatrizMap: EmpresaClassMap<FichaTecnicaMatriz>
    {
        public FichaTecnicaMatrizMap()
            : base("fichatecnicamatriz", 0)
        {
            References(x => x.Grade);

            HasMany(x => x.FichaTecnicaVariacaoMatrizs)
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan();
        }
    }
}