using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.Producao;
using Fashion.Framework.UnitOfWork;
using NUnit.Framework;

namespace Fashion.ERP.Testes.Persistencia.Producao
{
    [TestFixture]
    public class RemessaProducaoPersistencia : TestPersistentObject<RemessaProducao>
    {
        private Colecao _colecao;
        private ClassificacaoDificuldade _classificacaoDificuldade;
        private RemessaProducaoCapacidadeProdutiva _remessaProducaoCapacidadeProdutiva;

        public override RemessaProducao GetPersistentObject()
        {
            var remessaProducao = FabricaObjetos.ObtenhaRemessaProducao();
            remessaProducao.Colecao = _colecao;
            remessaProducao.RemessasProducaoCapacidadesProdutivas.Add(_remessaProducaoCapacidadeProdutiva);

            return remessaProducao;
        }

        public override void Init()
        {
            _colecao = FabricaObjetosPersistidos.ObtenhaColecao();
            _classificacaoDificuldade = FabricaObjetosPersistidos.ObtenhaClassificacaoDificuldade();
            
            _remessaProducaoCapacidadeProdutiva = FabricaObjetos.ObrenhaRemessaProducaoCapacidadeProdutiva();
            _remessaProducaoCapacidadeProdutiva.ClassificacaoDificuldade = _classificacaoDificuldade;
            
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaColecao(_colecao);
            FabricaObjetosPersistidos.ExcluaClassificacaoDificuldade(_classificacaoDificuldade);

            Session.Current.Flush();
        }
    }
}