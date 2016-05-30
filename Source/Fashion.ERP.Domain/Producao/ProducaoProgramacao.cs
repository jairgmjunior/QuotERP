using System;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Producao
{
    public class ProducaoProgramacao : DomainEmpresaBase<ProducaoProgramacao>
    {
        public virtual DateTime Data { get; set; }
        public virtual DateTime DataProgramada { get; set; }
        public virtual String Observacao { get; set; }
        public virtual long Quantidade { get; set; }
        public virtual Pessoa Funcionario { get; set; }
    }
}