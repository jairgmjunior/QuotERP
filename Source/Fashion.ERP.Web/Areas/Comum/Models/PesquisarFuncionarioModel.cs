using System.Collections.Generic;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class PesquisarFuncionarioModel : PesquisarModel
    {
        public IList<FuncaoFuncionario> FuncaoFuncionario { get; set; }

        public bool SelecaoUnica { get; set; }

        public string NomeGridOrigem { get; set; }

        public string ValidacaoDuplicadosGridOrigem { get; set; }
    }
}