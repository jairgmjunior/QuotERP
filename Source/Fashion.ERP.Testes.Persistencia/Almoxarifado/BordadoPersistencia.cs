using Fashion.ERP.Domain.Almoxarifado;

namespace Fashion.ERP.Testes.Persistencia.Almoxarifado
{
    public class BordadoPeristencia : TestPersistentObject<Bordado>
    {
        public override Bordado GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaBordado();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}