using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class UsuarioPersistencia : TestPersistentObject<Usuario>
    {
        private Pessoa _funcionario;
        private Permissao _permissao;
        private PerfilDeAcesso _perfilDeAcesso;

        public override Usuario GetPersistentObject()
        {
            var usuario = FabricaObjetos.ObtenhaUsuario();
            usuario.Funcionario = _funcionario;
            usuario.AddPermissao(_permissao);
            usuario.AddPerfilDeAcesso(_perfilDeAcesso);

            return usuario;
        }

        public override void Init()
        {
            _funcionario = FabricaObjetosPersistidos.ObtenhaFuncionario();
            _perfilDeAcesso = FabricaObjetosPersistidos.ObtenhaPerfilDeAcesso();
            _permissao = FabricaObjetosPersistidos.ObtenhaPermissao();

            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaPessoa(_funcionario);
            FabricaObjetosPersistidos.ExcluaPerfilDeAcesso(_perfilDeAcesso);
            FabricaObjetosPersistidos.ExcluaPermissao(_permissao);

            Session.Current.Flush();
        }
    }
}