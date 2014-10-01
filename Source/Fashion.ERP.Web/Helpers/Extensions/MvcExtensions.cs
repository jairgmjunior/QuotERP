using System;
using System.Collections;
using System.Linq;
using System.Web.Mvc;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Web.Helpers.Extensions
{
    public static class MvcExtensions
    {
        #region JoinErrors
        public static string JoinErrors(this ModelStateDictionary modelState)
        {
            return JoinErrors(modelState, ";");
        }
        #endregion

        #region JoinErrors
        public static string JoinErrors(this ModelStateDictionary modelState, string separator)
        {
            return string.Join(separator, modelState.Values.SelectMany(e => e.Errors).Select(p => p.ErrorMessage));
        }
        #endregion

        #region ToSelectList
        public static SelectList ToSelectList<TEnum>(this TEnum @enum)
        {
            var enumType = typeof(TEnum);
            object enumObject = @enum;

            // Verifica se é um tipo Nullable
            var underlyingType = Nullable.GetUnderlyingType(typeof(TEnum));
            if (underlyingType != null)
                enumType = underlyingType;

            if (!enumType.IsEnum)
                throw new ArgumentException("TEnum must be an enumerated type", "enum");

            var values = (from Enum e in Enum.GetValues(enumType)
                         let d = e.GetDisplay()
                         select new { Id = e, Name = d != null ? d.Name : e.ToString() }).ToList();

            return new SelectList(values, "Id", "Name", enumObject);
        }

        #endregion

        #region ToMultiSelectList
        public static MultiSelectList ToMultiSelectList<TEnum>(this TEnum @enum)
        {
            var enumType = typeof(TEnum);
            object enumObject = @enum;

            if (!enumType.IsArray)
                throw new ArgumentException("TEnum must be an array type", "enum");

            // Busca o tipo do array
            enumType = enumType.GetElementType();

            if (!enumType.IsEnum)
                throw new ArgumentException("TEnum must be an enumerated type", "enum");

            var values = (from Enum e in Enum.GetValues(enumType)
                          let d = e.GetDisplay()
                          select new { Id = e, Name = d != null ? d.Name : e.ToString() }).ToList();

            var enumList = enumObject as IList;
            return new MultiSelectList(values, "Id", "Name", enumList);
        }
        #endregion
    }
}