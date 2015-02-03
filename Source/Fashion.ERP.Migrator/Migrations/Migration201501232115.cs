using System;
using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201501232115)]
    public class Migration201501232115 : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Fashion.ERP.Migrator.Scripts._201501232115.permissao.sql");
        }

        public override void Down()
        {
        }
    }
}
