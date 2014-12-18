using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Almoxarifado
{
    public class ProcessosOperacionaisPersistencia : TestPersistentObject<ProcessoOperacional>
    {
        private DepartamentoProducao _departamentoProducao;
        private SetorProducao _setorProducao;
        private OperacaoProducao _operacaoProducao;

        public override ProcessoOperacional GetPersistentObject()
        {
            var processoOperacional = FabricaObjetos.ObtenhaProcessoOperacional();

            var sequenciaOperacional = FabricaObjetos.ObtenhaSequenciaOperacional();
            sequenciaOperacional.DepartamentoProducao = _departamentoProducao;
            sequenciaOperacional.SetorProducao = _setorProducao;
            sequenciaOperacional.OperacaoProducao = _operacaoProducao;

            return processoOperacional;
        }

        public override void Init()
        {
            _departamentoProducao = FabricaObjetosPersistidos.ObtenhaDepartamentoProducao();
            _operacaoProducao = FabricaObjetosPersistidos.ObtenhaOperacaoProducao();
            _setorProducao = FabricaObjetosPersistidos.ObtenhaSetorProducao();

            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaDepartamentoProducao(_departamentoProducao);
            FabricaObjetosPersistidos.ExcluaSetorProducao(_setorProducao);
            FabricaObjetosPersistidos.ExcluaOperacaoProducao(_operacaoProducao);

            Session.Current.Flush();
        }
    }
}