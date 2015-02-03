using System;
using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201502031545)]
    public class Migration201502031545 : Migration
    {
        public override void Up()
        {
            Alter.Table("simboloconservacao")
                .AlterColumn("descricao")
                .AsString(200);
        }

        public override void Down()
        {
        }
    }
}
