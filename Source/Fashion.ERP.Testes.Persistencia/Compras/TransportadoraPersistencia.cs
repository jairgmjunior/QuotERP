using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Compras
{
    public class TransportadoraPersistencia : TestPersistentObject<Pessoa>
    {
        public override Pessoa GetPersistentObject()
        {
            var transportadora = FabricaObjetos.ObtenhaTransportadora();
            return transportadora;
        }

        public override void Init()
        {
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            Session.Current.Flush();
        }
    }
}