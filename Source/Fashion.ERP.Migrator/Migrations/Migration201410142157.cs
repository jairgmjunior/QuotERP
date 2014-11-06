using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201410142157)]
    public class Migration201410142157 : Migration
    {
        public override void Up()
        {
            // TituloPagar
            Create.Table("titulopagar")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("numero").AsString(100)
                .WithColumn("parcela").AsInt32()
                .WithColumn("plano").AsInt32()
                .WithColumn("emissao").AsDateTime()
                .WithColumn("vencimento").AsDateTime()
                .WithColumn("prorrogacao").AsDateTime()
                .WithColumn("valor").AsDouble()
                .WithColumn("saldodevedor").AsDouble()
                .WithColumn("historico").AsString(100).Nullable()
                .WithColumn("observacao").AsString(4000).Nullable()
                .WithColumn("provisorio").AsBoolean()
                .WithColumn("situacaotitulo").AsString(255)
                .WithColumn("datacadastro").AsDateTime()
                .WithColumn("dataalteracao").AsDateTime()
                .WithColumn("fornecedor_id").AsInt64().ForeignKey("FK_titulopagar_fornecedor", "pessoa", "id")
                .WithColumn("unidade_id").AsInt64().ForeignKey("FK_titulopagar_unidade", "pessoa", "id")
                .WithColumn("banco_id").AsInt64().ForeignKey("FK_titulopagar_banco", "banco", "id");

            // TituloPagar
            Create.Table("titulopagarbaixa")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("numerobaixa").AsInt64()
                .WithColumn("datapagamento").AsDateTime()
                .WithColumn("dataalteracao").AsDateTime()
                .WithColumn("juros").AsDouble()
                .WithColumn("descontos").AsDouble()
                .WithColumn("despesas").AsDouble()
                .WithColumn("valor").AsDouble()
                .WithColumn("historico").AsString(100).Nullable()
                .WithColumn("observacao").AsString(4000).Nullable()
                .WithColumn("titulopagar_id").AsInt64().ForeignKey("FK_titulopagarbaixa_titulopagar", "titulopagar", "id");

            // RateioDespesaReceita
            Create.Table("rateiodespesareceita")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("valor").AsDouble()
                .WithColumn("titulopagar_id").AsInt64().ForeignKey("FK_rateiodespesareceita_titulopagar", "titulopagar", "id")
                .WithColumn("despesareceita_id").AsInt64().ForeignKey("FK_rateiodespesareceita_despesareceita", "despesareceita", "id");

            // RateioCentroCusto
            Create.Table("rateiocentrocusto")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("idtenant").AsInt64()
                .WithColumn("idempresa").AsInt64()
                .WithColumn("valor").AsDouble()
                .WithColumn("titulopagar_id").AsInt64().ForeignKey("FK_rateiocentrocusto_titulopagar", "titulopagar", "id")
                .WithColumn("centrocusto_id").AsInt64().ForeignKey("FK_rateiocentrocusto_centrocusto", "centrocusto", "id");

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201410142157.permissao.sql");
        }

        public override void Down()
        {
            Delete.Table("rateiocentrocusto");
            Delete.Table("rateiodespesareceita");
            Delete.Table("titulopagarbaixa");
            Delete.Table("titulopagar");
        }
    }
}