using System;
using System.Collections.Generic;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class SequenciaProducao : DomainBase<SequenciaProducao>
    {
        public virtual DateTime? DataEntrada { get; set; }
        public virtual DateTime? DataSaida { get; set; }
        public virtual DepartamentoProducao DepartamentoProducao { get; set; }
        public virtual SetorProducao SetorProducao { get; set; }
    }
}