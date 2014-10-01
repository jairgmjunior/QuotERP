using Fashion.ERP.Domain.Financeiro;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Financeiro
{
    public class ExtratoBancarioMap : FashionClassMap<ExtratoBancario>
    {
        public ExtratoBancarioMap()
            : base("extratobancario", 10)
        {
            Map(x => x.TipoLancamento).Not.Nullable();
            Map(x => x.Emissao).Not.Nullable();
            Map(x => x.Compensacao);
            Map(x => x.Descricao).Length(100);
            Map(x => x.Valor).Not.Nullable();
            Map(x => x.Compensado).Not.Nullable();
            Map(x => x.Cancelado).Not.Nullable();

            References(x => x.ContaBancaria).Not.Nullable();
        }
    }
}