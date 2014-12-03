using Fashion.ERP.Domain;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Almoxarifado
{
    public class MaterialPersistencia : TestPersistentObject<Material>
    {
        private OrigemSituacaoTributaria _origemSituacaoTributaria;
        private UnidadeMedida _unidadeMedida;
        private MarcaMaterial _marcaMaterial;
        private Subcategoria _subcategoria;
        private Familia _familia;
        private Arquivo _arquivo;
        private Pessoa _fornecedor;

        public override Material GetPersistentObject()
        {
            var material = FabricaObjetos.ObtenhaMaterial();

            material.OrigemSituacaoTributaria = _origemSituacaoTributaria;
            material.UnidadeMedida = _unidadeMedida;
            material.MarcaMaterial = _marcaMaterial;
            material.Subcategoria = _subcategoria;
            material.Familia = _familia;
            material.Foto = _arquivo;

            var custoMaterial = FabricaObjetos.ObtenhaCustoMaterial();
            custoMaterial.Fornecedor = _fornecedor;

            material.CustoMaterials.Add(custoMaterial);

            return material;
        }

        public override void Init()
        {
            _origemSituacaoTributaria = FabricaObjetosPersistidos.ObtenhaOrigemSituacaoTributaria();
            _unidadeMedida = FabricaObjetosPersistidos.ObtenhaUnidadeMedida();
            _marcaMaterial = FabricaObjetosPersistidos.ObtenhaMarcaMaterial();
            _subcategoria = FabricaObjetosPersistidos.ObtenhaSubCategoria();
            _familia = FabricaObjetosPersistidos.ObtenhaFamilia();
            _arquivo = FabricaObjetos.ObtenhaArquivo();
            _fornecedor = FabricaObjetosPersistidos.ObtenhaFornecedor();
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaOrigemSituacaoTributaria(_origemSituacaoTributaria);
            FabricaObjetosPersistidos.ExcluaUnidadeMedida(_unidadeMedida);
            FabricaObjetosPersistidos.ExcluaSubcategoria(_subcategoria);
            FabricaObjetosPersistidos.ExcluaMarcaMaterial(_marcaMaterial);
            FabricaObjetosPersistidos.ExcluaFamilia(_familia);
            FabricaObjetosPersistidos.ExcluaPessoa(_fornecedor);
            Session.Current.Flush();
        }
    }
}