using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201406260840)]
    public class Migration201406260840 : Migration
    {
        public override void Up()
        {
            Delete.FromTable("procedimentomodulocomprasfuncionario").AllRows();
            Delete.FromTable("procedimentomodulocompras").AllRows();

            //Insert.IntoTable("procedimentomodulocompras")
            //    .Row(new { codigo = 1, descricao = "Validar pedido de compra" })
            //    .Row(new { codigo = 2, descricao = "Atualizar o preço de custo de mercadoria" });
        }

        public override void Down()
        {
        }
    }
}