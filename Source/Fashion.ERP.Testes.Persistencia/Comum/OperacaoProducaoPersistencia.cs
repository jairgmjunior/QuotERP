using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fashion.ERP.Testes.Persistencia.Comum
{
    public class OperacaoProducaoPersistencia : TestPersistentObject<OperacaoProducao>
    {
        private SetorProducao _setorProducao;

        public override OperacaoProducao GetPersistentObject()
        {
            var operacaoProducao = FabricaObjetos.ObtenhaOperacao();
            operacaoProducao.SetorProducao = _setorProducao;

            return operacaoProducao;
        }

        public override void Init()
        {
            _setorProducao = FabricaObjetosPersistidos.ObtenhaSetorProducao();
            Session.Current.Flush();
        }

        public override void Cleanup()
        {
            FabricaObjetosPersistidos.ExcluaSetorProducao(_setorProducao);

            Session.Current.Flush();
        }
    }
}
