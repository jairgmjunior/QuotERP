using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Compras
{
    public class ParametroModuloCompraPersistencia : TestPersistentObject<ParametroModuloCompra>
    {


        public override ParametroModuloCompra GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaParametroModuloCompra();
        }
        
        public override void Init()
        {
           
        }

        public override void Cleanup()
        {
           
        }
    }
}