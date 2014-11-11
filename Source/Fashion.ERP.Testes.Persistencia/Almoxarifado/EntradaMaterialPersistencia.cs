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
        private Material _material;
        private UnidadeMedida _unidadeMedida;
        private EstoqueMaterial _estoqueMaterial;

        public override EntradaMaterial GetPersistentObject()
        {
            var entradaMaterial = FabricaObjetos.ObtenhaEntradaMaterial();

            var entradaItemMaterial = FabricaObjetos.ObtenhaEntradaItemMaterial();
            entradaItemMaterial.Material = _material;
            entradaItemMaterial.UnidadeMedidaCompra = _unidadeMedida;
            entradaItemMaterial.MovimentacaoEstoqueMaterial = FabricaObjetos.ObtenhaMovimentacaoEstoqueMaterial();
            entradaItemMaterial.MovimentacaoEstoqueMaterial.EstoqueMaterial = _estoqueMaterial;

            entradaMaterial.DepositoMaterialDestino = _depositoMaterialDestino;
            entradaMaterial.DepositoMaterialOrigem = _depositoMaterialOrigem;
            entradaMaterial.Fornecedor = _fornecedor;
            entradaMaterial.AddEntradaItemMaterial(entradaItemMaterial);

            return entradaMaterial;
        }

        public override void Init()
        {
            _fornecedor = FabricaObjetosPersistidos.ObtenhaFornecedor();
            _depositoMaterialDestino = FabricaObjetosPersistidos.ObtenhaDepositoMaterial();
            _depositoMaterialOrigem = FabricaObjetosPersistidos.ObtenhaDepositoMaterial();
            _material = FabricaObjetosPersistidos.ObtenhaMaterial();
            _unidadeMedida = FabricaObjetosPersistidos.ObtenhaUnidadeMedida();
            _estoqueMaterial = FabricaObjetosPersistidos.ObtenhaEstoqueMaterial();
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaDepositoMaterial(_depositoMaterialDestino);
            FabricaObjetosPersistidos.ExcluaDepositoMaterial(_depositoMaterialOrigem);
            FabricaObjetosPersistidos.ExcluaFornecedor(_fornecedor);
            FabricaObjetosPersistidos.ExcluaMaterial(_material);
            FabricaObjetosPersistidos.ExcluaUnidadeMedida(_unidadeMedida);
            FabricaObjetosPersistidos.ExcluaEstoqueMaterial(_estoqueMaterial);

            Session.Current.Flush();
        }
    }
}