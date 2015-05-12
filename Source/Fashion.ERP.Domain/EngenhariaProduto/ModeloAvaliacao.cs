using System;
using System.Collections.Generic;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class ModeloAvaliacao : DomainEmpresaBase<ModeloAvaliacao>
    {
        private IList<ModeloAprovacao> _modelosAprovados = new List<ModeloAprovacao>();

        public virtual string Tag { get; set; }
        public virtual long SequenciaTag { get; set; }
        public virtual int Ano { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual String Observacao { get; set; }
        public virtual Colecao Colecao { get; set; }
        public virtual ClassificacaoDificuldade ClassificacaoDificuldade { get; set; }
        public virtual String Complemento { get; set; }
        public virtual Boolean Aprovado { get; set; }
        public virtual Boolean? Catalogo { get; set; }
        public virtual long QuantidadeTotaAprovacao { get; set; }
        public virtual ModeloReprovacao  ModeloReprovacao { get; set; }
        
        public virtual IList<ModeloAprovacao> ModelosAprovados
        {
            get { return _modelosAprovados; }
            set { _modelosAprovados = value; }
        }

        public virtual string ObtenhaTagCompleta()
        {
            return Tag + "/" + Ano;
        }
    }
}