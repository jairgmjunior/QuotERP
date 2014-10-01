using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class CentroCustoPersistencia : TestPersistentObject<CentroCusto>
    {
        public override CentroCusto GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaCentroCusto();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}