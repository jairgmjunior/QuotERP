using Fashion.ERP.Domain.Financeiro;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Financeiro
{
    public class TituloPagarBaixaMap : EmpresaClassMap<TituloPagarBaixa>
    {
        public TituloPagarBaixaMap()
            : base("titulopagarbaixa", 0)
        {
            Map(x => x.NumeroBaixa).Not.Nullable();
            Map(x => x.DataPagamento).Not.Nullable();
            Map(x => x.DataAlteracao).Not.Nullable();
            Map(x => x.Juros).Not.Nullable();
            Map(x => x.Descontos).Not.Nullable();
            Map(x => x.Despesas).Not.Nullable();
            Map(x => x.Valor).Not.Nullable();
            Map(x => x.Historico).Length(100);
            Map(x => x.Observacao).Length(4000);

            References(x => x.TituloPagar);
        }
    }
}