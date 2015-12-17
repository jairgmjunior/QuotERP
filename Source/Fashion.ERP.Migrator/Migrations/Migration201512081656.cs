using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201512081656)]
    public class Migration201512081656 : Migration
    {
        public override void Up()
        {
            Alter.Table("programacaoproducao")
                .AddColumn("situacaoprogramacaoproducao")
                .AsString()
                .NotNullable().WithDefaultValue("Iniciada");

            Execute.Sql(@"UPDATE programacaoproducao
                          SET situacaoprogramacaoproducao = 'EmReserva'
                          FROM programacaoproducao
                          WHERE EXISTS
                          (
                            SELECT 1
                              FROM programacaoproducaomaterial
                              WHERE programacaoproducao.id = programacaoproducaomaterial.programacaoproducao_id and reservado = 1 and requisitado = 0
                          );");

            Execute.Sql(@"UPDATE programacaoproducao
                          SET situacaoprogramacaoproducao = 'EmRequisicao'
                          FROM programacaoproducao
                          WHERE EXISTS
                          (
                            SELECT 1
                              FROM programacaoproducaomaterial
                              WHERE programacaoproducao.id = programacaoproducaomaterial.programacaoproducao_id and requisitado = 1
                          );");
        }

        public override void Down()
        {
            
        }
    }
}
