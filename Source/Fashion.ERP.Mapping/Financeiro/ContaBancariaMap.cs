using Fashion.ERP.Domain.Financeiro;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Financeiro
{
    public class ContaBancariaMap : FashionClassMap<ContaBancaria>
    {
        public ContaBancariaMap()
            : base("contabancaria", 10)
        {
            Map(x => x.Agencia).Not.Nullable().Length(6);
            Map(x => x.NomeAgencia).Length(50);
            Map(x => x.Conta).Not.Nullable().Length(8);
            Map(x => x.TipoContaBancaria).Not.Nullable();
            Map(x => x.Gerente).Length(50);
            Map(x => x.Abertura);
            Map(x => x.Telefone).Length(20);

            References(x => x.Banco).Not.Nullable();
        }
    }
}