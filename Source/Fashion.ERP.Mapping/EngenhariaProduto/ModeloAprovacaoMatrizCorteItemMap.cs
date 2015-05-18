using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.EngenhariaProduto
{
    public class ModeloAprovacaoMatrizCorteItemMap: EmpresaClassMap<ModeloAprovacaoMatrizCorteItem>
    {
        public ModeloAprovacaoMatrizCorteItemMap()
            : base("modeloaprovacaomatrizcorteitem", 10)
        {
            Map(x => x.Quantidade).Not.Nullable();
            Map(x => x.QuantidadeVezes).Not.Nullable();

            References(x => x.Tamanho);
        }
    }
}
