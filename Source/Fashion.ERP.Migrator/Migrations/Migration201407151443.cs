using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201407151443)]
    public class Migration201407151443 : Migration
    {
        public override void Up()
        {
            Create.Column("idtenant").OnTable("empresa").AsInt64().WithDefaultValue(1);
        }

        public override void Down()
        {
            Delete.Column("idtenant").FromTable("empresa");
        }
    }
}