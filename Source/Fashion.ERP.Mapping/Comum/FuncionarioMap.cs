using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class FuncionarioMap : FashionClassMap<Funcionario>
    {
        public FuncionarioMap()
            : base("funcionario", 10)
        {
            Map(x => x.Codigo).Not.Nullable();
            Map(x => x.PercentualComissao).Not.Nullable();
            Map(x => x.DataCadastro).Not.Nullable();
            Map(x => x.DataDesligamento);
            Map(x => x.FuncaoFuncionario).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();            
        }
    }
}