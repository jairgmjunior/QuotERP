using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class ClassificacaoDificuldadePersistencia : TestPersistentObject<ClassificacaoDificuldade>
    {
        public override ClassificacaoDificuldade GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaClassificacaoDificuldade();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}