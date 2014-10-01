using System;
using System.Collections;
using System.Linq;

namespace Fashion.ERP.Web.Helpers.ValueInjecter
{
    public static class TypeExtensions
    {
        #region IsEnumerable
        //returns true if type is IEnumerable<> or ICollection<>, IList<> ...
        public static bool IsEnumerable(this Type type)
        {
            if (type.IsGenericType)
            {
                if (type.GetGenericTypeDefinition().GetInterfaces().Contains(typeof(IEnumerable)))
                    return true;
            }

            return false;
        }
        #endregion
    }
}