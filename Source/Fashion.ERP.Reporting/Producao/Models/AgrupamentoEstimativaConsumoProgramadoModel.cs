using System;
using System.Collections.Generic;

namespace Fashion.ERP.Reporting.Producao.Models
{
    public class AgrupamentoEstimativaConsumoProgramadoModel
    {
        public virtual String Valor { get; set; }

        public virtual String NomeAgrupamento { get; set; }

        public IEnumerable<MaterialEstimativaConsumoProgramadoModel> Materiais { get; set; }
    }
}