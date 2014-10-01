using Fashion.ERP.Domain.Almoxarifado;

namespace Fashion.ERP.Testes.Persistencia.Almoxarifado
{
    public class CategoriaPersistencia : TestPersistentObject<Categoria>
    {
        public override Categoria GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaCategoria();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}