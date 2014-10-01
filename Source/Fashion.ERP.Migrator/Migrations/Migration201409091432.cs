using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201409091432)]
    public class Migration201409091432 : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                DECLARE @id int
                select @id = id from permissao where action = 'Detalhar' and controller = 'Modelo'
                UPDATE permissao
	                SET permissaopai_id = @id, descricao = 'Sequência de Produção'
	                WHERE descricao = 'Sequência Produção' and area = 'EngenhariaProduto'");

            Execute.Sql(@"
                declare @id int

                select @id = id from permissao where action = 'Detalhar' and controller = 'Modelo'
                UPDATE permissao
	                SET permissaopai_id = @id
	                WHERE ACTION = 'materialcomposicaomodelo' and area = 'EngenhariaProduto'");
        }

        public override void Down()
        {
        }
    }
}