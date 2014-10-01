using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201408011535)]
    public class Migration201408011535: Migration
    {
        public override void Up()
        {
            Create.Column("codigo").OnTable("centrocusto").AsInt64().WithDefaultValue(0);
        }

        public override void Down()
        {
            Delete.Column("codigo").FromTable("centrocusto");
        }         
    }
}