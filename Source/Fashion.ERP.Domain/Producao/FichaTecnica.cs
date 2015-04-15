using System;
using System.Collections.Generic;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.Framework.Common.Base;

namespace Fashion.ERP.Domain.Producao
{
    public class FichaTecnica : DomainEmpresaBase<FichaTecnica>, IPesquisavelPorData
    {
        private readonly IList<FichaTecnicaFoto> _fichaTecnicaFotos = new List<FichaTecnicaFoto>();
        private readonly IList<FichaTecnicaSequenciaOperacional> _fichaTecnicaSequenciaOperacionals = new List<FichaTecnicaSequenciaOperacional>();
        private readonly IList<MaterialComposicaoCustoMatriz> _materialComposicaoCustoMatrizs = new List<MaterialComposicaoCustoMatriz>();

        public virtual string Tag { get; set; }
        public virtual long Ano { get; set; }
        public virtual string Observacao { get; set; }
        public virtual string Silk { get; set; }
        public virtual string Bordado { get; set; }
        public virtual string Pedraria { get; set; }
        public virtual long? TempoMaximoProducao { get; set; }
        public virtual DateTime DataAlteracao { get; set; }
        public virtual DateTime DataCadastro { get; set; }
        public virtual string Descricao { get; set; }
        public virtual string Detalhamento { get; set; }
        public virtual int? QuantidadeProducaoAprovada { get; set; }

        public virtual Artigo Artigo { get; set; }
        public virtual Classificacao Classificacao { get; set; }
        public virtual Marca Marca { get; set; }
        public virtual Colecao Colecao { get; set; }
        public virtual Segmento Segmento { get; set; }
        public virtual Natureza Natureza { get; set; }
        public virtual ClassificacaoDificuldade ClassificacaoDificuldade { get; set; }

        public virtual FichaTecnicaMatriz FichaTecnicaMatriz { get; set; }
        
        //public virtual int? Variante { get; set; }

        public virtual IList<FichaTecnicaFoto> FichaTecnicaFotos
        {
            get { return _fichaTecnicaFotos; }
        }

        public virtual IList<FichaTecnicaSequenciaOperacional> FichaTecnicaSequenciaOperacionals
        {
            get { return _fichaTecnicaSequenciaOperacionals; }
        }

        public virtual IList<MaterialComposicaoCustoMatriz> MaterialComposicaoCustoMatrizs
        {
            get { return _materialComposicaoCustoMatrizs; }
        }

        //public virtual Modelagem Modelagem { get; set; }
    }
}