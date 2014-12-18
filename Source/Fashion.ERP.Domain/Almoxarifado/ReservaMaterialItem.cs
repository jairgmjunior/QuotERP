using System;
using System.Collections.Generic;

namespace Fashion.ERP.Domain.Almoxarifado
{
    public class ReservaMaterialItem : DomainEmpresaBase<ReservaMaterialItem>
    {
        private IList<ReservaMaterialItem> _reservaMaterialItemsSubstitutos = new List<ReservaMaterialItem>();

        public virtual Double QuantidadeReserva { get; set; }
        public virtual Double QuantidadeAtendida { get; set; }
        public virtual DateTime PrevisaoUtilizacao { get; set; }
        public virtual SituacaoReservaMaterialItem SituacaoReservaMaterialItem { get; set; }
        public virtual Material Material { get; set; }

        public virtual IList<ReservaMaterialItem> ReservaMaterialItemSubstitutos
        {
            get { return _reservaMaterialItemsSubstitutos; }
            set { _reservaMaterialItemsSubstitutos = value; }
        }
    }
}