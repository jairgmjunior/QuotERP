using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class GridOperacaoProducaoModel
    {
        public long Id { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public double Tempo { get; set; }
        public double Custo { get; set; }
        public bool Ativo { get; set; }

        [Display(Name = "Setor do departamento produtivo")]
        public string SetorProducao { get; set; }

        [Display(Name = "Departamento produtivo")]
        public string DepartamentoProducao { get; set; }
    }
}