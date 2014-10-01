using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class DepartamentoProducaoPersistencia : TestPersistentObject<DepartamentoProducao>
    {
        public override DepartamentoProducao GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaDepartamentoProducao();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }         
    }
}