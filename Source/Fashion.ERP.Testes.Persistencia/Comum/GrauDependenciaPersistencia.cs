using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class GrauDependenciaPersistencia : TestPersistentObject<GrauDependencia>
    {
        public override GrauDependencia GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaGrauDependencia();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}