using System;
using System.Collections.Generic;

namespace Fashion.ERP.Reporting.Compras.Models
{
    public class MaterialNecessidadeCompraMaterialModel
    {
        public virtual long? IdMaterial { get; set; }
        
        public virtual String Descricao { get; set; }
        
        public virtual String Referencia { get; set; }
        
        public virtual String Categoria { get; set; }
        
        public virtual String Subcategoria { get; set; }
        
        public virtual String UnidadeMedida { get; set; }
        
        public virtual String NomeFoto { get; set; }
        
        public virtual String Colecao { get; set; }
        
        public virtual double QuantidadeReserva { get; set; }

        public virtual double QuantidadeReservaCancelada { get; set; }

        public virtual double QuantidadeReservaAtendida { get; set; }

        public virtual String Fornecedor { get; set; }

        public virtual IEnumerable<String> Fornecedores { get; set; }

        public virtual Double QuantidadeCompras { get; set; }

        public virtual Double QuantidadeEstoque { get; set; }

        public virtual Double QuantidadeReservada { get; set; }
    }
}