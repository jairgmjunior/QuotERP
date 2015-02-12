using System;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class FichaTecnica : DomainBase<FichaTecnica>
    {
        public virtual string Referencia { get; set; }
        public virtual string Descricao { get; set; }
        public virtual string Detalhamento { get; set; }
        public virtual int? Sequencia { get; set; }
        public virtual DateTime ProgramacaoProducao { get; set; }
        public virtual DateTime DataCadastro { get; set; }
        public virtual string Modelagem { get; set; }
        public virtual int QuantidadeProducao { get; set; }

        //public virtual Modelo Modelo { get; set; }
        public virtual Marca Marca { get; set; }
        public virtual Colecao Colecao { get; set; }
        public virtual Barra Barra { get; set; }
        public virtual Segmento Segmento { get; set; }
        public virtual ProdutoBase ProdutoBase { get; set; }
        public virtual Comprimento Comprimento { get; set; }
        public virtual Natureza Natureza { get; set; }
        public virtual ClassificacaoDificuldade ClassificacaoDificuldade { get; set; }
        public virtual Grade Grade { get; set; }
    }
}