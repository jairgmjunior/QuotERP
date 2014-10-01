using System;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class ProgramacaoBordado : DomainBase<ProgramacaoBordado>
    {
        public virtual string Descricao { get; set; }
        public virtual string NomeArquivo { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual int QuantidadePontos { get; set; }
        public virtual int QuantidadeCores { get; set; }
        public virtual string Aplicacao { get; set; }
        public virtual string Observacao { get; set; }

        //public virtual Modelo Modelo { get; set; }
        public virtual Pessoa ProgramadorBordado { get; set; }
        public virtual Arquivo Arquivo { get; set; }
    }
}