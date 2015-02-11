using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class VariacaoPersistencia : TestPersistentObject<Variacao>
    {
        public override Variacao GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaVariacao();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}