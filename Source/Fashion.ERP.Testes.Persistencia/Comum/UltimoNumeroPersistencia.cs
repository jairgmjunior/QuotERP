using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class UltimoNumeroPersistencia : TestPersistentObject<UltimoNumero>
    {
        public override UltimoNumero GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaUltimoNumero();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}