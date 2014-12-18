using System;
using System.Collections.Generic;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Almoxarifado
{
    public class ReservaMaterial : DomainEmpresaBase<ReservaMaterial>
    {
        private IList<ReservaMaterialItem> _reservaMaterialItems = new List<ReservaMaterialItem>();

        public virtual long Numero { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual DateTime PrevisaoPrimeiraUtilizacao { get; set; }
        public virtual String Observacao { get; set; }
        public virtual Boolean Finalizada { get; set; }
        public virtual Pessoa Requerente { get; set; }
        public virtual Pessoa Unidade { get; set; }

        public virtual Colecao Colecao { get; set; }
        public virtual String Referencia { get; set; }

        public virtual IList<ReservaMaterialItem> ReservaMaterialItems
        {
            get { return _reservaMaterialItems; }
            set { _reservaMaterialItems = value; }
        }
    }
}