using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class MeioPagamentoPersistencia : TestPersistentObject<MeioPagamento>
    {
        public override MeioPagamento GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaMeioPagamento();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}