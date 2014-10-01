using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class MarcaPersistencia : TestPersistentObject<Marca>
    {
        public override Marca GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaMarca();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}