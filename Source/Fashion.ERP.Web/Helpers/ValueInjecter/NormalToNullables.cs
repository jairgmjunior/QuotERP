using System;
using Omu.ValueInjecter;

namespace Fashion.ERP.Web.Helpers.ValueInjecter
{
    public class NormalToNullables : ConventionInjection
    {
        protected override bool Match(ConventionInfo c)
        {
            return c.SourceProp.Name == c.TargetProp.Name &&
                   c.SourceProp.Type == Nullable.GetUnderlyingType(c.TargetProp.Type);
        }
    }
}