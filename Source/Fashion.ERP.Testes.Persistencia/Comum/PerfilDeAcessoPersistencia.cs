using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class PerfilDeAcessoPersistencia : TestPersistentObject<PerfilDeAcesso>
    {
        private Permissao _permissao;

        public override PerfilDeAcesso GetPersistentObject()
        {
            var perfilDeAcesso = FabricaObjetos.ObtenhaPerfilDeAcesso();

            perfilDeAcesso.AddPermissao(_permissao);

            return perfilDeAcesso;
        }

        public override void Init()
        {
            _permissao = FabricaObjetosPersistidos.ObtenhaPermissao();
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaPermissao(_permissao);
            Session.Current.Flush();
        }
    }
}