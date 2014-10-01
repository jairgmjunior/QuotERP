using Fashion.ERP.Domain.Almoxarifado;

namespace Fashion.ERP.Testes.Persistencia.Almoxarifado
{
    public class FamiliaPersistencia : TestPersistentObject<Familia>
    {
        public override Familia GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaFamilia();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}