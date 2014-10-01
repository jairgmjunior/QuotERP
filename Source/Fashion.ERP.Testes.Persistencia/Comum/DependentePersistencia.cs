using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class DependentePersistencia : TestPersistentObject<Dependente>
    {
        private Cliente _cliente;
        private GrauDependencia _grauDependencia;

        public override Dependente GetPersistentObject()
        {
            var dependente = FabricaObjetos.ObtenhaDependente();
            dependente.GrauDependencia = _grauDependencia;
            _cliente.AddDependente(dependente);
            
            return dependente;
        }

        public override void Init()
        {
            _cliente = FabricaObjetosPersistidos.ObtenhaCliente();
            _grauDependencia = FabricaObjetosPersistidos.ObtenhaGrauDependencia();
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaCliente(_cliente);
            FabricaObjetosPersistidos.ExcluaGrauDependencia(_grauDependencia);
            Session.Current.Flush();
        }

        public override void DesfacaAssociacoes(Dependente persistentObject)
        {
            _cliente.RemoveDependente(persistentObject);
            //persistentObject.Cliente = null;
        }
    }
}