using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class GridMaterialCustoModel
    {
        public long? Id { get; set; }

        public long CodigoFornecedor { get; set; }

        [Display(Name = "Fornecedor")]
        public string Fornecedor { get; set; }
        
        [Display(Name = "Data")]
        public String DataCustoString { get; set; }
        
        [Display(Name = "Custo de Aquisição")]
        public double CustoAquisicao { get; set; }

        [Display(Name = "Custo")]
        public double Custo { get; set; }

        public Boolean Ativo { get; set; }

        [Display(Name = "Cadastro Manual")]
        public Boolean CadastroManual { get; set; }

        [Display(Name = "Responsável")]
        public String Responsavel { get; set; }

        public Boolean Editavel { get; set; }
    }
}