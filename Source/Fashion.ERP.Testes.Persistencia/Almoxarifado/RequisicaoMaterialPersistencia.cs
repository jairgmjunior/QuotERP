using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Almoxarifado
{
    public class RequisicaoMaterialPersistencia : TestPersistentObject<RequisicaoMaterial>
    {
        private Pessoa _funcionario;
        private Pessoa _unidade;
        private CentroCusto _centroCusto;
        private RequisicaoMaterialItem _requisicaoMaterialItem;
        private Material _material;
        private TipoItem _tipoItem;

        public override RequisicaoMaterial GetPersistentObject()
        {
            var requisicaoMaterial = FabricaObjetos.ObtenhaRequisicaoMaterial();

            requisicaoMaterial.Requerente = _funcionario;
            requisicaoMaterial.UnidadeRequerente = _unidade;
            requisicaoMaterial.UnidadeRequisitada = _unidade;
            requisicaoMaterial.RequisicaoMaterialItems.Add(_requisicaoMaterialItem);
            requisicaoMaterial.TipoItem = _tipoItem;
            requisicaoMaterial.CentroCusto = _centroCusto;

            return requisicaoMaterial;
        }

        public override void Init()
        {
            _funcionario = FabricaObjetosPersistidos.ObtenhaFuncionario();
            _unidade = FabricaObjetosPersistidos.ObtenhaUnidade();
            _material = FabricaObjetosPersistidos.ObtenhaMaterial();
            _centroCusto = FabricaObjetosPersistidos.ObtenhaCentroCusto();
            _tipoItem = FabricaObjetosPersistidos.ObtenhaTipoItem();


            _requisicaoMaterialItem = FabricaObjetos.ObtenhaRequisicaoMaterialItem();
            _requisicaoMaterialItem.Material = _material;
            _requisicaoMaterialItem.RequisicaoMaterialItemCancelado = FabricaObjetos.ObtenhaRequisicaoMaterialItemCancelado();
            
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaPessoa(_funcionario);
            FabricaObjetosPersistidos.ExcluaPessoa(_unidade);
            FabricaObjetosPersistidos.ExcluaCentroCusto(_centroCusto);
            FabricaObjetosPersistidos.ExcluaMaterial(_material);
            FabricaObjetosPersistidos.ExcluaTipoItem(_tipoItem);

            Session.Current.Flush();
        }
    }
}