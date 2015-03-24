using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class FichaTecnicaSequenciaOperacionalMap : EmpresaClassMap<FichaTecnicaSequenciaOperacional>
    {
        public FichaTecnicaSequenciaOperacionalMap()
            : base("fichatecnicasequenciaoperacional", 1)
        {
            Map(x => x.Custo).Not.Nullable();
            Map(x => x.Tempo).Not.Nullable();
            Map(x => x.PesoProdutividade).Not.Nullable();

            References(x => x.SetorProducao);
            References(x => x.DepartamentoProducao);
            References(x => x.OperacaoProducao);
        }
    }
}