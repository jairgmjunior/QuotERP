namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public interface IModeloDropdownModel
    {
        long? Colecao { get; set; }
        long? Funcionario { get; set; }
        long? Natureza { get; set; }
        long? Classificacao { get; set; }
        long? Artigo { get; set; }
        long? ProdutoBase { get; set; }
        long? Comprimento { get; set; }
        long? Barra { get; set; }
        long? Grade { get; set; }
        long? Marca { get; set; }
        long? Segmento { get; set; }
    }
}