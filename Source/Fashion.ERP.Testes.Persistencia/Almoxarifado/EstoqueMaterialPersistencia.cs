using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Almoxarifado
{
    public class EstoqueMaterialPersistencia : TestPersistentObject<EstoqueMaterial>
    {
        private DepositoMaterial _depositoMaterialDestino;
        private Material _material;
        private ReservaMaterial _reservaMaterial;
        
        public override EstoqueMaterial GetPersistentObject()
        {
            var estoqueCatalagoMaterial = FabricaObjetos.ObtenhaEstoqueMaterial();
            estoqueCatalagoMaterial.Material = _material;
            estoqueCatalagoMaterial.DepositoMaterial = _depositoMaterialDestino;
            
            return estoqueCatalagoMaterial;
        }

        public override void Init()
        {
            _depositoMaterialDestino = FabricaObjetosPersistidos.ObtenhaDepositoMaterial();
            _material = FabricaObjetosPersistidos.ObtenhaMaterial();
            _reservaMaterial = FabricaObjetosPersistidos.ObtenhaReservaMaterial();
            
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaDepositoMaterial(_depositoMaterialDestino);
            FabricaObjetosPersistidos.ExcluaMaterial(_material);
            FabricaObjetosPersistidos.ExcluaReservaMaterial(_reservaMaterial);
            Session.Current.Flush();
        }
    }
}