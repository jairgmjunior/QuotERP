using Fashion.ERP.Web.Areas.Comum.Models;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class RecebimentoChequeRecebidoModel
    {
        public double Valor { get; set; }
        public MeioPagamentoModel MeioPagamento { get; set; }
    }
}