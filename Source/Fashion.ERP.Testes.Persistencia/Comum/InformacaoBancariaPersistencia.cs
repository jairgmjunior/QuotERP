using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class InformacaoBancariaPersistencia : TestPersistentObject<InformacaoBancaria>
    {
        private Pessoa _pessoa;

        public override InformacaoBancaria GetPersistentObject()
        {
            var informacaoBancaria =  FabricaObjetos.ObtenhaInformacaoBancaria();
            informacaoBancaria.Pessoa = _pessoa;
            
            return informacaoBancaria;
        }

        public override void Init()
        {
            _pessoa = FabricaObjetosPersistidos.ObtenhaFuncionario();
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaPessoa(_pessoa);
            Session.Current.Flush();
        }
    }
}