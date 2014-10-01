using System;
using Omu.ValueInjecter;

namespace Fashion.ERP.Web.Helpers.ValueInjecter
{
    public class Unflat : UnflatLoopValueInjection
    {
        protected override bool TypesMatch(Type sourceType, Type targetType)
        {
            var underlyingType = Nullable.GetUnderlyingType(sourceType); // Se é um tipo nullable

            return targetType.IsAssignableFrom(sourceType) || targetType.IsAssignableFrom(underlyingType);
        }
    }
}