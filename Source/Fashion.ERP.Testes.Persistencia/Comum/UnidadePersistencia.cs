using System;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class UnidadePersistencia : TestPersistentObject<Pessoa>
    {
        public override Pessoa GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaUnidade();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}