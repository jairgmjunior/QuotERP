using System;
using System.Collections.Generic;
using Fashion.Framework.Common.Base;

namespace Fashion.ERP.Domain.Producao
{
    public class Producao : DomainEmpresaBase<Producao>, IPesquisavelPorData
    {
        private readonly IList<ProducaoItem> _producaoItens = new List<ProducaoItem>();
        
        public virtual long Lote { get; set; }
        public virtual long Ano { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual String Descricao { get; set; }
        public virtual String Observacao { get; set; }
        public virtual DateTime DataAlteracao { get; set; }
        public virtual TipoProducao TipoProducao { get; set; }
        public virtual SituacaoProducao SituacaoProducao{ get; set; }
        public virtual RemessaProducao RemessaProducao { get; set; }
        public virtual ProducaoProgramacao ProducaoProgramacao { get; set; }

        public virtual IList<ProducaoItem> ProducaoItens
        {
            get { return _producaoItens; }
        }
    }
}