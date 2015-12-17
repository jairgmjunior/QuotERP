using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201512031332)] 
    public class Migration201512031332 : Migration
    {
        public override void Up()
        {
            Alter.Table("programacaoproducao")
                .AddColumn("lote")
                .AsInt64()
                .Nullable();

            Alter.Table("programacaoproducao")
                .AddColumn("ano")
                .AsInt64()
                .NotNullable()
                .WithDefaultValue(2015);

            Execute.Sql(@"UPDATE programacaoproducao SET lote = numero");

            Alter.Table("programacaoproducao")
                .AlterColumn("lote")
                .AsInt64()
                .NotNullable();

            Execute.Sql(@"UPDATE programacaoproducao SET lote = fichatecnica.tag 
                from programacaoproducao, programacaoproducaoitem, fichatecnica 
                where programacaoproducaoitem.programacaoproducao_id = programacaoproducao.id 
                and programacaoproducaoitem.fichatecnica_id = fichatecnica.id");
            
            Delete.Column("numero").FromTable("programacaoproducao");
        }

        public override void Down()
        {
            
        }
    }
}
