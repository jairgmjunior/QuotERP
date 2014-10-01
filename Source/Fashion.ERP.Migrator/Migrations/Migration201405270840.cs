using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201405270840)]
    public class Migration201405270840 : Migration
    {
        public override void Up()
        {
            Alter.Table("modelo")
                .AddColumn("dataalteracao").AsDateTime().Nullable();
            Execute.Sql("UPDATE modelo SET dataalteracao = datacriacao;");
            Alter.Table("modelo")
                .AlterColumn("dataalteracao").AsDateTime().NotNullable();

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201405270840.ConsumoMaterialColecaoView.sql");
        }

        public override void Down()
        {
            Delete.Column("dataalteracao").FromTable("modelo");
        }
    }
}
