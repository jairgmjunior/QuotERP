using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Domain;
using Fashion.Framework.UnitOfWork.DinamicFilter;
using System;

namespace Fashion.ERP.Web.Models
{
    public class PesquisarModel
    {
        public string Coluna { get; set; }
        public string Valor { get; set; }

        #region Filtrar
        public FilterExpression Filtrar<T>() where T : DomainObject
        {
            if (string.IsNullOrWhiteSpace(Coluna))
                throw new Exception("Informe uma coluna para o filtro.");

            try
            {
                if (Valor == null)
                    return new FilterExpression("Coluna", ComparisonOperator.Any, null, LogicOperator.And);

                var tipoColuna = GetPropertyType<T>(Coluna);
                var value = Convert.ChangeType(Valor, tipoColuna);

                return tipoColuna == typeof(string)
                           ? new FilterExpression(Coluna, ComparisonOperator.Contains, value, LogicOperator.And)
                           : new FilterExpression(Coluna, ComparisonOperator.IsEqual, value, LogicOperator.And);
            }
            catch (Exception)
            {
                return new FilterExpression("Coluna", ComparisonOperator.IsDifferent, null, LogicOperator.And);
            }
        }
        #endregion

        #region GetPropertyType
        /// <summary>
        /// Retorna o tipo da propriedade.
        /// </summary>
        public static Type GetPropertyType<T>(string propertyName)
        {
            // Se houver propriedades aninhadas, buscar o tipo da última propriedade
            if (propertyName.Contains("."))
            {
                var typeName = propertyName.Split('.')[0];
                var type = typeof(T).GetProperty(typeName).PropertyType;

                return (Type)typeof(PesquisarModel)
                    .GetMethod("GetPropertyType")
                    .MakeGenericMethod(type)
                    .Invoke(null, new object[] { propertyName.Substring(propertyName.IndexOf('.') + 1) });
            }

            return typeof(T).GetProperty(propertyName).PropertyType;
        }
        #endregion
    }
}