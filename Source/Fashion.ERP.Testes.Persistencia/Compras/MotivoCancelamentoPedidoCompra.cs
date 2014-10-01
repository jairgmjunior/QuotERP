using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Compras
{
    public class MotivoCancelamentoPedidoCompraPersistencia : TestPersistentObject<MotivoCancelamentoPedidoCompra>
    {
       

        public override MotivoCancelamentoPedidoCompra GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaMotivoCancelamentoPedidoCompra();
        }
        
        public override void Init()
        {
           
        }

        public override void Cleanup()
        {
           
        }
    }
}