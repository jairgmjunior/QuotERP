using System.Web.Configuration;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Repository;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Almoxarifado
{
    public class ReferenciaExternaPersistencia : TestPersistentObject<ReferenciaExterna>
    {
        private Material _material;
        private Pessoa _fornecedor;

        public override ReferenciaExterna GetPersistentObject()
        {
            var referenciaExterna =  FabricaObjetos.ObtenhaReferenciaExterna();
            referenciaExterna.Material = _material;
            referenciaExterna.Fornecedor = _fornecedor;
            return referenciaExterna;
        }

        public override void Init()
        {
            _material = FabricaObjetosPersistidos.ObtenhaMaterial();
            _fornecedor = FabricaObjetosPersistidos.ObtenhaFornecedor();

            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaMaterial(_material);
            FabricaObjetosPersistidos.ExcluaFornecedor(_fornecedor);

            Session.Current.Flush();
        }
    }
}