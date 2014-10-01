using System;

namespace Fashion.ERP.Domain.Comum
{
    public class Funcionario : DomainBase<Funcionario>
    {
        public virtual long Codigo { get; set; }
        public virtual double PercentualComissao { get; set; }
        public virtual DateTime DataCadastro { get; set; }
        public virtual DateTime? DataDesligamento { get; set; }
        public virtual FuncaoFuncionario FuncaoFuncionario { get; set; }
        public virtual bool Ativo { get; set; }
    }
}