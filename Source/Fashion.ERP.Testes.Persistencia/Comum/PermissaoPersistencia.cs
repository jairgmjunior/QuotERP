using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class PermissaoPersistencia : TestPersistentObject<Permissao>
    {
        private Permissao _permissaoPai;

        public override Permissao GetPersistentObject()
        {
            var permissao = FabricaObjetos.ObtenhaPermissao();

            permissao.PermissaoPai = _permissaoPai;

            return permissao;
        }

        public override void Init()
        {
            _permissaoPai = FabricaObjetosPersistidos.ObtenhaPermissao();
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaPermissao(_permissaoPai);

            Session.Current.Flush();
        }
    }
}