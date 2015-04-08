using System;
using System.Collections.Generic;
using System.Linq;
using Fashion.ERP.Domain.EngenhariaProduto;
using Telerik.Reporting.Expressions;

namespace Fashion.ERP.Reporting.EngenhariaProduto
{
    public partial class MateriaisModelosAprovadosReport : Telerik.Reporting.Report
    {
        public MateriaisModelosAprovadosReport()
        {
            InitializeComponent();
        }

        [Function(Namespace = "MateriaisModelosAprovadosReport")]
        public static object ObtenhaMaterialComposicaoModelos(object sequenciasProducaoObject, object departamentosProducaoObject)
        {
            if (sequenciasProducaoObject == null || departamentosProducaoObject == null)
                return null;

            var sequenciasProducao = sequenciasProducaoObject as IEnumerable<SequenciaProducao>;
            
            if (sequenciasProducao == null)
                return null;

            var departamentosProducaoId = (departamentosProducaoObject as Object[]).Cast<long>().ToList();

            if (departamentosProducaoId == null || !departamentosProducaoId.Any())
                return sequenciasProducao.SelectMany(seq => seq.MaterialComposicaoModelos);

            return sequenciasProducao.Where(seq => departamentosProducaoId.Contains(seq.DepartamentoProducao.Id.Value))
                .SelectMany(seq => seq.MaterialComposicaoModelos);
        }
    }
}