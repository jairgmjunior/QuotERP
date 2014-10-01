using Fashion.ERP.Domain.Almoxarifado;

namespace Fashion.ERP.Testes.Persistencia.Almoxarifado
{
    public class UnidadeMedidaPersistencia : TestPersistentObject<UnidadeMedida>
    {
        public override UnidadeMedida GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaUnidadeMedida();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}