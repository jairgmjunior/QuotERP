using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Almoxarifado
{
    public class ReservaEstoqueMaterialPersistencia : TestPersistentObject<ReservaEstoqueMaterial>
    {
        private Material _material;
        private Pessoa _unidade;

        public override ReservaEstoqueMaterial GetPersistentObject()
        {
            var reservaEstoqueMaterial = FabricaObjetos.ObtenhaReservaEstoqueMaterial();
            reservaEstoqueMaterial.Material = _material;
            reservaEstoqueMaterial.Unidade = _unidade;

            return reservaEstoqueMaterial;
        }

        public override void Init()
        {
            _unidade = FabricaObjetosPersistidos.ObtenhaUnidade();
            _material = FabricaObjetosPersistidos.ObtenhaMaterial();
            
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaMaterial(_material);
            FabricaObjetosPersistidos.ExcluaPessoa(_unidade);

            Session.Current.Flush();
        }
    }
}