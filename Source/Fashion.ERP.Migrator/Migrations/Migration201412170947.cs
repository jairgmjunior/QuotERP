using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201412170947)]
    public class Migration201412170947 : Migration
    {
        public override void Up()
        {
            Alter.Table("pedidocompra")
                .AddColumn("valorencargos")
                .AsDouble()
                .NotNullable();

            Alter.Table("pedidocompra")
                .AddColumn("valorembalagem")
                .AsDouble()
                .NotNullable();

            Alter.Table("pedidocompra")
                .AlterColumn("contato")
                .AsDouble()
                .Nullable();

            Alter.Table("pedidocompra")
                .AddColumn("transportadora_id")
                .AsInt64()
                .Nullable()
                .ForeignKey("FK_pedidocompra_transportadora", "pessoa", "id");

            Alter.Table("pedidocompra")
                .AddColumn("funcionarioautorizador_id")
                .AsInt64()
                .Nullable()
                .ForeignKey("FK_pedidocompra_funcionarioautorizador", "pessoa", "id");

            Alter.Table("pedidocompraitem")
                .AddColumn("valordesconto")
                .AsDouble()
                .Nullable();

        }

        public override void Down()
        {
        }
    }
}
