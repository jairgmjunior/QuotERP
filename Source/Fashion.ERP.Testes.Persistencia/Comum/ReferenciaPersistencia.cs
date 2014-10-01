using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class ReferenciaPersistencia : TestPersistentObject<Referencia>
    {
        private Cliente _cliente;

        public override Referencia GetPersistentObject()
        {
            var referencia =  FabricaObjetos.ObtenhaReferencia();
            referencia.Cliente = _cliente;
            return referencia;
        }

        public override void Init()
        {
            _cliente = FabricaObjetosPersistidos.ObtenhaCliente();
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaCliente(_cliente);
            Session.Current.Flush();
        }
    }
}