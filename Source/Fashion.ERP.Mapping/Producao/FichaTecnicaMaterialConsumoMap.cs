using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class FichaTecnicaMaterialConsumoMap : EmpresaClassMap<FichaTecnicaMaterialConsumo>
    {
        public FichaTecnicaMaterialConsumoMap()
            : base("fichatecnicamaterialconsumo", 1)
        {
            Map(x => x.Custo);
            Map(x => x.Quantidade);

            References(x => x.Material);
            References(x => x.DepartamentoProducao);
        }
    }
}