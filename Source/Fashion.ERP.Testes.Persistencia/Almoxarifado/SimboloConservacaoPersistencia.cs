using Fashion.ERP.Domain;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.UnitOfWork;


namespace Fashion.ERP.Testes.Persistencia.Almoxarifado
{
    public class SimboloConservacaoPersistencia : TestPersistentObject<SimboloConservacao>
    {
        private Arquivo _arquivo;

        public override SimboloConservacao GetPersistentObject()
        {
            var simboloconservacao = FabricaObjetos.ObtenhaSimboloConservacao();
            simboloconservacao.Foto = _arquivo;

            return simboloconservacao;
        }

        public override void Init()
        {
            _arquivo = FabricaObjetos.ObtenhaArquivo();
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            Session.Current.Flush();
        }
    }
}
