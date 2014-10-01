using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Almoxarifado
{
    public class DepositoMateralPersistencia: TestPersistentObject<DepositoMaterial>
    {
        private Pessoa _unidade;
        private Pessoa _funcionario;

        public override DepositoMaterial GetPersistentObject()
        {
            var depositoMaterial = FabricaObjetos.ObtenhaDepositoMaterial();
            
            depositoMaterial.Unidade = _unidade;
            depositoMaterial.AddFuncionario(_funcionario);

            return depositoMaterial;
        }

        public override void Init()
        {
            _funcionario = FabricaObjetosPersistidos.ObtenhaFuncionario();
            _unidade = FabricaObjetosPersistidos.ObtenhaUnidade();
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaPessoa(_unidade);
            FabricaObjetosPersistidos.ExcluaPessoa(_funcionario);
            Session.Current.Flush();
        }
    }
}