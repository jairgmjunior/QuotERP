using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Compras
{
    public class PedidoCompraPersistencia : TestPersistentObject<PedidoCompra>
    {
        private Pessoa _fornecedor;
        private Pessoa _transportadora;
        private Pessoa _comprador;
        private Pessoa _funcionarioautorizador;
        private Pessoa _unidadeEstocadora;
        private Prazo _prazo;
        private MeioPagamento _meioPagamento;

        public override PedidoCompra GetPersistentObject()
        {
            var pedidoCompra = FabricaObjetos.ObtenhaPedidoCompra();

            pedidoCompra.Fornecedor = _fornecedor;
            pedidoCompra.Transportadora = _transportadora;
            pedidoCompra.Comprador = _comprador;
            pedidoCompra.FuncionarioAutorizador = _funcionarioautorizador;
            pedidoCompra.UnidadeEstocadora = _unidadeEstocadora;
            pedidoCompra.Prazo = _prazo;
            pedidoCompra.MeioPagamento = _meioPagamento;

            return pedidoCompra;
        }
        
        public override void Init()
        {
            _fornecedor = FabricaObjetosPersistidos.ObtenhaFornecedor();
            _transportadora = FabricaObjetosPersistidos.ObtenhaTransportadora();
            _comprador = FabricaObjetosPersistidos.ObtenhaFuncionario();
            _funcionarioautorizador = FabricaObjetosPersistidos.ObtenhaFuncionario();
            _unidadeEstocadora = FabricaObjetosPersistidos.ObtenhaUnidade();
            _prazo = FabricaObjetosPersistidos.ObtenhaPrazo();
            _meioPagamento = FabricaObjetosPersistidos.ObtenhaMeioPagamento();

            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaFornecedor(_fornecedor);
            FabricaObjetosPersistidos.ExcluaTransportadora(_transportadora);
            FabricaObjetosPersistidos.ExcluaPessoa(_comprador);
            FabricaObjetosPersistidos.ExcluaPessoa(_funcionarioautorizador);
            FabricaObjetosPersistidos.ExcluaPessoa(_unidadeEstocadora);
            FabricaObjetosPersistidos.ExcluaPrazo(_prazo);
            FabricaObjetosPersistidos.ExcluaMeioPagamento(_meioPagamento);

            Session.Current.Flush();
        }
    }
}