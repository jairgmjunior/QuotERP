using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201411251815)]
    public class Migration201411251815 : Migration
    {
        public override void Up()
        {
            Alter.Table("parametromodulocompra")
                .AddColumn("percentualcriacaopedidoautorizadorecebimento").AsDouble().Nullable();

            Update.Table("parametromodulocompra").Set(new { percentualcriacaopedidoautorizadorecebimento = 0 }).AllRows();

            Alter.Table("parametromodulocompra")
                .AlterColumn("percentualcriacaopedidoautorizadorecebimento").AsDouble();
        }

        public override void Down()
        {
            Delete.Column("percentualcriacaopedidoautorizadorecebimento").FromTable("parametromodulocompra");
        }
    }
}