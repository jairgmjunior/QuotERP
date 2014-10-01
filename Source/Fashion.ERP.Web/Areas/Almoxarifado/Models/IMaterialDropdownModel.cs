namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public interface IMaterialDropdownModel
    {
        long? GeneroFiscal { get; set; }
        long? UnidadeMedida { get; set; }
        long? MarcaMaterial { get; set; }
        long? Familia { get; set; }
        long? TipoItem { get; set; }
        long? Categoria { get; set; }
        long? Subcategoria { get; set; }
        long? OrigemSituacaoTributaria { get; set; }
    }
}