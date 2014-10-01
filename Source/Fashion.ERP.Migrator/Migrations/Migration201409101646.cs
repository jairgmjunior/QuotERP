using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201409101646)]
    public class Migration201409101646 : Migration
    {

        public override void Up()
        {
            Execute.Sql(@"
                DECLARE @id int
                SELECT @id = id from permissao where action = 'Index' and controller = 'PedidoCompra'
                UPDATE permissao
	                SET permissaopai_id = @id, descricao = 'Validação'
	                WHERE action = 'Validar' and area = 'Compras'
		        
                DELETE FROM permissaotousuario where permissao_id in (
                    SELECT id from permissao WHERE descricao = '2. Validação da Compra' and area = 'Compras')
                DELETE FROM permissao WHERE descricao = '2. Validação da Compra' and area = 'Compras'	
                
                UPDATE permissao
	                SET descricao = 'Pedido de Compra'
	                WHERE action = 'Index' and area = 'Compras' and controller = 'PedidoCompra'
                UPDATE permissao
	                SET descricao = 'Recebimento de Compra'
	                WHERE descricao = '3. Recebimento de Compra'");
        }

        public override void Down()
        {
        }
    }
}