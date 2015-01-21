using System;
using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201501201054)]
    public class Migration201501201054 : Migration
    {
        public override void Up()
        {
            Alter.Table("requisicaomaterial")
                .AddColumn("dataalteracao")
                .AsDateTime()
                .WithDefaultValue(DateTime.Now)
                .NotNullable();

            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201501201054.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}
