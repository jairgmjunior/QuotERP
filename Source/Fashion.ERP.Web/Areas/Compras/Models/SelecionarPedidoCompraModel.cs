using System;
using System.Collections.Generic;
using Fashion.Framework.UnitOfWork.DinamicFilter;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class SelecionarPedidoCompraModel
    {
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public long? IdFornecedor { get; set; }

        #region Filtrar
        public IEnumerable<FilterExpression> Filtrar(bool necessitaAutorizacao)
        {
            if (!IdFornecedor.HasValue)
                throw new Exception("Informe o fornecedor.");

            if ((DataFinal.HasValue && DataFinal.HasValue) == false)
                throw new Exception("Informe a data inicial e final.");

            var regras = new List<FilterExpression>();

            if (necessitaAutorizacao)
                regras.Add(new FilterExpression("Autorizado", ComparisonOperator.IsEqual, true, LogicOperator.And));

            try
            {
                regras.Add(new FilterExpression("Fornecedor.Id", ComparisonOperator.IsEqual, IdFornecedor, LogicOperator.And));
                regras.Add(new FilterExpression("DataCompra", ComparisonOperator.LessThanOrEqual, DataFinal, LogicOperator.And));
                regras.Add(new FilterExpression("DataCompra", ComparisonOperator.LessThanOrEqual, DataFinal, LogicOperator.And));
            }
            catch (Exception)
            {
                regras.Add(new FilterExpression("DataCompra", ComparisonOperator.IsEqual, DateTime.Now.Date, LogicOperator.And));
            }

            return regras;
        }
        #endregion 
    }
}