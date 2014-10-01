using Fashion.ERP.Domain.EngenhariaProduto;

namespace Fashion.ERP.Testes.Persistencia.EngenhariaProduto
{
    public class ArtigoPersistencia : TestPersistentObject<Artigo>
    {
        public override Artigo GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaArtigo();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}