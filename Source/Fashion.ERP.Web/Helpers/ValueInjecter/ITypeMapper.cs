namespace Fashion.ERP.Web.Helpers.ValueInjecter
{
    public interface ITypeMapper<in TSource, TTarget>
    {
        TTarget Flat(TSource source, TTarget target);
        TTarget Unflat(TSource source, TTarget target);
    }
}