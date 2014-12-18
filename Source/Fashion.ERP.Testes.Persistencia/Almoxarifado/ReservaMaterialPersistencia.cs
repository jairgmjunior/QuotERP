using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Almoxarifado
{
    public class ReservaMaterialPersistencia : TestPersistentObject<ReservaMaterial>
    {
        private Pessoa _funcionario;
        private Pessoa _unidade;
        private Colecao _colecao;
        private ReservaMaterialItem _reservaMaterialItem;
        private ReservaMaterialItem _reservaMaterialItemSubstituto;
        private Material _material;

        public override ReservaMaterial GetPersistentObject()
        {
            var reservaMaterial = FabricaObjetos.ObtenhaReservaMaterial();

            reservaMaterial.Requerente = _funcionario;
            reservaMaterial.Unidade = _unidade;
            reservaMaterial.Colecao = _colecao;
            reservaMaterial.ReservaMaterialItems.Add(_reservaMaterialItem);

            return reservaMaterial;
        }

        public override void Init()
        {
            _funcionario = FabricaObjetosPersistidos.ObtenhaFuncionario();
            _unidade = FabricaObjetosPersistidos.ObtenhaUnidade();
            _material = FabricaObjetosPersistidos.ObtenhaMaterial();
            _colecao = FabricaObjetosPersistidos.ObtenhaColecao();

            _reservaMaterialItem = FabricaObjetos.ObtenhaReservaMaterialItem();
            _reservaMaterialItem.Material = _material;

            _reservaMaterialItemSubstituto = FabricaObjetos.ObtenhaReservaMaterialItem();
            _reservaMaterialItemSubstituto.Material = _material;
            
            _reservaMaterialItem.ReservaMaterialItemSubstitutos.Add(_reservaMaterialItemSubstituto);

            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaPessoa(_funcionario);
            FabricaObjetosPersistidos.ExcluaPessoa(_unidade);
            FabricaObjetosPersistidos.ExcluaColecao(_colecao);

            Session.Current.Flush();
        }
    }
}