using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Almoxarifado
{
    public class SaidaItemMaterialPersistencia : TestPersistentObject<SaidaItemMaterial>
    {
        private SaidaMaterial _saidaMaterial;
        private Material _material;

        public override SaidaItemMaterial GetPersistentObject()
        {
            var saidaItemMaterial = FabricaObjetos.ObtenhaSaidaItemMaterial();

            saidaItemMaterial.Material = _material;
            saidaItemMaterial.SaidaMaterial = _saidaMaterial;
            
            return saidaItemMaterial;
        }

        public override void Init()
        {
            _saidaMaterial = FabricaObjetosPersistidos.ObtenhaSaidaMaterial();
            _material = FabricaObjetosPersistidos.ObtenhaMaterial();
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaSaidaMaterial(_saidaMaterial);
            FabricaObjetosPersistidos.ExcluaMaterial(_material);
            Session.Current.Flush();
        }
    }
}
