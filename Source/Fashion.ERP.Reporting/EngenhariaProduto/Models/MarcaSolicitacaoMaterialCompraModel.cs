using System;
using System.Collections.Generic;

namespace Fashion.ERP.Reporting.EngenhariaProduto.Models
{
    public class MarcaSolicitacaoMaterialCompraModel
    {
        public virtual String Marca { get; set; }

        public IEnumerable<MaterialSolicitacaoMaterialCompraModel> Materiais { get; set; }
    }
}