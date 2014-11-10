using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Repository;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Almoxarifado
{
    public class SaidaMaterialPersistencia : TestPersistentObject<SaidaMaterial>
    {
        private DepositoMaterial _depositoMaterialOrigem;
        private DepositoMaterial _depositoMaterialDestino;
        
        public override SaidaMaterial GetPersistentObject()
        {
            var saidaMaterial = FabricaObjetos.ObtenhaSaidaMaterial();
            saidaMaterial.DepositoMaterialDestino = _depositoMaterialDestino;
            saidaMaterial.DepositoMaterialOrigem= _depositoMaterialOrigem;
            
            return saidaMaterial;
        }

        public override void Init()
        {
            _depositoMaterialOrigem = FabricaObjetosPersistidos.ObtenhaDepositoMaterial();
            _depositoMaterialDestino = FabricaObjetosPersistidos.ObtenhaDepositoMaterial();
            
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            RepositoryFactory.Create<DepositoMaterial>().Delete(_depositoMaterialOrigem);
            RepositoryFactory.Create<DepositoMaterial>().Delete(_depositoMaterialDestino);

            Session.Current.Flush();
        }
    }
}