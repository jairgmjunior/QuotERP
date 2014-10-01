namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class GridUnidadeMedidaModel
    {
        public long Id { get; set; }
        public string Sigla { get; set; }
        public string Descricao { get; set; }
        public double FatorMultiplicativo { get; set; }
        public bool Ativo { get; set; }
    }
}