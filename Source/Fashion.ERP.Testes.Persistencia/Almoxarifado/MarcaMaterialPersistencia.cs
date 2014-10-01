using Fashion.ERP.Domain.Almoxarifado;

namespace Fashion.ERP.Testes.Persistencia.Almoxarifado
{
    public class MarcaMaterialPersistencia : TestPersistentObject<MarcaMaterial>
    {
        public override MarcaMaterial GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaMarcaMaterial();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}