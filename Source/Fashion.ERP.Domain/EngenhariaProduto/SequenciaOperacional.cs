using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class SequenciaOperacional : DomainBase<SequenciaOperacional>
    {
        public virtual long Sequencia { get; set; }
        public virtual DepartamentoProducao DepartamentoProducao { get; set; }
        public virtual SetorProducao SetorProducao { get; set; }
        public virtual OperacaoProducao OperacaoProducao { get; set; }
    }
}
