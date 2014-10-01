using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201407111731)]
    public class Migration201407111731: Migration
    {
        public override void Up()
        {
            Delete.Column("codigo").FromTable("centrocusto");
        }

        public override void Down()
        {
            Create.Column("codigo").OnTable("centrocusto").AsInt64();
        }         
    }
}