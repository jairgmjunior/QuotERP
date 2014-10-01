using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class ClientePersistencia : TestPersistentObject<Cliente>
    {
        private AreaInteresse _areaInteresse;

        public override Cliente GetPersistentObject()
        {
            var cliente = FabricaObjetos.ObtenhaCliente();
            cliente.AreaInteresse = _areaInteresse;
            return cliente;
        }

        public override void Init()
        {
            _areaInteresse = FabricaObjetosPersistidos.ObtenhaAreaInteresse();
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
        }
    }
}