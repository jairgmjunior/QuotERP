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
        public static object ObtenhaMaterialComposicaoModelos(object materiaisConsumoObject, object departamentosProducaoObject)
        {
            if (materiaisConsumoObject == null || departamentosProducaoObject == null)
                return null;

            var materiaisConsumo = materiaisConsumoObject as IEnumerable<ModeloMaterialConsumo>;

            if (materiaisConsumo == null)
                return null;

            var departamentosProducaoId = (departamentosProducaoObject as Object[]).Cast<long>().ToList();

            if (departamentosProducaoId == null || !departamentosProducaoId.Any())
                return materiaisConsumo;

            return materiaisConsumo.Where(materiais => departamentosProducaoId.Contains(materiais.DepartamentoProducao.Id.Value));
        }
    }
}