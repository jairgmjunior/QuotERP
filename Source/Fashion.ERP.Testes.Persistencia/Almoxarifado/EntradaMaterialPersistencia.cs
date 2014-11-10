using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Almoxarifado
{
    public class EntradaMaterialPersistencia : TestPersistentObject<EntradaMaterial>
    {
        private DepositoMaterial _depositoMaterialOrigem;
        private DepositoMaterial _depositoMaterialDestino;
        private Pessoa _fornecedor;

        public override EntradaMaterial GetPersistentObject()
        {
            var entradaMaterial = FabricaObjetos.ObtenhaEntradaMaterial();

            entradaMaterial.DepositoMaterialDestino = _depositoMaterialDestino;
            entradaMaterial.DepositoMaterialOrigem = _depositoMaterialOrigem;
            entradaMaterial.Fornecedor = _fornecedor;

            return entradaMaterial;
        }

        public override void Init()
        {
            _fornecedor = FabricaObjetosPersistidos.ObtenhaFornecedor();
            _depositoMaterialDestino = FabricaObjetosPersistidos.ObtenhaDepositoMaterial();
            _depositoMaterialOrigem = FabricaObjetosPersistidos.ObtenhaDepositoMaterial();
            
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaDepositoMaterial(_depositoMaterialDestino);
            FabricaObjetosPersistidos.ExcluaDepositoMaterial(_depositoMaterialOrigem);
            FabricaObjetosPersistidos.ExcluaFornecedor(_fornecedor);
            
            Session.Current.Flush();
        }
    }
}