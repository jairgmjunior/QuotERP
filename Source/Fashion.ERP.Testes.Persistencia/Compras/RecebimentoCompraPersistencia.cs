using System.Linq;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Compras
{
    public class RecebimentoCompraPersistencia : TestPersistentObject<RecebimentoCompra>
    {
        private Pessoa _fornecedor;
        private Pessoa _comprador;
        private Pessoa _unidadeEstocadora;
        private ConferenciaEntradaMaterial _conferenciaEntradaMaterial;
        private PedidoCompra _pedidoCompra;
        private RecebimentoCompraItem _recebimentoCompraItem;
        private Material _material;
        private ConferenciaEntradaMaterialItem _conferenciaEntradaMaterialItem;
        private DetalhamentoRecebimentoCompraItem _detalhamentoRecebimentoPedidoCompra;

        public override RecebimentoCompra GetPersistentObject()
        {
            var recebimentoCompra = FabricaObjetos.ObtenhaRecebimentoCompra();
            
            recebimentoCompra.Fornecedor = _fornecedor;
            recebimentoCompra.Unidade = _unidadeEstocadora;
            recebimentoCompra.ConferenciaEntradaMateriais.Add(_conferenciaEntradaMaterial);
            recebimentoCompra.PedidoCompras.Add(_pedidoCompra);
            recebimentoCompra.RecebimentoCompraItens.Add(_recebimentoCompraItem);
            recebimentoCompra.DetalhamentoRecebimentoCompraItens.Add(_detalhamentoRecebimentoPedidoCompra);

            return recebimentoCompra;
        }

        public override void Init()
        {
            _fornecedor = FabricaObjetosPersistidos.ObtenhaFornecedor();
            _comprador = FabricaObjetosPersistidos.ObtenhaFuncionario();
            _unidadeEstocadora = FabricaObjetosPersistidos.ObtenhaUnidade();
            _conferenciaEntradaMaterial = FabricaObjetosPersistidos.ObtenhaConferenciaEntradaMaterial();
            _pedidoCompra = FabricaObjetosPersistidos.ObtenhaPedidoCompra();
            _material = FabricaObjetosPersistidos.ObtenhaMaterial();

            _conferenciaEntradaMaterialItem = _conferenciaEntradaMaterial.ConferenciaEntradaMaterialItens[0];

            _detalhamentoRecebimentoPedidoCompra = FabricaObjetos.ObtenhaDetalhamentoRecebimentoPedidoCompra();
            _detalhamentoRecebimentoPedidoCompra.PedidoCompra = _pedidoCompra;
            _detalhamentoRecebimentoPedidoCompra.PedidoCompraItem = _pedidoCompra.PedidoCompraItens.First();

            _recebimentoCompraItem = FabricaObjetos.ObtenhaRecebimentoCompraItem();
            _recebimentoCompraItem.Material = _material;
            _recebimentoCompraItem.ConferenciaEntradaMaterialItens.Add(_conferenciaEntradaMaterialItem);
            _recebimentoCompraItem.DetalhamentoRecebimentoCompraItens.Add(_detalhamentoRecebimentoPedidoCompra);

            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaFornecedor(_fornecedor);
            FabricaObjetosPersistidos.ExcluaPessoa(_comprador);
            FabricaObjetosPersistidos.ExcluaPessoa(_unidadeEstocadora);
            FabricaObjetosPersistidos.ExcluaConferenciaEntradaMaterial(_conferenciaEntradaMaterial);
            FabricaObjetosPersistidos.ExcluaPedidoCompra(_pedidoCompra);

            Session.Current.Flush();
        }
    }
}