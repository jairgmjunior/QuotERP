using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Almoxarifado
{
    public class SaidaMaterialPersistencia : TestPersistentObject<SaidaMaterial>
    {
        private DepositoMaterial _depositoMaterialOrigem;
        private DepositoMaterial _depositoMaterialDestino;
        private Material _material;
        private EstoqueMaterial _estoqueMaterial;
        
        public override SaidaMaterial GetPersistentObject()
        {
            var saidaMaterial = FabricaObjetos.ObtenhaSaidaMaterial();
            
            var saidaItemMaterial = FabricaObjetos.ObtenhaSaidaItemMaterial();
            saidaItemMaterial.Material = _material;
            saidaItemMaterial.MovimentacaoEstoqueMaterial = FabricaObjetos.ObtenhaMovimentacaoEstoqueMaterial();
            saidaItemMaterial.MovimentacaoEstoqueMaterial.EstoqueMaterial = _estoqueMaterial;
            
            saidaMaterial.DepositoMaterialDestino = _depositoMaterialDestino;
            saidaMaterial.DepositoMaterialOrigem = _depositoMaterialOrigem;
            saidaMaterial.AddSaidaItemMaterial(saidaItemMaterial);
            return saidaMaterial;
        }

        public override void Init()
        {
            _depositoMaterialDestino = FabricaObjetosPersistidos.ObtenhaDepositoMaterial();
            _depositoMaterialOrigem = FabricaObjetosPersistidos.ObtenhaDepositoMaterial();
            _material = FabricaObjetosPersistidos.ObtenhaMaterial();
            _estoqueMaterial = FabricaObjetosPersistidos.ObtenhaEstoqueMaterial();
            
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaDepositoMaterial(_depositoMaterialDestino);
            FabricaObjetosPersistidos.ExcluaDepositoMaterial(_depositoMaterialOrigem);
            FabricaObjetosPersistidos.ExcluaMaterial(_material);
            FabricaObjetosPersistidos.ExcluaEstoqueMaterial(_estoqueMaterial);

            Session.Current.Flush();
        }
    }
}