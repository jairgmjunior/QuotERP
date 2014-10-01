using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class TamanhoPersistencia : TestPersistentObject<Tamanho>
    {
        public override Tamanho GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaTamanho();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}