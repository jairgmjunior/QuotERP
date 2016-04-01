using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class GridMaterialCustoModel
    {
        public long? Id { get; set; }

        public long CodigoFornecedor { get; set; }

        public string Fornecedor { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Data { get; set; }

        public double CustoAquisicao { get; set; }

        public double Custo { get; set; }

        public Boolean Ativo { get; set; }

        public Boolean CadastroManual { get; set; }

        public String Responsavel { get; set; }

        public Boolean Editavel { get; set; }
    }
}