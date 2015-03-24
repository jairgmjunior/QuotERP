using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class GridFichaTecnicaProcessosModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Setor Produção")]
        [Required(ErrorMessage = "Informe o setor de produção")]
        public string SetorProducao { get; set; }

        [Display(Name = "Departamento Produção")]
        [Required(ErrorMessage = "Informe o departamento de produção")]
        public string DepartamentoProducao { get; set; }

        [Display(Name = "Operação Produção")]
        [Required(ErrorMessage = "Informe a operação de produção")]
        public string OperacaoProducao { get; set; }

        [Display(Name = "Custo(R$)")]
        [Required(ErrorMessage = "Informe o custo")]
        public double Custo { get; set; }

        [Display(Name = "Tempo(s)")]
        [Required(ErrorMessage = "Informe o tempo")]
        public double Tempo { get; set; }

        [Display(Name = "Peso Produtividade")]
        [Required(ErrorMessage = "Informe o peso produtividade")]
        public double PesoProdutividade { get; set; }
    }
}