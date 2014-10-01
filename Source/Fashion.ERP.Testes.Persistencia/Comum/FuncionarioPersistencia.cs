using Fashion.ERP.Domain.Comum;
using NUnit.Framework;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    [TestFixture]
    public class FuncionarioPersistencia : TestPersistentObject<Pessoa>
    {
        public override Pessoa GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaPessoa();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}