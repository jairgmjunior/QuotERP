using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class PesquisaFornecedorModel
    {
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        
        [Display(Name = "Nome fantasia")]
        public string NomeFantasia { get; set; }
        
        [Display(Name = "Cpf/Cnpj")]
        public string CpfCnpj { get; set; }
        
        [Display(Name = "Cidade")]
        public long? Cidade { get; set; }
        
        [Display(Name = "Estado")]
        public long? Uf { get; set; } 

        [Display(Name = "Data")]
        public DateTime? DataInicio { get; set; }

        [Display(Name = "Até")]
        public DateTime? DataFim { get; set; }

        public string ModoConsulta { get; set; }

        [Display(Name = "Agrupar por")]
        public string AgruparPor { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }

        public IList<GridFornecedorModel> Grid { get; set; } 
    }
}