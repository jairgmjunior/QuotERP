using System;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.Framework.Common.Base;

namespace Fashion.ERP.Domain.Producao
{
    public class FichaTecnica : DomainEmpresaBase<FichaTecnica>, IPesquisavelPorData
    {
        public virtual string Tag { get; set; }
        public virtual long Ano { get; set; }
        public virtual string Observacao { get; set; }
        public virtual string Silk { get; set; }
        public virtual string Bordado { get; set; }
        public virtual string Pedraria { get; set; }
        public virtual long TempoMaximoProducao { get; set; }
        public virtual DateTime DataAlteracao { get; set; }
        public virtual DateTime DataCadastro { get; set; }
        public virtual string Descricao { get; set; }
        public virtual string Detalhamento { get; set; }
        
        //todo verificar com o Nélio a existência dessa propriedade
        public virtual DateTime ProgramacaoProducao { get; set; }

        //todo substituir essa propriedade pela soma das quantidades da matriz
        public virtual int QuantidadeProducaoAprovada { get; set; }
        
        public virtual Artigo Artigo { get; set; }
        public virtual Classificacao Classificacao { get; set; }
        public virtual Marca Marca { get; set; }
        public virtual Colecao Colecao { get; set; }
        public virtual Segmento Segmento { get; set; }
        public virtual Natureza Natureza { get; set; }
        public virtual ClassificacaoDificuldade ClassificacaoDificuldade { get; set; }

        public virtual FichaTecnicaMatriz FichaTecnicaMatriz { get; set; }
        public virtual int? Variante { get; set; }
        //public virtual Modelagem Modelagem { get; set; }
    }
}