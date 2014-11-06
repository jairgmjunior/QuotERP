using Fashion.ERP.Domain.Financeiro;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Financeiro
{
    public class TituloReceberBaixaMap : FashionClassMap<TituloReceberBaixa>
    {
        public TituloReceberBaixaMap()
            : base("tituloreceberbaixa", 10)
        {
            Map(x => x.NumeroBaixa).Not.Nullable();
            Map(x => x.DataRecebimento).Not.Nullable();
            Map(x => x.ValorBaixa).Not.Nullable();
            Map(x => x.Multa).Not.Nullable();
            Map(x => x.Juros).Not.Nullable();
            Map(x => x.Descontos).Not.Nullable();
            Map(x => x.Despesas).Not.Nullable();
            Map(x => x.ValorRecebido).Not.Nullable();
            Map(x => x.Historico).Length(100);
            Map(x => x.Observacao).Length(4000);
            Map(x => x.TipoBaixaReceber).Not.Nullable();

            References(x => x.TituloReceber);
        }
    }
}