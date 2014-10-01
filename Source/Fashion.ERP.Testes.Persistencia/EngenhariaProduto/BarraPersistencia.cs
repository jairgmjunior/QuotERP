using Fashion.ERP.Domain.EngenhariaProduto;

namespace Fashion.ERP.Testes.Persistencia.EngenhariaProduto
{
    public class BarraPersistencia : TestPersistentObject<Barra>
    {
        public override Barra GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaBarra();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}