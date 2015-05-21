using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class FichaTecnicaMaterialConsumoVariacaoMap:EmpresaClassMap<FichaTecnicaMaterialConsumoVariacao>
    {
        public FichaTecnicaMaterialConsumoVariacaoMap() : base("fichatecnicamaterialconsumovariacao", 1)
        {
            Map(x => x.Custo);
            Map(x => x.Quantidade);
            Map(x => x.CompoeCusto);

            References(x => x.Material);
            References(x => x.Tamanho);
            References(x => x.DepartamentoProducao);
            References(x => x.FichaTecnicaVariacaoMatriz);
        }
    }
}