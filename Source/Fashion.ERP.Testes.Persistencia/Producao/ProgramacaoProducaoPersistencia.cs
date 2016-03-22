using Fashion.ERP.Domain.Almoxarifado;
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
        private RemessaProducao _remessaProducao;
        private FichaTecnicaJeans _fichaTecnica;
        private Tamanho _tamanho;
        private Material _material;
        private DepartamentoProducao _departamentoProducao;
        private ProgramacaoProducaoMatrizCorte _programacaoProducaoMatrizCorte;
        private ProgramacaoProducaoMatrizCorteItem _programacaoProducaoMatrizCorteItem;
        private ProgramacaoProducaoMaterial _programacaoProducaoMaterial;
        private ProgramacaoProducaoItem _programacaoProducaoItem;

        public override ProgramacaoProducao GetPersistentObject()
        {
            var programacaoProducao = FabricaObjetos.ObtenhaProgramacaoProducao();

            //programacaoProducao.FichaTecnica = _fichaTecnica;
            programacaoProducao.Funcionario = _funcionario;
            programacaoProducao.RemessaProducao = _remessaProducao;
            //programacaoProducao.ProgramacaoProducaoMatrizCorte = _programacaoProducaoMatrizCorte;
            programacaoProducao.ProgramacaoProducaoMateriais.Add(_programacaoProducaoMaterial);
            programacaoProducao.ProgramacaoProducaoItems.Add(_programacaoProducaoItem);

            return programacaoProducao;
        }

        public override void Init()
        {
            _funcionario = FabricaObjetosPersistidos.ObtenhaFuncionario();
            _remessaProducao = FabricaObjetosPersistidos.ObtenhaRemessaProducao();
            _tamanho = FabricaObjetosPersistidos.ObtenhaTamanho();
            _fichaTecnica = FabricaObjetosPersistidos.ObtenhaFichaTecnica();
            _material = FabricaObjetosPersistidos.ObtenhaMaterial();
            _departamentoProducao = FabricaObjetosPersistidos.ObtenhaDepartamentoProducao();
            
            _programacaoProducaoMatrizCorte = FabricaObjetos.ObtenhaProgramacaoProducaoMatrizCorte();
            _programacaoProducaoMatrizCorteItem = FabricaObjetos.ObtenhaProgramacaoProducaoMatrizCorteItem();
            _programacaoProducaoMatrizCorteItem.Tamanho = _tamanho;
            _programacaoProducaoMatrizCorte.ProgramacaoProducaoMatrizCorteItens.Add(_programacaoProducaoMatrizCorteItem);
            
            _programacaoProducaoItem = FabricaObjetos.ObtenhaProgramacaoProducaoItem();
            _programacaoProducaoItem.FichaTecnica = _fichaTecnica;
            _programacaoProducaoItem.ProgramacaoProducaoMatrizCorte = _programacaoProducaoMatrizCorte;

            _programacaoProducaoMaterial = FabricaObjetos.ObtenhaProducaoProducaoMaterial();
            _programacaoProducaoMaterial.Material = _material;
            //_programacaoProducaoMaterial.ReservaMaterial = FabricaObjetosPersistidos.ObtenhaReservaMaterial();
            _programacaoProducaoMaterial.DepartamentoProducao = _departamentoProducao;
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaFichaTecnicaJeans(_fichaTecnica);
            FabricaObjetosPersistidos.ExcluaRemessaProducao(_remessaProducao);
            FabricaObjetosPersistidos.ExcluaTamanho(_tamanho);
            FabricaObjetosPersistidos.ExcluaPessoa(_funcionario);
            FabricaObjetosPersistidos.ExcluaMaterial(_material);
            FabricaObjetosPersistidos.ExcluaDepartamentoProducao(_departamentoProducao);

            Session.Current.Flush();
        }
    }
}