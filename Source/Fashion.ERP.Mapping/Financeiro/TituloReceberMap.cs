using Fashion.ERP.Domain.Financeiro;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Financeiro
{
    public class TituloReceberMap : EmpresaClassMap<TituloReceber>
    {
        public TituloReceberMap()
            : base("tituloreceber", 0)
        {
            Map(x => x.Numero).Length(30).Not.Nullable();
            Map(x => x.Parcela).Not.Nullable();
            Map(x => x.Plano).Not.Nullable();
            Map(x => x.Emissao).Not.Nullable();
            Map(x => x.Vencimento).Not.Nullable();
            Map(x => x.Prorrogacao).Not.Nullable();
            Map(x => x.Valor).Not.Nullable();
            Map(x => x.SaldoDevedor).Not.Nullable();
            Map(x => x.ValorDespesas).Not.Nullable();
            Map(x => x.Historico).Length(100);
            Map(x => x.Observacao).Length(4000);
            Map(x => x.Provisorio).Not.Nullable();
            Map(x => x.SituacaoTitulo).Not.Nullable();
            Map(x => x.DataCadastro).Not.Nullable();
            Map(x => x.DataAlteracao).Not.Nullable();
            Map(x => x.OrigemTituloReceber).Not.Nullable();

            References(x => x.Cliente).Not.Nullable();
            References(x => x.Funcionario).Not.Nullable();
            References(x => x.Unidade).Not.Nullable();
            References(x => x.Banco).Not.Nullable();

            HasMany(x => x.TituloReceberBaixas)
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);

            HasMany(x => x.DespesaTituloRecebers)
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}