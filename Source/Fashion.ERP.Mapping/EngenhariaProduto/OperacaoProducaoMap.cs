using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.EngenhariaProduto
{
    public class OperacaoProducaoMap : FashionClassMap<OperacaoProducao>
    {
        public OperacaoProducaoMap()
            : base("operacaoproducao", 0)
        {
            Map(x => x.Descricao).Not.Nullable().Length(100);
            Map(x => x.Tempo).Not.Nullable();
            Map(x => x.Custo).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();

            References(x => x.SetorProducao).Not.Nullable();
        } 
    }
}