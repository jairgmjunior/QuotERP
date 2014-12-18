using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class OperacaoProducaoPersistencia : TestPersistentObject<OperacaoProducao>
    {
        private SetorProducao _setorProducao;

        public override OperacaoProducao GetPersistentObject()
        {
            var operacaoProducao = FabricaObjetos.ObtenhaOperacao();
            operacaoProducao.SetorProducao = _setorProducao;
            return operacaoProducao;
        }

        public override void Init()
        {
            _setorProducao = FabricaObjetosPersistidos.ObtenhaSetorProducao();
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaSetorProducao(_setorProducao);

            Session.Current.Flush();
        }
    }
}
