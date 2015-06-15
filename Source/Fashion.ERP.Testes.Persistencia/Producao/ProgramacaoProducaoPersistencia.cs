using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.Producao;
using Fashion.Framework.UnitOfWork;
using NUnit.Framework;

namespace Fashion.ERP.Testes.Persistencia.Producao
{
    [TestFixture]
    public class ProgramacaoProducaoPersistencia : TestPersistentObject<ProgramacaoProducao>
    {
        private Pessoa _funcionario;
        private Colecao _colecao;
        private FichaTecnica _fichaTecnica;
        private Tamanho _tamanho;
        private ProgramacaoProducaoMatrizCorte _programacaoProducaoMatrizCorte;
        private ProgramacaoProducaoMatrizCorteItem _programacaoProducaoMatrizCorteItem;
        
        public override ProgramacaoProducao GetPersistentObject()
        {
            var programacaoProducao = FabricaObjetos.ObtenhaProgramacaoProducao();

            programacaoProducao.FichaTecnica = _fichaTecnica;
            programacaoProducao.Funcionario = _funcionario;
            programacaoProducao.Colecao = _colecao;
            programacaoProducao.ProgramacaoProducaoMatrizCorte = _programacaoProducaoMatrizCorte;

            return programacaoProducao;
        }

        public override void Init()
        {
            _funcionario = FabricaObjetosPersistidos.ObtenhaFuncionario();
            _colecao = FabricaObjetosPersistidos.ObtenhaColecao();
            _tamanho = FabricaObjetosPersistidos.ObtenhaTamanho();
            _fichaTecnica = FabricaObjetosPersistidos.ObtenhaFichaTecnica();

            _programacaoProducaoMatrizCorte = FabricaObjetos.ObtenhaProgramacaoProducaoMatrizCorte();
            _programacaoProducaoMatrizCorteItem = FabricaObjetos.ObtenhaProgramacaoProducaoMatrizCorteItem();
            _programacaoProducaoMatrizCorteItem.Tamanho = _tamanho;
            _programacaoProducaoMatrizCorte.ProgramacaoProducaoMatrizCorteItens.Add(_programacaoProducaoMatrizCorteItem);

            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            //FabricaObjetosPersistidos.ExcluaGrade(_grade);
       
            Session.Current.Flush();
        }
    }
}