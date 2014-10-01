using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Fashion.ERP.Web.Helpers.Extensions;

namespace Fashion.ERP.Web.Helpers.Attributes
{
    /// <summary>
    /// Valida uma inscrição estadual.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class InscricaoEstadualAttribute : ValidationAttribute
    {
        #region Variáveis
        private readonly string _ufProperty;
        #endregion

        #region Construtores
        public InscricaoEstadualAttribute(string ufProperty)
        {
            _ufProperty = ufProperty;
        }
        #endregion

        #region IsValid

        /// <summary>
        /// Determines whether the specified value of the object is valid. 
        /// </summary>
        /// <returns>
        /// true if the specified value is valid; otherwise, false.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ie = value as string;

            if (string.IsNullOrEmpty(ie))
                return null;

            PropertyInfo ufPropertyInfo = validationContext.ObjectType.GetProperty(_ufProperty);
            
            if (ufPropertyInfo == null)
                return new ValidationResult(String.Format("Não foi possível encontrar um propriedade com o nome {0}", _ufProperty));

            var ufPropertyValue = ufPropertyInfo.GetValue(validationContext.ObjectInstance, null) as string;

            ie = ie.Replace(".", string.Empty)
                .Replace("/", string.Empty)
                .Replace("-", string.Empty)
                .Replace("_", string.Empty)
                .Replace(" ", string.Empty);

            if (ie.IsInscricaoEstadual(ufPropertyValue) == false)
                return new ValidationResult(String.Format("A inscrição estadual informada não é válida para a seguinte UF: {0}", ufPropertyValue));

            return null;
        }
        #endregion
    }
}