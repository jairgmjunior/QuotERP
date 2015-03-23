using System;
using System.Collections.Generic;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.Producao;

namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class ModeloAprovado : DomainBase<ModeloAprovado>
    {
        private IList<FichaTecnica> _fichaTecnicas = new List<FichaTecnica>();

        public virtual string Tag { get; set; }
        public virtual int Ano { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual String Observacao { get; set; }
        public virtual DateTime DataProgramacaoProducao { get; set; }
        public virtual long Quantidade { get; set; }

        public virtual Colecao Colecao { get; set; }
        public virtual Pessoa Funcionario { get; set; }
        public virtual ClassificacaoDificuldade ClassificacaoDificuldade { get; set; }

        public virtual IList<FichaTecnica> FichaTecnicas
        {
            get { return _fichaTecnicas; }
            set { _fichaTecnicas = value; }
        }
    }
}