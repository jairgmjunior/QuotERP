namespace Fashion.ERP.Web.Models
{
    public interface IPesquisaModel
    {
        string TipoRelatorio { get; set; }
        
        string AgruparPor { get; set; }
        
        string OrdenarPor { get; set; }

        string OrdenarEm { get; set; }
    }
}