using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Compras
{
    public class PedidoCompraPersistencia : TestPersistentObject<PedidoCompra>
    {
        private Pessoa _fornecedor;
        private Pessoa _comprador;
        private Pessoa _unidadeEstocadora;
        private Prazo _prazo;
        private MeioPagamento _meioPagamento;

        public override PedidoCompra GetPersistentObject()
        {
            var pedidoCompra = FabricaObjetos.ObtenhaPedidoCompra();

            pedidoCompra.Fornecedor = _fornecedor;
            pedidoCompra.Comprador = _comprador;
            pedidoCompra.UnidadeEstocadora = _unidadeEstocadora;
            pedidoCompra.Prazo = _prazo;
            pedidoCompra.MeioPagamento = _meioPagamento;

            return pedidoCompra;
        }
        
        public override void Init()
        {
            _fornecedor = FabricaObjetosPersistidos.ObtenhaFornecedor();
            _comprador = FabricaObjetosPersistidos.ObtenhaFuncionario();
            _unidadeEstocadora = FabricaObjetosPersistidos.ObtenhaUnidade();
            _prazo = FabricaObjetosPersistidos.ObtenhaPrazo();
            _meioPagamento = FabricaObjetosPersistidos.ObtenhaMeioPagamento();

            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaFornecedor(_fornecedor);
            FabricaObjetosPersistidos.ExcluaPessoa(_comprador);
            FabricaObjetosPersistidos.ExcluaPessoa(_unidadeEstocadora);
            FabricaObjetosPersistidos.ExcluaPrazo(_prazo);
            FabricaObjetosPersistidos.ExcluaMeioPagamento(_meioPagamento);

            Session.Current.Flush();
        }
    }
}