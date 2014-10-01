using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Almoxarifado
{
    public class ConferenciaEntradaMaterialPersistencia : TestPersistentObject<ConferenciaEntradaMaterial>
    {
        private Pessoa _comprador;
        private ConferenciaEntradaMaterialItem _conferenciaEntradaMaterialItem;
        private Material _material;
        private UnidadeMedida _unidadeMedida;

        public override ConferenciaEntradaMaterial GetPersistentObject()
        {
            var conferenciaEntradaMaterial =  FabricaObjetos.ObtenhaConferenciaEntradaMaterial();
            conferenciaEntradaMaterial.Comprador = _comprador;
            conferenciaEntradaMaterial.ConferenciaEntradaMaterialItens.Add(_conferenciaEntradaMaterialItem);

            return conferenciaEntradaMaterial;
        }
        
        public override void Init()
        {
            _comprador = FabricaObjetosPersistidos.ObtenhaFuncionario();
            _material = FabricaObjetosPersistidos.ObtenhaMaterial();
            _unidadeMedida = FabricaObjetosPersistidos.ObtenhaUnidadeMedida();

            _conferenciaEntradaMaterialItem = FabricaObjetos.ObtenhaConferenciaEntradaMaterialItem();
            _conferenciaEntradaMaterialItem.Material = _material;
            _conferenciaEntradaMaterialItem.UnidadeMedida = _unidadeMedida;
            
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaPessoa(_comprador);
            
            Session.Current.Flush();
        }
    }
}