using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class FornecedorPersistencia : TestPersistentObject<Pessoa>
    {
        private TipoFornecedor _tipoFornecedor;

        public override Pessoa GetPersistentObject()
        {
            var fornecedor = FabricaObjetos.ObtenhaFornecedor();
            
            fornecedor.Fornecedor.TipoFornecedor = _tipoFornecedor;

            return fornecedor;
        }

        public override void Init()
        {
            _tipoFornecedor = FabricaObjetosPersistidos.ObtenhaTipoFornecedor();
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaTipoFornecedor(_tipoFornecedor);
            Session.Current.Flush();
        }
    }
}