using FluentMigrator;

namespace Fashion.ERP.Migrator
{
    [Migration(201409031644)]
    public class Migration201409031644 : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"UPDATE permissao
                SET action = 'MaterialComposicaoModelo',
                controller = 'MaterialComposicaoModelo'
                WHERE descricao = 'Materiais de Composição' and area = 'EngenhariaProduto'");

            Execute.Sql(@"UPDATE permissao
                SET action = 'SequenciaProducao',
                controller = 'SequenciaProducao'
                WHERE descricao = 'Sequência Produção' and area = 'EngenhariaProduto'");
        }

        public override void Down()
        {
            Execute.Sql(@"UPDATE permissao
                SET action = 'Composicao',
                controller = 'Modelo'
                WHERE descricao = 'Materiais de Composição' and area = 'EngenhariaProduto'");

            Execute.Sql(@"
                UPDATE permissao
                SET action = 'SequenciaProducao',
                controller = 'Modelo'
                WHERE descricao = 'Sequência Produção' and area = 'EngenhariaProduto'");
        }
    }
}