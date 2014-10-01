using Fashion.ERP.Domain.EngenhariaProduto;

namespace Fashion.ERP.Testes.Persistencia.EngenhariaProduto
{
    public class NaturezaPersistencia : TestPersistentObject<Natureza>
    {
        public override Natureza GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaNatureza();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}