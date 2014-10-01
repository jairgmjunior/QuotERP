using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Compras;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Compras
{
    public class PedidoCompraItemPersistencia : TestPersistentObject<PedidoCompraItem>
    {
        private Material _material;
        private UnidadeMedida _unidademedida;
        private PedidoCompra _pedidocompra;
        private PedidoCompraItemCancelado _pedidoCompraItemCancelado;
        private MotivoCancelamentoPedidoCompra _motivoCancelamentoPedidoCompra;

        public override PedidoCompraItem GetPersistentObject()
        {
            var pedidoCompraItem = FabricaObjetos.ObtenhaPedidoCompraItem();

            pedidoCompraItem.Material = _material;
            pedidoCompraItem.UnidadeMedida = _unidademedida;
            pedidoCompraItem.PedidoCompra = _pedidocompra;
            pedidoCompraItem.PedidoCompraItemCancelado = _pedidoCompraItemCancelado;

            return pedidoCompraItem;
        }
        
        public override void Init()
        {
            _material = FabricaObjetosPersistidos.ObtenhaMaterial();
            _unidademedida = FabricaObjetosPersistidos.ObtenhaUnidadeMedida();
            _pedidocompra = FabricaObjetosPersistidos.ObtenhaPedidoCompra();
            _motivoCancelamentoPedidoCompra = FabricaObjetosPersistidos.ObtenhaMotivoCancelamentoPedidoCompra();
            
            _pedidoCompraItemCancelado = FabricaObjetos.ObtenhaPedidoCompraItemCancelado();
            _pedidoCompraItemCancelado.MotivoCancelamentoPedidoCompra = _motivoCancelamentoPedidoCompra;

            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaMaterial(_material);
            FabricaObjetosPersistidos.ExcluaUnidadeMedida(_unidademedida);
            FabricaObjetosPersistidos.ExcluaPedidoCompra(_pedidocompra);
            FabricaObjetosPersistidos.ExcluaMotivoCancelamentoPedidoCompra(_motivoCancelamentoPedidoCompra);
          
            Session.Current.Flush();
        }
    }
}