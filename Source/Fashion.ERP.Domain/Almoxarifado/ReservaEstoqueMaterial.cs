using System;
using System.Collections.Generic;

namespace Fashion.ERP.Domain.Almoxarifado
{
    public class ReservaEstoqueMaterial : DomainEmpresaBase<ReservaEstoqueMaterial>
    {
        private IList<ReservaMaterialItem> _reservaMaterialItems = new List<ReservaMaterialItem>();

        public virtual Double Quantidade { get; set; }

        public virtual IList<ReservaMaterialItem> ReservaMaterialItems
        {
            get { return _reservaMaterialItems; }
            set { _reservaMaterialItems = value; }
        }
    }
}