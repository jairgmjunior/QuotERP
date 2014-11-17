using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201411141420)]
    public class Migration201411141420 : Migration
    {
        public override void Up()
        {
            Alter.Table("modelo")
                .AddColumn("chaveexterna")
                .AsString()
                .Nullable();
        }

        public override void Down()
        {
        }
    }
}