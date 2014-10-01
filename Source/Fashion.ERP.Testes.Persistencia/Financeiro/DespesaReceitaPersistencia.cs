using Fashion.ERP.Domain.Financeiro;
using NUnit.Framework;

namespace Fashion.ERP.Testes.Persistencia.Financeiro
{
    [TestFixture]
    public class DespesaReceitaPersistencia : TestPersistentObject<DespesaReceita>
    {
        public override DespesaReceita GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaDespesaReceita();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}