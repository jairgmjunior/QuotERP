using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public sealed class InformacaoBancariaMap : FashionClassMap<InformacaoBancaria>
    {
        public InformacaoBancariaMap()
            : base("informacaobancaria", 10)
        {
            Map(x => x.Agencia).Not.Nullable().Length(6);
            Map(x => x.Conta).Not.Nullable().Length(20);
            Map(x => x.TipoConta).Not.Nullable();
            Map(x => x.DataAbertura);
            Map(x => x.Titular).Length(100);
            Map(x => x.Telefone).Length(14);

            References(x => x.Pessoa);
            References(x => x.Banco).Not.Nullable();
        }
    }
}