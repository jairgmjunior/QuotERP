using Fashion.ERP.Domain.Financeiro;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Financeiro
{
    public class TituloPagarMap : EmpresaClassMap<TituloPagar>
    {
        public TituloPagarMap()
            : base("titulopagar", 0)
        {
            Map(x => x.Numero).Length(30).Not.Nullable();
            Map(x => x.Parcela).Not.Nullable();
            Map(x => x.Plano).Not.Nullable();
            Map(x => x.Emissao).Not.Nullable();
            Map(x => x.Vencimento).Not.Nullable();
            Map(x => x.Prorrogacao).Not.Nullable();
            Map(x => x.Valor).Not.Nullable();
            Map(x => x.SaldoDevedor).Not.Nullable();
            Map(x => x.Historico).Length(100);
            Map(x => x.Observacao).Length(4000);
            Map(x => x.Provisorio).Not.Nullable();
            Map(x => x.SituacaoTitulo).Not.Nullable();
            Map(x => x.DataCadastro).Not.Nullable();
            Map(x => x.DataAlteracao).Not.Nullable();

            References(x => x.Fornecedor).Not.Nullable();
            References(x => x.Unidade).Not.Nullable();
            References(x => x.Banco).Not.Nullable();

            HasMany(x => x.TituloPagarBaixas)
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);

            HasMany(x => x.RateioCentroCustos)
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);

            HasMany(x => x.RateioDespesaReceitas)
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}