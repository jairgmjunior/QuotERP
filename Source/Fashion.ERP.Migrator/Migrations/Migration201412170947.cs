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
                .Nullable();

            Alter.Table("pedidocompra")
                .AddColumn("valorembalagem")
                .AsDouble()
                .Nullable();

<<<<<<< HEAD
            //Alter.Table("pedidocompra")
            //    .AlterColumn("contato")
            //    .AsDouble()
            //    .Nullable();

=======
>>>>>>> 00959be7d5c2749c24c9fe7d83ab0bd619a0740f
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
