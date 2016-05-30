using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.Producao;
using Fashion.Framework.UnitOfWork;
using NUnit.Framework;

namespace Fashion.ERP.Testes.Persistencia.Producao
{
    [TestFixture]
    public class ProducaoProgramacaoPersistencia : TestPersistentObject<ProducaoProgramacao>
    {
        private Pessoa _funcionario;
        private Pessoa _unidade;

        public override ProducaoProgramacao GetPersistentObject()
        {
            var programacaoProducao = FabricaObjetos.ObtenhaProducaoProgramacao();
            
            programacaoProducao.Funcionario = _funcionario;

            return programacaoProducao;
        }

        public override void Init()
        {
            _funcionario = FabricaObjetosPersistidos.ObtenhaFuncionario();
            _unidade = FabricaObjetosPersistidos.ObtenhaUnidade();

            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaPessoa(_funcionario);
            FabricaObjetosPersistidos.ExcluaPessoa(_unidade);

            Session.Current.Flush();
        }
    }
}