using System;
using System.Collections;
using System.Collections.Generic;

namespace Fashion.ERP.Web.Helpers.ValueInjecter
{
    public class EnumerableTypeMapper<TSource, TTarget> : ITypeMapper<TSource, TTarget>
        where TSource : class
        where TTarget : class
    {
        #region Flat
        public TTarget Flat(TSource source, TTarget target)
        {
            if (source == null) return null;
            var targetArgumentType = typeof(TTarget).GetGenericArguments()[0];

            var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(targetArgumentType));
            var add = list.GetType().GetMethod("Add");

            foreach (var o in (IEnumerable)source)
            {
                var t = Creator.Create(targetArgumentType);
                add.Invoke(list, new[] { Mapper.Flat(o, t, o.GetType(), targetArgumentType) });
            }
            return (TTarget)list;
        }
        #endregion

        #region Unflat
        public TTarget Unflat(TSource source, TTarget target)
        {
            if (source == null) return null;
            var targetArgumentType = typeof(TTarget).GetGenericArguments()[0];

            var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(targetArgumentType));
            var add = list.GetType().GetMethod("Add");

            foreach (var o in (IEnumerable)source)
            {
                var t = Creator.Create(targetArgumentType);
                add.Invoke(list, new[] { Mapper.Unflat(o, t, o.GetType(), targetArgumentType) });
            }
            return (TTarget)list;
        }
        #endregion
    }
}