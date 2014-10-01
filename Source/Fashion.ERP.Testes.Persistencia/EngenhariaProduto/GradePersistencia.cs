using System;
using Fashion.ERP.Domain.EngenhariaProduto;

namespace Fashion.ERP.Testes.Persistencia.EngenhariaProduto
{
    public class GradePersistencia : TestPersistentObject<Grade>
    {
        public override Grade GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaGrade();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}