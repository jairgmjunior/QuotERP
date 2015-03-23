using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201503231541)]
    public class Migration201503231541 : Migration
    {
        public override void Up()
        {
            Alter.Column("observacao").OnTable("modeloaprovado").AsString().Nullable();
        }

        public override void Down()
        {
        }
    }
}