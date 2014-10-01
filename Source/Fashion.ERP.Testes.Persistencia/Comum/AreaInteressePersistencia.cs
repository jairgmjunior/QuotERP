using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class AreaInteressePersistencia : TestPersistentObject<AreaInteresse>
    {
        public override AreaInteresse GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaAreaInteresse();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}