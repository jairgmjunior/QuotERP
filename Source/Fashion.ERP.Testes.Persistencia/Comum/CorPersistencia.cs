using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class CorPersistencia : TestPersistentObject<Cor>
    {
        public override Cor GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaCor();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}