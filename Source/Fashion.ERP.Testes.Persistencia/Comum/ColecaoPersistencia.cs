using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class ColecaoPersistencia : TestPersistentObject<Colecao>
    {
        public override Colecao GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaColecao();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}