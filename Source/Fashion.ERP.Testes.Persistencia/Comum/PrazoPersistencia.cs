using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class PrazoPersistencia : TestPersistentObject<Prazo>
    {
        public override Prazo GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaPrazo();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}