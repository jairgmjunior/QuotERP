using Omu.ValueInjecter;

namespace Fashion.ERP.Web.Helpers.ValueInjecter
{
    public class TypeMapper<TSource, TTarget> : ITypeMapper<TSource, TTarget>
    {
        #region Flat
        public virtual TTarget Flat(TSource source, TTarget target)
        {
            target.InjectFrom<Flat>(source)
                .InjectFrom<DomainToId>(source)
                .InjectFrom<DomainToNullabeId>(source)
                .InjectFrom<NullablesToNormal>(source)
                .InjectFrom<NormalToNullables>(source)
                .InjectFrom<FlatReadOnlyCollection>(source);

            return target;
        }
        #endregion

        #region Unflat
        public virtual TTarget Unflat(TSource source, TTarget target)
        {
            target.InjectFrom<Unflat>(source)
                .InjectFrom<NullablesToNormal>(source)
                .InjectFrom<NormalToNullables>(source)
                .InjectFrom<IdToDomain>(source);

            return target;
        }
        #endregion
    }
}