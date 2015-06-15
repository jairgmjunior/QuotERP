using System;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Common.Base;

namespace Fashion.ERP.Domain.Producao
{
    public class ProgramacaoProducao : DomainEmpresaBase<ProgramacaoProducao>, IPesquisavelPorData
    {
        public virtual long Numero { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual DateTime DataProgramada { get; set; }
        public virtual String Observacao { get; set; }
        public virtual long Quantidade { get; set; }
        public virtual DateTime DataAlteracao { get; set; }

        public virtual Pessoa Funcionario { get; set; }
        public virtual Colecao Colecao { get; set; }
        public virtual FichaTecnica FichaTecnica { get; set; }

        public virtual ProgramacaoProducaoMatrizCorte ProgramacaoProducaoMatrizCorte { get; set; }
    }
}