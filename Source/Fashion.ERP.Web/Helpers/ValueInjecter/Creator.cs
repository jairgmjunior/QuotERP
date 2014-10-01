using System;
using System.Collections.Generic;

namespace Fashion.ERP.Web.Helpers.ValueInjecter
{
    public static class Creator
    {
        #region Create
        public static object Create(Type type)
        {
            if (type.IsEnumerable())
            {
                return Activator.CreateInstance(typeof(List<>).MakeGenericType(type.GetGenericArguments()[0]));
            }

            if (type.IsInterface)
                throw new Exception("Don't know any implementation of this type: " + type.Name);

            return Activator.CreateInstance(type);
        }
        #endregion
    }
}