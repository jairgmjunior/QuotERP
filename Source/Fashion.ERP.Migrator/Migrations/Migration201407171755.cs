using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201407171755)]
    public class Migration201407171755 : Migration
    {
        public override void Up()
        {
            Create.Column("idempresa").OnTable("unidade").AsInt64().WithDefaultValue(1);
        }

        public override void Down()
        {
            Delete.Column("idempresa").FromTable("unidade");
        }
    }
}