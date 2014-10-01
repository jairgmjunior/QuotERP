using Fashion.ERP.Domain.Financeiro;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Financeiro
{
    public class ChequeRecebidoMap : FashionClassMap<ChequeRecebido>
    {
        public ChequeRecebidoMap()
            : base("chequerecebido", 10)
        {
            Map(x => x.Comp);
            Map(x => x.Agencia).Not.Nullable().Length(6);
            Map(x => x.Conta).Not.Nullable().Length(8);
            Map(x => x.NumeroCheque).Not.Nullable().Length(6);
            Map(x => x.Cmc7).Length(30);
            Map(x => x.Valor).Not.Nullable();
            Map(x => x.Nominal).Length(100);
            Map(x => x.DataEmissao).Not.Nullable();
            Map(x => x.DataVencimento).Not.Nullable();
            Map(x => x.DataProrrogacao);
            Map(x => x.Praca).Not.Nullable().Length(100);
            Map(x => x.Historico).Length(4000);
            Map(x => x.Observacao).Length(4000);
            Map(x => x.Saldo).Not.Nullable();
            Map(x => x.Compensado).Not.Nullable();

            References(x => x.Cliente).Not.Nullable();
            References(x => x.Banco).Not.Nullable();
            References(x => x.Emitente).Not.Nullable();
            References(x => x.Unidade).Not.Nullable();

            HasManyToMany(x => x.Funcionarios)
                .Table("chequerecebidofuncionario")
                    .ParentKeyColumn("chequerecebido_id")
                    .ChildKeyColumn("funcionario_id")
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);

            HasManyToMany(x => x.PrestadorServicos)
                .Table("chequerecebidoprestadorservico")
                    .ParentKeyColumn("chequerecebido_id")
                    .ChildKeyColumn("prestadorservico_id")
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);

            HasMany(x => x.BaixaChequeRecebidos)
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);

            HasMany(x => x.OcorrenciaCompensacoes)
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}