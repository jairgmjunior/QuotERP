using System;
using Fashion.ERP.Web.Helpers.ValueInjecter;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Domain;

namespace Fashion.ERP.Web.Helpers
{
    #region Mapper
    public static class Mapper
    {
        #region Flat<TTarget>
        public static TTarget Flat<TTarget>(DomainObject source) where TTarget : IModel
        {
            var target = (TTarget)Creator.Create(typeof(TTarget));
            return MapperFactory.GetMapper<DomainObject, TTarget>().Flat(source, target);
        }
        #endregion

        #region Flat<TTarget>
        public static TTarget Flat<TTarget>(DomainObject source, TTarget target) where TTarget : IModel
        {
            return MapperFactory.GetMapper<DomainObject, TTarget>().Flat(source, target);
        }
        #endregion

        #region Unflat<TTarget>
        public static TTarget Unflat<TTarget>(IModel source, TTarget target) where TTarget : DomainObject
        {
            return MapperFactory.GetMapper<IModel, TTarget>().Unflat(source, target);
        }
        #endregion

        #region Unflat<TTarget>
        public static TTarget Unflat<TTarget>(IModel source) where TTarget : DomainObject
        {
            var target = (TTarget)Creator.Create(typeof(TTarget));
            return MapperFactory.GetMapper<IModel, TTarget>().Unflat(source, target);
        }
        #endregion

        #region Flat
        public static object Flat(object source, object target, Type sourceType, Type targetType)
        {
            if (sourceType != typeof(DomainObject) || targetType != typeof(IModel))
                return null;

            target = target ?? Creator.Create(targetType);
            var getMapper = typeof(MapperFactory).GetMethod("GetMapper").MakeGenericMethod(sourceType, targetType);
            var mapper = getMapper.Invoke(null, null);
            var map = mapper.GetType().GetMethod("Flat");
            return map.Invoke(mapper, new[] { source, target });
        }
        #endregion

        #region Unflat
        public static object Unflat(object source, object target, Type sourceType, Type targetType)
        {
            if (sourceType != typeof(IModel) || targetType != typeof(DomainObject))
                return null;

            target = target ?? Creator.Create(targetType);
            var getMapper = typeof(MapperFactory).GetMethod("GetMapper").MakeGenericMethod(sourceType, targetType);
            var mapper = getMapper.Invoke(null, null);
            var map = mapper.GetType().GetMethod("Unflat");
            return map.Invoke(mapper, new[] { source, target });
        }
        #endregion
    }
    #endregion
}