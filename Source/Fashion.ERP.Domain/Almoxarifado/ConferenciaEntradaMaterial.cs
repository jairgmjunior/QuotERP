using System;
using System.Collections.Generic;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Almoxarifado
{
    public class ConferenciaEntradaMaterial : DomainBase<ConferenciaEntradaMaterial>
    {
        private IList<ConferenciaEntradaMaterialItem> _conferenciaEntradaMaterialItens = new List<ConferenciaEntradaMaterialItem>();

        public virtual long Numero { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual DateTime DataAtualizacao { get; set; }
        public virtual string Observacao { get; set; }
        public virtual bool Conferido { get; set; }
        public virtual bool Autorizado { get; set; }
        public virtual Pessoa Comprador { get; set; }

        public virtual IList<ConferenciaEntradaMaterialItem> ConferenciaEntradaMaterialItens
        {
            get { return _conferenciaEntradaMaterialItens; }
            set { _conferenciaEntradaMaterialItens = value; }
        }
    }
}