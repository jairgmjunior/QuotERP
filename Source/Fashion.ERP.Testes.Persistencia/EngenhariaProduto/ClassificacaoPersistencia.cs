using Fashion.ERP.Domain.EngenhariaProduto;

namespace Fashion.ERP.Testes.Persistencia.EngenhariaProduto
{
    public class ClassificacaoPersistencia : TestPersistentObject<Classificacao>
    {
        public override Classificacao GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaClassificacao();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}