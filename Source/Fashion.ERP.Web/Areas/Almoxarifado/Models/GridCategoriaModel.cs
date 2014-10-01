namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class GridCategoriaModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string CodigoNcm { get; set; }
        public string GeneroCategoria { get; set; }
        public string TipoCategoria { get; set; }
        public bool Ativo { get; set; }
    }
}