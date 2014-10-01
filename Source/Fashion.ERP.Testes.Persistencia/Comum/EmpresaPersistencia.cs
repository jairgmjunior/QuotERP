using System;
using Fashion.ERP.Domain.Comum;
using NUnit.Framework;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    [TestFixture]
    public class EmpresaPersistencia : TestPersistentObject<Pessoa>
    {
        public override Pessoa GetPersistentObject()
        {
            return FabricaObjetos.ObtenhaEmpresa();
        }

        public override void Init()
        {
        }

        public override void Cleanup()
        {
        }
    }
}