using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class PrestadorServicoPersistencia : TestPersistentObject<PrestadorServico>
    {
        private Pessoa _unidade;

        public override PrestadorServico GetPersistentObject()
        {
            var prestadorServico = FabricaObjetos.ObtenhaPrestadorServico();

            prestadorServico.Unidade = _unidade;
            return prestadorServico;
        }

        public override void Init()
        {
            _unidade = FabricaObjetosPersistidos.ObtenhaUnidade();
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaPessoa(_unidade);
            Session.Current.Flush();
        }         
    }
}