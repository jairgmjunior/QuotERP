using System;
using System.Collections.Generic;

namespace Fashion.ERP.Web.Helpers.ValueInjecter
{
    public static class MapperFactory
    {
        #region Mappers
        private static readonly IDictionary<Type, object> Mappers = new Dictionary<Type, object>();
        #endregion

        #region GetMapper<TSource, TTarget>
        public static ITypeMapper<TSource, TTarget> GetMapper<TSource, TTarget>()
        {
            //if we have a specified TypeMapper for <TSource,Target> return it
            if (Mappers.ContainsKey(typeof(ITypeMapper<TSource, TTarget>)))
                return Mappers[typeof(ITypeMapper<TSource, TTarget>)] as ITypeMapper<TSource, TTarget>;

            ITypeMapper<TSource, TTarget> mapper = null;
            //if both Source and Target types are Enumerables return new EnumerableTypeMapper<TSource,TTarget>()
            if (typeof(TSource).IsEnumerable() && typeof(TTarget).IsEnumerable())
            {
                mapper = (ITypeMapper<TSource, TTarget>)Activator.CreateInstance(typeof(EnumerableTypeMapper<,>).MakeGenericType(typeof(TSource), typeof(TTarget)));
            }

            //return the default TypeMapper
            if (mapper == null)
                mapper = new TypeMapper<TSource, TTarget>();

            AddMapper(mapper);
            return mapper;
        }
        #endregion

        #region AddMapper<TS, TT>
        public static void AddMapper<TS, TT>(ITypeMapper<TS, TT> o)
        {
            Mappers.Add(typeof(ITypeMapper<TS, TT>), o);
        }
        #endregion

        #region ClearMappers
        public static void ClearMappers()
        {
            Mappers.Clear();
        }
        #endregion
    }
}