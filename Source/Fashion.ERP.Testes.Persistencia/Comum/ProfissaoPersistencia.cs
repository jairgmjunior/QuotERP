using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class ProfissaoPersistencia : TestPersistentObject<Profissao>
    {
        public override Profissao GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaProfissao();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}