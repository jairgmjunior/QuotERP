using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201407211440)]
    public class Migration201407211440 : Migration
    {
        public override void Up()
        {
            Create.Column("idtenant").OnTable("unidade").AsInt64().WithDefaultValue(1);
        }

        public override void Down()
        {
            Delete.Column("idtenant").FromTable("unidade");
        }
    }
}