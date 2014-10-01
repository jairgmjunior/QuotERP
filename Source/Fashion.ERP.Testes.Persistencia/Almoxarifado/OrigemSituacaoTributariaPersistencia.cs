using Fashion.ERP.Domain.Almoxarifado;

namespace Fashion.ERP.Testes.Persistencia.Almoxarifado
{
    public class OrigemSituacaoTributariaPersistencia : TestPersistentObject<OrigemSituacaoTributaria>
    {
        public override OrigemSituacaoTributaria GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaOrigemSituacaoTributaria();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}