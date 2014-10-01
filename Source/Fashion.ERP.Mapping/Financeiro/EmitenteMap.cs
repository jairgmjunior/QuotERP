using Fashion.ERP.Domain.Financeiro;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Financeiro
{
    public class EmitenteMap : FashionClassMap<Emitente>
    {
        public EmitenteMap()
            : base("emitente", 10)
        {
            Map(x => x.Agencia).Not.Nullable().Length(6);
            Map(x => x.Conta).Not.Nullable().Length(8);
            Map(x => x.Nome1).Not.Nullable().Length(100);
            Map(x => x.CpfCnpj1).Not.Nullable().Length(18);
            Map(x => x.Documento1).Length(20);
            Map(x => x.OrgaoExpedidor1).Length(20);
            Map(x => x.Nome2).Length(100);
            Map(x => x.CpfCnpj2).Length(18);
            Map(x => x.Documento2).Length(20);
            Map(x => x.OrgaoExpedidor2).Length(20);
            Map(x => x.ClienteDesde).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();

            References(x => x.Banco).Not.Nullable();
        }
    }
}